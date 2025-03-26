#include "Painter.h"

Painter::Painter(void)
{
	col = Color::White;
	//count_texture = 0;
	SetColor(col);
	//LoadTexture("sea_1.bmp");
}

Painter::~Painter(void)
{
}

//void Painter::LoadTexture(const char *_ch)
//{
//	AUX_RGBImageRec *_texture = auxDIBImageLoadA("sea_1.bmp");
//	glGenTextures(2, &textures[0]);
//	glBindTexture(GL_TEXTURE_2D, textures[0]);
//	count_texture++;
//	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
//	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
//	glTexImage2D(GL_TEXTURE_2D, 0, 3, _texture->sizeX, _texture->sizeY, 0 , GL_RGB, GL_UNSIGNED_BYTE, _texture->data);
//}

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

void Painter::DrawRect(const float _x, const float _y, const Color c)
{
	SetColor(c);
	glColor3f(color[0], color[1], color[2]);

	glBegin(GL_QUADS);
	glVertex2f(_x, _y);
	glVertex2f(_x + (float)wid, _y);
	glVertex2f(_x + (float)wid, _y + (float)hei);
	glVertex2f(_x, _y + (float)hei);
	glEnd();
}
void Painter::DrawPoint(const short _x, const short _y, const short _size, Color _col)
{
	SetColor(_col);
	glColor3f(color[0], color[1], color[2]);
	glPointSize(_size);

	glBegin(GL_POINTS);
	glVertex2s(_x, _y);
	glEnd();
}

void Painter::DrawRect(const float _x, const float _y)
{
	glColor3f(color[0], color[1], color[2]);

	//glBindTexture(GL_TEXTURE_2D, textures[0]);

	glBegin(GL_QUADS);
	//glTexCoord2f(0.0, 0.0);
	glVertex2f(_x, _y);
	//glTexCoord2f(0.0, 1.0);
	glVertex2f(_x + (float)wid, _y);
	//glTexCoord2f(1.0, 1.0);
	glVertex2f(_x + (float)wid, _y + (float)hei);
	//glTexCoord2f(1.0, 0.0);
	glVertex2f(_x, _y + (float)hei);
	glEnd();
}
void Painter::DrawRect(const float _x, const float _y, const unsigned int _tex)
{
	glBindTexture(GL_TEXTURE_2D, _tex);

	glBegin(GL_QUADS);
		glVertex2f(_x, _y);
		glTexCoord2f(0.0, 0.0);
		glVertex2f(_x + (float)wid, _y);
		glTexCoord2f(0.0, 1.0);
		glVertex2f(_x + (float)wid, _y + (float)hei);
		glTexCoord2f(1.0, 1.0);
		glVertex2f(_x, _y + (float)hei);
		glTexCoord2f(1.0, 0.0);
	glEnd();
}
void Painter::DrawRect(const int _x, const int _y, const int _x2, const int _y2) const
{
	glColor3f(color[0], color[1], color[2]);

	glBegin(GL_QUADS);
	glVertex2i(_x, _y);
	glVertex2i(_x2, _y);
	glVertex2i(_x2, _y2);
	glVertex2i(_x, _y2);
	glEnd();
}