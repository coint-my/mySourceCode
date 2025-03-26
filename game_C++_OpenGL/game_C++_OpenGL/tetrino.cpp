#include "tetrino.h"

tetrino::tetrino(int w, int h)
{
	width = w;
	height = h;

	x = 0;
	y = 0;

	rotation = 0;
}

tetrino::~tetrino(void)
{
}

bool tetrino::active(const int _x, const int _y, tetrino::Name n, int rot)
{
	bool a[4][4];

	switch(n)
	{
	case T:
		if(rot == 0)
		{
			a[0][0] = 0; a[1][0] = 0; a[2][0] = 0; a[3][0] = 0;
			a[0][1] = 0; a[1][1] = 0; a[2][1] = 0; a[3][1] = 0;
			a[0][2] = 0; a[1][2] = 1; a[2][2] = 0; a[3][2] = 0;
			a[0][3] = 1; a[1][3] = 1; a[2][3] = 1; a[3][3] = 0;
		}
		else if(rot == 1)
		{
			a[0][0] = 0; a[1][0] = 0; a[2][0] = 0; a[3][0] = 0;
			a[0][1] = 1; a[1][1] = 0; a[2][1] = 0; a[3][1] = 0;
			a[0][2] = 1; a[1][2] = 1; a[2][2] = 0; a[3][2] = 0;
			a[0][3] = 1; a[1][3] = 0; a[2][3] = 0; a[3][3] = 0;
		}
		else if(rot == 2)
		{
			a[0][0] = 0; a[1][0] = 0; a[2][0] = 0; a[3][0] = 0;
			a[0][1] = 0; a[1][1] = 0; a[2][1] = 0; a[3][1] = 0;
			a[0][2] = 1; a[1][2] = 1; a[2][2] = 1; a[3][2] = 0;
			a[0][3] = 0; a[1][3] = 1; a[2][3] = 0; a[3][3] = 0;
		}
		else if(rot == 3)
		{
			a[0][0] = 0; a[1][0] = 0; a[2][0] = 0; a[3][0] = 0;
			a[0][1] = 0; a[1][1] = 1; a[2][1] = 0; a[3][1] = 0;
			a[0][2] = 1; a[1][2] = 1; a[2][2] = 0; a[3][2] = 0;
			a[0][3] = 0; a[1][3] = 1; a[2][3] = 0; a[3][3] = 0;
		}
		break;
	case L:
		if(rot == 0)
		{
			a[0][0] = 0; a[1][0] = 0; a[2][0] = 0; a[3][0] = 0;
			a[0][1] = 1; a[1][1] = 0; a[2][1] = 0; a[3][1] = 0;
			a[0][2] = 1; a[1][2] = 0; a[2][2] = 0; a[3][2] = 0;
			a[0][3] = 1; a[1][3] = 1; a[2][3] = 0; a[3][3] = 0;
		}
		else if(rot == 1)
		{
			a[0][0] = 0; a[1][0] = 0; a[2][0] = 0; a[3][0] = 0;
			a[0][1] = 0; a[1][1] = 0; a[2][1] = 0; a[3][1] = 0;
			a[0][2] = 1; a[1][2] = 1; a[2][2] = 1; a[3][2] = 0;
			a[0][3] = 1; a[1][3] = 0; a[2][3] = 0; a[3][3] = 0;
		}
		else if(rot == 2)
		{
			a[0][0] = 0; a[1][0] = 0; a[2][0] = 0; a[3][0] = 0;
			a[0][1] = 1; a[1][1] = 1; a[2][1] = 0; a[3][1] = 0;
			a[0][2] = 0; a[1][2] = 1; a[2][2] = 0; a[3][2] = 0;
			a[0][3] = 0; a[1][3] = 1; a[2][3] = 0; a[3][3] = 0;
		}
		else if(rot == 3)
		{
			a[0][0] = 0; a[1][0] = 0; a[2][0] = 0; a[3][0] = 0;
			a[0][1] = 0; a[1][1] = 0; a[2][1] = 0; a[3][1] = 0;
			a[0][2] = 0; a[1][2] = 0; a[2][2] = 1; a[3][2] = 0;
			a[0][3] = 1; a[1][3] = 1; a[2][3] = 1; a[3][3] = 0;
		}
		break;
	case Z:
		if(rot == 0 || rot == 2)
		{
			a[0][0] = 0; a[1][0] = 0; a[2][0] = 0; a[3][0] = 0;
			a[0][1] = 0; a[1][1] = 0; a[2][1] = 0; a[3][1] = 0;
			a[0][2] = 1; a[1][2] = 1; a[2][2] = 0; a[3][2] = 0;
			a[0][3] = 0; a[1][3] = 1; a[2][3] = 1; a[3][3] = 0;
		}
		else if(rot == 1 || rot == 3)
		{
			a[0][0] = 0; a[1][0] = 0; a[2][0] = 0; a[3][0] = 0;
			a[0][1] = 0; a[1][1] = 1; a[2][1] = 0; a[3][1] = 0;
			a[0][2] = 1; a[1][2] = 1; a[2][2] = 0; a[3][2] = 0;
			a[0][3] = 1; a[1][3] = 0; a[2][3] = 0; a[3][3] = 0;
		}
		break;
	case I:
		if(rot == 0 || rot == 2)
		{
			a[0][0] = 1; a[1][0] = 0; a[2][0] = 0; a[3][0] = 0;
			a[0][1] = 1; a[1][1] = 0; a[2][1] = 0; a[3][1] = 0;
			a[0][2] = 1; a[1][2] = 0; a[2][2] = 0; a[3][2] = 0;
			a[0][3] = 1; a[1][3] = 0; a[2][3] = 0; a[3][3] = 0;
		}
		else if(rot == 1 || rot == 3)
		{
			a[0][0] = 0; a[1][0] = 0; a[2][0] = 0; a[3][0] = 0;
			a[0][1] = 0; a[1][1] = 0; a[2][1] = 0; a[3][1] = 0;
			a[0][2] = 0; a[1][2] = 0; a[2][2] = 0; a[3][2] = 0;
			a[0][3] = 1; a[1][3] = 1; a[2][3] = 1; a[3][3] = 1;
		}
		break;
	case O:
			a[0][0] = 0; a[1][0] = 0; a[2][0] = 0; a[3][0] = 0;
			a[0][1] = 0; a[1][1] = 0; a[2][1] = 0; a[3][1] = 0;
			a[0][2] = 1; a[1][2] = 1; a[2][2] = 0; a[3][2] = 0;
			a[0][3] = 1; a[1][3] = 1; a[2][3] = 0; a[3][3] = 0;
		break;
	}
	return a[_x][_y];
}


void tetrino::init(tetrino::Name n)
{
	name = n;
	rotation = 0;

	for(int i = 0; i < 4; i++)
		for(int j = 0; j < 4; j++)
			map[i][j] = false;
	
	for(int i = 0; i < 4; i++)
		for(int j = 0; j < 4; j++)
			if(active(i, j, n, 0))
			{
				map[i][j] = active(i, j, n, 0);
			}
}

void tetrino::rotationTetrino()
{
	rotation++;

	if(rotation > 3)
		rotation = 0;

	for(int i = 0; i < 4; i++)
		for(int j = 0; j < 4; j++)
			map[i][j] = false;
	
	for(int i = 0; i < 4; i++)
	{
		for(int j = 0; j < 4; j++)
			if(active(i, j, name, rotation))
			{
				map[i][j] = active(i, j, name, rotation);
			}
	}
}

void tetrino::offset(const int dx, const int dy)
{
	x += dx;
	y += dy;
}

void tetrino::draw(painter &p) const
{
	//p.setColor(painter::GREEN);

	for(int i = 0; i < 4; i++)
		for(int j = 0; j < 4; j++)
		{
			if(map[i][j])
			{
				p.rect((x + i) * width, (y + j) * height, (x + i + 1) * width - 1, (y + j + 1) * height - 1);
			}
		}
}

void tetrino::draw(painter &p, painter::Color c) const
{
	p.setColor(c);

	for(int i = 0; i < 4; i++)
		for(int j = 0; j < 4; j++)
		{
			if(map[i][j])
			{
				p.rect((x + i) * width, (y + j) * height, (x + i + 1) * width - 1, (y + j + 1) * height - 1);
			}
		}
}

bool tetrino::intersect(const int _x, const int _y)
{
	for(int i = 0; i < 4; i++)
	{
		for(int j = 0; j < 4; j++)
		{
			if(map[i][j])
			{
				if(x + i == _x && y + j == _y)
					return true;
			}
		}
	}
	return false;
}

int tetrino::IsDown(tetrino::Direction dir) const
{
	int temp = 0;
	int uper = 0;

	for(int i = 0; i < 4; i++)
	{
		for(int j = 0; j < 4; j++)
		{
			if(map[i][j] && dir == 2)
			{
				if(j > uper)
					uper = j;
				
			}
			else if(map[i][j] && dir == 1)
				temp = i;
			else if(map[i][j] && dir == -1)
			{
				temp = i;
				return temp + 1;
			}
		}
	}
	if(dir == 2)
		return uper + 1;
	return temp + 1;
}
