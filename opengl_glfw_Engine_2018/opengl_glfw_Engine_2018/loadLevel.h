#pragma once

#include <string>
#include <fstream>
#include <sstream>
#include <iostream>

#include <vector>
#include "TestCube.h"

class loadLevel
{
public: int wid;
public: int zbuff;
//public:	unsigned int pp;
public: std::vector<glm::mat4> data;
private: std::string _d;

public: loadLevel(char *_fileName)
{
	//data = new TestCube[100];

	std::ifstream file;
	file.exceptions(std::ifstream::failbit | std::ifstream::badbit);

	std::stringstream temp_str;

	try
	{
		file.open(_fileName);
		temp_str << file.rdbuf();

		std::string t_str = temp_str.str();

		const int size_w = t_str.find('\n', 0);
		char *t_str_w = new char[size_w];
		t_str.copy(t_str_w, size_w, 1);

		const int size_h = t_str.find('\n', size_w);
		char *t_str_h = new char[size_h];
		t_str.copy(t_str_h, size_h, size_w + 2);

		std::string temp_d = t_str.substr(size_w + size_h + 2, std::string::npos).c_str();

		wid = atoi(t_str_w);
		zbuff = atoi(t_str_h);

		for (size_t i = 0; i < temp_d.length(); i++)
		{
			if (temp_d[i] == '\n')
				temp_d.erase(temp_d.begin() + i);
		}

		_d = temp_d;

		delete[] t_str_w;
		delete[] t_str_h;
	}
	catch (std::ifstream::failure e)
	{
		std::cout << "ошибка загрузки уровня, в файле " << _fileName << std::endl;
		std::cout << "err -> " << e.what() << std::endl;
	}

	file.close();
}

private: void Initialize(glm::mat4 &_model, glm::vec3 _pos = glm::vec3(), glm::vec3 _rot = glm::vec3(0.1, 0.1, 0.1),
	glm::vec3 _scale = glm::vec3(1, 1, 1))
{
	_model = glm::translate(_model, _pos);
	//_model = glm::rotate(model, (GLfloat)glfwGetTime() * angle, rotation);
	_model = glm::scale(_model, _scale);
}

public: void Load()
{
	unsigned int count = 0;
	data.clear();

	for (size_t z = 0; z < zbuff; z++)
	{
		for (size_t x = 0; x < wid; x++)
		{
			if (_d[count] == '.')
			{
				glm::mat4 t;
				Initialize(t, glm::vec3((x + 1) * 1, 0, (z + 1) * 1));

				data.push_back(t);
			}
			else if (_d[count] == '1')
			{
				glm::mat4 t;
				Initialize(t, glm::vec3((x + 1) * 1, 1, (z + 1) * 1));
				data.push_back(t);
			}
			else if (_d[count] == '2')
			{
				glm::mat4 t;
				Initialize(t, glm::vec3((x + 1) * 1, 1, (z + 1) * 1));
				data.push_back(t);

				glm::mat4 t2;
				Initialize(t2, glm::vec3((x + 1) * 1, 2, (z + 1) * 1));
				data.push_back(t2);
			}
			else if (_d[count] == '3')
			{
				glm::mat4 t;
				Initialize(t, glm::vec3((x + 1) * 1, 1, (z + 1) * 1));
				data.push_back(t);

				glm::mat4 t2;
				Initialize(t2, glm::vec3((x + 1) * 1, 2, (z + 1) * 1));
				data.push_back(t2);

				glm::mat4 t3;
				Initialize(t3, glm::vec3((x + 1) * 1, 3, (z + 1) * 1));
				data.push_back(t3);
			}
			else if (_d[count] == '4')
			{
				glm::mat4 t;
				Initialize(t, glm::vec3((x + 1) * 1, 1, (z + 1) * 1));
				data.push_back(t);

				glm::mat4 t2;
				Initialize(t2, glm::vec3((x + 1) * 1, 2, (z + 1) * 1));
				data.push_back(t2);

				glm::mat4 t3;
				Initialize(t3, glm::vec3((x + 1) * 1, 3, (z + 1) * 1));
				data.push_back(t3);

				glm::mat4 t4;
				Initialize(t4, glm::vec3((x + 1) * 1, 4, (z + 1) * 1));
				data.push_back(t4);
			}

			++count;
		}
	}
}
public:~loadLevel()
{
	data.clear();
}
};