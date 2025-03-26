#pragma once
#include "Painter.h"

class Blook
{
	int x, y;
	int width, height;
	int index;
	Painter::Color c;
	int counter;
	int end_counter;
	bool isAlive;
public:
	Blook();
	Blook(const int _x, const int _y, const int _wid, const int _hei, const Painter::Color _c,
		  const int _index, const int _endCounter);
	~Blook(void);

	int getX() const { return x; }
	int getY() const { return y; }

	int getCounter() const { return counter; }
	int getEndCounter() const { return end_counter; }
	bool getIsAlive() const { return isAlive; }
	void setIsAlive(const bool _alive) { isAlive = _alive; }
	void setCounter(const int _counter) { counter = _counter; }

	int Index() const { return index; }
	int getW() const { return width; }
	int getH() const { return height; }
	float Top() const { return y; }
	float Bottom() const { return y + height; }
	float Left() const { return x; }
	float Right() const { return x + width; }
	void draw(Painter &p) const;
};
