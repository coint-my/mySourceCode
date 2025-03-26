#include "ObjCir.h"


ObjCir::ObjCir(void) : alive(10), curr(0), isAlive(true)
{

}

ObjCir::ObjCir(graphic::Circlef &_cir, graphic::Color &_col, int _al)
{
	cir.Init(_cir.GetPos(), _cir.GetRad(), _cir.GetSegment());
	col = graphic::Color(_col.r, _col.g, _col.b);
	alive = _al;
	curr = 0;
	isAlive = true;
}

ObjCir::~ObjCir(void)
{

}

void ObjCir::Update()
{
	curr++;

	cir.Offset(direction);

	if(curr > alive)
		isAlive = false;
}
