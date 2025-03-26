#include "Painter.h"

Painter::Painter(void)
{

}

Painter::~Painter(void)
{
}

void Painter::DrawRectRGB(const int _x, const int _y, const int _x2, const int _y2, const Color _color) const
{
	glColor3f(_color.r, _color.g, _color.b);

	glBegin(GL_QUADS);
	glVertex2i(_x, _y);
	glVertex2i(_x + _x2, _y);
	glVertex2i(_x + _x2, _y + _y2);
	glVertex2i(_x, _y + _y2);
	glEnd();
}

void Painter::DrawRectTexture(const int _x, const int _y, const int _wid, const int _hei, const unsigned int _text) const
{
	glBindTexture(GL_TEXTURE_2D, _text);

	glColor3f(_col.r, _col.g, _col.b);

	glBegin(GL_QUADS);
	glTexCoord2f(0.0, 0.0);
	glVertex2i(_x, _y);
	glTexCoord2f(1.0, 0.0);
	glVertex2i(_x + _wid, _y);
	glTexCoord2f(1.0, 1.0);
	glVertex2i(_x + _wid, _y + _hei);
	glTexCoord2f(0.0, 1.0);
	glVertex2i(_x, _y + _hei);

	glEnd();
}

void Painter::DrawRectTexture(const int _x, const int _y, const int _wid, const int _hei, const float _wid_text_e, 
				const float _hei_text_e, const float _wid_text_s, const float _hei_text_s, const unsigned int _text) const
{
	glBindTexture(GL_TEXTURE_2D, _text);

	glEnable(GL_ALPHA_TEST);
	glAlphaFunc(GL_NOTEQUAL, 0);

	glColor3f(_col.r, _col.g, _col.b);

	glBegin(GL_QUADS);
	glTexCoord2f(_wid_text_s, _hei_text_e);
	glVertex2i(_x, _y);
	glTexCoord2f(_wid_text_e, _hei_text_e);
	glVertex2i(_x + _wid, _y);
	glTexCoord2f(_wid_text_e, _hei_text_s);
	glVertex2i(_x + _wid, _y + _hei);
	glTexCoord2f(_wid_text_s, _hei_text_s);
	glVertex2i(_x, _y + _hei);

	glEnd();

	glDisable(GL_ALPHA_TEST);
}

void Painter::DrawCircle(const float _x, const float _y, const float _rad, Color &_color) const
{
	glColor3f(_color.r, _color.g, _color.b);

	glBegin(GL_POLYGON);

	short quality = 12;

	for(int i = 0; i < quality; i++)
	{
		double pos_x = sin(i * (3.1415 * 2) / quality) * _rad;
		double pos_y = cos(i * (3.1415 * 2) / quality) * _rad;
		glVertex2f(_x + pos_x, _y + pos_y);
	}

	glEnd();
}
