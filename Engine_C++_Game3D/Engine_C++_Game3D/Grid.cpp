#include "Grid.h"

Grid::Grid(void) : x(10)
{

}

Grid::Grid(const int _x) : x(_x)
{
	
}

void Grid::draw(const int _offx, const int _offy, const int _scaleGrid, const int _scaleStep) const
{
	int offX = _offx;
	int offY = _offy;
	int lentch = (_scaleStep * _scaleGrid);
	glEnable(GL_LINE_STIPPLE);
	glLineStipple(1, 0xA1A1);

	glColor3f(0.0, 0.4, 0.1);

	glBegin(GL_LINES);
	for(int i(0); i <= x; i++)
	{
		glVertex2i(i * lentch + offX, offY);
		glVertex2i(i * lentch + offX, offY + (x * lentch));
	}
	for(int i(0); i <= x; i++)
	{
		glVertex2i(offX, i * lentch + offY);
		glVertex2i(offX + (x * lentch), i * lentch + offY);
	}
	glEnd();
	glDisable(GL_LINE_STIPPLE);
}

Grid::~Grid(void)
{
}
