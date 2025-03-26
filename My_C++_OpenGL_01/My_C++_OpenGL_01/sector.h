#pragma once

#include <gl/glut.h>
#include <vector>
#include "EditGrid.h"

class sector
{
private:
	std::vector<point> pos;
	int scale;
public:
	sector();

	sector(const std::vector<point> p);

	void Scale(const int s);

	void draw();

	~sector(void);
};
