#include "Rect.h"

Rect::Rect() : x(0), y(0), x2(0), y2(0)
{
	isVisible = true;
}

Rect::Rect(int _x, int _y, int _x2, int _y2) : x(_x), y(_y), x2(_x2), y2(_y2)
{
	isVisible = true;
}

Rect::~Rect(void)
{
}

void Rect::draw() const
{
	glColor4f(0.3, 0.3, 0.0, 0.35);
	glRecti(x, y, x2, y2);
}
