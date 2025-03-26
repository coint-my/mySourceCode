#pragma once
#include "tetrino.h"
#include "painter.h"
#include <cstdlib>

class unity
{
private:
	int width;
	int height;

	tetrino t;

	bool **map;
public:
	const int wCount;
	const int hCount;

	unity(int widCount, int heiCount, int w, int h);
	~unity(void);

	void draw(painter &p);
	void tempRand();
	bool moveDown();
	void move(const tetrino::Direction dir);

	void removeLine(const int ind_y);

	tetrino getTet() {return t;}
	bool Map(const int x, const int y) const;
};
