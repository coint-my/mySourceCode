#include "painter.h"

painter::painter(void)
{
}

painter::~painter(void)
{
}

void painter::rect(int x1, int y1, int x2, int y2)
{
	glBegin(GL_QUADS);
	glVertex2i(x1, y1);
	glVertex2i(x2, y1);
	glVertex2i(x2, y2);
	glVertex2i(x1, y2);
	glEnd();
}

void painter::setColor(painter::Color color)
{
	static const struct 
	{
		float r, g, b;
	}colors[] = 
	{
		{1.0, 0.0, 0.0},
		{0.0, 1.0, 0.0},
		{0.0, 0.0, 1.0},
		{1.0, 1.0, 0.0}
	};

	glColor3f(colors[color].r, colors[color].g, colors[color].b);
}
