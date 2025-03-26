#include "arrayPoints.h"

arrayPoints::arrayPoints(void)
{
}

arrayPoints::arrayPoints(const int width, const int height)
{
	count = 50;
	arr = new PointA[count];
	angle = 0;
	speed = 2;

	angSpray = (30 + 0.01) / count;

	for(int i = 0; i < count; i++)
	{
		arr[i].x = rand() % width;
		arr[i].y = rand() % height;
	}
}

void arrayPoints::cor(const int x, const int y)
{
	for(int i = 0; i < count; i++)
	{
		float fac = sqrt((x - arr[i].x) * (x - arr[i].x) + (y - arr[i].y) * (y - arr[i].y));

		arr[i].x -= speed * (arr[i].x - x) / fac;
		arr[i].y -= speed * (arr[i].y - y) / fac;
	}
}

void arrayPoints::gravityOreol(const double alpha)
{
	posy += alpha / 17;

	for(int i = 0; i < count; i++)
	{
		arr[i].y += posy;
	}
}

arrayPoints::arrayPoints(const int ar, const int _angle, const int _spray, const int lenth, const int lenthLive)
{
	count = ar;
	arr = new PointA[count];
	angle = _angle;
	isActive = false;
	angSpray = (_spray + 0.01) / count;
	speed = 7;
	counter = 0;
	posy = 0;

	for(int i = 0; i < count; i++)
	{
		arr[i].lenth = rand() % lenth + lenthLive;
		arr[i].x = 0;
		arr[i].y = 0;
		init(0, 0, i);
	}
}

arrayPoints::~arrayPoints(void)
{
	//delete [] arr;
}

void arrayPoints::updateOreol()
{
	if(isActive)
	{
		counter++;

		float rotStep = 360 / count + 1;

		for(int i = 0; i < count; i++)
		{
			double a = (i * rotStep) * (3.14159 / 180);

			if(counter < arr[i].lenth)
			{
				arr[i].x += cos(a) * speed;
				arr[i].y += sin(a) * speed;
			}
			else
			{
				if(counter > 25)
				{
					isActive = false;
					counter = 0;
					posy = 0;
				}
			}
		}
		gravityOreol(5);
	}
}

void arrayPoints::initPosition(const int vx, const int vy)
{
	float rotStep = 360 / count + 1;

	for(int i = 0; i < count; i++)
	{
		double a = (i * rotStep) * (3.14159 / 180);

		init(arr[i].y = vy + sin(a) * 5, arr[i].x = vx + cos(a) * 5, i);
	}
}

void arrayPoints::init(const int yInit, const int xInit, const int ind)
{
	arr[ind]._y = yInit;
	arr[ind]._x = xInit;
	arr[ind].x = xInit;
	arr[ind].y = yInit;
}

void arrayPoints::drawArr()
{
	for(int i = 0; i < count; i++)
	{
		glVertex2f(arr[i].x, arr[i].y);
	}
}

float arrayPoints::radius(const float x, const float y, const float endx, const float endy)
{
	float dx;
	float dy;

	if(x > endx)
		dx = x - endx;
	else
		dx = endx - x;
	if(y > endy)
		dy = y - endy;
	else
		dy = endy - y;

	return dx + dy;
}

void arrayPoints::update(const int x, const int y)
{
	float ang = 0;

	for(int i = 0; i < count; i++)
	{
		double a = (angle + ang) * (3.14159 / 180);

		arr[i].y += sin((double)a)*speed;
		arr[i].x += cos((double)a)*speed;
		
		float f = radius(x, y, arr[i].x, arr[i].y);

		if(f > arr[i].lenth)
			init(y, x, i);

		ang += angSpray;
	}
}

void arrayPoints::updateAB(const int x_x, const int y_y, const int dx, const int dy)
{
	for(int i = 0; i < count; i++)
	{
		arr[i].y -= speed;

		if(arr[i].y < y_y - arr[i].lenth)
		{
			int xx = rand() % (dx - x_x) + x_x;
			int yy = rand() % (dy - y_y) + y_y;

			init(yy, xx, i);
		}
	}
}