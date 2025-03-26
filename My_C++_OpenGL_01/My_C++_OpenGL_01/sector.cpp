#include "sector.h"

sector::sector(void)
{
	scale = 1;
}
sector::sector(const std::vector<point> p)
{
	scale = 1;
	pos = p;
}

sector::~sector(void)
{
}

void sector::Scale(const int s)
{
	scale = s;
}

void sector::draw()
{
	glBegin(GL_QUADS);

	for(int i = 0; i < pos.size(); i++)
	{
		glTexCoord2d(0, 0);
		glVertex3f(pos[i].x * scale, -50, pos[i].z * scale);
		glTexCoord2d(0, 1);
		glVertex3f(pos[i].dx * scale, -50, pos[i].dz * scale);
		glTexCoord2d(1, 1);
		glVertex3f(pos[i].dx * scale, 50, pos[i].dz * scale);
		glTexCoord2d(1, 0);
		glVertex3f(pos[i].x * scale, 50, pos[i].z * scale);
	}

	glEnd();
}