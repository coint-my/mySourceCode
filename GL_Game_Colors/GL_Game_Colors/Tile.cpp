#include "Tile.h"


Tile::Tile(void)
{
	x = 0;
	y = 0;
	rad = 1;
	segment = 4;
}


Tile::~Tile(void)
{
}

void Tile::SetTupe(const TupeColor _col)
{
	switch(_col)
	{
	case 0:
		color = Color(1.0, 0.0, 0.0);
		break;
	case 1:
		color = Color(0.1, 0.5, 0.01);
		break;
	case 2:
		color = Color(0.0, 0.0, 1.0);
		break;
	case 3:
		color = Color(0.5, 0.5, 0.5);
		break;
	case 4:
		color = Color(0.95, 1.0, 0.12); 
		break;
	}
	tupe = _col;
}

void Tile::Initialize(const int _x, const int _y, const int _rad, const int _seg, const TupeColor _col)
{
	x = _x;
	y = _y;
	rad = _rad;
	segment = _seg;
	tupe = _col;
	SetTupe(_col);
}
