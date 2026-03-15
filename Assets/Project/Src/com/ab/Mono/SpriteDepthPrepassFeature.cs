using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace ab.Mono
{
    /// <summary>
    /// URP RenderFeature: renders sprites with the "DepthOnly" shader pass
    /// front-to-back before the main 2D pass (Unity 6 / Render Graph).
    ///
    /// After this prepass, back sprites whose pixels are fully covered by
    /// front sprites fail the depth test and are skipped by the GPU.
    ///
    /// Requirements:
    ///   - Sprites must use a shader with a "DepthOnly" pass (TileUnlit).
    ///   - Each layer must have a unique Z position.
    ///     Front layer: Z = 0, next: Z = 1, back: Z = 2, etc.
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

            public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
            {
                var renderingData = frameData.Get<UniversalRenderingData>();
                var cameraData    = frameData.Get<UniversalCameraData>();
                var resourceData  = frameData.Get<UniversalResourceData>();

                if (cameraData.isPreviewCamera) return;

                var rendererListDesc = new RendererListDesc(DepthOnlyTag, renderingData.cullResults, cameraData.camera)
                {
                    sortingCriteria  = SortingCriteria.CommonOpaque,
                    renderQueueRange = RenderQueueRange.all
                };

                var rendererList = renderGraph.CreateRendererList(rendererListDesc);

                using var builder = renderGraph.AddRasterRenderPass<PassData>("SpriteDepthPrepass", out var passData);

                passData.RendererList = rendererList;

                builder.UseRendererList(rendererList);
                builder.SetRenderAttachmentDepth(resourceData.activeDepthTexture, AccessFlags.Write);
                builder.AllowPassCulling(false);

                builder.SetRenderFunc(static (PassData data, RasterGraphContext ctx) =>
                    ctx.cmd.DrawRendererList(data.RendererList));
            }
        }

        private class PassData
        {
            public RendererListHandle RendererList;
        }
    }
}
