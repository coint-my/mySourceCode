#include <iostream>
#include <gl/glut.h>
#include <string.h>
#include <glut/glui.h>
#include "Map.h"

#define M_PI 3.14159 * 2

#pragma comment(lib, "opengl32.lib")
#pragma comment(lib, "glut32.lib")

short mass[] = 
{
	1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
	0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1,
	1, 0, 0, 0, 2, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1,
	1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1,
	1, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 1,
	1, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1,
	1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1,
	1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1,
	1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 0, 1,
	1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1
};

int width = 600;
int height = 400;
short direct = 0;

bool isDown_R = false;
bool isDown_L = false;
bool isDown_B = false;
bool isDown_T = false;

Map map;
Tile player;

void keyboard(unsigned char _key, int _x, int _y)
{
	if(_key == 'd')
		direct = 2;
	if(_key == 'a')
		direct = 1;
	if(_key == 'w')
		direct = 3;
	if(_key == 's')
		direct = 4;

	if(_key == ',')
	{
		glScalef(0.99, 0.99, 0);
	}
	if(_key == '.')
	{
		glScalef(1.01, 1.01, 0);
	}

	if(_key > 48 && _key < 57)
	{
		char c = _key;
		char *cc = &c;
		map.LoadLevel(atoi(cc));
		player = Tile(5, TILE_H + 10, TILE_W / 2, TILE_H / 2, Color(0.5, 0.01, 0.01));
	}
}
void keySpecial(int _key, int _x, int _y)
{
	if(_key == GLUT_KEY_RIGHT)
		isDown_R = true;
	else if(_key == GLUT_KEY_LEFT)
		isDown_L = true;
	else if(_key == GLUT_KEY_UP)
		isDown_T = true;
	else if(_key == GLUT_KEY_DOWN)
		isDown_B = true;
}

void keySpecialUp(int _key, int _x, int _y)
{
	if(_key == GLUT_KEY_RIGHT)
		isDown_R = false;
	else if(_key == GLUT_KEY_LEFT)
		isDown_L = false;
	else if(_key == GLUT_KEY_UP)
		isDown_T = false;
	else if(_key == GLUT_KEY_DOWN)
		isDown_B = false;
}

void display()
{
	glClear(GL_COLOR_BUFFER_BIT);
	map.Draw();
	Painter::DrawBlook(player.GetX(), player.GetY(), player.GetWid(), player.GetHei(), player.color);
	glutSwapBuffers();
}

void timer(int = 0)
{
	map.Update(player, direct);

	if(isDown_R)
		direct = 2;
	else if(isDown_L)
		direct = 1;
	else if(isDown_T)
		direct = 3;
	else if(isDown_B)
		direct = 4;
	else direct = 0;

	glutPostRedisplay();
	glutTimerFunc(20, timer, 0);
}

void Initialize()
{
	glutInitWindowPosition(100, 100);
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB);
	glutInitWindowSize(width, height);
	glutCreateWindow("test");
	glClearColor(0.0, 0.0, 0.0, 1.0);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(0, width, height, 0, -1, 1);

	map.Init(mass, 15, 10);
	player = Tile(5, TILE_H + 10, TILE_W / 2, TILE_H / 2, Color(0.5, 0.01, 0.01));
	glTranslatef(width / 2, height / 2, 0);

	std::cout << "Player : " << sizeof(player) << " size" << std::endl;
	std::cout << "Map : " << sizeof(mass) << " size" << std::endl;
}

int main(int argc, char** argv)
{
	glutInit(&argc, argv);

	Initialize();

	glutDisplayFunc(display);
	
	glutKeyboardFunc(keyboard);
	
	glutSpecialFunc(keySpecial);
	glutSpecialUpFunc(keySpecialUp);

	glutTimerFunc(30, timer, 0);

	glutMainLoop();

	return 0;
}