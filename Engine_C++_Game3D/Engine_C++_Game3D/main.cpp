#include <gl/glut.h>
#include "Editor.h"

//#pragma comment(lib, "glut32.lib");
#pragma comment(lib, "glu32.lib");

static const int width = 1000;
static const int height = 700;

Editor edit;

void display()
{
	glClear(GL_COLOR_BUFFER_BIT);

	edit.draw();

	glutSwapBuffers();
}

void timer(int = 0)
{
	glutPostRedisplay();
	glutTimerFunc(40, timer, 0);
	edit.update();
}

void mouse(int button, int state, int _x, int _y)
{
	edit.mouse(button, state, _x, _y);
}
void move(int _x, int _y)
{
	edit.move(_x, _y);
}
void keyboard(unsigned char _key, int _x, int _y)
{
	edit.keyboard(_key, _x, _y);
}

void key(int _key, int _x, int _y)
{
	edit.key(_key, _x, _y);
}
void passMove(int _x, int _y)
{
	edit.passMove(_x, _y);
}

void Initialize()
{
	glClearColor(0.0, 0.0, 0.0, 1.0);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(45, width / height, 0.1, 1000);
	//glFrustum(-100, 100, -100, 100, 10, 1000);
	//glOrtho(-width, width, -height, height, -1000, 1000);

	edit.Initialize();
}

int main(int argc, char **argv)
{
	glutInit(&argc, argv);
	glutInitWindowPosition(100, 100);
	glutInitWindowSize(width, height);
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA);
	glutCreateWindow("Engine_C++_Game3D");

	Initialize();

	glutMouseFunc(mouse);
	glutMotionFunc(move);
	glutPassiveMotionFunc(passMove);
	glutSpecialFunc(key);
	glutKeyboardFunc(keyboard);

	timer();
	glutDisplayFunc(display);

	glutMainLoop();
}
