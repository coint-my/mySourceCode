#include "ArrStar.h"


ArrStar::ArrStar(void) : speed(1)
{
}


ArrStar::~ArrStar(void)
{
	stars.clear();
	dir.clear();
}

void ArrStar::Init(const int _countStar, const int _lenth, const int _size, vovan::Recti &_initRect, vovan::Color &_col)
{
	srand(clock());
	vovan::Recti tempR(_initRect);
	color = _col;

	int x = 0;
	int y = 0;

	for (int i = 0; i < _lenth; i++)
	{
		x = rand() % tempR.GetWid() + tempR.GetVect().x;
		y = rand() % tempR.GetHei() + tempR.GetVect().y;

		Star star(_countStar);
		star.Init(_size, vovan::Vector2f(x, y));

		vovan::Vector2f randDir((rand() % 100 / 50.0 - 1.0) * speed, (rand() % 100 / 50.0 - 1.0) * speed);

		dir.push_back(randDir);
		stars.push_back(star);
	}
	if(stars.size() > 100)
	{
		stars.clear();
		dir.clear();
	}
}

void ArrStar::Update()
{
	for (int i = 0; i < stars.size(); i++)
	{
		stars.at(i).Offset(dir.at(i));
	}
}
