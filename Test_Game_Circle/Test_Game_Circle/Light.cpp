#include "Light.h"
#include "gl/glut.h"

Light::Light(void) : pos(), col_dif()
{
}

Light::Light(Color4f& _pos, Color4f& _col_dif) : pos(_pos), col_dif(_col_dif)
{

}

Light::~Light(void)
{
}

void Light::draw() const
{
	const float _amb[] = { col_dif.r, col_dif.g, col_dif.b, col_dif.a };
	const float _pos[] = { pos.r, pos.g, pos.b, pos.a };

	glLightfv(GL_LIGHT1, GL_DIFFUSE, _amb);
	glLightfv(GL_LIGHT1, GL_POSITION, _pos);
	//glLightfv(GL_LIGHT0, GL_DIFFUSE, _amb);
}
