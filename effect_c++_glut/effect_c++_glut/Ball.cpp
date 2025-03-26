#include "Ball.h"

Ball::Ball(void): x(1), y(1), size(10), speed(5)
{
	radius = 5;
}

Ball::Ball(const float _x, const float _y) : x(_x), y(_y), size(10), speed(7)
{
	radius = 45;

	setRadian(radius);
}

Ball::~Ball(void)
{
}

float Ball::setRadian(const float _rad)
{
	float r = (_rad) * 3.14159 / 180;

	vx = sin(r) * speed;
	vy = cos(r) * speed;

	return r;
}

void Ball::Draw(Painter &p) const
{
	p.DrawRect(x, y, size, size, Painter::Red);
}

void Ball::UpdateBall()
{
	x += vx;
	y += vy;
}
