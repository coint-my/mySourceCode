#include "EffectR.h"


EffectR::EffectR(void) : speed(1), grav(0.0)
{

}


EffectR::~EffectR(void)
{
	arr.clear();
}

void EffectR::Init(const int _len, vovan::Rectf &_r, const vovan::Vector2f &_box, vovan::Color &_c)
{
	box = vovan::Vector2f(_box.x, _box.y);
	srand(time(0));

	for (int i = 0; i < _len; i++)
	{
		vovan::Rectf tempR;
		vovan::Vector2f _dir((rand() % 100 / 50.0 - 1.0) * speed, (rand() % 100 / 50.0 - 1.0) * speed);
		tempR.Initialize(vovan::Rectf(vovan::Vector2f(rand() % (int)_r.GetWid() + _r.Left(), rand() % (int)_r.GetHei() + _r.Top()), box.x, box.y));
		Obj ob;
		ob.Init(tempR, _c, _dir);
		arr.push_back(ob);
	}
}

void EffectR::Update(const bool _grav)
{
	vovan::Vector2f tempG(0, (0.5 + grav));

	float x = 0;
	float y = 0;

	if(_grav)
		grav += 0.2;
	else
	{
		int t = clock();
		x = sin(t * (PI * 2 / 180)) * 10;
		y = cos(t * (PI * 2 / 180)) * 10;
	}

	for (int i = 0; i < arr.size(); i++)
	{
		arr.at(i).Offset(vovan::Vector2f(arr.at(i).GetDir().x, arr.at(i).GetDir().y));

		arr.at(i).Offset(tempG);

		if(!_grav)
			arr.at(i).Offset(vovan::Vector2f(x, y));
	}
}