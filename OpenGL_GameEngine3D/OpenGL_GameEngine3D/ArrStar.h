#pragma once
#include "Star.h"

class ArrStar
{
private:
	std::vector<Star> stars;
	std::vector<vovan::Vector2f> dir;
	vovan::Color color;
	float speed;
public:
	ArrStar(void);
	~ArrStar(void);

	void Init(const int _countStar, const int _lenth, const int _size, vovan::Recti &_initRect, vovan::Color &_col);
	void Update();

	std::vector<Star> &GetStars() { return stars; }
	vovan::Color &GetColor() { return color; }
};

