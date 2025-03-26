#include "ManagerEff.h"
#include <cstdlib>
#include <cmath>
#include <ctime>


ManagerEff::ManagerEff(void) : mass(0), alive(10)
{
	curr = 0;
}

ManagerEff::ManagerEff(const int _mass, const int _alive, graphic::Recti &_initRect, int _mSize)
{
	mass = _mass;
	alive = _alive;
	initRect = _initRect;
	curr = 0;

	srand(time(0));
	Init(_initRect, mass, _mSize);
}

ManagerEff::~ManagerEff(void)
{
	cir.clear();
}

void ManagerEff::Init(const graphic::Recti _rect, int _mass, int _maxSize)
{
	initRect = _rect;
	cir.clear();
	
	for (int i = 0; i < _mass; i++)
	{
		float xp = rand() % initRect.GetWid() + initRect.Left();
		float yp = rand() % initRect.GetHei() + initRect.Top();
		int rad = rand() % _maxSize + 3;
		int ali = rand() % 880 + 2640;
		graphic::Circlef cc(graphic::Vector2f(xp, yp), rad, 5);
		ObjCir ob(cc, graphic::Color(rand() % 70 + 30, 0, 0), ali);
		float x = (rand() % 200) / 100.0 - 1.0;
		float y = (rand() % 200) / 100.0 - 1.0;
		ob.SetDir(graphic::Vector2f((x * 0.3), (y * 0.3)));
		cir.push_back(ob);
	}
}

void ManagerEff::Update(graphic::Vector2f _gravity)
{
	for (int i = 0; i < cir.size(); i++)
	{
		cir[i].Offset(_gravity);
		
		cir[i].Update();
	}

	for (int d = 0; d < cir.size(); d++)
	{
		if(!cir.at(d).IsAlive())
		{
			cir.erase(cir.begin() + d);
			mass--;
			break;
		}
	}

	curr++;
}
