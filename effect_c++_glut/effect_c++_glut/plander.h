#pragma once
#include "Painter.h"

class plander
{
private:
	int x, y;
	int vx, vy;
	int lenth;
	int height;
public:
	enum Move{Left, Right, None};
	plander(void);
	plander(const int _x, const int _y, const int _vx, const int _vy, const int _lenth);
	~plander(void);

	void Draw(Painter &p) const;
	void MovePlane(Move m);
	int getX() const {return x;}
	int getY() const {return y;}
	int getVX() const {return vx;}
	int getVY() const {return vy;}
	int getLenth() const {return lenth;}
	void setLenth(const int _lenth) {lenth = _lenth;}
};
