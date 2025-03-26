#pragma once
#include <vector>
#include <cstdlib>
#include <math.h>
#include <ctime>
#include "Obj.h"

class EffectR
{
private:
	std::vector<Obj> arr;
protected:
	vovan::Vector2f box;
	float speed;
	float grav;
public:
	EffectR(void);
	virtual ~EffectR(void);

	void Init(const int _len, vovan::Rectf &_r, const vovan::Vector2f &_box, vovan::Color &_c);
	void SetSpeed(const float _s) { speed = _s; }
	void Update(const bool _grav);

	std::vector<Obj> &GetArr() { return arr; }
};

