using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace ab.Mono
{
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

        private sealed class SpriteDepthPrepassPass : ScriptableRenderPass
        {
            private static readonly ShaderTagId DepthOnlyTag = new ShaderTagId("DepthOnly");

#pragma warning disable CS0672, CS0618
            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                var sortingSettings = new SortingSettings(renderingData.cameraData.camera)
                {
                    criteria = SortingCriteria.CommonOpaque
                };
                var drawSettings   = new DrawingSettings(DepthOnlyTag, sortingSettings) { enableInstancing = true };
                var filterSettings = new FilteringSettings(RenderQueueRange.all);

                var listParams   = new RendererListParams(renderingData.cullResults, drawSettings, filterSettings);
                var rendererList = context.CreateRendererList(ref listParams);

                var cmd = CommandBufferPool.Get("SpriteDepthPrepass");
                cmd.DrawRendererList(rendererList);
                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }
#pragma warning restore CS0672, CS0618
        }
    }
}
