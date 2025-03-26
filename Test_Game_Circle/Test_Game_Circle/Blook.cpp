#include "Blook.h"
#include "StringRead.h"
#include "gl/glut.h"

Blook::Blook(void) : wid(0), hei(0), is_show(false)
{
	std::strcpy(label, "");
}

Blook::Blook(const Point2f &_pos, const int _wid, const int _hei) : wid(_wid), hei(_hei), is_show(false)
{
	pos =_pos;

	std::strcpy(label, "");

	Point2i p[] = { Point2i(0, 0), Point2i(wid, 0), Point2i(wid, hei), Point2i(0, hei),	};

	load_point(p, 4);
}

Blook::Blook(const char _name[10], const Point2f &_pos, const int _wid, const int _hei) : wid(_wid), hei(_hei), is_show(true)
{
	std::strcpy(label, _name);

	pos =_pos;

	Point2i p[] = { Point2i(0, 0), Point2i(wid, 0), Point2i(wid, hei), Point2i(0, hei),	};

	load_point(p, 4);
}

Blook::Blook(const char _name[10], const Point2f &_pos, const int _wid, const int _hei, const Colorf& _color) : wid(_wid), hei(_hei), is_show(true)
{
	set_color(_color);
	std::strcpy(label, _name);

	pos =_pos;

	Point2i p[4] = { Point2i(10, 10), Point2i(30, 10), Point2i(30, 30), Point2i(10, 30)	};

	load_point(p, 4);
}

Blook::Blook(float _m, const Point2f &_pos, const int _wid, const int _hei) : wid(_wid), hei(_hei), is_show(false)
{
	pos =_pos;
	std::strcpy(label, "");
	set_weight(_m);

	Point2i _mass[16] = 
	{
		Point2i(0, 0),
		Point2i(0, hei),
		Point2i(wid / 4, (hei / 4) * 3),
		Point2i(wid / 4, hei / 4),
		Point2i(0, 0),
		Point2i(wid, 0),
		Point2i((wid / 4) * 3, hei / 4),
		Point2i(wid / 4, hei / 4),
		Point2i(wid, 0),
		Point2i(wid, hei),
		Point2i((wid / 4) * 3, (hei / 4) * 3),
		Point2i((wid / 4) * 3, hei / 4),
		Point2i(wid, hei),
		Point2i(0, hei),
		Point2i(wid / 4, (hei / 4) * 3),
		Point2i((wid / 4) * 3, (hei / 4) * 3)
	};

	load_point(_mass, 16);
}

//Blook::Blook(const Blook& _bl)
//{
//	std::strncpy(label, _bl.label, 10);
//	this->is_show = _bl.is_show;
//	this->weight = _bl.weight;
//	this->wid = _bl.wid;
//	this->hei = _bl.hei;
//	this->size = _bl.size;
//	this->pos = _bl.pos;
//	this->color = _bl.color;
//
//	for(int i = 0; i < size; i++)
//		this->points[i] = Point2i(_bl.points[i]);
//}

Blook::~Blook(void)
{
}

//Blook& Blook::operator=(const Blook& _bl)
//{
//	std::strncpy(label, _bl.label, 10);
//	this->is_show = _bl.is_show;
//	this->weight = _bl.weight;
//	this->wid = _bl.wid;
//	this->hei = _bl.hei;
//	this->size = _bl.size;
//	this->pos = _bl.pos;
//	this->color = _bl.color;
//
//	for(int i = 0; i < size; i++)
//		this->points[i] = Point2i(_bl.points[i]);
//
//	return *(this);
//}

void Blook::show_lable() const
{
	StringRead::ShowString(label, pos.x + 5, pos.y + (hei / 2) + 5);
}
void Blook::init_wall()
{
	Point2i _mass[8] = 
	{
		Point2i(0, 0),
		Point2i(0, 0),
		Point2i(wid - 2, 0),
		Point2i(0, hei - 2),
		Point2i(wid, hei),
		Point2i(wid, hei),
		Point2i(2, hei),
		Point2i(wid, 2)
	};
	load_point(_mass, 8);
}

void Blook::draw() const
{
	float difuse_light[] = { color.r, color.g, color.b, 1.0 };
	glLightfv(GL_LIGHT0, GL_DIFFUSE, difuse_light);

	glPushMatrix();
		glTranslatef(pos.x, pos.y, 0);
		
		glBegin(GL_QUADS);

		glNormal3f(0.0, 0.0, 1.0);

		for(int i = 0; i < size; i++)
			glVertex2i(points[i].x, points[i].y);

		glEnd();
	glPopMatrix();

	if(is_show)
	{
		glDisable(GL_LIGHTING);
		glDisable(GL_LIGHT0);
		glDisable(GL_LIGHT1);
		glDisable(GL_DEPTH_TEST);

		glColor3f(color.r, color.g, color.b);
		show_lable();

		glEnable(GL_LIGHTING);
		glEnable(GL_LIGHT0);
		glEnable(GL_LIGHT1);
		glEnable(GL_DEPTH_TEST);
	}
}

void Blook::draw(const float _z, const Point3f& _normal) const
{
	const float difuse_light[] = { color.r, color.g, color.b, 1.0 };
	glLightfv(GL_LIGHT0, GL_DIFFUSE, difuse_light);
	glPushMatrix();
		glTranslatef(pos.x, pos.y, _z);

		glBegin(GL_QUADS);
		glNormal3f(_normal.x, _normal.y, _normal.z);

		for(int i = 0; i < size; i++)
			glVertex2i(points[i].x, points[i].y);

		glEnd();

	glPopMatrix();
}
