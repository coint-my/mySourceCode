#include "Painter.h"


Painter::Painter(void)
{
}


Painter::~Painter(void)
{
}

void Painter::DrawTile(const int _rad, const int _seg, const float _x, const float _y, const Color &_col)
{
	glColor3f(_col.r, _col.g, _col.b);
	glBegin(GL_POLYGON);

	for(int i = 0; i < _seg; i++)
	{
		float angle = M_PI / _seg;
		float pos_x = _x + (cos(angle * i) * _rad);
		float pos_y = _y + (sin(angle * i) * _rad);

		glVertex2f(pos_x, pos_y);
	}

	glEnd();
}

void Painter::DrawCircle(const int _rad, const float _x, const float _y, const Color &_col)
{
	glLineWidth(4.0f);

	glColor3f(_col.r, _col.g, _col.b);

	glBegin(GL_LINE_STRIP);

	for(int i = 0; i < 24; i++)
	{
		float angle = M_PI / 23;
		float pos_x = _x + (cos(angle * i) * _rad);
		float pos_y = _y + (sin(angle * i) * _rad);

		glVertex2f(pos_x, pos_y);
	}

	glEnd();
}
