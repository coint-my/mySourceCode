#include "VovanLib.h"
namespace vovan
{
	bool Color::operator==(Color &_col)
	{
		if((r == _col.r) && (g == _col.g) && (b == _col.b) && (a == _col.a))
			return true;
		else
			return false;
	}

	bool Color::operator!=(Color &_col)
	{
		if(*(this) == _col)
			return false;
		return true;
	}

	void Circlef::Init(const Vector2f &_pos, const int _rad, const int _seg)
	{
		pos.x = _pos.x;
		pos.y = _pos.y;
		rad = _rad;
		seg = _seg;
	}

	bool Recti::Intersect(Recti &_r)
	{
		if((Right() > _r.Left()) && (Left() < _r.Right()) && (Bottom() > _r.Top()) && (Top() < _r.Bottom()))
			return true;
		else
			return false;
	}

	const Recti &Recti::operator=(const Recti &_r)
	{
		pos.x = _r.pos.x;
		pos.y = _r.pos.y;
		wid = _r.wid;
		hei = _r.hei;
		return *this;
	}

	bool Rectf::Intersect(Rectf &_r)
	{
		if((Right() > _r.Left()) && (Left() < _r.Right()) && (Bottom() > _r.Top()) && (Top() < _r.Bottom()))
			return true;
		else
			return false;
	}

	/*const Rectf &Rectf::operator=(const Rectf &_r)
	{
		pos.x = _r.pos.x;
		pos.y = _r.pos.y;
		wid = _r.wid;
		hei = _r.hei;
		return *this;
	}*/
}