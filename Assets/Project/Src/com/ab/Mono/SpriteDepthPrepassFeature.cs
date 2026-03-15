using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace ab.Mono
{
    /// <summary>
    /// URP RenderFeature: renders sprites with "DepthOnly" shader pass
    /// front-to-back before the main 2D pass.
    ///
    /// After this prepass, back sprites whose pixels are fully covered by
    /// front sprites fail the depth test and are skipped by the GPU.
    ///
    /// Requirements:
    ///   - Sprites must use a shader with a "DepthOnly" pass (TileUnlit).
    ///   - Each layer must have a unique Z position so depth comparison works.
    ///     Front layer: Z = 0, next: Z = 1, back: Z = 2, etc.
    ///
    /// Setup:
    ///   1. Merge the PR so the script is in the project.
    ///   2. Wait for Unity to compile (no errors in Console).
    ///   3. Select your UniversalRenderer2D asset in the Project window.
    ///   4. In Inspector click "Add Renderer Feature" → "Sprite Depth Prepass Feature".
    /// </summary>
    public class SpriteDepthPrepassFeature : ScriptableRendererFeature
    {
        private SpriteDepthPrepassPass _pass;

        public override void Create()
        {
            _pass = new SpriteDepthPrepassPass
            {
                renderPassEvent = RenderPassEvent.BeforeRenderingOpaques
            };
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (renderingData.cameraData.isPreviewCamera) return;
            renderer.EnqueuePass(_pass);
        }

        // ----------------------------------------------------------------

        private sealed class SpriteDepthPrepassPass : ScriptableRenderPass
        {
            private static readonly ShaderTagId DepthOnlyTag = new ShaderTagId("DepthOnly");

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                // SortingSettings requires a camera reference — create per-frame
                var sortingSettings = new SortingSettings(renderingData.cameraData.camera)
                {
                    criteria = SortingCriteria.CommonOpaque // front-to-back + sorting layer
                };

                var drawSettings = new DrawingSettings(DepthOnlyTag, sortingSettings)
                {
                    enableDynamicBatching = renderingData.supportsDynamicBatching,
                    enableInstancing      = true
                };

                var filterSettings = new FilteringSettings(RenderQueueRange.all);

                context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref filterSettings);
            }
        }
    }
}
