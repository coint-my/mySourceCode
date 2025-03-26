#include <iostream>
#include <cmath>
#include <gl/glut.h>

#define M_PI 3.14159 * 2

#pragma comment(lib, "opengl32.lib")
#pragma comment(lib, "glut32.lib")

int width = 600;
int height = 400;
int rot = 24;
int rad = 100;

void keyboard(unsigned char _key, int _x, int _y)
{
	if(_key == 'w')
	{
		rot++;
	}
	else if(_key == 's')
	{
		rot--;
	}
	else if(_key == 'e')
	{
		rad++;
	}
	else if(_key == 'd')
	{
		rad--;
	}

	glutPostRedisplay();
}

void display()
{
	glClear(GL_COLOR_BUFFER_BIT);

	glColor3f(1.0, 0.7, 0.7);
	glBegin(GL_LINE_STRIP);
	for(int i = 0; i < rot; i++)
	{
		float angle = M_PI / (rot - 1);
		float _x = (width / 2) + (cos(angle * i) * rad);
		float _y = (height / 2) + (sin(angle * i) * rad);

		glVertex2f(_x, _y);
	}
	glEnd();

	glFlush();
}

void Initialize()
{
	glutInitWindowPosition(10, 10);
	glutInitWindowSize(width, height);
	glutCreateWindow("test");
	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(0, width, height, 0, -1, 1);
	glClearColor(0.0, 0.0, 0.0, 1.0);

	glutDisplayFunc(display);
}

int main(int argc, char** argv)
{
	glutInit(&argc, argv);

	Initialize();

	glutKeyboardFunc(keyboard);

	glutMainLoop();

	return 0;
}