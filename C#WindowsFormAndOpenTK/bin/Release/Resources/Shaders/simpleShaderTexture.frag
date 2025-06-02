#version 330

out vec4 outputColor;

uniform vec4 Color;
in vec2 texCoord;

uniform sampler2D texture0;

void main()
{
	vec4 tex = texture(texture0, texCoord) * Color;
	if(tex.a < 0.1)
		discard;
	outputColor = tex;
}