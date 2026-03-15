using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace ab.Mono
{
    /// <summary>
    /// URP RenderFeature (Unity 6 / Render Graph): renders sprites with
    /// the "DepthOnly" shader pass front-to-back before the main 2D pass.
    ///
    /// After this prepass, back sprites whose pixels are fully covered by
    /// front sprites fail the depth test and are skipped by the GPU.
    ///
    /// Requirements:
    ///   - Sprites must use a shader with a "DepthOnly" pass (TileUnlit).
    ///   - Each layer must have a unique Z position.
    ///     Front layer: Z = 0, next: Z = 1, back: Z = 2, etc.
    ///
    /// Setup:
    ///   Add this feature to the UniversalRenderer2D asset via
    ///   "Add Renderer Feature" → "Sprite Depth Prepass Feature".
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

        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            var renderingData = frameData.Get<UniversalRenderingData>();
            var cameraData    = frameData.Get<UniversalCameraData>();
            var lightData     = frameData.Get<UniversalLightData>();
            var resourceData  = frameData.Get<UniversalResourceData>();

            if (cameraData.isPreviewCamera) return;

            var sortingCriteria = SortingCriteria.CommonOpaque;
            var drawSettings = RenderingUtils.CreateDrawingSettings(
                new ShaderTagId("DepthOnly"),
                ref renderingData,
                ref cameraData,
                ref lightData,
                sortingCriteria);

            var filterSettings = new FilteringSettings(RenderQueueRange.all);

            using var builder = renderGraph.AddRasterRenderPass<PassData>(
                "SpriteDepthPrepass", out var passData);

            passData.DrawSettings   = drawSettings;
            passData.FilterSettings = filterSettings;
            passData.CullResults    = renderingData.cullResults;

            builder.SetRenderAttachmentDepth(resourceData.activeDepthTexture, AccessFlags.Write);
            builder.AllowPassCulling(false);

            builder.SetRenderFunc(static (PassData data, RasterGraphContext ctx) =>
                ctx.cmd.DrawRenderers(data.CullResults, ref data.DrawSettings, ref data.FilterSettings));
        }

        // ----------------------------------------------------------------

        private class PassData
        {
            public DrawingSettings   DrawSettings;
            public FilteringSettings FilterSettings;
            public CullingResults    CullResults;
        }

        private sealed class SpriteDepthPrepassPass : ScriptableRenderPass
        {
            private static readonly ShaderTagId DepthOnlyTag = new ShaderTagId("DepthOnly");

            // Compatibility path for non-RenderGraph mode
            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                var sortingSettings = new SortingSettings(renderingData.cameraData.camera)
                {
                    criteria = SortingCriteria.CommonOpaque
                };
                var drawSettings   = new DrawingSettings(DepthOnlyTag, sortingSettings) { enableInstancing = true };
                var filterSettings = new FilteringSettings(RenderQueueRange.all);
                context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref filterSettings);
            }
        }
    }
}
