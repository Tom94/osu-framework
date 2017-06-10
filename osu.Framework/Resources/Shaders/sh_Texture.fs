#ifdef GL_ES
	precision mediump float;
#endif

#include "sh_Utils.h"

varying vec4 v_Colour;
varying vec2 v_TexCoord;

uniform sampler2D m_Sampler;
uniform sampler2D m_ScreenSampler;

uniform float g_AlphaThreshold;
uniform vec2 g_ScreenSize;

uniform bool g_PremultiplyAlpha;

void main(void)
{
	vec2 uv = gl_FragCoord.xy / g_ScreenSize;
	float screenAlpha = texture2D(m_ScreenSampler, uv).a;
	if (screenAlpha > g_AlphaThreshold)
		discard;

	gl_FragColor = toSRGB(v_Colour * texture2D(m_Sampler, v_TexCoord, -0.9));
	if (g_PremultiplyAlpha)
  		gl_FragColor = vec4(gl_FragColor.rgb * gl_FragColor.a, gl_FragColor.a);
}
