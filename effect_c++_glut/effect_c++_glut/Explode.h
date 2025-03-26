#pragma once
#include <vector>
#include <gl/glut.h>
#include <cstdlib>
#include <cmath>
#include "programEffect.h"

class Explode
{
private:
	std::vector<Pixel3> part;
	const float speedPart;
	float counter;
	float sCount;
	bool enebleGravity;
	float setGravity;
	int pSize;
	int alive;
public:
	bool isActive;
	const int count;
	Explode(void);
	Explode(const int _count, const float _speed);
	~Explode(void);

	void initExplodePosition(const float _x, const float _y, const float _z, const int radExplode);

	void display();
	void SetGravity(bool act, const float _gravity) { enebleGravity = act; setGravity = _gravity;}
};
