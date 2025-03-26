#pragma once

#include "Utilites.h"
#include "Shader_Glad.h"
#include "PointLight.h"
#include "Model.h"

class TestCube
{
public: glm::mat4 model;
public: glm::vec3 position;
public: glm::vec3 rotation;
public: glm::vec3 scale;
private: GLfloat angle;

public: TestCube()
{
	angle = 0;
}

public: void Initialize(glm::vec3 _pos = glm::vec3(), glm::vec3 _rot = glm::vec3(0.1, 0.1, 0.1),
						glm::vec3 _scale = glm::vec3(1, 1, 1)) 
{
	position = _pos;
	rotation = _rot;
	scale = _scale;

	model = glm::translate(model, position);
	model = glm::rotate(model, (GLfloat)glfwGetTime() * angle, rotation);
	model = glm::scale(model, scale);
}

public: void Update()
{
	model = glm::translate(model, position);
	model = glm::rotate(model, (GLfloat)glfwGetTime() * angle, rotation);
	model = glm::scale(model, scale);
}

public: ~TestCube()
{
}

};