#pragma once
#include "Model.h"
#include "PointLight.h"
#include <time.h>

static int amount;
static int curr_amount;
class CopyModel
{
private:
	Model* model;
	//------transform--------
	glm::vec3 pos;
	glm::vec3 scale;
	glm::vec4 rot;
	//------matrixModel------
	glm::mat4* mod;
public:
	CopyModel(Model* _model) : pos(0.0f), scale(0.01f), rot(0.0f, 1.0f, 0.0f, 0.0f)
	{
		model = _model;
		curr_amount = pow(amount, 3);
		mod = new glm::mat4[curr_amount];

		int step = 25;
		int ind = 0;
		for (size_t z = 0; z < amount; z++)
		{
			for (size_t y = 0; y < amount; y++)
			{
				for (size_t x = 0; x < amount; x++)
				{
					mod[ind] = glm::mat4(1.0f);
					mod[ind] = glm::scale(mod[ind], glm::vec3(0.1f));
					mod[ind] = glm::rotate(mod[ind], glm::radians(-90.0f), glm::vec3(1.0f, 0.0f, 0.0f));
					mod[ind] = glm::translate(mod[ind], glm::vec3(x * step, y * step, z * step));
					ind++;
				}
			}
		}

		Initialize();
	}

	void Initialize()
	{
		// configure instanced array
		// -------------------------
		unsigned int buffer;
		glGenBuffers(1, &buffer);
		glBindBuffer(GL_ARRAY_BUFFER, buffer);
		glBufferData(GL_ARRAY_BUFFER, curr_amount * sizeof(glm::mat4), &mod[0], GL_STATIC_DRAW);

		for (int i = 0; i < model->meshes.size(); i++)
		{
			unsigned int VAO = model->meshes[i].VAO;
			glBindVertexArray(VAO);
			// set attribute pointers for matrix (4 times vec4)
			glEnableVertexAttribArray(3);
			glVertexAttribPointer(3, 4, GL_FLOAT, GL_FALSE, sizeof(glm::mat4), (void*)0);
			glEnableVertexAttribArray(4);
			glVertexAttribPointer(4, 4, GL_FLOAT, GL_FALSE, sizeof(glm::mat4), (void*)(sizeof(glm::vec4)));
			glEnableVertexAttribArray(5);
			glVertexAttribPointer(5, 4, GL_FLOAT, GL_FALSE, sizeof(glm::mat4), (void*)(2 * sizeof(glm::vec4)));
			glEnableVertexAttribArray(6);
			glVertexAttribPointer(6, 4, GL_FLOAT, GL_FALSE, sizeof(glm::mat4), (void*)(3 * sizeof(glm::vec4)));

			glVertexAttribDivisor(3, 1);
			glVertexAttribDivisor(4, 1);
			glVertexAttribDivisor(5, 1);
			glVertexAttribDivisor(6, 1);

			glBindVertexArray(0);
		}
	}

	void draw(glm::mat4& _proj, glm::mat4& _view, Shader_Glad* _shader)
	{
		mod[0] = glm::rotate(mod[0], glm::radians((float)glfwGetTime() * 5.0f), glm::vec3(0.0f, 1.0f, 0.0f));
		_shader->use();
		_shader->setMat4("projection", _proj);
		_shader->setMat4("view", _view);
		_shader->setInt("texture_diffuse1", 0);
		glActiveTexture(GL_TEXTURE0);
		glBindTexture(GL_TEXTURE_2D, model->textures_loaded[0].id); // note: we also made the textures_loaded vector public (instead of private) from the model class.
		for (unsigned int i = 0; i < model->meshes.size(); i++)
		{
			glBindVertexArray(model->meshes[i].VAO);
			glDrawElementsInstanced(GL_TRIANGLES, model->meshes[i].indices.size(), GL_UNSIGNED_INT, 0, curr_amount);
			glBindVertexArray(0);
		}
	}

	//inline void SetPos(glm::vec3& _pos) 
	//{ 
	//	pos = _pos;			
	//}

	//inline void SetRotate(float _angle)
	//{
	//	rot = glm::vec4(glm::vec3(rot.x, rot.y, rot.z), _angle);
	//}

	//inline void SetRotate(float _angle, glm::vec3& _axis)
	//{
	//	rot = glm::vec4(_axis, _angle);
	//}

	/*virtual void draw(Shader_Glad& _shader)
	{
		glm::mat4 mod(1.0f);
		mod = glm::translate(mod, pos);
		mod = glm::rotate(mod, glm::radians(rot.w), glm::vec3(rot.x, rot.y, rot.z));
		mod = glm::scale(mod, scale);

		model->Draw(_shader);
	}*/
	/*virtual void draw(PointLight& _light)
	{
		glm::mat4 mod(1.0f);
		mod = glm::translate(mod, pos);
		mod = glm::rotate(mod, glm::radians(rot.w), glm::vec3(rot.x, rot.y, rot.z));
		mod = glm::scale(mod, scale);

		_light.SetModel(mod);

		model->Draw(_light.GetLightShader());
	}*/
};