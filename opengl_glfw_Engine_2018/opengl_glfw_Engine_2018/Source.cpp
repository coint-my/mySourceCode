#define GLEW_STATIC
#pragma once
#pragma comment(lib, "glew32s.lib")
#pragma comment(lib, "glfw3.lib")
#pragma comment(lib, "SOIL.lib")
#pragma comment(lib, "opengl32.lib")
#pragma comment(lib, "assimp-vc140-mt.lib")

#include "CameraHandle.h"
#include "PointLight.h"
#include "Model.h"

#include "loadLevel.h"

#include <iostream>
#include <cmath>

#include "src/SOIL.h"
GLuint texture1;

static int wid = 1280, hei = 740;
//окно OpenGL
GLFWwindow* window;
// Камера
CameraHandle cam_h(glm::vec3(8, 1, 4));
PointLight light = PointLight(glm::vec3(20, 5, 10));
loadLevel l_level("Level1.txt");
const int maxCubX = 20, maxCubZ = 20;
//Shader_Glad *shader;
//Model *box;

//GLuint LoadShaders(const char * vertex_file_path, const char * fragment_file_path)
//{
//	setlocale(LC_ALL, "Russian");
//	// Создаем шейдеры
//	GLuint VertexShaderID = glCreateShader(GL_VERTEX_SHADER);
//	GLuint FragmentShaderID = glCreateShader(GL_FRAGMENT_SHADER);
//
//	// Загружаем код Вершинного Шейдера из файла
//	std::string VertexShaderCode;
//	std::ifstream VertexShaderStream(vertex_file_path, std::ios::in);
//	if (VertexShaderStream.is_open())
//	{
//		std::stringstream sstr;
//		sstr << VertexShaderStream.rdbuf();
//		VertexShaderCode = sstr.str();
//		VertexShaderStream.close();
//	}
//
//	// Загружаем код Фрагментного шейдера из файла
//	std::string FragmentShaderCode;
//	std::ifstream FragmentShaderStream(fragment_file_path, std::ios::in);
//	if (FragmentShaderStream.is_open()) {
//		std::stringstream sstr;
//		sstr << FragmentShaderStream.rdbuf();
//		FragmentShaderCode = sstr.str();
//		FragmentShaderStream.close();
//	}
//
//	GLint Result = GL_FALSE;
//	int InfoLogLength;
//
//	// Компилируем Вершинный шейдер
//	printf("Компиляция шейдера: %s\n", vertex_file_path);
//	char const * VertexSourcePointer = VertexShaderCode.c_str();
//	glShaderSource(VertexShaderID, 1, &VertexSourcePointer, NULL);
//	glCompileShader(VertexShaderID);
//
//	// Выполняем проверку Вершинного шейдера
//	glGetShaderiv(VertexShaderID, GL_COMPILE_STATUS, &Result);
//	glGetShaderiv(VertexShaderID, GL_INFO_LOG_LENGTH, &InfoLogLength);
//	if (InfoLogLength > 0) {
//		std::vector<char> VertexShaderErrorMessage(InfoLogLength + 1);
//		glGetShaderInfoLog(VertexShaderID, InfoLogLength, NULL, &VertexShaderErrorMessage[0]);
//		fprintf(stdout, "%s\n", &VertexShaderErrorMessage[0]);
//	}
//
//	// Компилируем Фрагментный шейдер
//	printf("Компиляция шейдера: %s\n", fragment_file_path);
//	char const * FragmentSourcePointer = FragmentShaderCode.c_str();
//	glShaderSource(FragmentShaderID, 1, &FragmentSourcePointer, NULL);
//	glCompileShader(FragmentShaderID);
//
//	// Проверяем Фрагментный шейдер
//	glGetShaderiv(FragmentShaderID, GL_COMPILE_STATUS, &Result);
//	glGetShaderiv(FragmentShaderID, GL_INFO_LOG_LENGTH, &InfoLogLength);
//	if (InfoLogLength > 0) {
//		std::vector<char> FragmentShaderErrorMessage(InfoLogLength + 1);
//		glGetShaderInfoLog(FragmentShaderID, InfoLogLength, NULL, &FragmentShaderErrorMessage[0]);
//		fprintf(stdout, "%s\n", &FragmentShaderErrorMessage[0]);
//	}
//
//	// Создаем шейдерную программу и привязываем шейдеры к ней
//	fprintf(stdout, "Создаем шейдерную программу и привязываем шейдеры к ней\n");
//	GLuint ProgramID = glCreateProgram();
//	glAttachShader(ProgramID, VertexShaderID);
//	glAttachShader(ProgramID, FragmentShaderID);
//	glLinkProgram(ProgramID);
//
//	// Проверяем шейдерную программу
//	glGetProgramiv(ProgramID, GL_LINK_STATUS, &Result);
//	glGetProgramiv(ProgramID, GL_INFO_LOG_LENGTH, &InfoLogLength);
//	if (InfoLogLength > 0) {
//		std::vector<char> ProgramErrorMessage(InfoLogLength + 1);
//		glGetProgramInfoLog(ProgramID, InfoLogLength, NULL, &ProgramErrorMessage[0]);
//		fprintf(stdout, "%s\n", &ProgramErrorMessage[0]);
//	}
//
//	glDeleteShader(VertexShaderID);
//	glDeleteShader(FragmentShaderID);
//
//	return ProgramID;
//}

void key_callback(GLFWwindow* window, int key, int scancode, int action, int mode)
{
	// Когда пользователь нажимает ESC, мы устанавливаем свойство WindowShouldClose в true, 
	// и приложение после этого закроется
	if (key == GLFW_KEY_ESCAPE && action == GLFW_PRESS)
		glfwSetWindowShouldClose(window, GL_TRUE);

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

glm::vec3 cubePositions[] = {
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
	// Back face		// normals		// texture coords
	-0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
	0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
	0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 0.0f,
	0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
	-0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
	-0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f,
	// Front face
	-0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
	0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 0.0f,
	0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
	0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
	-0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f,
	-0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
	// Left face
	-0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
	-0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
	-0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
	-0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
	-0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
	-0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
	// Right face
	0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
	0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
	0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
	0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
	0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
	0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
	// Bottom face
	-0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
	0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
	0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
	0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
	-0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f,
	-0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
	// Top face
	-0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f,
	0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
	0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 1.0f,
	0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
	-0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f,
	-0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f
};

GLuint VBO, VAO, EBO;
// Create transformations
glm::mat4 model;
glm::mat4 view;
glm::mat4 projection;

void render()
{
	glClearColor(0.1f, 0.1f, 0.1f, 1.0f);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT | GL_STENCIL_BUFFER_BIT);

	cam_h.UpdateCam(view);

	projection = glm::perspective(cam_h.cam.Zoom, (GLfloat)wid / (GLfloat)hei, 0.1f, 10000.0f);
	view = cam_h.cam.GetViewMatrix();

	// also draw the lamp object
	light.UpdateLamp(projection, view, model);

	glBindVertexArray(0);
	glUseProgram(0);
}

void Initialize()
{
	glEnable(GL_DEPTH_TEST);

	/*int width, height;
	unsigned char* image = SOIL_load_image("texture//stone.jpg", &width, &height, 0, SOIL_LOAD_RGB);*/
	//shader = Shader("shader//textureVS.txt", "shader//textureFS.txt");

	cam_h.Initialize();
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
	glfwSetWindowPos(window, 200, 200);

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

	//Shader_Glad ourShader("shader//model_loadingVS.txt", "shader//model_loadingFS.txt");

	/*std::cout << "loading model (nanosuit.obj)" << std::endl;
	Model ourModel("resources//nanosuit//nanosuit.obj");
	std::cout << "is ok (nanosuit.obj)" << std::endl;*/

	Shader_Glad shader("shader//arrayVS.txt", "shader//arrayFS.txt");
	//Model rock("resources//rock//rock.obj");
	Model rock("resources/box/box_02.FBX");
	//Model rock("resources//box_wood_1.FBX");
	//Model rock("resources//armor_helm_1.FBX");
	l_level.Load();
	//unsigned int amount = 250000;
	//glm::mat4* modelMatrices;
	//modelMatrices = new glm::mat4[amount];
	//l_level.Load();
	//srand(glfwGetTime()); // initialize random seed	
	//float radius = 150.0;
	//float offset = 130.0f;
	//for (unsigned int i = 0; i < amount; i++)
	//{
	//	glm::mat4 model;
	//	// 1. translation: displace along circle with 'radius' in range [-offset, offset]
	//	float angle = (float)i / (float)amount * 360.0f;
	//	float displacement = (rand() % (int)(2 * offset * 100)) / 100.0f - offset;
	//	float x = sin(angle) * radius + displacement;
	//	displacement = (rand() % (int)(2 * offset * 100)) / 100.0f - offset;
	//	float y = displacement * 0.4f; // keep height of asteroid field smaller compared to width of x and z
	//	displacement = (rand() % (int)(2 * offset * 100)) / 100.0f - offset;
	//	float z = cos(angle) * radius + displacement;
	//	model = glm::translate(model, glm::vec3(x, y, z));

	//	// 2. scale: Scale between 0.05 and 0.25f
	//	float scale = (rand() % 20) / 100.0f + 0.05;
	//	model = glm::scale(model, glm::vec3(scale));

	//	// 3. rotation: add random rotation around a (semi)randomly picked rotation axis vector
	//	float rotAngle = (rand() % 360);
	//	model = glm::rotate(model, rotAngle, glm::vec3(0.4f, 0.6f, 0.8f));

	//	// 4. now add to list of matrices
	//	modelMatrices[i] = model;
	//}

	// configure instanced array
	// -------------------------
	unsigned int buffer;
	glGenBuffers(1, &buffer);
	glBindBuffer(GL_ARRAY_BUFFER, buffer);
	//glBufferData(GL_ARRAY_BUFFER, amount * sizeof(glm::mat4), &modelMatrices[0], GL_STATIC_DRAW);
	glBufferData(GL_ARRAY_BUFFER, l_level.data.size() *sizeof(glm::mat4), &l_level.data[0], GL_STATIC_DRAW);

	// set transformation matrices as an instance vertex attribute (with divisor 1)
	// note: we're cheating a little by taking the, now publicly declared, VAO of the model's mesh(es) and adding new vertexAttribPointers
	// normally you'd want to do this in a more organized fashion, but for learning purposes this will do.
	// -----------------------------------------------------------------------------------------------------------------------------------
	for (unsigned int i = 0; i < rock.meshes.size(); i++)
	{
		unsigned int VAO = rock.meshes[i].VAO;
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

	while (!glfwWindowShouldClose(window))
	{
		render();

		shader.use();
		shader.setMat4("projection", projection);
		shader.setMat4("view", view);
		shader.setInt("texture_diffuse1", 0);

		glActiveTexture(GL_TEXTURE0);
		glBindTexture(GL_TEXTURE_2D, rock.textures_loaded[0].id);
		for (unsigned int i = 0; i < rock.meshes.size(); i++)
		{
			glBindVertexArray(rock.meshes[i].VAO);
			glDrawElementsInstanced(GL_TRIANGLES, rock.meshes[i].indices.size(), GL_UNSIGNED_INT, 0, l_level.data.size());
			//glDrawElementsInstanced(GL_TRIANGLES, rock.meshes[i].indices.size(), GL_UNSIGNED_INT, 0, amount);
			glBindVertexArray(0);
		}

		glfwSwapBuffers(window);
		glfwPollEvents();
	}

	glDeleteVertexArrays(1, &VAO);
	glDeleteBuffers(1, &VBO);
	glDeleteBuffers(1, &EBO);

	glfwTerminate();
	return 0;
}