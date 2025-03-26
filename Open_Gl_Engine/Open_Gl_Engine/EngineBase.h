#pragma once
#include <iostream>
#include <ctime>
#include <vector>

#define MATRIX_TOP 1
#define MATRIX_PERSPECTIVE 2

enum EditState { EDIT_MAP_2D = 1, VIEW_3D_MODE };

struct Color
{
	short red, green, blue;

	Color(void) 
	{
		srand(time(0));
		red = rand() % 245 + 10;
		green = rand() % 245 + 10;
		blue = rand() % 245 + 10;
	}
	Color(const short _r, const short _g, const short _b) : red(_r), green(_g), blue(_b) { }
	~Color(void) { }
};

struct Translation
{
	float x, y, z;

	Translation(void) : x(0), y(0), z(0) { }
	Translation(const float _x, const float _y, const float _z) : x(_x), y(_y), z(_z) { }
	~Translation(void) { }
};

struct Rotation
{
	Translation dir;
	float rot;

	Rotation(void) : rot(0), dir(1.0, 0, 0) { }
	Rotation(const float _rot, const Translation &_dir) : rot(_rot), dir(_dir) { }
	~Rotation(void) { }
};

