#include "GameObject.h"


GameObject::GameObject(void) : isTexture(false)
{
	
}


GameObject::~GameObject(void)
{
	points.clear();
	textPoints.clear();
}

void GameObject::LoadPoints(const Translation *const _list, const int _count)
{
	points.clear();

	for (int i = 0; i < _count; i++)
		points.push_back(_list[i]);
}
