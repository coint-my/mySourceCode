#include "Tile.h"


Tile::Tile(void) : isActive(false)
{
	x = 0;
	y = 0;
	wid = 1;
	hei = 1;
	color = Color();
}

Tile::Tile(int _x, int _y, int _wid, int _hei, Color _col)
{
	x = _x;
	y = _y;
	wid = _wid;
	hei = _hei;
	color = _col;
	isActive = false;
}

Tile::~Tile(void)
{
}

void Tile::init(const int _x, const int _y, const int _wid, const int _hei, Color _col)
{
	x = _x;
	y = _y;
	wid = _wid;
	hei = _hei;
	color = _col;
}

bool Tile::Intersect(Tile _t)
{
	if((this->Right() > _t.Left()) && (this->Left() < _t.Right()) && (this->Bottom() > _t.Top()) && (this->Top() < _t.Bottom()))
		return true;
	else 
		return false;
}
