#version 330 core

out vec4 outColor;
uniform float u_time;

void main()
{
	outColor = vec4(0.8 + cos(u_time * 5), 1.0, 1.0f, 1.0f);
}