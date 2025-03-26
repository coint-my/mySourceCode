#include "player.h"
#include <cmath>
#include <GL\glut.h>
#include "renderer.h"
#include "setting.h"

void player::set_init_br()
{
	Rectangle r = buff.rec_2;

	for (size_t y = 0; y < DESC_H; y++)
		for (size_t x = 0; x < DESC_W; x++)
		{
			if (Rectangle::intersect(r, rect[x][y]) && _table->get_field(x, y) == POSIBLE)
				isRect[x][y] = true;
			else
				isRect[x][y] = false;
		}
}

void player::draw_br()
{
	for (size_t y = 0; y < DESC_H; y++)
		for (size_t x = 0; x < DESC_W; x++)
		{
			if (isRect[x][y])
				renderer::draw_Rect(rect[x][y], Color(10, 10, 10));
		}
}

void player::up_sec()
{
	static bool fl = false;

	if (buff.is_chek == CHEK2)
	{
		Color c1;

		if (fl)
		{
			c1 = Color(20, 50, 20);
		}
		else
			c1 = Color(20, 200, 20);

		fl = !fl;
		fig2->set_col(c1);
	}
}

bool player::handle()
{
	if (buff.is_chek == NONE && !isHandleMouse)
	{
		renderer::draw_polygon(*fig);
		if (!run_figure())
			return false;
	}
	if (buff.is_chek == CHEK2)
	{
		draw_br();
		renderer::draw_polygon(*fig2);
	}
	return true;
}

void player::mouse(int button, int state, int x, int y)
{
	if (isHandleMouse)
	{
		if ((buff.is_chek == CHEK1) && (buff.is_player) && (button == GLUT_LEFT) && (state == GLUT_UP))
		{
			buff.is_chek = CHEK2;
			set_init_br();

			fig2 = new data_figure();
			fig2->initialize(_table->get_figure(1).get_seg(), _table->get_figure(1).get_rad() - 15, Position(_table->get_pos(buff.bind_x, buff.bind_y)),
				Color(20, 50, 20), L"активная");

			printf("button ok (is_CHEK2)\n");
		}
		else if ((buff.is_chek == CHEK2) && (buff.is_fin) && (button == GLUT_LEFT) && (state == GLUT_UP))
		{
			buff.is_chek = NONE;
			isHandleMouse = false;
			isHandle = true;
			_table->set_field(buff.find_x, buff.find_y, POSIBLE);

			fig = new data_figure();
			fig->initialize(_table->get_figure(1).get_seg(), _table->get_figure(1).get_rad() - 5, Position(_table->get_pos(buff.bind_x, buff.bind_y)),
				Color(200, 200, 150), L"move");

			if (fig2)
			{
				delete fig2;
				fig2 = NULL;
			}

			resetActive();

			printf("button ok (is_NONE) ; get_index_figure (x = %i, y = %i)\n", buff.find_x, buff.find_y);
		}
		else if (button == GLUT_RIGHT_BUTTON && state == GLUT_UP)
		{
			reset();
			isHandleMouse = true;
		}
	}
}

void player::moution_mouse(int x, int y)
{
	if (isHandleMouse)
	{
		//get_intersect(x, y);
		get_sqrt(x, y);
	}
}

void player::get_sqrt(int xx, int yy)
{
	if (buff.is_chek == CHEK1)
	{
		for (size_t y = 0; y < DESC_H; y++)
		{
			for (size_t x = 0; x < DESC_W; x++)
			{
				const Position &_p = _table->get_pos(x, y);
				float dist = sqrt((_p.x - xx) * (_p.x - xx) + (_p.y - yy) * (_p.y - yy));
				float rad = _table->get_figure(1).get_rad();

				if ((dist < rad) && (_table->get_field(x, y) == pl/*PLAYER*/))
				{
					buff.is_player = true;

					Rectangle r = Rectangle(0, 0, (rad * 2 * 3), (rad * 2 * 3));
					r.set_center(_p.x, _p.y);
					buff.rec_1 = r;
					buff.bind_x = x;
					
					r = Rectangle(0, 0, (rad * 2 * 5), (rad * 2 * 5));
					r.set_center(_p.x, _p.y);
					buff.rec_2 = r;
					buff.bind_y = y;

					return;
				}
				else if ((dist < rad) && _table->get_field(x, y) == POSIBLE)
				{
					_table->set_field(x, y, ACTIVE);
					buff.is_player = false;
				}
				else if ((dist > rad) && (_table->get_field(x, y) == ACTIVE))
				{
					_table->set_field(x, y, POSIBLE);
					buff.is_player = false;
				}
				else
				{
					buff.is_player = false;
				}
			}
		}
	}
	else if (buff.is_chek == CHEK2)
	{
		for (size_t y = 0; y < DESC_H; y++)
		{
			for (size_t x = 0; x < DESC_W; x++)
			{
				const Position &_p = _table->get_pos(x, y);
				float dist = sqrt((_p.x - xx) * (_p.x - xx) + (_p.y - yy) * (_p.y - yy));
				float rad = _table->get_figure(1).get_rad();

				if ((dist < rad) && _table->get_field(x, y) == POSIBLE)
				{
					Rectangle mous = Rectangle(xx, yy, 1, 1);

					if (Rectangle::intersect(mous, buff.rec_1))
					{
						_table->set_field(x, y, ACTIVE);
						buff.find_x = x;
						buff.find_y = y;
						buff.is_fin = true;

						return;
					}
					else if (Rectangle::intersect(mous, buff.rec_2))
					{
						_table->set_field(x, y, ACTIVE);
						buff.find_x = x;
						buff.find_y = y;
						buff.is_fin = true;

						return;
					}

				}
				else if ((dist > rad) && (_table->get_field(x, y) == ACTIVE))
				{
					_table->set_field(x, y, POSIBLE);
					buff.is_fin = false;
				}

			}
		}
	}
}

bool player::run_figure()
{
	if (isHandle)
	{
		Position p1 = Position(fig->get_pos());
		Position p2 = Position(_table->get_pos(buff.find_x, buff.find_y));

		double dist = p_sqrt(p1, p2);

		float xp = SPEED_TURN * (p1.x - p2.x) / dist;
		float yp = SPEED_TURN * (p1.y - p2.y) / dist;

		if (dist > 5)
		{
			fig->offset(-xp, -yp);

			return true;
		}
		else
		{
			if (!Rectangle::intersect(buff.rec_1, Rectangle(fig->get_pos().x, fig->get_pos().y, 1, 1)))
				_table->set_field(buff.bind_x, buff.bind_y, POSIBLE);

			Rectangle rr = Rectangle(buff.rec_1);
			rr.set_center(_table->get_pos(buff.find_x, buff.find_y).x, _table->get_pos(buff.find_x, buff.find_y).y);
			for (size_t y = 0; y < DESC_H; y++)
				for (size_t x = 0; x < DESC_W; x++)
					if (Rectangle::intersect(rr, rect[x][y]) && _table->get_field(x, y) == en/*ENEMY*/)
						_table->set_field(x, y, pl/*PLAYER*/);

			_table->set_field(buff.find_x, buff.find_y, pl/*PLAYER*/);
			reset();
		}
	}
	return false;
}

double player::p_sqrt(const Position & p1, const Position & p2)
{
	double val = sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
	return val;
}

void player::get_intersect(int xx, int yy)
{
	Rectangle rr = Rectangle(xx, yy, 1, 1);

	for (size_t y = 0; y < DESC_H; y++)
		for (size_t x = 0; x < DESC_W; x++)
		{
			if (Rectangle::intersect(rect[x][y], rr))
			{
				_table->set_field(x, y, ACTIVE);
			}
			else
				_table->set_field(x, y, POSIBLE);
		}
}

void player::reset()
{
	buff = p_buff();
	isHandle = false;
	isHandleMouse = false;

	if (fig)
	{
		delete fig;
		fig = NULL;
	}
	if (fig2)
	{
		delete fig2;
		fig2 = NULL;
	}
}

void player::resetActive()
{
	for (size_t y = 0; y < DESC_H; y++)
		for (size_t x = 0; x < DESC_W; x++)
			if (_table->get_field(x, y) == ACTIVE)
				_table->set_field(x, y, POSIBLE);
}

player::player() : entity(L"Player")
{
	isHandle = false;
	isHandleMouse = false;
	number_player++;
	wprintf(L"создания игрока № (%i) имя (%ls)\n", number_player, get_name());
	fig = NULL;
	fig2 = NULL;
}

player::player(table & t, int pla, int ene, const wchar_t* _nam) : entity(_nam)
{
	pl = pla;
	en = ene;

	_table = &t;
	for (size_t y = 0; y < DESC_H; y++)
		for (size_t x = 0; x < DESC_W; x++)
		{
			rect[x][y] = Rectangle(0, 0, _table->get_figure(1).get_rad(), _table->get_figure(1).get_rad());
			rect[x][y].set_center(_table->get_pos(x, y).x, _table->get_pos(x, y).y);
			isRect[x][y] = false;
		}

	isHandle = false;
	isHandleMouse = false;
	wprintf(L"создания игрока № (%i) имя (%ls)\n", number_player, get_name());
	number_player++;

	fig = NULL;
	fig2 = NULL;
}

player::~player()
{
}

bool Rectangle::intersect(const Rectangle & r1, const Rectangle & r2)
{
	if ((r1.get_x() < r2.right()) && (r1.right() > r2.get_x()) && (r1.get_y() < r2.bottom()) && (r1.bottom() > r2.get_y()))
		return true;
	else
		return false;
}
