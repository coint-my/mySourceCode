#include <cstdlib>
#include "Painter.h"
#include "Pole.h"
#include "LogGame.h"
#include "Character.h"

const int COUNT_TEXTURE = 20;

ui textures[COUNT_TEXTURE];

Painter _paint;
LogGame _game;

MyTexture m_texture;

void Display()
{
	glClear(GL_COLOR_BUFFER_BIT);

	glEnable(GL_TEXTURE_2D);
	//glEnable(GL_ALPHA_TEST);
	//glAlphaFunc(GL_NOTEQUAL, 0);
	//рисуем поле боя

	_game.Draw(_paint);

	//glDisable(GL_ALPHA_TEST);
	glDisable(GL_TEXTURE_2D);
	glutSwapBuffers();
}

void Mouse(int _button, int _state, int _x, int _y)
{
	_game.Mouse(_button, _state, _x, _y);
}
void PassiveMouse(int _x, int _y)
{
	_game.PassMouse(_x, _y);
}

void ActiveMouse(int _x, int _y)
{
	_game.ActiveMouse(_x, _y);
}

void LoadTexture(const char *_ch, const ui _t, const bool _isAlpha)
{
	//AUX_RGBImageRec *_texture = auxDIBImageLoadA(_ch);
	//if(_isAlpha)
	//{
	//	int _w = _texture->sizeX;
	//	int _h = _texture->sizeY;

	//	unsigned char *_b = new unsigned char[4 * _w * _h];

	//	for(int i = 0; i < _w * _h; i++)
	//	{
	//		_b[4 * i + 0] = _texture->data[3 * i + 0];
	//		_b[4 * i + 1] = _texture->data[3 * i + 1];
	//		_b[4 * i + 2] = _texture->data[3 * i + 2];
	//		if((_texture->data[3 * i + 0] == 0) && (_texture->data[3 * i + 1] == 0) && (_texture->data[3 * i + 2] == 0))
	//			_b[4 * i + 3] = 0;
	//		else
	//			_b[4 * i + 3] = 255;
	//	}

	//	_texture->data = _b;

	//	glBindTexture(GL_TEXTURE_2D, textures[_t - 1]);
	//	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	//	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	//	glTexImage2D(GL_TEXTURE_2D, 0, 4, _texture->sizeX, _texture->sizeY, 0 , GL_RGBA, GL_UNSIGNED_BYTE, _texture->data);

	//	//delete _b;
	/*}
	else
	{
		glBindTexture(GL_TEXTURE_2D, textures[_t - 1]);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
		glTexImage2D(GL_TEXTURE_2D, 0, 3, _texture->sizeX, _texture->sizeY, 0, GL_RGB, GL_UNSIGNED_BYTE, _texture->data);
	}*/
}

void Initialize()
{
	glClearColor(0.0, 0.0, 0.0, 1.0);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(0, 800, 600, 0, -100, 100);
	//загрузка текстур
	glGenTextures(COUNT_TEXTURE, &textures[0]);
	m_texture.LoadTexture("texture\\grass_zombi.bmp");
	m_texture.LoadTexture("texture\\goblin_01.bmp");
	m_texture.LoadTexture("texture\\pistol_01.bmp");
	m_texture.LoadTexture("texture\\pistol_02.bmp");
	m_texture.LoadTexture("texture\\pistol_f_01.bmp");
	m_texture.LoadTexture("texture\\pistol_f_02.bmp");
	/*LoadTexture("texture\\grass_zombi.bmp", 1, false);
	LoadTexture("texture\\goblin_01.bmp", 2, true);
	LoadTexture("texture\\pistol_01.bmp", 3, true);
	LoadTexture("texture\\pistol_02.bmp", 4, true);
	LoadTexture("texture\\pistol_f_01.bmp", 5, true);
	LoadTexture("texture\\pistol_f_02.bmp", 6, true);
	LoadTexture("texture\\enemy_02_vall.bmp", 7, true);
	LoadTexture("texture\\enemy_03_vall.bmp", 8, true);
	LoadTexture("texture\\enemy_04_vall.bmp", 9, true);
	LoadTexture("texture\\pistol_03_passive.bmp", 10, true);
	LoadTexture("texture\\pistol_03_attack.bmp", 11, true);
	LoadTexture("texture\\bullet_01.bmp", 12, true);
	LoadTexture("texture\\bullet_explode_01.bmp", 13, true);
	LoadTexture("texture\\pistol_04_passive.bmp", 14, true);
	LoadTexture("texture\\pistol_04_attack.bmp", 15, true);
	LoadTexture("texture\\bullet_explode_02.bmp", 16, true);
	LoadTexture("texture\\gun_blast.bmp", 17, true);
	LoadTexture("texture\\lighting_01.bmp", 18, true);*/

	//инициализация игры
	_game.Initialize(textures);
}

void timer(int = 0)
{
	_game.Update();

	glutPostRedisplay();
	glutTimerFunc(50, timer, 0);
}

int main(int argc, char **argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB);
	glutInitWindowPosition(100, 100);
	glutInitWindowSize(800, 600);
	glutCreateWindow("Game_Zombie");
	
	Initialize();

	glutDisplayFunc(Display);
	//glutKeyboardFunc(Keyboard);
	glutMouseFunc(Mouse);
	glutPassiveMotionFunc(PassiveMouse);
	glutMotionFunc(ActiveMouse);
	timer();

	glutMainLoop();

	return 0;
}
