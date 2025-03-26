#pragma once
#include <gl/glut.h>

struct Color
{
	float r, g, b;
	Color(void) : r(1.0), g(1.0), b(1.0) { }
	Color(float _r, float _g, float _b) : r(_r), g(_g), b(_b) { }
};

class Painter
{
public:
	Painter(void);
	~Painter(void);

	static void DrawBlook(const int _x, const int _y, const int _w, const int h, Color _c);
	static void DrawPoint(const float _x, const float _y);
	static void DrawTriangle(const short _dir, const float _x, const float _y, const int _width, const Color _c);
};

