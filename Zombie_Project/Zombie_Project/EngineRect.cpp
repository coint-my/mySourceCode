#include "EngineRect.h"

EngineRect::EngineRect(void) : x(0), y(0), bx(0), by(0), wid(5), hei(5) 
{

}

EngineRect::EngineRect(int _x, int _y, int _wid, int _hei) : x(_x), y(_y), bx(_x), by(_y), wid(_wid), hei(_hei) 
{

}

void EngineRect::Reset()
{
	x = bx;
	y = by;
}

bool EngineRect::IsIntersect(EngineRect &_r)
{
	bool is_ok = false;

	if((this ->Right() > _r.Left()) && (this ->Left() < _r.Right()) && (this->Bottom() > _r.Top()) && (this->Top() < _r.Bottom()))
		is_ok = true;

	return is_ok;
}

EngineRect::~EngineRect(void)
{
}
