#pragma comment(lib, "glew32s.lib")
#include "Shader.h"
#include "CameraHandle.h"
#include "PointLight.h"

//#include <gl/glfw3.h>
#include <iostream>
#include <cmath>

#pragma comment(lib, "SOIL.lib")
#include "src/SOIL.h"
GLuint texture1;

static int wid = 800, hei = 600;
//окно OpenGL
GLFWwindow* window;
// Камера
CameraHandle cam_h;
PointLight light;
//Shader_Glad shaderLight;
//Shader_Glad lamp;
unsigned int lightVAO;
//glm::vec3 l_pos(1.2f, 1.0f, 2.0f);

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

//GLfloat vertices[] = {
//	0.5f,  0.5f, 0.0f,   1.0f, 0.0f, 0.0f,   1.0f, 1.0f, // Top Right
//	0.5f, -0.5f, 0.0f,   0.0f, 1.0f, 0.0f,   1.0f, 0.0f, // Bottom Right
//	-0.5f, -0.5f, 0.0f,   0.0f, 0.0f, 1.0f,   0.0f, 0.0f, // Bottom Left
//	-0.5f,  0.5f, 0.0f,   1.0f, 1.0f, 0.0f,   0.0f, 1.0f  // Top Left
//};
//GLuint indices[] = {  // Помните, что мы начинаем с 0!
//	0, 1, 3,   // Первый треугольник
//	1, 2, 3    // Второй треугольник
//};

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
//Shader shader;

void render()
{
	glClearColor(0.1f, 0.1f, 0.1f, 1.0f);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	// be sure to activate shader when setting uniforms/drawing objects
	//shaderLight.use();
	//shaderLight.setVec3("light.position", l_pos);
	//shaderLight.setVec3("viewPos", cam_h.cam.GetPos());

	//// light properties
	//shaderLight.setVec3("light.ambient", 0.2f, 0.2f, 0.2f);
	//shaderLight.setVec3("light.diffuse", 0.5f, 0.5f, 0.5f);
	//shaderLight.setVec3("light.specular", 1.0f, 1.0f, 1.0f);

	//// material properties
	//shaderLight.setVec3("material.specular", 0.5f, 0.5f, 0.5f);
	//shaderLight.setFloat("material.shininess", 64.0f);

	// Create transformations
	glm::mat4 model;
	glm::mat4 view;
	glm::mat4 projection;

	cam_h.UpdateCam(view);

	projection = glm::perspective(cam_h.cam.Zoom, (GLfloat)wid / (GLfloat)hei, 0.1f, 100.0f);
	view = cam_h.cam.GetViewMatrix();

	light.UpdateLight(cam_h, projection, view, model);

	/*shaderLight.setMat4("projection", projection);
	shaderLight.setMat4("view", view);
	shaderLight.setMat4("model", model);*/

	//light.Update(cam_h, projection, view, model);

	glActiveTexture(GL_TEXTURE0);
	glBindTexture(GL_TEXTURE_2D, texture1);

	glBindVertexArray(VAO);
	//glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
	//glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0);
	for (GLuint i = 0; i < 10; i++)
	{
		glm::mat4 model;
		model = glm::translate(model, cubePositions[i]);
		GLfloat angle = 0.1f * i;
		model = glm::rotate(model, (GLfloat)glfwGetTime() * angle, glm::vec3(1.0f, 0.3f, 0.5f));
		//glUniformMatrix4fv(modelLoc, 1, GL_FALSE, glm::value_ptr(model));
		light.SetModel(model);
		//shaderLight.setMat4("model", model);

		glDrawArrays(GL_TRIANGLES, 0, 36);
	}

	// also draw the lamp object
	light.UpdateLamp(projection, view, model);
	//lamp.use();
	//lamp.setMat4("projection", projection);
	//lamp.setMat4("view", view);
	//model = glm::mat4();
	//model = glm::translate(model, l_pos);
	//model = glm::scale(model, glm::vec3(0.2f)); // a smaller cube
	//lamp.setMat4("model", model);

	glBindVertexArray(light.GetLightVAO());
	glDrawArrays(GL_TRIANGLES, 0, 36);

	glBindVertexArray(0);
	glUseProgram(0);
}

void Initialize()
{
	glGenVertexArrays(1, &VAO);
	glGenBuffers(1, &VBO);
	/*glGenBuffers(1, &EBO);*/

	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);

	/*glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices), indices, GL_STATIC_DRAW);*/

	glBindVertexArray(VAO);
	// Position attribute
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(GLfloat), (GLvoid*)0);
	glEnableVertexAttribArray(0);
	// нормали
	glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(GLfloat), (GLvoid*)(3 * sizeof(GLfloat)));
	glEnableVertexAttribArray(1);
	// TexCoord attribute
	glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 8 * sizeof(GLfloat), (GLvoid*)(6 * sizeof(GLfloat)));
	glEnableVertexAttribArray(2);

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

	int width, height;
	unsigned char* image = SOIL_load_image("texture//stone.jpg", &width, &height, 0, SOIL_LOAD_RGB);
	//shader = Shader("shader//textureVS.txt", "shader//textureFS.txt");

	cam_h.Initialize();

	glGenTextures(1, &texture1);
	glBindTexture(GL_TEXTURE_2D, texture1);

	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);	
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
	
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, image);
	glGenerateMipmap(GL_TEXTURE_2D);

	SOIL_free_image_data(image);
	glBindTexture(GL_TEXTURE_2D, 0);

	light.Initialize(vertices, VBO);
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
	glfwSetWindowPos(window, 400, 300);

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