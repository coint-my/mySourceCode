#include "manager_game.h"
#include "renderer.h"
#include <GL\glut.h>

void manager_game::render()
{
	_table->draw_field();

	if (g_buff.round == PL1)
	{
		if (!_player->handle())
		{
			g_buff.round = PL2;
			_player2->isHandleMouse = true;

			int s = 0;
			calculate_score(PLAYER, s);
			_player->set_score(s);
			s = 0;
			calculate_score(ENEMY, s);
			_player2->set_score(s);

			if (Is_OVER())
				g_buff.round = GAME_OVER;
		}
	}
	else if(g_buff.round == PL2)
	{
		if (!_player2->handle())
		{
			g_buff.round = PL1;
			_player->isHandleMouse = true;

			int s = 0;
			calculate_score(PLAYER, s);
			_player->set_score(s);
			s = 0;
			calculate_score(ENEMY, s);
			_player2->set_score(s);

			if (Is_OVER())
				g_buff.round = GAME_OVER;
		}
	}
	else if (g_buff.round == GAME_OVER)
	{
		if (_player->get_score() > _player2->get_score())
		{
			glColor3ub(255, 255, 255);
			renderer::ShowString(_player->get_name(), WIDTH / 2 - 100, HEIGHT - 10);
			renderer::ShowString(L" WINS!!!", (WIDTH / 2 + 20), HEIGHT - 10);
		}
		else
		{
			glColor3ub(255, 255, 255);
			renderer::ShowString(_player2->get_name(), WIDTH / 2 - 100, HEIGHT - 10);
			renderer::ShowString(L" WINS!!!", (WIDTH / 2 + 20), HEIGHT - 10);
		}
	}
	else if (g_buff.round == GAME_MENU)
	{
		men.draw_menu();
	}

	if (_player != NULL && _player2 != NULL)
	{
		glColor3ub(50, 200, 50);
		renderer::ShowString(_player->get_name(), 0, HEIGHT - 10);
		renderer::ShowString(_player->get_score(), 13 * 9, HEIGHT - 10);

		glColor3ub(50, 50, 200);
		renderer::ShowString(_player2->get_name(), WIDTH - (16 * 9), HEIGHT - 10);
		renderer::ShowString(_player2->get_score(), WIDTH - (3 * 9), HEIGHT - 10);
	}
}

void manager_game::update()
{

}

void manager_game::up_second()
{
	_counter = _counter + 1 % 60;

	if (state_menu == PLAYER_COMP)
		_p->up_sec();
	else if (state_menu == PLAYER_PLAYER)
		g_buff.round == PL1 ? _p->up_sec() : _p2->up_sec();
}

void manager_game::mouse(int b, int s, int x, int y)
{
	if (b == GLUT_MIDDLE_BUTTON && s == GLUT_UP)
	{
		state_menu = EMPTY;
		g_buff.round = GAME_MENU;

		if (_p)
			_p->resetActive();
	}
	if (g_buff.round == GAME_MENU && (b == GLUT_LEFT_BUTTON && s == GLUT_UP))
	{
		Rectangle m = Rectangle(x, y, 1, 1);

		for (size_t i = 0; i < PUNCT_MENU; i++)
		{
			if (Rectangle::intersect(m, men.get_menu(i).rect))
			{
				state_menu = i;
				break;
			}
		}
		int test = 0;
		switch (state_menu)
		{
		case PLAYER_COMP:
			player_vs_comp();
			break;
		case PLAYER_PLAYER:
			player_vs_player();
			break;
		case COMP_COMP:
			comp_vs_comp();
			break;
		case RESET_POLE:
			_table->reset_pole();
			break;
		case EXIT_GAME:
			exit(0);
			break;
		default:
			break;
		}
	}
	if (state_menu == PLAYER_COMP)
		_p->mouse(b, s, x, y);
	else if (state_menu == PLAYER_PLAYER)
		g_buff.round == PL1 ? _p->mouse(b, s, x, y) : _p2->mouse(b, s, x, y);
}

void manager_game::m_mouse(int x, int y)
{
	if (g_buff.round == GAME_MENU)
	{
		Rectangle m = Rectangle(x, y, 1, 1);

		for (size_t i = 0; i < PUNCT_MENU; i++)
		{
			if (Rectangle::intersect(m, men.get_menu(i).rect))
				men.get_menu(i).set_color(true);
			else
				men.get_menu(i).set_color(false);
		}
	}
	if (state_menu == PLAYER_COMP)
		_p->moution_mouse(x, y);
	else if (state_menu == PLAYER_PLAYER)
		g_buff.round == PL1 ? _p->moution_mouse(x, y) : _p2->moution_mouse(x, y);
}

void manager_game::calculate_score(const int _tupe_figure, int& score)
{
	int temp = 0;
	for (size_t y = 0; y < DESC_H; y++)
		for (size_t x = 0; x < DESC_W; x++)
		{
			if (_table->get_field(x, y) == _tupe_figure)
				temp++;
		}
	score = temp;
}

void manager_game::player_vs_comp()
{
	if (_p)
	{
		delete _p;
		_p = NULL;
	}

	_p = new player(*_table, PLAYER, ENEMY, L"Player one :");
	_player = _p;
	_player->isHandleMouse = true;
	
	if (_enemy)
	{
		delete _enemy;
		_enemy = NULL;
	}

	_enemy = new enemy(*_table, ENEMY, PLAYER, L"Player two");
	_player2 = _enemy;

	g_buff.round = PL1;
}

void manager_game::player_vs_player()
{
	if (_p)
	{
		delete _p;
		_p = NULL;
	}

	_p = new player(*_table, PLAYER, ENEMY, L"Player one :");
	_player = _p;
	_player->isHandleMouse = true;

	if (_p2)
	{
		delete _p2;
		_p2 = NULL;
	}

	_p2 = new player(*_table, ENEMY, PLAYER, L"Player two");
	_player2 = _p2;

	g_buff.round = PL1;
}

void manager_game::comp_vs_comp()
{
	if (_enemy)
	{
		delete _enemy;
		_enemy = NULL;
	}

	_enemy = new enemy(*_table, PLAYER, ENEMY, L"Player one :");
	_player = _enemy;
	_player->isHandleMouse = true;

	if (_enemy2)
	{
		delete _enemy2;
		_enemy2 = NULL;
	}

	_enemy2 = new enemy(*_table, ENEMY, PLAYER, L"Player two");
	_player2 = _enemy2;

	g_buff.round = PL1;
}

manager_game::manager_game()
{
	_table = new table();
	wprintf(L"выделение динамической памяти игровой стол: (%i)байт.\n", sizeof(table));

	_player = NULL;
	_player2 = NULL;
	wprintf(L"выделение динамической памяти юнита (%ls) : (%i)байт.\n", "player", sizeof(player));

	state_menu = EMPTY;
}

bool manager_game::Is_OVER()
{
	int flag = 0;
	for (size_t y = 0; y < DESC_H; y++)
		for (size_t x = 0; x < DESC_W; x++)
		{
			if (_table->get_field(x, y) == POSIBLE)
				flag++;
			
		}
	return flag == 0 ? true : false;
}

manager_game::~manager_game()
{
	delete _table;
	/*delete _enemy;
	delete _enemy2;*/
	delete _player;
	delete _player2;
}
