#include "ScrollBar.h"

ScrollBar::ScrollBar(void) : color(Color(1.0, 0.0, 0.0))
{
	//hei = 10;
}

ScrollBar::~ScrollBar(void)
{
}

void ScrollBar::Offset(const int _x, const int _y)
{
	Set_X(_x);
	Set_Y(_y);
}

void ScrollBar::DrawBar(Painter &_p) const
{
	_p.DrawRectRGB(x, y, wid, hei, color);
}
