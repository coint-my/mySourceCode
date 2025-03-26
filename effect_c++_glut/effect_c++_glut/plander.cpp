#include "plander.h"

plander::plander(void) : x(0), y(0), vx(1), vy(1), lenth(20), height(10) 
{
}
plander::plander(const int _x, const int _y, const int _vx, const int _vy, const int _lenth) : 
x(_x), y(_y), vx(_vx), vy(_vy), lenth(_lenth), height(10)
{
}

plander::~plander(void)
{
}

void plander::Draw(Painter &p) const
{
	p.DrawRect(x, y, lenth, height, Painter::Blue);
}

void plander::MovePlane(plander::Move m)
{
	switch(m)
	{
	case Left:
		x -= vx;
		break;
	case Right:
		x += vx;
		break;
	default:
		
		break;
	}
}
