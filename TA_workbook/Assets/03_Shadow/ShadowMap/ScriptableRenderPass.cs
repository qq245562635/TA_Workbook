using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ShadowMapPass : ScriptableRenderPass
{
    RenderTargetHandle shadowmapHandle;
    RenderTexture shadowmapTexture;

    public ShadowMapPass()
    {
        shadowmapHandle.Init("_ShadowMapTexture");
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        // CommandBuffer cmd = CommandBufferPool.Get("ShadowMapPass");
        // // Set render target to shadow map texture
        // context.SetRenderTarget(shadowmapTexture);
        // // Clear the shadow map
        // cmd.ClearRenderTarget(true, true, Color.clear);
        // context.ExecuteCommandBuffer(cmd);
        // cmd.Clear();
        // // Render the scene from the light's perspective here
        // // ...
        // CommandBufferPool.Release(cmd);
    }

    public void Setup()
    {
        shadowmapTexture = new RenderTexture(1024, 1024, 16, RenderTextureFormat.Depth);
        shadowmapTexture.Create();
    }
}
