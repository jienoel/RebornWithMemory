Shader "Sprites/Blend" { 
 
Properties {
	_Blend ("Blend", Range (0, 1) ) = 0.5 
	_MainTex ("Texture 1", 2D) = "" 
	_Texture2 ("Texture 2", 2D) = ""
}
 
SubShader {	
  Tags {Queue = Transparent}
    Cull Off
    ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha
	Pass {
		SetTexture[_MainTex]
		SetTexture[_Texture2] { 
			ConstantColor (0,0,0, [_Blend]) 
			Combine texture Lerp(constant) previous
		}		
	}
} 
 
}