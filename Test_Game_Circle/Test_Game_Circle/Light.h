#pragma once
#include "GameObject.h"

class Light
{
private:
	Color4f pos;
	Color4f col_dif;
public:
	Light(void);
	Light(Color4f& _pos, Color4f& _col_dif);
	~Light(void);

	void draw() const;
};

