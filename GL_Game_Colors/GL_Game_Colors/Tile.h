#pragma once
#include "Painter.h"

enum TupeColor
{
	RED, GREEN, BLUE, GRAY, YELLOW
};

class Tile
{
private:
	int x, y;
	int rad;
	int segment;
	Color color;
	TupeColor tupe;
public:
	Tile(void);
	~Tile(void);

	void Initialize(const int _x, const int _y, const int _rad, const int _seg, const TupeColor _col);
	void SetTupe(const TupeColor _col);
	
	int GetX() { return x; }
	int GetY() { return y; }
	int GetRad() {return rad; }
	int GetSegment() { return segment; }
	Color GetColor() { return color; }
	TupeColor GetTupe() { return tupe; }
};

