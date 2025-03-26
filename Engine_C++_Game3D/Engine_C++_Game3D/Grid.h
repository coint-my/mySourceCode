#pragma once
#include <gl/glut.h>

class Grid
{
private:
	int x;
public:
	Grid(void);
	Grid(const int _x);
	void setX(const int _x) { x = _x; }
	int X() const { return x; }
	void draw(const int _offx, const int _offy, const int _scaleGrid, const int _gridStep) const;
	~Grid(void);
};
