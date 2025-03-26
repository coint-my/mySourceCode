#pragma once
#include <gl/glut.h>

static bool isVisible;

class Rect
{
public:
	int x, y;
	int x2, y2;
	Rect();
	Rect(int _x, int _y, int _x2, int _y2);
	int left() {if(x < x2) return x; return x2;}
	int right() {if(x < x2) return x2; return x;}
	int top() {if(y < y2) return y; return y2;}
	int bottom() {if(y < y2) return y2; return y;}
	void draw() const;
	~Rect(void);
};
