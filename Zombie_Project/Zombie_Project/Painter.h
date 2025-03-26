#pragma once
//#pragma comment(lib, "glut32.lib")
//#pragma comment(lib, "glaux.lib")
//#pragma comment(lib, "gl/OpenGL32.lib")

//#include "gl/glaux.h"
#include "gl/freeglut.h"
#include "gl/src/SOIL.h"
#include <iostream>
//#include "gl/freeglut_std.h"
//#include "gl/freeglut_ext.h"
#include <cmath>

struct Color
{
	float r, g, b;

	Color(void) : r(1.0), g(1.0), b(1.0) {};
	Color(const float _red, const float _green, const float _blue) : r(_red), g(_green), b(_blue) {};
};

static GLuint _texture = 0;

struct texture
{
	int chanel;
	unsigned char* data;
	GLint width, height;
	GLuint id;

	bool LoadTexture(const char* _fileName)
	{
		data = SOIL_load_image(_fileName, &width, &height, &chanel, SOIL_LOAD_AUTO);
		if (data == 0)
			return false;
		_texture++;
		return true;
	}

	void freeImage()
	{
		_texture--;
	}
	~texture()
	{
		//freeImage();
		//glDeleteTextures(1, &id);
		//SOIL_free_image_data(data);
		//delete[] data;
	}
};

class MyTexture
{
public: texture tex;

public: MyTexture() { }
public: MyTexture(const char* fileName) { LoadTexture(fileName); }

public: inline void LoadTexture(const char* _fn)
{
	if (!tex.LoadTexture(_fn))
		printf("texture failure! file path(%s)", _fn);

	//glGenTextures(2, &tex.id);
	tex.id = _texture;
	glBindTexture(GL_TEXTURE_2D, tex.id);

	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);

	if (tex.chanel == 3)
		glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, tex.width, tex.height, 0,
			GL_RGB, GL_UNSIGNED_BYTE, tex.data);
	if (tex.chanel == 4)
	{
		glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
		//glDepthMask(GL_FALSE);

		glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, tex.width, tex.height, 0,
			GL_RGBA, GL_UNSIGNED_BYTE, tex.data);
	}
}
		~MyTexture()
		{
			/*tex.freeImage();
			glDeleteTextures(1, &tex.id);
			printf("---destructor---");
			system("PAUSE");*/
		}
};

class Painter
{
private:
	Color _col;
public:
	Painter(void);
	~Painter(void);

	void SetColor(Color &_c) { _col = _c; }
	void DrawRectRGB(const int _x, const int _y, const int _x2, const int _y2, const Color _color) const;
	void DrawRectTexture(const int _x, const int _y, const int _wid, const int _hei, const unsigned int _text) const;
	void DrawRectTexture(const int _x, const int _y, const int _wid, const int _hei, const float _wid_text_e, const float _hei_text_e,
						 const float _wid_text_s, const float _hei_text_s, const unsigned int _text) const;
	void DrawCircle(const float _x, const float _y, const float _rad, Color &_col) const;
};
