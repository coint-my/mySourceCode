#pragma once
#include <vector>
#include <cstdlib>
#include <ctime>
#include "Painter.h"

using std::vector;

struct Part
{
	float x, y;
	float dx, dy;
	float size;
};

class Effect
{
private:
	vector<Part> arr;
	bool is_explode;
	short _cl;
public:
	Effect(void);
	void InitEffect(const short _x, const short _y, const short _count_x, const short _count_y, const short _size);
	void DrawEffect(Painter _paint) const;
	void UpdateEffect(const short _alive, const float _size);
	bool IsExplode() const { return is_explode; }
	~Effect(void);
};
