#pragma once

#ifndef RENDEROBJRCT_H
#define RENDEROBJECT_H

#include <iostream>
#include <gl\glut.h>
#include "GameObject.h"
#include "Figure.h"

class RenderObject
{
private:
	std::vector<GameObject> list_obj;
public:
	RenderObject(void);
	~RenderObject(void);

	void AddObject(const GameObject& _obj);
	void Render();
	void Update();
};

#endif

