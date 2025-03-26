#pragma once
#include "Tile.h"
#include "Painter.h"
#include <ctime>

#define WIDTH 780
#define HEIGHT 480
#define COUNT_W 10
#define COUNT_H 7

class Pole
{
private:
	unsigned int texture;
	Tile tiles[COUNT_W][COUNT_H];
public:
	Pole(void);
	~Pole(void);

	void Draw_T(Painter & _paint, const short _colum) const;
	void Initialize(const unsigned int _t);
	void IntersectVisible(const int _x, const int _y);
	void SetTile(bool _impos[][COUNT_H]);
	Tile GetTile(const int _w, const int _h) const { return tiles[_w][_h]; }
};
