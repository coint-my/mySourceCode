#include "E_Rect.h"


E_Rect::E_Rect(void) : x(0), y(0), wid(1), hei(1)
{
}

E_Rect::E_Rect(const float _x, const float _y, const float _wid, const float _hei) : x(_x), y(_y), wid(_wid), hei(_hei)
{
}

E_Rect::E_Rect(const E_Rect& _rect)
{
	x = _rect.Left();
	y = _rect.Top();
	wid = (_rect.Right() - _rect.Left());
	hei = (_rect.Bottom() - _rect.Top());
}

E_Rect::~E_Rect(void)
{
}

bool E_Rect::IsIntersect(E_Rect& _rect1, E_Rect& _rect2)
{
	if((_rect1.Right() > _rect2.Left()) && (_rect1.Left() < _rect2.Right()) && (_rect1.Bottom() > _rect2.Top()) && (_rect1.Top() < _rect2.Bottom()))
		return true;
	return false;
}
