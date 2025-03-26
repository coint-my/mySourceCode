#pragma once
#include <cmath>
#include <gl/glut.h>

#define M_PI 3.14159 * 2

#pragma comment(lib, "opengl32.lib")
#pragma comment(lib, "glut32.lib")

struct Color
{
	float r, g, b;
	Color(void) {};
	Color(float _r, float _g, float _b) : r(_r), g(_g), b(_b) {};
};

class Painter
{
public:
	Painter(void);
	~Painter(void);

	void DrawTile(const int _rad, const int _seg, const float _x, const float _y, const Color &_col);
	void DrawCircle(const int _rad, const float _x, const float _y, const Color &_col);
};

