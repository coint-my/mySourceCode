#include "Star.h"


Star::Star(void) : count(10)
{
}

Star::Star(const int _count) : count(_count)
{
}

Star::~Star(void)
{
	points.clear();
}

void Star::Init(const int _size, vovan::Vector2f &_pos)
{
	float ang = 360 / count;
	float x = 0;
	float y = 0;

	pos = _pos;
	
	for (int i = 0; i < count; i++)
	{
		if(i % 2 == 0)
		{
			x = sin(i * (ang * PI / 180)) * _size;
			y = cos(i * (ang * PI / 180)) * _size; 
		}
		else
		{
			x = sin(i * (ang * PI / 180)) * _size / 2;
			y = cos(i * (ang * PI / 180)) * _size / 2;
		}
		points.push_back(vovan::Vector2f(x + pos.x, y + pos.y));
	}
}

void Star::Offset(vovan::Vector2f &_pos)
{
	for (int i = 0; i < points.size(); i++)
	{
		points.at(i).x += _pos.x;
		points.at(i).y += _pos.y;
	}
}
