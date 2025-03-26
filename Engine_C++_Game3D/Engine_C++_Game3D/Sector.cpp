#include "Sector.h"

Sector::Sector(void)
{
}

Sector::~Sector(void)
{
}

void Sector::LoadSector(const std::vector<Point> _points)
{
	points = _points;
}

void Sector::DrawSector3D() const
{

}

void Sector::DrawSector(const int _offx, const int _offy, const int _lentch) const
{
	glColor4f(0.0, 1.0, 0.0, 0.5);

	glBegin(GL_LINE_LOOP);

	for(int i = 0; i < points.size(); i++)
	{
		glVertex2i((points[i].x * _lentch) + _offx, (points[i].y * _lentch) + _offy);
	}

	glEnd();
	glColor3f(color.GetColor()[0], color.GetColor()[1], color.GetColor()[2]);
	glBegin(GL_POINTS);

	for(int i = 0; i < points.size(); i++)
	{
		glColor3f(points[i].col.GetColor()[0], points[i].col.GetColor()[1], points[i].col.GetColor()[2]);
		glVertex2i((points[i].x * _lentch) + _offx, (points[i].y * _lentch) + _offy);
	}

	glEnd();
}
