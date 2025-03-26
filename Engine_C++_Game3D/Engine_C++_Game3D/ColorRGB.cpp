#include "ColorRGB.h"

ColorRGB::ColorRGB(void)
{
	r = 0.1;
	g = 0.1;
	b = 0.1;
}

void ColorRGB::SetColor(float _r, float _g, float _b)
{
	r = _r;
	g = _g;
	b = _b;
}

float* ColorRGB::GetColor() const
{
	float color[3];
	color[0] = r;
	color[1] = g;
	color[2] = b;
	return color;
}

ColorRGB::~ColorRGB(void)
{
}
