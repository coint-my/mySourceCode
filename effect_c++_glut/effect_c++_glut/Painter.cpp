#include "Painter.h"

Painter::Painter(void)
{
	col = Color::White;
	SetColor(col);
}

Painter::~Painter(void)
{
}

void Painter::SetColor(const Painter::Color c)
{
	switch(c)
	{
	case Red:
		color[0] = 1.0f;
		color[1] = 0.0f;
		color[2] = 0.0f;
		break;
	case Green:
		color[0] = 0.0f;
		color[1] = 1.0f;
		color[2] = 0.0f;
		break;
	case Blue:
		color[0] = 0.0f;
		color[1] = 0.0f;
		color[2] = 1.0f;
		break;
	default :
		color[0] = 1.0f;
		color[1] = 1.0f;
		color[2] = 1.0f;
		break;
	}
}
void Painter::SetColor(float _r, float _g, float _b)
{
	color[0] = _r;
	color[1] = _g;
	color[2] = _b;
}

void Painter::DrawRect(const float _x, const float _y, const int _w, const int _h, const Color c)
{
	SetColor(c);
	glColor3f(color[0], color[1], color[2]);

	glBegin(GL_QUADS);
	glVertex2f(_x, _y);
	glVertex2f(_x + _w, _y);
	glVertex2f(_x + _w, _y + _h);
	glVertex2f(_x, _y + _h);
	glEnd();
}
void Painter::DrawRect(const float _x, const float _y)
{
	glColor3f(color[0], color[1], color[2]);

	glBegin(GL_QUADS);
	glVertex2f(_x, _y);
	glVertex2f(_x + wid, _y);
	glVertex2f(_x + wid, _y + hei);
	glVertex2f(_x, _y + hei);
	glEnd();
}