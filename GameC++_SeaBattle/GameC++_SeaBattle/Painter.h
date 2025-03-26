#pragma once
#include <gl/glut.h>
#include <gl/glaux.h>

#pragma comment(lib, "glut32.lib");
#pragma comment(lib, "opengl32.lib");
#pragma comment(lib, "glaux.lib");

class Painter
{
public:
	Painter(void);
	~Painter(void);

	static short wid;
	static short hei;

	enum Color {Red, Green, Blue, White};
	void SetColor(float _r, float _g, float _b);
	void DrawRect(const float _x, const float _y, const Color c);
	void DrawRect(const float _x, const float _y);
	void DrawRect(const float _x, const float _y, const unsigned int _tex);
	void DrawRect(const int _x, const int _y, const int _x2, const int _y2) const;
	void DrawPoint(const short _x, const short _y, const short _size, Color _col);
	//void LoadTexture(const char *_ch);

private:
	void SetColor(const Color c);
	float color[3];
	Color col;

	//unsigned int textures[5];
	//short count_texture;

};
