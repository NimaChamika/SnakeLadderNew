Shader "GUI/3D Text Shader2" { 
	Properties {
		_MainTex ("Font Texture", 2D) = "Black" {}
		_Color ("Text Color", Color) = (255,255,255,1)
	}
 
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Lighting Off Cull Off ZWrite Off Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha
		Pass {
			Color [_Color]
			SetTexture [_MainTex] {
				combine primary, texture * primary
			}
		}
	}
}