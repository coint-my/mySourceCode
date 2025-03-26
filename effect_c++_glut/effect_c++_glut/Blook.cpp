#include "Blook.h"

Blook::Blook(): counter(0), end_counter(5), isAlive(true)
{
}

Blook::Blook(const int _x, const int _y, const int _wid, const int _hei, const Painter::Color _c, const int _index
             ,const int _endCounter): isAlive(true)
{
	x = _x;
	y = _y;
	width = _wid;
	height = _hei;
	c = _c;
	index = _index;
	counter = 0;
	end_counter = _endCounter;
}

Blook::~Blook(void)
{
}

void Blook::draw(Painter &p) const
{
	if(isAlive)
		p.DrawRect(x, y, width, height, c);
}
