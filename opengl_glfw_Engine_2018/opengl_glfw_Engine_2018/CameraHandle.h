#pragma once

#include "Camera.h"
#include <iostream>

#include "gl/glfw3.h"

class CameraHandle
{
public: Camera cam;
public: bool keys[1024];
private: GLfloat lastX = 200, lastY = 300;
private: bool firstMouse = true;

private: GLfloat deltaTime = 0.0f;
private: GLfloat lastFrame = 0.0f;

private: int _frame;
private: GLdouble _lastTime;
private: enum { SYNCRONIZE_OFF, SYNCRONIZE_ON_60, SYNCRONIZE_ON_30 };

public: CameraHandle(glm::vec3 _pos)
{
	cam.Position = _pos;
	_frame = 0;
	_lastTime = glfwGetTime();
}

public: void Initialize()
{
	// включить или выключить синхронизацию
	glfwSwapInterval(SYNCRONIZE_ON_60);
}

// Function prototypes
public: void key_callback(GLFWwindow* window, int key, int scancode, int action, int mode)
{
	if (key == GLFW_KEY_ESCAPE && action == GLFW_PRESS)
		glfwSetWindowShouldClose(window, GL_TRUE);
	if (key >= 0 && key < 1024)
	{
		if (action == GLFW_PRESS)
			keys[key] = true;
		else if (action == GLFW_RELEASE)
			keys[key] = false;
	}
}
public:	void scroll_callback(GLFWwindow* window, double xoffset, double yoffset)
{
	cam.ProcessMouseScroll(yoffset);
}
public:	void mouse_callback(GLFWwindow* window, double xpos, double ypos)
{
	if (firstMouse)
	{
		lastX = xpos;
		lastY = ypos;
		firstMouse = false;
	}

	GLfloat xoffset = xpos - lastX;
	GLfloat yoffset = lastY - ypos;  // Reversed since y-coordinates go from bottom to left

	lastX = xpos;
	lastY = ypos;

	cam.ProcessMouseMovement(xoffset, yoffset);
}

public: void UpdateCam(glm::mat4 &_view)
{
	// Set frame time
	GLfloat currentFrame = glfwGetTime();
	deltaTime = currentFrame - lastFrame;
	lastFrame = currentFrame;

	Do_Movement();

	_view = cam.GetViewMatrix();

	GLdouble currentTime = glfwGetTime();
	_frame++;
	if (currentTime - _lastTime >= 0)
	{
		std::cout << "frame second = " << _frame << std::endl;

		_frame = 0;
		_lastTime += 1.0;
	}
}

private: void Do_Movement()
{
	// Camera controls
	if (keys[GLFW_KEY_W])
		cam.ProcessKeyboard(FORWARD, deltaTime);
	if (keys[GLFW_KEY_S])
		cam.ProcessKeyboard(BACKWARD, deltaTime);
	if (keys[GLFW_KEY_A])
		cam.ProcessKeyboard(LEFT, deltaTime);
	if (keys[GLFW_KEY_D])
		cam.ProcessKeyboard(RIGHT, deltaTime);
}
};