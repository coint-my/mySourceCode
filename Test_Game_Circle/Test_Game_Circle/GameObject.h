#pragma once

#include <vector>
#include "Options.h"

enum Direction { NONE, LEFT, RIGHT, TOP, BOTTOM };

struct Point2i
{
	int x, y;
	Point2i() : x(0), y(0) {}
	Point2i(const int _x, const int _y) : x(_x), y(_y) {}
	~Point2i() {}
};

struct Point2f
{
	float x, y;
	Point2f() : x(0), y(0) {}
	Point2f(const float _x, const float _y) : x(_x), y(_y) {}
	~Point2f() {}
};

struct Point3f
{
	float x, y, z;
	Point3f() : x(0), y(0), z(0) {}
	Point3f(const float _x, const float _y, const float _z) : x(_x), y(_y), z(_z) {}
	~Point3f() {}
};

struct Color
{
	int r, g, b;
	Color() : r(50), g(50), b(50) {}
	Color(int _r, int _g, int _b) : r(_r), g(_g), b(_b) {}
	~Color() {}
};
struct Colorf
{
	float r, g, b;
	Colorf() : r(0.5), g(0.5), b(0.5) {}
	Colorf(float _r, float _g, float _b) : r(_r), g(_g), b(_b) {}
	~Colorf() {}
};
struct Color4f
{
	float r, g, b, a;
	Color4f() : r(0.5), g(0.5), b(0.5), a(0) {}
	Color4f(float _r, float _g, float _b, float _a) : r(_r), g(_g), b(_b), a(_a) {}
	~Color4f() {}
};

class GameObject
{
protected:
	Colorf color;
	Point2f pos;
	int weight;
	int size;
	Point2i points[16];
	void load_point(const Point2i* _pp, const int _count);
public:
	GameObject(void);
	virtual ~GameObject(void);

	virtual void draw() const {}

	inline void set_color(const Colorf &_color) { color = _color; }
	inline void set_weight(const int _w) { weight = _w; }
	inline void set_pos(const Point2f& _p) { pos = _p; }
	inline void set_offset(const float _x, const float _y) { pos.x += _x; pos.y += _y; }

	inline int get_weight() const { return weight; }
	inline const Point2f& get_pos() const { return pos; }


};

