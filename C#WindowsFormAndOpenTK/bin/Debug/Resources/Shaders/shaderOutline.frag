#version 330 core

out vec4 outColor;
uniform float u_time;

//in vec3 myNormal;
//in vec4 FragPos;

//uniform vec3 cameraPos;

void main()
{
	//vec3 viewDir = normalize(cameraPos - vec3(FragPos));
	//float intensity = dot(normalize(myNormal), viewDir);

	//outColor = vec4(vec3(intensity), 1.0f);

	outColor = vec4(0.8 + cos(u_time * 5), 1.0, 1.0f, 1.0f);
}