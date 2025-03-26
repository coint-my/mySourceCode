#include "Painter.h"


Painter::Painter(void)
{
}


Painter::~Painter(void)
{
}

void Painter::DrawBlook(const int _x, const int _y, const int _w, const int _h, Color _c)
{
	glColor3f(_c.r, _c.g, _c.b);
	glRecti(_x, _y, _x + _w, _y + _h);
}

void Painter::DrawPoint(const float _x, const float _y)
{
	glColor3f(1.0, 0.0, 0.0);
	glPointSize(5);
	glBegin(GL_POINTS);
	glVertex2f(_x, _y);
	glEnd();
}

void Painter::DrawTriangle(const short _dir, const float _x, const float _y, const int _width, const Color _c)
{
	glColor3f(_c.r, _c.g, _c.b);

	glBegin(GL_POLYGON);

	switch(_dir)
	{
	case 1:
		glVertex2f(_x, _y + _width / 2);
		glVertex2f(_x + _width, _y);
		glVertex2f(_x + _width, _y + _width);
		break;
	case 2:
		glVertex2f(_x, _y);
		glVertex2f(_x + _width, _y + _width / 2);
		glVertex2f(_x, _y + _width);
		break;
	case 3:
		glVertex2f(_x + _width / 2, _y);
		glVertex2f(_x + _width, _y + _width);
		glVertex2f(_x, _y + _width);
		break;
	case 4:
		glVertex2f(_x, _y);
		glVertex2f(_x + _width, _y);
		glVertex2f(_x + _width / 2, _y + _width);
		break;
	}

	glEnd();
}
