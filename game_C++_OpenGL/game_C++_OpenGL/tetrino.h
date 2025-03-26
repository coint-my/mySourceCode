#pragma once
#include "painter.h"

class tetrino
{
private:
	int x;
	int y;
	int width;
	int height;

	int rotation;

public:
	bool map[4][4];

	enum Name {T, L, Z, I, O};
	enum Direction {LEFT = -1, RIGHT = 1, UP = 0, DOWN = 2};

	Name name;
	bool active(const int _x, const int _y, Name n, int rot);

	tetrino(int w, int h);

	void init(const Name n);

	void rotationTetrino();

	void offset(const int dx, const int dy);

	void draw(painter &p) const;
	void draw(painter &p, painter::Color c) const;

	int X() const {return x;};
	int Y() const {return y;};
	void X(const int _x) {x = _x;};
	void Y(const int _y) {y = _y;};

	bool intersect(const int _x, const int _y);
	int IsDown(Direction dir) const;

	int Rotation() const {return rotation;}

	~tetrino(void);
};
