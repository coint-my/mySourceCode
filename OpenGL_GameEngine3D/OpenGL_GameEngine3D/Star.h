#pragma once
#include <vector>
#include <cstdlib>
#include <math.h>
#include <ctime>
#include "Obj.h"

class Star
{
private:
	const int count;

	vovan::Vector2f pos;

	std::vector<vovan::Vector2f> points;
public:
	Star(void);
	Star(const int _count);
	virtual ~Star(void);

	void Init(const int _size, vovan::Vector2f &_pos);
	void Offset(vovan::Vector2f &_pos);

	std::vector<vovan::Vector2f> &GetPoints() { return points; }
	vovan::Vector2f &GetPosition() { return pos; }
};

