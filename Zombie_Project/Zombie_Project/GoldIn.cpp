#include "GoldIn.h"

GoldIn::GoldIn(void)
{
}

GoldIn::~GoldIn(void)
{
	golds.clear();
	rects.clear();
}

void GoldIn::Draw()
{
	for(int i = 0; i < rects.size(); i++)
	{
		glRasterPos2i(rects[i].Get_X(), rects[i].Get_Y() + 25);

		char _str[15];
		itoa(golds[i], _str, 10);

		int j = 0;
		while(j < 15)
		{
			glutBitmapCharacter(GLUT_BITMAP_TIMES_ROMAN_24, _str[j]);
			j++;
		}
	}
}

void GoldIn::Add(const EngineRect &_rect, const short cost_gold)
{
	golds.push_back(cost_gold / 2);
	rects.push_back(_rect);
}

void GoldIn::Intersect(const int _x, const int _y, short &_gold)
{
	EngineRect _r = EngineRect(_x, _y, 2, 2);

	for(int i = 0; i < rects.size(); i++)
	{
		if(_r.IsIntersect(rects[i]))
		{
			_gold += golds[i];
			rects.erase(rects.begin() + i);
			golds.erase(golds.begin() + i);
			break;
		}
	}
}
