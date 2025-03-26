#pragma once

#include <iostream>
#include <math.h>
#include "setting.h"
#include "GL\freeglut.h"

namespace Vova
{
	class Point
	{
	public: float x, y, z;
	public: Point() : x(0), y(0), z(0) { }
	public: Point(const float _x, const float _y, const float _z) : x(_x), y(_y), z(_z) { }

	public:~Point() {}
	};// класс точки по координатам x, y, z

	class Color
	{
	public: unsigned r, g, b, a;
	public: Color() : r(50), g(50), b(50), a(0) { }
	public: Color(const unsigned _r, const unsigned _g, const unsigned _b, const unsigned _a) :
		r(_r), g(_g), b(_b), a(_a) { }

	public:~Color() {}
	};//класс цвета r, g, b, a

	class Camera : public Point
	{
	public: float angleX, angleY;
	public: float speed;
	public: Point rotation;

	public: Camera() : Point(0, 0, 0), angleX(1), angleY(1), speed(0) { }
	public: Camera(const float _x, const float _y, const float _z) : Point(_x, _y, _z), 
		angleX(1), angleY(1), speed(0) { }

	public: void Rotation(Point& _pos, const Point _poi)
	{		
		float xx = _pos.x;
		float yy = _pos.y;
		float zz = _pos.z;

		//rotated_point.x = point.x * cos(angle) - point.y * sin(angle);
		//rotated_point.y = point.x * sin(angle) + point.y * cos(angle);

		xx = _poi.x + (_pos.x - _poi.x) * cos(0.1) - (_pos.y - _poi.y) * sin(0.1);
		yy = _poi.y + (_pos.x - _poi.x) * sin(0.1) + (_pos.y - _poi.y) * cos(0.1);

		_pos.x = xx;
		_pos.y = yy;
		_pos.z = zz;
	}
	public: template<typename T>inline T Radian(T _val)
	{
		auto temp = _val / 180 * PI;
		return temp;
	}

	public: void Position(Point& _pos)
	{
		
	}

	public: ~Camera() { }
	};

	class Poligon : public Point, public Color
	{
	public: Point points[4];
	public: Poligon() { }
	public: Poligon(const Point& _p1, const Point& _p2, const Point& _p3, const Point& _p4)
	{
		points[0] = _p1;
		points[1] = _p2;
		points[2] = _p3;
		points[3] = _p4;
	}
	public: ~Poligon() { }
	};
}