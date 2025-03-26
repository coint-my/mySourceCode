#pragma once
#include "Obj.h"
#include "Painter.h"

struct Vector2
{
	float x, y;
	Vector2(void) : x(0), y(0) {}
	Vector2(const float _x, const float _y) : x(_x), y(_y) {}
};

class Tile : protected Obj
{
private:
	bool isActive;
protected:
	void init(const int _x, const int _y, const int _wid, const int _hei, Color _col);
public:
	Color color;

	Tile(void);
	Tile(int _x, int _y, int _wid, int _hei, Color _col);
	~Tile(void);

	float GetX() { return x; }
	float GetY() { return y; }
	int GetWid() { return wid; }
	int GetHei() { return hei; }

	//правая сторона
	float Right() { return x + wid; }
	//левая сторона
	float Left() { return x; }
	//верхняя сторона
	float Top() { return y; }
	//нижняя сторона
	float Bottom() { return y + hei; }

	//проверка на пересечения
	bool Intersect(Tile _t);
	//активный тайл
	bool IsActive() { return isActive; }

	//центральная точка
	Vector2 Center() { return Vector2(x + (wid / 2), y + (hei / 2)); }

	void SetX(const float _x) { x = _x; }
	void SetY(const float _y) { y = _y; }
	void SetActive(const bool _act) { isActive = _act; }
	void OffseXY(const float _x, const float _y) { x += _x; y += _y; }
	void SetWid(const int _w) { wid = _w; }
	void SetHei(const int _h) { hei = _h; }
};

