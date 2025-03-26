#pragma once
#include "ObjGame.h"

class Tile : public ObjGame
{
private:
	bool isEmpty;
public:
	bool is_intersect;

	Tile(void);
	Tile(const int _x, const int _y, const int _width, const int _height);
	~Tile(void);
};
