#include "Maps.h"


Maps::Maps(void)
{
	const int p = 10;
	int r = 50;
	int angl = 360 / p;

	Vector2f pos[p];

	for (int i = 0; i < p; i++)
	{
		float x = cos(i * (angl * PI / 180)) * r;
		float y = sin(i * (angl * PI / 180)) * r;

		pos[i] = Vector2f(x, y);
	}

	baseShape.Initialize(pos, p, r);
}

Maps::~Maps(void)
{
	map.clear();
	imposible.clear();
	r1.clear();
}

bool Maps::IsColor(const Color &_col, const Color &_col2)
{
	if((_col.r == _col2.r) && (_col.g == _col2.g) && (_col.b == _col2.b) && (_col.a == _col2.a))
		return true;
	else return false;
}

void Maps::SetR1R2(vovan::Recti &_r1, Color &_col)
{
	r1.clear();

	vovan::Recti tR;

	for (int i = 0; i < map.size(); i++)
	{
		tR.Initialize(vovan::Recti(vovan::Vector2i((int)map.at(i).GetPosition().x, (int)map.at(i).GetPosition().y), 2, 2));

		if((tR.Intersect(_r1)) && (IsColor(map.at(i).GetColor(), _col)))
			r1.push_back(i);
	}
}

void Maps::Update()
{
	for (int i = 0; i < imposible.size(); i++)
		if(!imposible[i])
			map[i].SetColor(map[i].GetColorPass());
	if(cur_cursor > -1)
		if(!imposible[cur_cursor])
			map[cur_cursor].SetColor(map[cur_cursor].GetColorActive());
}

void Maps::InitMatrix(const int _wid, const int _hei)
{
	for (int h = 0; h < _hei; h++)
	{
		for (int w = 0; w < _wid; w++)
		{
			Shapes sh = baseShape;
			sh.SetColor(sh.GetColorPass());
			sh.Offset(Vector2f(w * (sh.GetRadius() * 2) + 50, h * (sh.GetRadius() * 2) + 50));
			
			map.push_back(sh);
			imposible.push_back(false);
		}
	}
}
