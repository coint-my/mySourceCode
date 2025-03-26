#include "Effect.h"

Effect::Effect(void)
{
	_cl = 0;
}

Effect::~Effect(void)
{

}

void Effect::InitEffect(const short _x, const short _y, const short _count_x, const short _count_y, const short _size)
{
	arr.clear();

	for(short y = 0; y < _count_y; y++)
	{
		for(short x = 0; x < _count_x; x++)
		{
			Part _p;
			_p.x = _x + (x * _size);
			_p.y = _y + (y * _size);
			_p.size = _size;
			_p.dx = ((float)rand() / RAND_MAX * 4.5 - 2.25);
			_p.dy = ((float)rand() / RAND_MAX * 4.5 - 2.25);
			arr.push_back(_p);
		}
	}

	_cl = 0;

	is_explode = true;
}
void Effect::DrawEffect(Painter _paint) const
{
	for(int i = 0; i < arr.size(); i++)
	{
		Painter::Color _c = Painter::Color::Red;

		_paint.DrawPoint(arr[i].x, arr[i].y, arr[i].size, _c);
	}
}

void Effect::UpdateEffect(const short _alive, const float _size)
{
	_cl++;

	if(_cl > _alive && is_explode)
	{
		arr.clear();
		_cl = 0;
		is_explode = false;
	}

	for(int i = 0; i < arr.size(); i++)
	{
		arr[i].x += arr[i].dx;
		arr[i].y += arr[i].dy;
		arr[i].size += _size;
	}
}
