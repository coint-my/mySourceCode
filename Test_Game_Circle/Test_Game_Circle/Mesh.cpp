#include "Mesh.h"
#include "gl/glut.h"

Mesh::Mesh(void)
{
}


Mesh::~Mesh(void)
{
}

void Mesh::initialize(const Point2i *_pp, const int _count)
{
	/*for (int i = 0; i < _count; i++)
	{
		points.push_back(_pp[i]);
	}*/

	load_point(_pp, _count);
}

void Mesh::draw() const
{
	glPushMatrix();
		glTranslatef(pos.x, pos.y, 0);
		glColor3ub(color.r, color.g, color.b);
		glBegin(GL_POLYGON);
		for(int i = 0; i < size; i++)
			glVertex2i(points[i].x, points[i].y);
		/*for (std::vector<Point2i>::const_iterator i = points.begin(); i != points.end(); i++)
		{
			glVertex2i(i->x, i->y);
		}*/
		glEnd();
	glPopMatrix();
}
