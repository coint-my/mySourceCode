#ifndef OBJCIR_H
#define OBJCIR_H

#include "h_graphic.h"

class ObjCir
{
private:
	bool isAlive;

	int alive;
	int curr;
	graphic::Vector2f direction;

	graphic::Circlef cir;
	graphic::Color col;
public:
	ObjCir(void);
	ObjCir(graphic::Circlef &__cir, graphic::Color &_col, int _al);
	~ObjCir(void);

	bool IsAlive() { return isAlive; }

	void SetDir(graphic::Vector2f &_dir) { direction.x = _dir.x; direction.y = _dir.y; }
	void Offset(graphic::Vector2f &_off) { direction.x += _off.x; direction.y += _off.y; }
	void Update();

	graphic::Vector2f GetDir() { return direction; }
	graphic::Circlef GetCircle() { return cir; }
	graphic::Color GetColor() { return col; }
};

#endif

