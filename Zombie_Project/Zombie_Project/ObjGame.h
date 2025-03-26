#pragma once

class ObjGame
{
private:
	int x, y;
	int width, height;
public:
	ObjGame(void);
	~ObjGame(void);
	int X() const { return x; }
	int Y() const { return y; }
	int Width() const { return width; }
	int Height() const { return height; }
	void SetX(const int _x) { x = _x; }
	void SetY(const int _y) { y = _y; }
	void SetWidth(const int _w) { width = _w; }
	void SetHeight(const int _h) { height = _h; }
};
