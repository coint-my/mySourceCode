#include "Trigger.h"
#include "gl/glut.h"

Trigger::Trigger(void) : isActive(true)
{
	rect = E_Rect();
	std::strcpy(name, "none ");
	name[29] = '\0';
	Colorf(0.1, 0.1, 0.1);
}

Trigger::Trigger(const char* _name, E_Rect& _rect, const Colorf& _color) : isActive(true), color(_color)
{
	std::strcpy(name, _name);
	name[29] = '\0';
	
	rect = E_Rect(_rect);
}


Trigger::~Trigger(void)
{
}

void Trigger::render() const
{
	if(isActive)
	{
		const float amb_light[] = { color.r, color.g, color.b, 1.0 };
		glLightfv(GL_LIGHT0, GL_DIFFUSE, amb_light);
		glPushMatrix();
		glRectf(rect.Left(), rect.Top(), rect.Right(), rect.Bottom());
		glPopMatrix();
	}
}
