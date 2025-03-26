#include "enemy.h"
#include "player.h"
#include <time.h>
#include "renderer.h"
#include "setting.h"

void enemy::init()
{
	init_ind.clear();
	//инициализация начального массива компютера
	for (size_t y = 0; y < DESC_H; y++)
		for (size_t x = 0; x < DESC_W; x++)
			if (_table->get_field(x, y) == pl)
				init_ind.push_back(s_index(x, y));

	list_vars.clear();
}

void enemy::init_rang1(const int xx, const int yy, std::vector<best_ind>& _list)
{
	std::vector<s_index> posib;
	Rectangle g_r = get_r1(xx, yy);

	for (size_t y = 0; y < DESC_H; y++)
		for (size_t x = 0; x < DESC_W; x++)
		{
			Rectangle t_r = get_r1(x, y, _table->get_figure(0).get_rad());

			if (Rectangle::intersect(g_r, t_r) && (_table->get_field(x, y) == POSIBLE))
				posib.push_back(s_index(x, y));
		}

	for (size_t i = 0; i < posib.size(); i++)
	{
		g_r = get_r1(posib[i].x, posib[i].y);

		int _val = 0;

		for (size_t y = 0; y < DESC_H; y++)
			for (size_t x = 0; x < DESC_W; x++)
			{
				Rectangle t_r = get_r1(x, y, _table->get_figure(0).get_rad());

				if (Rectangle::intersect(g_r, t_r) && (_table->get_field(x, y) == en))
					_val++;
			}

		if (_val > 0)
			_list.push_back(best_ind(xx, yy, posib[i].x, posib[i].y, _val, true));
	}

	posib.clear();
}

void enemy::init_rang2(const int xx, const int yy, std::vector<best_ind>& _list)
{
	std::vector<s_index> posib;
	Rectangle g_r = get_r1(xx, yy);
	Rectangle g_r2 = get_r1(xx, yy, (_table->get_figure(0).get_rad() * 2) * 5);

	for (size_t y = 0; y < DESC_H; y++)
		for (size_t x = 0; x < DESC_W; x++)
		{
			Rectangle t_r = get_r1(x, y, _table->get_figure(0).get_rad());

			if (Rectangle::intersect(g_r2, t_r) && (_table->get_field(x, y) == POSIBLE) && !Rectangle::intersect(g_r, t_r))
				posib.push_back(s_index(x, y));
		}

	for (size_t i = 0; i < posib.size(); i++)
	{
		g_r = get_r1(posib[i].x, posib[i].y);

		int _val = 0;

		for (size_t y = 0; y < DESC_H; y++)
			for (size_t x = 0; x < DESC_W; x++)
			{
				Rectangle t_r = get_r1(x, y, _table->get_figure(0).get_rad());

				if (Rectangle::intersect(g_r, t_r) && (_table->get_field(x, y) == en))
					_val++;
			}

		if (_val > 0)
			_list.push_back(best_ind(xx, yy, posib[i].x, posib[i].y, _val, false));
	}

	posib.clear();
}

inline const Rectangle & enemy::get_r1(const int xx, const int yy) const
{
	int ra = _table->get_figure(0).get_rad();
	Rectangle r = Rectangle(0, 0, (ra * 2) * 3, (ra * 2) * 3);
	r.set_center(_table->get_pos(xx, yy).x, _table->get_pos(xx, yy).y);
	return r;
}

inline const Rectangle & enemy::get_r1(const int xx, const int yy, const int _size) const
{
	Rectangle r = Rectangle(0, 0, _size, _size);
	r.set_center(_table->get_pos(xx, yy).x, _table->get_pos(xx, yy).y);
	return r;
}

const best_ind & enemy::get_best_var() const
{
	best_ind val;

	best_ind val1;
	best_ind val2;

	for (size_t i = 0; i < list_vars.size(); i++)
	{
		if (val1.value < list_vars[i].value && list_vars[i].isRang1)
			val1 = list_vars[i];
	}

	for (size_t i = 0; i < list_vars.size(); i++)
	{
		if (val2.value < list_vars[i].value && !list_vars[i].isRang1)
			val2 = list_vars[i];
	}

	if (val1.value == val2.value)
		val = val1;
	else if (val1.value < val2.value)
	{
		if (val2.value > 2)
			val = val2;
		else
			val = val1;
	}
	else if (val1.value > val2.value)
		val = val1;

	return val;
}

const best_ind& enemy::empty_turn() const
{
	best_ind val = best_ind(0, 0, 0, 0, -1, true);
	std::vector<best_ind> t_ind;
	Rectangle g_r;

	for (size_t i = 0; i < init_ind.size(); i++)
	{
		g_r = get_r1(init_ind[i].x, init_ind[i].y);

		for (size_t y = 0; y < DESC_H; y++)
			for (size_t x = 0; x < DESC_W; x++)
			{
				Rectangle r = get_r1(x, y, _table->get_figure(0).get_rad());

				if (Rectangle::intersect(g_r, r) && (_table->get_field(x, y) == POSIBLE))
					t_ind.push_back(best_ind(init_ind[i].x, init_ind[i].y, x, y, 0, true));
			}
	}

	if (t_ind.size() > 1)
		val = t_ind[rand() % t_ind.size()];

	t_ind.clear();

	return val;
}

bool enemy::handle()
{
	if (isHandleMouse)
	{
		isHandleMouse = false;
		init();
		
		for (size_t i = 0; i < init_ind.size(); i++)
		{
			init_rang1(init_ind[i].x, init_ind[i].y, list_vars);
			init_rang2(init_ind[i].x, init_ind[i].y, list_vars);
		}

		if (list_vars.size() == 0)
		{
			curr_turn = empty_turn();

			if (curr_turn.value == -1)
				return false;
		}
		else
		{
			curr_turn = get_best_var();

			if(curr_turn.value == 0)
				curr_turn = empty_turn();
		}

		fig = new data_figure();
		fig->initialize(_table->get_figure(1).get_seg(), _table->get_figure(1).get_rad() - 15, Position(_table->get_pos(curr_turn.x1, curr_turn.y1)),
			Color(20, 50, 20), get_name());

		isHandle = true;
	}

	if (fig)
		renderer::draw_polygon(*fig);
	if (!run_figure())
		return false;

	return true;
}

bool enemy::run_figure()
{
	if (isHandle && fig)
	{
		Position p1 = Position(fig->get_pos());
		Position p2 = Position(_table->get_pos(curr_turn.x2, curr_turn.y2).x, _table->get_pos(curr_turn.x2, curr_turn.y2).y);

		double dist = player::p_sqrt(p1, p2);

		float xp = SPEED_TURN * (p1.x - p2.x) / dist;
		float yp = SPEED_TURN * (p1.y - p2.y) / dist;

		if (dist > 5)
		{
			fig->offset(-xp, -yp);

			return true;
		}
		else
		{
			isHandle = false;

			if (fig)
			{
				delete fig;
				fig = NULL;

				Rectangle g_r = get_r1(curr_turn.x2, curr_turn.y2);

				for (size_t y = 0; y < DESC_H; y++)
					for (size_t x = 0; x < DESC_W; x++)
					{
						Rectangle t_r = get_r1(x, y, _table->get_figure(0).get_rad());

						if (Rectangle::intersect(g_r, t_r) && (_table->get_field(x, y) == en))
							_table->set_field(x, y, pl);
					}

				_table->set_field(curr_turn.x2, curr_turn.y2, pl);
				if (!curr_turn.isRang1)
					_table->set_field(curr_turn.x1, curr_turn.y1, POSIBLE);
			}
		}

	}
	return false;
}

enemy::enemy(table &_t, int pla, int ene, const wchar_t* _nam) : entity(_nam)
{
	_table = &_t;
	pl = pla;
	en = ene;

	srand(time(NULL));
	fig = NULL;
}

enemy::~enemy()
{
}
