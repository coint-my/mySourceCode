#include <iostream>
#include <gl/glut.h>
#include <gl/GLU.h>
#include "ManagerEditor.h"

#pragma comment(lib, "opengl32.lib")
#pragma comment(lib, "glut32.lib")
#pragma comment(lib, "glu32.lib")

const int width = 800;
const int height = 600;

ManagerEditor man;

void render()
{
	glClear(GL_COLOR_BUFFER_BIT);

	man.Render();

	glutSwapBuffers();
}

void mouse(int button, int state, int _x, int _y)
{
	man.Mouse(button, state, _x, _y);
}
void passMouse(int _x, int _y)
{
	man.PassMouse(_x, _y);
}

void Initialize3D()
{
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA);
	glutInitWindowPosition(100, 100);
	glutInitWindowSize(width, height);
	glutCreateWindow("GameEngine3D");
	
	glClearColor(0.0, 0.0, 0.0, 1.0);

	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(20, width / height, 1, 1000);

	glTranslatef(0, 0, -60);
}

void Initialize2D()
{
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA);
	glutInitWindowPosition(100, 100);
	glutInitWindowSize(width, height);
	glutCreateWindow("GameEngine2D");

	glClearColor(0.0, 0.0, 0.0, 1.0);

	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(0, width, height, 0, -1000, 1000);

	man.Init();
}

void Update(int = 0)
{
	man.Update();
	glutPostRedisplay();
	glutTimerFunc(30, Update, 0);
}

int main(int argc, char** argv)
{
	glutInit(&argc, argv);

	Initialize2D();

	glutDisplayFunc(render);
	glutMouseFunc(mouse);
	glutPassiveMotionFunc(passMouse);
	Update();

	glutMainLoop();

	return 1;
}