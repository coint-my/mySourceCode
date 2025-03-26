#include "programm.h"
#include "painter.h"
#include "tetrino.h"
#include "unity.h"
#include "tetrinoAI.h"
#include <gl/glut.h>
#include <cstdlib>
#include <cmath>

static int width = 500;
static int height = 700;

int countDown = 0;

painter p;
unity u(8, 12, width, height);
tetrinoAI tetAI;

void Initialize()
{
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(0, width, height, 0, -1, 1);
	glClearColor(0.0, 0.0, 0.0, 1.0);
}

void display()
{
	glClear(GL_COLOR_BUFFER_BIT);
	glColor3f(1.0, 1.0, 1.0);
	u.draw(p);
	glFlush();
}

void timer(int = 0)
{
	glutPostRedisplay();
	glutTimerFunc(30, timer, 0);
}

void timer2(int = 0)
{
	//glutPostRedisplay();
	glutTimerFunc(1000, timer2, 0);
	
	u.moveDown();

}

void key(int ch, int x, int y)
{
	if(ch == GLUT_KEY_RIGHT)
	{
		u.move(tetrino::RIGHT);
	}
	else if(ch == GLUT_KEY_LEFT)
	{
		u.move(tetrino::LEFT);
	}
	else if(ch == GLUT_KEY_DOWN)
	{
		countDown++;
		if(countDown > 3)
		{
			u.moveDown();
			countDown = 0;
		}
		
	}
	else if(ch == GLUT_KEY_UP)
	{
		u.move(tetrino::UP);
	}
}

void main(int argc, char **argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);
	glutInitWindowSize(width, height);
	glutInitWindowPosition(100, 50);
	glutCreateWindow("game");

	Initialize();

	glutDisplayFunc(display);
	glutSpecialFunc(key);
	timer();
	timer2();

	glutMainLoop();
}
