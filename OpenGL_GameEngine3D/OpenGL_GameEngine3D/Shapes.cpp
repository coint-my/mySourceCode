#include "Shapes.h"

Shapes::Shapes(void) : rad(5), colActive(200, 20, 10), colPass(20, 50, 50), colPlayer(0, 200, 0), colEnemy(0, 0, 200)
{
}


Shapes::~Shapes(void)
{
	points.clear();
}

void Shapes::SetPoints(const std::vector<vovan::Vector2f> &_poi, const vovan::Vector2f &_pos)
{
	points.clear();
	position = _pos;

	for (int i = 0; i < _poi.size(); i++)
	{
		points.push_back(_poi.at(i));
	}
}

void Shapes::Initialize(const vovan::Vector2f *_point, const int _count, const int _rad)
{
	rad = _rad;
	for (int i = 0; i < _count; i++)
	{
		points.push_back(_point[i]);
	}
}

void Shapes::Offset(const vovan::Vector2f &_position)
{
	position.x += _position.x;
	position.y += _position.y;

	for (int i = 0; i < points.size(); i++)
	{
		points.at(i).x += _position.x;
		points.at(i).y += _position.y;
	}
}

void Shapes::Update()
{

}

void Shapes::Draw()
{

}