#pragma once
#include "Model.h"
#include <time.h>

class myModelFBX
{
private:
	std::string fileName;
	Shader_Glad* shaderModel;
	Model* model;
	//-----------------------
	glm::vec3 pos;
	glm::vec3 scale;
	glm::vec4 rot;	
public:
	myModelFBX(char* _fileNameFBX) : fileName(_fileNameFBX), pos(0.0f), scale(0.1f), rot(0.0f, 0.01f, 0.0f, 0.001f)
	{
		shaderModel = new Shader_Glad("shader//modelVS.txt", "shader//modelFS.txt");
		model = new Model(_fileNameFBX);
		fileName = _fileNameFBX;

		std::cout << "Create file myModelFBX (" << fileName << ")" << std::endl;
	}

	void SetPos(glm::vec3 _pos) { pos = _pos; }

	void draw(glm::mat4& _projection, glm::mat4& _view)
	{
		glm::mat4 mod = glm::mat4(1.0f);

		mod = glm::translate(mod, pos);
		mod = glm::rotate(mod, clock() * rot.w, glm::vec3(rot.x, rot.y, rot.z));
		mod = glm::scale(mod, scale);

		/*mod = glm::translate(mod, glm::vec3(0, 0, -30));*/
		//mod = glm::rotate(mod, clock() * 0.006f, glm::vec3(0.0f, 0.1f, 0.0f));
		//mod = glm::translate(mod, glm::vec3(10, 0, 0));

		shaderModel->use();		
		shaderModel->setMat4("projection", _projection);
		shaderModel->setMat4("view", _view);
		shaderModel->setMat4("model", mod);

		model->Draw(*shaderModel);
	}

	~myModelFBX()
	{
		std::cout << "file myModelFBX delete (" << fileName << ")" << std::endl;
		delete shaderModel;
		delete model;
	}
};