#pragma once
#include <vector>
#include <gl/glut.h>
#include "ColorRGB.h"

struct Point
{
	int x;
	int y;
	ColorRGB col;
	Point(const int _x, const int _y) : x(_x), y(_y)
	{}
};

using namespace std;

class Sector
{
private:
	vector<Point> points;
	ColorRGB color;
public:
	Sector(void);
	~Sector(void);

	void LoadSector(const std::vector<Point> _points);
	int GetLentch() const {return points.size();}
	vector<Point> getPoints() const {return points;}
	void setColor(float _r, float _g, float _b) {color.SetColor(_r, _g, _b);}
	void setPoints(const int _p, const Point &_point) {points[_p] = _point;}
	void DrawSector3D() const;
	void DrawSector(const int _offx, const int _offy, const int _lentch) const;
};
