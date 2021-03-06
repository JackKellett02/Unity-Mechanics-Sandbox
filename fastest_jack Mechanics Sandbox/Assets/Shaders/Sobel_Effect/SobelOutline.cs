using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using static UnityEngine.Rendering.ColorParameter;
using static UnityEngine.Rendering.FloatParameter;

[UnityEngine.Rendering.PostProcessing.PostProcess(typeof(SobelOutlineRenderer), PostProcessEvent.BeforeStack, "SobelOutline")]
public class SobelOutline : PostProcessEffectSettings {
	public FloatParameter thickness = new FloatParameter { value = 1.0f };
	public FloatParameter depthMultiplier = new FloatParameter { value = 1.0f };
	public FloatParameter depthBias = new FloatParameter { value = 1.0f };
	public FloatParameter normalMultiplier = new FloatParameter { value = 1.0f };
	public FloatParameter normalBias = new FloatParameter { value = 10.0f };
	public ColorParameter color = new ColorParameter { value = Color.black };
}

public sealed class SobelOutlineRenderer : PostProcessEffectRenderer<SobelOutline> {
	public override void Render(PostProcessRenderContext context) {
		var sheet = context.propertySheets.Get(Shader.Find("PostProcessing/SobelOutline"));

		sheet.properties.SetFloat("_OutlineThickness", settings.thickness);
		sheet.properties.SetFloat("_OutlineDepthMultiplier", settings.depthMultiplier);
		sheet.properties.SetFloat("_OutlineDepthBias", settings.depthBias);
		sheet.properties.SetFloat("_OutlineNormalMultiplier", settings.normalMultiplier);
		sheet.properties.SetFloat("_OutlineNormalBias", settings.normalBias);
		sheet.properties.SetColor("_OutlineColor", settings.color);

		context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
	}
}