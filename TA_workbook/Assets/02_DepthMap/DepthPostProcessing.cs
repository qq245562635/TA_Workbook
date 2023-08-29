using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DepthPostProcessing : ScriptableRendererFeature
{
    class DepthPass : ScriptableRenderPass
    {
        public Material depthMaterial = null;
        RenderTargetIdentifier source { get; set; }
        RenderTargetHandle destination { get; set; }

        public void Setup(RenderTargetIdentifier sourceRT)
        {
            this.source = sourceRT;
            destination.Init("_DepthPostProcessingTexture");
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get("DepthPass");
            RenderTextureDescriptor opaqueDescriptor = renderingData.cameraData.cameraTargetDescriptor;
            opaqueDescriptor.depthBufferBits = 0;

            cmd.GetTemporaryRT(destination.id, opaqueDescriptor, FilterMode.Point);
            cmd.Blit(source, destination.Identifier(), depthMaterial);
            cmd.Blit(destination.Identifier(), source);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    DepthPass depthPass;
    public Material depthMaterial;

    public override void Create()
    {
        depthPass = new DepthPass();
        depthPass.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        depthPass.Setup(renderer.cameraColorTarget);
        depthPass.depthMaterial = depthMaterial;
        renderer.EnqueuePass(depthPass);
    }
}
