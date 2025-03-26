#pragma once

#include "Shader_Glad.h"

#include "CameraHandle.h"

class PointLight
{
private: glm::vec3 position;
private: Shader_Glad shaderLight;
private: Shader_Glad lamp;
private: unsigned int lightVAO, VBO;

public: GLfloat shinines;

public: PointLight(glm::vec3 _pos = glm::vec3())
{
	position = _pos;
	shinines = 32.0f;
}

public: void Initialize(GLfloat _data[], unsigned int &_VBO)
{
	shaderLight = Shader_Glad("shader//lightsVS.txt", "shader//lightsFS.txt");
	lamp = Shader_Glad("shader//lampVS.txt", "shader//lampFS.txt");
	glGenVertexArrays(1, &lightVAO);
	//glGenBuffers(1, &VBO);
	//glBindBuffer(GL_ARRAY_BUFFER, VBO);

	//glBufferData(GL_ARRAY_BUFFER, sizeof(_data), _data, GL_STATIC_DRAW);
	glBindVertexArray(lightVAO);
	glBindBuffer(GL_ARRAY_BUFFER, _VBO);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(0);

	shaderLight.use();
	shaderLight.setInt("material.diffuse", 0);

	//glGenVertexArrays(1, &lightVAO);
	//glBindVertexArray(lightVAO);

	//glBindBuffer(GL_ARRAY_BUFFER, VBO);

	//glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)0);
	//glEnableVertexAttribArray(0);

}

public: void UpdateLight(CameraHandle &camera, glm::mat4 &projection, glm::mat4 &view, glm::mat4 &model)
{
	shaderLight.use();
	shaderLight.setVec3("light.position", position);
	shaderLight.setVec3("viewPos", camera.cam.GetPos());

	// light properties
	shaderLight.setVec3("light.ambient", 0.2f, 0.2f, 0.2f);
	shaderLight.setVec3("light.diffuse", 0.5f, 0.5f, 0.5f);
	shaderLight.setVec3("light.specular", 1.0f, 1.0f, 1.0f);

	// material properties
	shaderLight.setVec3("material.specular", 0.5f, 0.5f, 0.5f);
	shaderLight.setFloat("material.shininess", 64.0f);

	shaderLight.setMat4("projection", projection);
	shaderLight.setMat4("view", view);
	shaderLight.setMat4("model", model);
}

public: void UpdateLamp(glm::mat4 &projection, glm::mat4 &view, glm::mat4 &model)
{
	lamp.use();
	lamp.setMat4("projection", projection);
	lamp.setMat4("view", view);
	model = glm::mat4();
	model = glm::translate(model, position);
	model = glm::scale(model, glm::vec3(0.1f)); // a smaller cube
	lamp.setMat4("model", model);
}
		 
public: unsigned int GetLightVAO() { return lightVAO; }

public: Shader_Glad GetLampShader() { return lamp; }

public: void SetModel(glm::mat4 &model)
{
	shaderLight.setMat4("model", model);
}

~PointLight()
{
	glDeleteVertexArrays(1, &lightVAO);
	glDeleteBuffers(1, &VBO);
}
};