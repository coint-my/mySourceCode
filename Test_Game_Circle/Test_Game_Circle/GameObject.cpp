#include "GameObject.h"


GameObject::GameObject(void) : weight(10), size(0)
{
}


GameObject::~GameObject(void)
{

}

void GameObject::load_point(const Point2i* _pp, const int _count)
{
	size = _count;

	for (int i = 0; i < _count; i++)
		points[i] = _pp[i];
}

