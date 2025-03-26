#pragma once
#include "Painter.h"
#include <cmath>

class Ball
{
private:
	float x, y;
	int size;
	int speed;
	int radius;
public:
	float vx, vy;
	Ball(void);
	Ball(const float _x, const float _y);
	float setRadian(const float _rad);
	void Draw(Painter &p) const;
	int getRad() const { return radius; }
	void setRad(const int _r) { radius = _r;}
	void setX(const float _x) {x = _x;}
	void setY(const float _y) {y = _y;}
	float X() const {return x;}
	float Y() const {return y;}
	float Top() const { return y; }
	float Bottom() const { return y + size; }
	float Left() const { return x; }
	float Right() const { return x + size; }
	int getSpeed() const {return speed;}
	int getSize() const {return size;}
	void UpdateBall();

	~Ball(void);
};
