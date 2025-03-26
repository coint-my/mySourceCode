#include "MyRect.h"

MyRect::MyRect(void)
{
	x = y = 0;
	x2 = y2 = 1;
}
MyRect::MyRect(const short _x, const short _y, const short _x2, const short _y2)
{
	x = _x;
	y = _y;
	x2 = _x2;
	y2 = _y2;
}

short MyRect::Left()
{
	return x;
}

short MyRect::Right()
{
	return x2;
}

short MyRect::Top()
{
	return y;
}

short MyRect::Bottom()
{
	return y2;
}

MyRect::~MyRect(void)
{
}
