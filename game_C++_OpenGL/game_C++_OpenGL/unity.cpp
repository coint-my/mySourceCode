#include "unity.h"

unity::unity(int widCount, int heiCount, int w, int h) : wCount(widCount), hCount(heiCount), t(w / widCount, h / heiCount)
{
	width = w / widCount;
	height = h / heiCount;

	map = new bool*[hCount];
	for(int i = 0; i < wCount; ++i)
		map[i] = new bool[wCount];

	for(int i = 0; i < wCount; ++i)
		for(int j = 0; j < hCount; ++j)
			map[i][j] = false;
	tempRand();
}

unity::~unity(void)
{
}

bool unity::Map(const int x, const int y) const 
{
	return map[x][y];
}

void unity::tempRand()
{
	t.init(static_cast<tetrino::Name>(rand() % 5));
}

bool unity::moveDown()
{
	tetrino tt = t;

	bool triger = false;

	tt.offset(0, 1);

	for(int i = 0; i < wCount; ++i)
	{
		for(int j = 0; j < hCount; ++j)
		{
			if(map[i][j] && tt.intersect(i, j))
				triger = true;
			else if(tt.Y() > hCount - tt.IsDown(tetrino::DOWN))
				triger = true;
		}
	}
	if(!triger)
		t = tt;
	else
	{
		for(int i = 0; i < wCount; ++i)
		{
			for(int j = 0; j < hCount; ++j)
			{
				if(i >= t.X() && j >= t.Y())
					if(t.intersect(i, j))
						map[i][j] = t.intersect(i, j);
			}
		}
		int istrue = 0;
		for(int j = 0; j < hCount; ++j)
		{
			for(int i = 0; i < wCount; ++i)
			{
				if(map[i][j])
					istrue++;
			}
			if(istrue >= wCount)
			{
				removeLine(j);
			}
			istrue = 0;
		}
		t.X(wCount / 2 - 2);
		t.Y(-1);
		tempRand();
		return true;
	}
	return false;
}

void unity::move(const tetrino::Direction dir)
{
	tetrino tt = t;

	bool triger = false;

	if(dir == tetrino::LEFT)
	{
		tt.offset(-1, 0);
	}
	else if(dir == tetrino::RIGHT)
	{
		tt.offset(1, 0);
	}
	else if(dir == tetrino::UP)
	{
		tt.rotationTetrino();
	}
	for(int i = 0; i < wCount; ++i)
	{
		for(int j = 0; j < hCount; ++j)
		{
			if((tt.X() < 0 - (tt.IsDown(tetrino::LEFT) - 1)) || (tt.X() > wCount - tt.IsDown(tetrino::RIGHT)))
				triger = true;
			else if(map[i][j] && tt.intersect(i, j))
				triger = true;
		}
	}
	if(!triger)
		t = tt;
}

void unity::draw(painter &p)
{
	p.setColor(painter::ELLOWY);

	for(int i = 0; i < wCount; i++)
	{
		for(int j = 0; j < hCount; j++)
		{
			if(!map[i][j])
			{
				p.rect((width / 2 - 1) + (i * width), (height / 2 - 1) + (height * j), 
					(width / 2 - 1) + 2 + (i * width), (height / 2 - 1) + 2 + (height * j));
			}
			else
			{
				p.rect(i * width, j * height, (i + 1) * width - 1, (j + 1) * height - 1);
			}
		}
	}
	t.draw(p, painter::RED);
}

void unity::removeLine(const int ind_y)
{
	for(int i = 0; i < wCount; i++)
		map[i][ind_y] = false;
	
	bool **isa = map;

	for(int i = ind_y; i > 1; i--)
	{
		for(int j = 0; j < wCount; j++)
		{	
			isa[j][i] = map[j][i - 1];
		}
	}

	map = isa;
}
