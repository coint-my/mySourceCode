#pragma once

class E_Rect
{
private:
	float x, y, wid, hei;
public:
	E_Rect(void);
	E_Rect(const float _x, const float _y, const float _wid, const float _hei);
	E_Rect(const E_Rect& _rect);
	~E_Rect(void);

	float Left() const { return x; }
	float Right() const { return x + wid; }
	float Top() const { return y; }
	float Bottom() const { return y + hei; }

	inline void set_xy(const float _x, const float _y) { x = _x; y = _y; }
	inline void offset(const float _x, const float _y) { x += _x; y += _y; }

	static bool IsIntersect(E_Rect& _rect1, E_Rect& _rect2);
};

