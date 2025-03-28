﻿#version 330 core

layout(location = 0) in vec3 aPos;
layout(location = 1) in vec3 aNormal;
layout(location = 2) in vec2 aTexCoords;

uniform mat4 projection;
uniform mat4 model;
uniform mat4 view;

//uniform vec3 outLine;

void main()
{
	//vec3 newPos = aNormal * -outLine;
	//vec3 p = aPos - aNormal;
	vec4 crntPos = vec4(aPos, 1.0f) * model * view * projection;
	gl_Position = crntPos;
}