#pragma once
#include "EngineRect.h"
#include "Painter.h"
#include <vector>

class GoldIn
{
private:
	std::vector<EngineRect> rects;
	std::vector<int> golds;
public:
	GoldIn(void);
	~GoldIn(void);

	void Draw();
	void Add(const EngineRect &_rect, const short cost_gold);

	void Intersect(const int _x, const int _y, short &_gold);
};
