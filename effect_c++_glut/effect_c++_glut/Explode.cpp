#include "Explode.h"

Explode::Explode(void) : count(10) , speedPart(0.7f), enebleGravity(false), pSize(9),
	sCount(0), alive(0), setGravity(0.0005f), isActive(false)
{
}

Explode::Explode(const int _count, const float _speed) : count(_count) , speedPart(_speed), enebleGravity(false), pSize(9),
	sCount(0), alive(0), setGravity(0.0005f), isActive(false)
{
}

Explode::~Explode(void)
{
}

void Explode::display()
{
	sCount++;
	alive++;
	if(alive == 55)
	{
		part.clear();
		isActive = true;
	}

	if(sCount > 5)
	{
		sCount = 0;
		if(pSize > 1)
			pSize--;
	}
	glPointSize(pSize);
	glBegin(GL_POINTS);
		int k = part.size();
		for(int i = 0; i < part.size(); i++)
		{
			part[i].x += part[i].vx;
			part[i].y += part[i].vy;
			part[i].z += part[i].vz;
			if(enebleGravity)
			{
				if(part[i].vx > 0.5)
					part[i].vx -= 0.03;
				if(part[i].vx < -0.5)
					part[i].vx += 0.03;
				counter += setGravity;
				part[i].y += counter;
			}
			glColor3f(part[i].getColor()[0], part[i].getColor()[1], part[i].getColor()[2]);
			glVertex3f(part[i].x, part[i].y, part[i].z);
		}
	glEnd();
}

void Explode::initExplodePosition(const float _x, const float _y, const float _z, const int radExplode)
{
	part.clear();

	counter = 0;
	pSize = 9;
	alive = 0;

	for(int i = 0; i < count; i++)
	{
		float rotPos = i * (3.14159 / count * 2);

		Pixel3 &p = Pixel3(_x + (rand() % radExplode - radExplode), _y + (rand() % radExplode - radExplode),
			_z, Pixel3::red);

		p.vx = cos(rotPos) * speedPart;

		p.vy = sin(rotPos) * speedPart;

		p.vz = _z;

		part.push_back(p);
	}
}
