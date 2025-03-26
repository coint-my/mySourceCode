#include "Obj.h"


Obj::Obj(void) : rect(), col()
{
}


Obj::~Obj(void)
{
}

void Obj::Init(const vovan::Rectf &_r, const vovan::Color &_c, const vovan::Vector2f &_dir)
{
	rect.Initialize(_r); 
	col = vovan::Color(_c);
	dir = vovan::Vector2f(_dir);
}
