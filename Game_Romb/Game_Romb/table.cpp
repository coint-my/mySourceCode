#include "table.h"
#include "renderer.h"
#include "setting.h"

void table::draw_field()
{
	for (size_t i = 0; i < DESC_W; i++)
	{
		for (size_t j = 0; j < DESC_H; j++)
		{
			switch (_field[i][j])
			{
			case POSIBLE:
				_data[POSIBLE - 2].set_pos(pos[i][j]);
				renderer::draw_polygon(_data[POSIBLE - 2]);
				break;
			case PLAYER:
				_data[PLAYER - 2].set_pos(pos[i][j]);
				renderer::draw_polygon(_data[PLAYER - 2]);
				break;
			case ENEMY:
				_data[ENEMY - 2].set_pos(pos[i][j]);
				renderer::draw_polygon(_data[ENEMY - 2]);
				break;
			case ACTIVE:
				_data[ACTIVE - 2].set_pos(pos[i][j]);
				renderer::draw_polygon(_data[ACTIVE - 2]);
				break;
			}
		}
	}
}

int table::get_field(const int x, const int y) const
{
	return _field[x][y];
}

const data_figure& table::get_figure(const int val) const
{
	return _data[val];
}

bool table::set_field(const int x, const int y, const int val)
{
	if ((x >= 0 && x < DESC_W) && (y >= 0 && y < DESC_H))
	{
		_field[x][y] = val;

		//wprintf(L"дано значение фигуре от поля: value(%i)\n", val);
		return true;
	}
	else
	{
		wprintf(L"ошибка выход за пределы игрового стола: x(%i), y(%i)\n", x, y);
		return false;
	}
}

const Position & table::get_pos(const int x, const int y) const
{
	return pos[x][y];
}

void table::reset_pole()
{
	for (size_t i = 0; i < DESC_W; i++)
		for (size_t j = 0; j < DESC_H; j++)
			_field[i][j] = POSIBLE;

	set_field(DESC_W - 1, 0, ENEMY);
	set_field(0, DESC_H - 1, PLAYER);
}

table::table()
{
	for (size_t i = 0; i < DESC_W; i++)
		for (size_t j = 0; j < DESC_H; j++)
		{
			Position p = Position(RADIUS_FIGURE + i * (RADIUS_FIGURE * 2), RADIUS_FIGURE + j * (RADIUS_FIGURE * 2));
			pos[i][j] = p;
			_field[i][j] = POSIBLE;
		}

	set_field(DESC_W - 1, 0, ENEMY);
	set_field(0, DESC_H - 1, PLAYER);

	_data[0].initialize(SEGMENT_FIGURE, RADIUS_FIGURE, Position(), Color(50, 50, 50), L"Пусто");

	_data[1].initialize(SEGMENT_FIGURE, RADIUS_FIGURE, Position(), Color(50, 250, 50), L"Игрок");

	_data[2].initialize(SEGMENT_FIGURE, RADIUS_FIGURE, Position(), Color(50, 50, 250), L"Противник");

	_data[3].initialize(SEGMENT_FIGURE, RADIUS_FIGURE, Position(), Color(250, 50, 50), L"активность");
}


table::~table()
{
	
}
