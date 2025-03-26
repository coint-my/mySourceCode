#include "renderer.h"
#include <cmath>
#include "gl\glut.h"

void renderer::draw_polygon(const data_figure& fig)
{
	glColor3ub(fig.get_color().r, fig.get_color().g, fig.get_color().b);

	float angle = (3.14159821 * 2) / fig.get_seg();

	glBegin(GL_POLYGON);

	for (size_t i = 0; i < fig.get_seg(); i++)
		glVertex2f(fig.get_pos().x + (sin(angle * i + (22 * 3.1415 / 180)) * fig.get_rad()), fig.get_pos().y + (cos(angle * i + (22 * 3.1415 / 180)) * fig.get_rad()));

	glEnd();
}

void renderer::draw_Rect(const Rectangle & rec, const Color& col)
{
	glColor3ub(col.r, col.g, col.b);
	glRectf(rec.get_x(), rec.get_y(), rec.right(), rec.bottom());
}

void renderer::ShowString(const wchar_t *_string, const int _x, const int _y)
{
	glRasterPos2i(_x, _y);

	while (*_string)
	{
		glutBitmapCharacter(GLUT_BITMAP_TIMES_ROMAN_24, *_string++);
	}
}

void renderer::ShowString(int _intejer, const int _x, const int _y)
{
	char _str[15];
	itoa(_intejer, _str, 10);

	glRasterPos2i(_x, _y);

	int i = 0;
	while (i < 20)
	{
		glutBitmapCharacter(GLUT_BITMAP_TIMES_ROMAN_24, _str[i]);
		i++;
	}
}

renderer::renderer()
{
}


renderer::~renderer()
{
}
