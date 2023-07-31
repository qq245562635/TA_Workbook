using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomRenderFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
        public Material blitMaterial = null;
        //public int blitMaterialPassIndex = -1;
        //Ŀ��RenderTexture 
        public RenderTexture renderTexture = null;

    }
    public Settings settings = new Settings();
    private CustomPass blitPass;

    public override void Create()
    {
        blitPass = new CustomPass(name, settings);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (settings.blitMaterial == null)
        {
            Debug.LogWarningFormat("��ʧblit����");
            return;
        }
        blitPass.renderPassEvent = settings.renderPassEvent;
        blitPass.Setup(renderer.cameraDepthTarget);
        renderer.EnqueuePass(blitPass);
    }
}

public class CustomPass : ScriptableRenderPass
{
    private CustomRenderFeature.Settings settings;
    string m_ProfilerTag;
    RenderTargetIdentifier source;

    public CustomPass(string tag, CustomRenderFeature.Settings settings)
    {
        m_ProfilerTag = tag;
        this.settings = settings;
    }

    public void Setup(RenderTargetIdentifier src)
    {
        source = src;
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer command = CommandBufferPool.Get(m_ProfilerTag);
        command.Blit(source, settings.renderTexture, settings.blitMaterial);
        context.ExecuteCommandBuffer(command);
        CommandBufferPool.Release(command);
    }
}