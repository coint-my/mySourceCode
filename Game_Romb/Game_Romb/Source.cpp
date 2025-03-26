#include "gl\glut.h"
#include "manager_game.h"
#include <cmath>

#pragma comment(lib, "glut32.lib")

manager_game m_game;

void render()
{
	glClear(GL_COLOR_BUFFER_BIT);

	m_game.render();

	glFlush();
}

void Initialize()
{
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(0, WIDTH, HEIGHT, 0, -100, 100);

	glClearColor(0, 0, 0, 1.0);
}
void up_sec(int = 0)
{
	m_game.up_second();
	glutTimerFunc(500, up_sec, 0);
}
void update(int = 0)
{
	m_game.update();
	glutPostRedisplay();
	glutTimerFunc(30, update, 0);
}
void mouse(int b, int s, int x, int y)
{
	m_game.mouse(b, s, x, y);
}
void mout_mouse(int x, int y)
{
	m_game.m_mouse(x, y);
}

int main(int argc, char**argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGBA);
	glutInitWindowPosition(300, 100);
	glutInitWindowSize(WIDTH, HEIGHT);
	glutCreateWindow("Rombi");

	Initialize();

	update();
	up_sec();
	glutDisplayFunc(render);
	glutMouseFunc(mouse);
	glutPassiveMotionFunc(mout_mouse);

	glutMainLoop();
	return 0;
}