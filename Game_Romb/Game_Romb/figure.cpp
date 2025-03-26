#include "figure.h"

float data_figure::get_seg() const
{
	return this->_segment;
}

float data_figure::get_rad() const
{
	return this->_radius;
}

wchar_t * data_figure::get_name() const
{
	return _name;
}

Position &data_figure::get_pos() const
{
	return *(_position);
}

Color &data_figure::get_color() const
{
	return *(_color);
}

void data_figure::offset(const float x, const float y)
{
	_position->x += x;
	_position->y += y;
}

void data_figure::set_seg(const float seg)
{
	if (seg > 3 && seg < 50)
		_segment = seg;
	else
		wprintf(L"error segment : value(%f) CLASS NAME [%ls]\n", seg, _name);
}

void data_figure::set_rad(const float rad)
{
	if (rad > 10 && rad < 200)
		_radius = rad;
	else
		wprintf(L"error radius : value(%f) CLASS NAME [%ls]\n", rad, _name);
}

void data_figure::set_pos(const Position & pos)
{
	_position->Init(pos.x, pos.y);
}

void data_figure::set_col(const Color & col)
{
	bool ok = (col.r > 0 && col.r < 256) && (col.g > 0 && col.g < 256) && (col.b > 0 && col.b < 256);

	if (ok)
		_color->Init(col.r, col.g, col.b);
	else
		printf("error Color : value(red->%i, green->%i, blue->%i) CLASS NAME [%ls]\n", col.r, col.g, col.b, _name);
}


void data_figure::initialize(const float seg, const float rad, const Position& pos, const Color& col, const wchar_t* name)
{
	wcscpy(_name, name);
	
	//_segment = seg;
	set_seg(seg);
	//_radius = rad;
	set_rad(rad);
	//_position->Init(pos.x, pos.y);
	set_pos(pos);
	//_color->Init(col.r, col.g, col.b);
	set_col(col);
}

data_figure::data_figure() : _segment(0), _radius(0)
{
	_position = new Position();
	_color = new Color();
	_name = new wchar_t[MAX_NAME_FIGURE];
	
	setlocale(LC_ALL, "russian");
}

data_figure::~data_figure()
{
	delete _position;
	delete _color;
	delete[] _name;
}
