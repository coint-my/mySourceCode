#include <iostream>
#include <cmath>
#include <gl/glut.h>
#include "GameManager.h"

GameManager game;

void display()
{
	glClear(GL_COLOR_BUFFER_BIT);
	game.Draw();
	glFlush();
}

void passMouse(int _x, int _y)
{
	game.PassiveMouse(_x, _y);
}

void mouse(int button, int state, int x, int y)
{
	game.Mouse(button, state, x, y);
}

void activeMouse(int _x, int _y)
{
	game.ActiveMouse(_x, _y);
}

void timer(int = 0)
{
	glutPostRedisplay();
	glutTimerFunc(30, timer, 0);
}

void timer2(int = 0)
{
	game.Timer();
	glutTimerFunc(200, timer2, 0);
}

void Initialize()
{
	glutInitWindowPosition(10, 10);
	glutInitWindowSize(width, height);
	glutCreateWindow("Game_Color");
	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(0, width, height, 0, -1, 1);
	glClearColor(0.0, 0.0, 0.0, 1.0);

	glutDisplayFunc(display);

	game.InitPole();

	timer();
	timer2();
}

int main(int argc, char** argv)
{
	glutInit(&argc, argv);

	Initialize();
	glutPassiveMotionFunc(passMouse);
	glutMotionFunc(activeMouse);
	glutMouseFunc(mouse);

	glutMainLoop();

	return 0;
}