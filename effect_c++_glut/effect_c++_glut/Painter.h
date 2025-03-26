#pragma once
#include <gl/glut.h>

#pragma comment(lib, "glut32.lib");
#pragma comment(lib, "opengl32.lib");

class Painter
{
public:
	Painter(void);
	~Painter(void);

	static short wid;
	static short hei;

	enum Color {Red, Green, Blue, White};
	void SetColor(float _r, float _g, float _b);
	void DrawRect(const float _x, const float _y, const int _w, const int _h, const Color c);
	void DrawRect(const float _x, const float _y);

private:
	void SetColor(const Color c);
	float color[3];
	Color col;
};
