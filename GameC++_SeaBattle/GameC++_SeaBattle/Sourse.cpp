#pragma once
#include <iostream>
#include "gl/glut.h"
#include "Painter.h"
#include "Pole.h"
#include "BattleGame.h"
#include "Effect.h"

#pragma comment(lib, "glut32.lib")
#pragma comment(lib, "opengl32.lib")

Painter _p;
Painter _pColor;
short Painter::wid = 24;
short Painter::hei = 24;
short counter_round_time = 0;
short counter_char = 0;
float color_val = 0;
float r_enemy;

int mouse_x, mouse_y;
unsigned int textures[10];

bool is_visible_enemy = false;
bool is_player_turn = true;
bool is_enemy_turn = false;
bool is_effect = true;
bool is_end_battle = false;

Pole player;
Pole playerVisible;
Pole enemy;
Pole enemyVisible;
BattleGame battle_game;
MyRect rect_enemy;
MyRect rect_player;
std::vector<Effect> arr_effect;

void Keyboard(unsigned char key, int _x, int _y)
{
	if(key == 'p')
	{
		//p.InitRandom();
	}
}

void LoadTexture(const char *_ch, const unsigned int _t)
{
	AUX_RGBImageRec *_texture = auxDIBImageLoadA(_ch);
	glBindTexture(GL_TEXTURE_2D, textures[_t - 1]);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexImage2D(GL_TEXTURE_2D, 0, 3, _texture->sizeX, _texture->sizeY, 0 , GL_RGB, GL_UNSIGNED_BYTE, _texture->data);
}

void ShowShuttle()
{
	short wid = Painter::wid;
	short hei = Painter::hei;

	for(short j = 0; j < HEIGHT_POLE; j++)
	{
		for(short i = 0; i < WIDTH_POLE; i++)
		{
			if(enemy.GetGex(i, j).value == 3 && enemyVisible.GetGex(i, j).value != 1 && enemyVisible.GetGex(i, j).value != 4)
			{
				Gex _gex = enemy.GetGex(i, j);
				enemyVisible.SetGex(i, j, _gex);
			}
		}
	}
}

void UpdateGame(Pole &_pole, const short _count)
{
	glEnable(GL_TEXTURE_2D);

	if(_count >= 9)
	{	
		_pole.Draw(_p, color_val -= 0.005);
		ShowShuttle();
		is_enemy_turn = false;
		is_player_turn = false;
		is_end_battle = true;

		char _char[25] = "    (GAME OVER)";

		for(short _c = 0; _c < counter_char; _c++)
		{
			battle_game.SetStringLinear(_char[_c], _pole.offset_x + 15 * _c, _pole.offset_y + (HEIGHT_POLE * (Painter::hei + 1)) / 2);
		}

		counter_round_time++;
		if(counter_round_time > 22)
		{
			counter_round_time = 0;

			if(counter_char < 25)
				counter_char++;
		}
	}
	else
	{
		//glBindTexture(GL_TEXTURE_2D, textures[0]);
		_pole.Draw(_p, textures);

		if(arr_effect.size() > 0)
		{
			for(short i = 0; i < arr_effect.size(); i++)
			{
				arr_effect[i].UpdateEffect(70, -0.03f);

				if(!arr_effect[i].IsExplode())
				{
					arr_effect.erase(arr_effect.begin() + i);
				}
			}
		}
	}
	glDisable(GL_TEXTURE_2D);
}

void Mouse(int button, int state, int _x, int _y)
{
	if(button == 0 && state == GLUT_UP)
	{
		mouse_x = _x;
		mouse_y = _y;

		if(is_player_turn)
		{
			is_player_turn = (battle_game.Shoot_Down_Player(enemy, enemyVisible, _x, _y));

			if(!is_player_turn)
			{
				is_enemy_turn = true;
				counter_round_time = 0;
			}
			else
			{
				if(arr_effect.size() < 10)
				{
					Effect _eff;
					Gex _gex = battle_game.Get_Object(enemyVisible, _x, _y);
					_eff.InitEffect(_gex.x, _gex.y, 8, 8, 4);
					arr_effect.push_back(_eff);
				}
			}
		}
	}
}

void Display()
{
	glClear(GL_COLOR_BUFFER_BIT);

	if(!is_end_battle)
	{
		if(is_enemy_turn)
		{
			_pColor.SetColor(0.0, r_enemy, 0.0);
			_pColor.DrawRect(rect_enemy.Left(), rect_enemy.Top(), rect_enemy.Right(), rect_enemy.Bottom());
			battle_game.SetString("Enemy ", 20 + ((WIDTH_POLE * Painter::wid) / 2 - 20), 14);
		}
		else
		{
			_pColor.SetColor(0.0, r_enemy, 0.0);
			_pColor.DrawRect(rect_player.Left(), rect_player.Top(), rect_player.Right(), rect_player.Bottom());
			battle_game.SetString("Player ", enemyVisible.offset_x + ((WIDTH_POLE * Painter::wid) / 2 - 20), 14);
		}
	}

	UpdateGame(enemyVisible, battle_game.GetCountEnemy());
	UpdateGame(playerVisible, battle_game.GetCountPlayer());

	//тест
	//glEnable(GL_TEXTURE_2D);
	//glBindTexture(GL_TEXTURE_2D, textures[0]);
	//_p.DrawRect(300, 300, true);
	//glDisable(GL_TEXTURE_2D);

	if(arr_effect.size() > 0)
	{
		for(short i = 0; i < arr_effect.size(); i++)
			arr_effect[i].DrawEffect(_p);
	}
	glutSwapBuffers();
}


void Initialize()
{
	glClearColor(0.0, 0.0, 0.0, 1.0);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(0, 640, 480, 0, -1, 1);	
	player.InitRandom();
	playerVisible = player;
	enemy = Pole(300, 20);
	enemy.InitRandom();
	enemyVisible = Pole(300, 20);
	rect_enemy = MyRect(playerVisible.offset_x - 2, playerVisible.offset_y - 2, 
		playerVisible.offset_x + (WIDTH_POLE * (Painter::wid + 1)) + 1, playerVisible.offset_y + (HEIGHT_POLE * (Painter::hei + 1)) + 1);
	rect_player = MyRect(enemyVisible.offset_x - 2, enemyVisible.offset_y - 2, 
		enemyVisible.offset_x + (WIDTH_POLE * (Painter::wid + 1)) + 1, enemyVisible.offset_y + (HEIGHT_POLE * (Painter::hei + 1)) + 1);
	is_visible_enemy = false;

	glGenTextures(10, &textures[0]);

	LoadTexture("sea_1.bmp", 1);
	LoadTexture("dead_1.bmp", 2);
	LoadTexture("miss_1.bmp", 3);
	LoadTexture("shuttle_1.bmp", 4);
}

void timer(int = 0)
{
	if(is_effect)
	{
		if(r_enemy < 0.7)
			r_enemy += 0.1;
		else
			is_effect = false;
	}
	else
	{
		if(r_enemy > 0.1)
			r_enemy -= 0.1;
		else
			is_effect = true;
	}
	glutPostRedisplay();
	glutTimerFunc(50, timer, 0);
}

void timer_enemy(int = 0)
{
	if(is_enemy_turn && counter_round_time > 10)
	{
		Gex _gex;
		is_enemy_turn = battle_game.Shoot_Down_Enemy(player, playerVisible, _gex);

		if(!is_enemy_turn)
			is_player_turn = true;
		else
		{
			Effect _eff;
			_eff.InitEffect(_gex.x, _gex.y, 8, 8, 4);
			arr_effect.push_back(_eff);
		}

		counter_round_time = 0;
	}

	counter_round_time++;

	glutTimerFunc(200, timer_enemy, 0);
}

int main(int argc, char** argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB);
	glutInitWindowPosition(100, 100);
	glutInitWindowSize(640, 480);
	glutCreateWindow("Game_SeaBattle");
	
	Initialize();

	glutDisplayFunc(Display);
	glutKeyboardFunc(Keyboard);
	glutMouseFunc(Mouse);
	timer();
	timer_enemy();

	glutMainLoop();

	return 0;
}
