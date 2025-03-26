#include "Tile.h"

Tile::Tile(void)
{
	SetX(10);
	SetY(10);
	SetWidth(50);
	SetHeight(50);
	isEmpty = true;
	is_intersect = false;
}

Tile::Tile(const int _x, const int _y, const int _width, const int _height)
{
	SetX(_x);
	SetY(_y);
	SetWidth(_width);
	SetHeight(_height);
	isEmpty = true;
	is_intersect = false;
}

Tile::~Tile(void)
{
}
