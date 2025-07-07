#version 330 core

layout(location = 0) in vec3 aPos;
layout(location = 1) in vec3 aNormal;
layout(location = 2) in vec2 aTexCoords;

uniform mat4 projection;
uniform mat4 model;
uniform mat4 view;

//uniform float myLen;
//uniform mat4 myScale;

//out vec3 myNormal;
//out vec4 FragPos;

void main()
{
	vec3 np = vec3(1.01, 1.01, 1.01);
	vec4 crntPos = vec4(aPos, 1.0f) * model * view * projection;
	gl_Position = crntPos;
	//FragPos = crntPos;

	//vec4 crntPos = vec4(aPos + (aNormal * 0.1), 1.0f) * model * view * projection;
	//gl_Position = crntPos;
}