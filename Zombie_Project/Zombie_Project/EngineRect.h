#pragma once

class EngineRect
{
protected:
	int x, y, wid, hei;
	int bx, by;
public:
	EngineRect(void);
	EngineRect(int _x, int _y, int _wid, int _hei);
	void Set_X(const int _x) { x = _x; }
	void Set_Y(const int _y) { y = _y; }
	void Set_Wid(const int _wid) { wid = _wid; }
	void Set_Hei(const int _hei) { hei = _hei; }
	void Reset();
	int Get_X() { return x; }
	int Get_Y() { return y; }
	int Get_Width() { return wid; }
	int Get_Height() { return hei; }
	int Right() { return x + wid; }
	int Left() { return x; }
	int Top() { return y; }
	int Bottom() { return y + hei; }
	bool IsIntersect(EngineRect &_r);

	~EngineRect(void);
};
