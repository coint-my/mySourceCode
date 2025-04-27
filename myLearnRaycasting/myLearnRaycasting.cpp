#include <iostream>
#include "freeglut.h"
#include "glut.h"

#pragma comment(lib, "freeglut.lib")

int WID = 1280, HEI = 720;

void myRender()
{

}

void myChangeSize(int w, int h)
{
	// предупредим деление на ноль
	if (h == 0)
		h = 1;

	// определяем окно просмотра
	glViewport(0, 0, w, h);
}

int main(int argc, char** argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE | GLUT_DEPTH);
	glutInitWindowPosition(200, 100);
	glutInitWindowSize(WID, HEI);
	glutCreateWindow("raycasting");
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glViewport(0, 0, WID, HEI);
	gluOrtho2D(0, WID, HEI, 0);
	glClearColor(0, 0, 0, 1);
	glutDisplayFunc(myRender);
	glutReshapeFunc(myChangeSize);

	glutMainLoop();

    return 0;
}