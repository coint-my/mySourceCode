#pragma comment(lib, "glew32s.lib")
#include "Shader.h"
#include "CameraHandle.h"
#include "PointLight.h"

//#include <gl/glfw3.h>
#include <iostream>
#include <cmath>

#pragma comment(lib, "SOIL.lib")
#include "src/SOIL.h"
#include "CopyModel.h"
//#include <filesystem>
GLuint texture1;

static int wid = 1200, hei = 800;
//окно OpenGL
GLFWwindow* window;
// Камера
CameraHandle cam_h;
PointLight light;
//shader for model
//Shader_Glad shaderModel;
//self model
Model* modelM;
Model* modelSkyBox;
Model* modelMainCraft;
//my self model tested

vector<CopyModel*> arr;
CopyModel* myModel;
Shader_Glad* shaderModel;
Shader_Glad* shaderInstance;
unsigned int lightVAO;

void key_callback(GLFWwindow* window, int key, int scancode, int action, int mode)
{
	// Когда пользователь нажимает ESC, мы устанавливаем свойство WindowShouldClose в true, 
	// и приложение после этого закроется
	if (key == GLFW_KEY_ESCAPE && action == GLFW_PRESS)
		glfwSetWindowShouldClose(window, GL_TRUE);
	if (key == GLFW_KEY_DELETE && action == GLFW_RELEASE)
	{
		for (size_t i = 0; i < arr.size(); i++)
		{
			delete arr[i];
		}
		arr.clear();
	}

	cam_h.key_callback(window, key, scancode, action, mode);
}
void mouse_callback(GLFWwindow* window, double xpos, double ypos)
{
	cam_h.mouse_callback(window, xpos, ypos);
}
void scroll_callback(GLFWwindow* window, double xoffset, double yoffset)
{
	cam_h.scroll_callback(window, xoffset, yoffset);
}

glm::vec3 cubePositions[] = 
{
	glm::vec3(0.0f,  0.0f,  0.0f),
	glm::vec3(2.0f,  5.0f, -15.0f),
	glm::vec3(-1.5f, -2.2f, -2.5f),
	glm::vec3(-3.8f, -2.0f, -12.3f),
	glm::vec3(2.4f, -0.4f, -3.5f),
	glm::vec3(-1.7f,  3.0f, -7.5f),
	glm::vec3(1.3f, -2.0f, -2.5f),
	glm::vec3(1.5f,  2.0f, -2.5f),
	glm::vec3(1.5f,  0.2f, -1.5f),
	glm::vec3(-1.3f,  1.0f, -1.5f)
};

float vertices[] = {
	// positions          // normals           // texture coords
	-0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f,  0.0f,
	0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f,  0.0f,
	0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f,  1.0f,
	0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f,  1.0f,
	-0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f,  1.0f,
	-0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f,  0.0f,

	-0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f,  0.0f,
	0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f,  0.0f,
	0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f,  1.0f,
	0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f,  1.0f,
	-0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f,  1.0f,
	-0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f,  0.0f,

	-0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f,  0.0f,
	-0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f,  1.0f,
	-0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f,  1.0f,
	-0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f,  1.0f,
	-0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f,  0.0f,
	-0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f,  0.0f,

	0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f,  0.0f,
	0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f,  1.0f,
	0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f,  1.0f,
	0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f,  1.0f,
	0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f,  0.0f,
	0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f,  0.0f,

	-0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f,  1.0f,
	0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f,  1.0f,
	0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f,  0.0f,
	0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f,  0.0f,
	-0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f,  0.0f,
	-0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f,  1.0f,

	-0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f,  1.0f,
	0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f,  1.0f,
	0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f,  0.0f,
	0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f,  0.0f,
	-0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f,  0.0f,
	-0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f,  1.0f
};

GLuint VBO, VAO, EBO;

int frame = 0;
int frame_current = 0;
double time_c = 0.0;
double time_e = 0.0;
double time_temp = 0.0;
double time_sec = 0.0;

void frameCount()
{
	time_c = (double)clock();
	time_temp = (time_c - time_e) / 1000.0;
	time_e = time_c;
	frame++;
	time_sec += time_temp;
	if (time_sec >= 1.0)
	{
		time_sec = 0.0;		
		frame_current = frame;
		frame = 0;
	}
	string str = "fps ";
	string str2 = std::to_string(frame_current);
	glfwSetWindowTitle(window, (str + str2).c_str());
}

void render()
{
	glClearColor(0.1f, 0.1f, 0.1f, 1.0f);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	//frame counter
	frameCount();

	// Create transformations
	glm::mat4 model;
	glm::mat4 view;
	glm::mat4 projection;

	cam_h.UpdateCam(view);

	projection = glm::perspective(cam_h.cam.Zoom, (GLfloat)wid / (GLfloat)hei, 0.1f, 1000.0f);
	view = cam_h.cam.GetViewMatrix();

	////light.UpdateLight(cam_h, projection, view, model);

	/*shaderLight.setMat4("projection", projection);
	shaderLight.setMat4("view", view);
	shaderLight.setMat4("model", model);*/

	//light.Update(cam_h, projection, view, model);

	////glActiveTexture(GL_TEXTURE0);
	////glBindTexture(GL_TEXTURE_2D, texture1);

	glBindVertexArray(VAO);
	//glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
	//glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0);
	//for (GLuint i = 0; i < 10; i++)
	//{
	//	glm::mat4 mod;
	//	mod = glm::translate(mod, cubePositions[i]);
	//	GLfloat angle = 0.1f * i;
	//	mod = glm::rotate(mod, (GLfloat)glfwGetTime() * angle, glm::vec3(1.0f, 0.3f, 0.5f));
	//	//glUniformMatrix4fv(modelLoc, 1, GL_FALSE, glm::value_ptr(model));
	//	light.SetModel(mod);
	//	//shaderLight.setMat4("model", model);

	//	glDrawArrays(GL_TRIANGLES, 0, 36);
	//}

	//for (GLuint i = 0; i < 10; i++)
	//{	
	//	glm::mat4 mod;
	//	mod = glm::translate(mod, glm::vec3(0.0f, 0.0f, -1.5f * i));
	//	GLfloat angle = 0.01f * i;
	//	mod = glm::rotate(mod, (GLfloat)glfwGetTime() * angle, glm::vec3(1.0f, 0.3f, 0.5f));
	//	//glUniformMatrix4fv(modelLoc, 1, GL_FALSE, glm::value_ptr(model));
	//	light.SetModel(mod);
	//	//shaderLight.setMat4("model", model);

	//	glDrawArrays(GL_TRIANGLES, 0, 36);
	//}
	
	/*for (size_t i = 0; i < arr.size(); i++)
	{
		arr[i]->SetRotate(glfwGetTime() * 19.0f, glm::vec3(0.0f, 0.0f, 1.0f));
		arr[i]->draw(light);
	}*/

	//--------skyBox-----------
	////glm::mat4 mod(1.0f);
	////mod = glm::rotate(mod, glm::radians(-90.0f), glm::vec3(0.0f, 0.0f, 1.0f));
	////mod = glm::scale(mod, glm::vec3(10.0f));
	////mod = glm::translate(mod, glm::vec3(0.0f, 0.0f, -5.0f));
	////
	////shaderModel->use();
	////shaderModel->setMat4("projection", projection);
	////shaderModel->setMat4("view", view);
	////shaderModel->setMat4("model", mod);
	////
	////modelSkyBox->Draw(*shaderModel);

	//--------------------------------
	glEnable(GL_CULL_FACE);
	glCullFace(GL_FRONT);
	glFrontFace(GL_CW);
	/*int quantity = 10;
	for (size_t z = 1; z <= quantity; z++)
	{
		for (size_t y = 1; y <= quantity; y++)
		{
			for (size_t x = 1; x <= quantity; x++)
			{
				mod = glm::mat4(1.0f);
				mod = glm::scale(mod, glm::vec3(0.1f));
				mod = glm::rotate(mod, glm::radians(-90.0f), glm::vec3(1.0f, 0.0f, 0.0f));
				mod = glm::translate(mod, glm::vec3(x * 20, y * 20, z * 20));

				shaderModel->setMat4("projection", projection);
				shaderModel->setMat4("view", view);
				shaderModel->setMat4("model", mod);

				modelMainCraft->Draw(*shaderModel);
			}
		}
	}*/
	myModel->draw(projection, view, shaderInstance);
	glDisable(GL_CULL_FACE);

	//draw models
	//shaderModel.use();

	//shaderModel.setMat4("projection", projection);
	//shaderModel.setMat4("view", view);

	// render the loaded model
	//glm::mat4 model_02 = glm::mat4(1.0f);
	//model_02 = glm::translate(model_02, glm::vec3(0.0f, 0.0f, 0.0f)); // translate it down so it's at the center of the scene
	//model_02 = glm::scale(model_02, glm::vec3(0.1f, 0.1f, 0.1f));	// it's a bit too big for our scene, so scale it down
	//shaderModel.setMat4("model", model_02);
	//modelM->Draw(shaderModel);
	//test
	//light.SetModel(model_02);
	
	// also draw the lamp object
	////light.UpdateLamp(projection, view, model);
	//lamp.use();
	//lamp.setMat4("projection", projection);
	//lamp.setMat4("view", view);
	//model = glm::mat4();
	//model = glm::translate(model, l_pos);
	//model = glm::scale(model, glm::vec3(0.2f)); // a smaller cube
	//lamp.setMat4("model", model);

	////glBindVertexArray(light.GetLightVAO());
	////glDrawArrays(GL_TRIANGLES, 0, 36);

	glBindVertexArray(0);
	glUseProgram(0);
}

void Initialize()
{
	//glGenVertexArrays(1, &VAO);
	//glGenBuffers(1, &VBO);
	///*glGenBuffers(1, &EBO);*/

	//glBindBuffer(GL_ARRAY_BUFFER, VBO);
	//glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);

	/*glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices), indices, GL_STATIC_DRAW);*/

	//glBindVertexArray(VAO);
	//// Position attribute
	//glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(GLfloat), (GLvoid*)0);
	//glEnableVertexAttribArray(0);
	//// нормали
	//glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(GLfloat), (GLvoid*)(3 * sizeof(GLfloat)));
	//glEnableVertexAttribArray(1);
	//// TexCoord attribute
	//glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 8 * sizeof(GLfloat), (GLvoid*)(6 * sizeof(GLfloat)));
	//glEnableVertexAttribArray(2);

	//glGenVertexArrays(1, &lightVAO);
	//glBindVertexArray(lightVAO);

	//glBindBuffer(GL_ARRAY_BUFFER, VBO);
	
	//glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)0);
	//glEnableVertexAttribArray(0);

	//shader = Shader();
	//shaderLight = Shader_Glad("shader//lightsVS.txt", "shader//lightsFS.txt");
	//lamp = Shader_Glad("shader//lampVS.txt", "shader//lampFS.txt");

	glEnable(GL_DEPTH_TEST);

	/*shaderLight.use();
	shaderLight.setInt("material.diffuse", 0);*/

	////int width, height;
	////unsigned char* image = SOIL_load_image("texture//stone.jpg", &width, &height, 0, SOIL_LOAD_RGB);
	
	////shaderModel = new Shader_Glad("shader//modelVS.txt", "shader//modelFS.txt");
	shaderInstance = new Shader_Glad("shader//instanceVS.txt", "shader//instanceFS.txt");

	//my model this
	////modelM = new Model("resources/test/test.FBX");
	////modelSkyBox = new Model("resources/skyBox/sky.FBX");
	modelMainCraft = new Model("resources/cubeMainCraft/firstCube.FBX");
	amount = 10;
	myModel = new CopyModel(modelMainCraft);
	//testing my model
	//myModel = new myModelFBX("resources/test.FBX");

	//arr.push_back(m);
	

	cam_h.Initialize();

	////glGenTextures(1, &texture1);
	////glBindTexture(GL_TEXTURE_2D, texture1);
	////
	////glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);	
	////glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
	////
	////glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	////glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	////
	////glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, image);
	////glGenerateMipmap(GL_TEXTURE_2D);
	////
	////SOIL_free_image_data(image);
	////glBindTexture(GL_TEXTURE_2D, 0);

	//light.Initialize(vertices, VBO);

	//create theh element of myModel
}

int main()
{
	//Инициализация GLFW
	glfwInit();
	//Настройка GLFW
	//Задается минимальная требуемая версия OpenGL. 
	//Мажорная 
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	//Минорная
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	//Установка профайла для которого создается контекст
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);
	//Выключение возможности изменения размера окна
	glfwWindowHint(GLFW_RESIZABLE, GL_FALSE);

	setlocale(LC_ALL, "Russian");

	window = glfwCreateWindow(wid, hei, "OpenGL shaders version", nullptr, nullptr);
	glfwSetWindowPos(window, 300, 100);

	if (window == nullptr)
	{
		std::cout << "Не удалось создать окно opengl" << std::endl;
		glfwTerminate();
		return -1;
	}

	glfwMakeContextCurrent(window);
	glewExperimental = GL_TRUE;

	if (glewInit() != GLEW_OK)
	{
		std::cout << "Не удалось инициализировать GLEW" << std::endl;
		return -1;
	}

	int width, height;
	glfwGetFramebufferSize(window, &width, &height);

	glViewport(0, 0, width, height);

	glfwSetKeyCallback(window, key_callback);
	glfwSetCursorPosCallback(window, mouse_callback);
	glfwSetScrollCallback(window, scroll_callback);

	// Спрятать курсор на форме и заморозить его
	glfwSetInputMode(window, GLFW_CURSOR, GLFW_CURSOR_DISABLED);

	Initialize();

	while (!glfwWindowShouldClose(window))
	{
		glfwPollEvents();

		render();

		glfwSwapBuffers(window);
	}

	glDeleteVertexArrays(1, &VAO);
	glDeleteBuffers(1, &VBO);
	glDeleteBuffers(1, &EBO);

	glfwTerminate();
	return 0;
}