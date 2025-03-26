#include "Map.h"


Map::Map(void)
{
}


Map::~Map(void)
{
}

void Map::Init(short _val[], const short _cw, const short _ch)
{
	tiles.clear();

	for (int h = 0; h < _ch; h++)
	{
		for (int w = 0; w < _cw; w++)
		{
			int ind = _cw * h + w;

			if(_val[ind] == 1)
			{
				Tile _t = Tile((w * (TILE_W + 1) - 300), (h * (TILE_H + 1) - 200), TILE_W, TILE_H, Color(0.0, 0.61, 0.01));

				tiles.push_back(_t);
			}
			else if(_val[ind] == 2)
			{
				Tile _t = Tile((w * (TILE_W + 1) - 300), (h * (TILE_H + 1) - 200), TILE_W, TILE_H, Color(0.94, 0.92, 0.45));
				_t.SetActive(true);

				tiles.push_back(_t);
			}
		}
	}
}

void Map::Draw()
{
	for (int i = 0; i < e_patch.size(); i++)
	{
		Painter::DrawPoint(e_patch[i].x, e_patch[i].y);
	}
	for (int i = 0; i < tiles.size(); i++)
	{
		Painter::DrawBlook(tiles[i].GetX(), tiles[i].GetY(), tiles[i].GetWid(), tiles[i].GetHei(), tiles[i].color);
	}
	for (int i = 0; i < enemys.size(); i++)
	{
		Painter::DrawTriangle(enemys[i].GetDirection(), enemys[i].GetX(), enemys[i].GetY(), enemys[i].GetWid(), enemys[i].color);
		//Painter::DrawBlook(enemys[i].GetX(), enemys[i].GetY(), enemys[i].GetWid(), enemys[i].GetHei(), enemys[i].color);
		//Painter::DrawPoint(enemys[i].Center().x, enemys[i].Center().y);
	}
}

void Map::Update(Tile &_pl, const short _dir)
{
	Tile _p = _pl;

	for (int i = 0; i < enemys.size(); i++)
	{
		enemys[i].Update(tiles, e_patch);
	}

	short _sp = 3;
	bool flag = false;

	if(_dir == 2)
		_p.OffseXY(_sp, 0);
	else if(_dir == 1)
		_p.OffseXY(-_sp, 0);
	else if(_dir == 3)
		_p.OffseXY(0, -_sp);
	else if(_dir == 4)
		_p.OffseXY(0, _sp);
	else 
		return;

	for(int i = 0; i < tiles.size(); i++)
	{
		if(_p.Intersect(tiles[i]) && !tiles[i].IsActive())
			flag = true;
		else if(_p.Intersect(tiles[i]) && tiles[i].IsActive())
		{
			if(_dir == 1)
			{
				Tile _t = tiles[i];
				_t.OffseXY(-_sp, 0);
				short ind = i;
				bool f = false;
				for (int j = 0; j < tiles.size(); j++)
				{
					if(_t.Intersect(tiles[j]) && ind != j)
					{
						f = flag = true;
						break;
					}
				}
				if(!f)
				{
					tiles[i] = _t;
				}
			}
			else if(_dir == 2)
			{
				Tile _t = tiles[i];
				_t.OffseXY(_sp, 0);
				short ind = i;
				bool f = false;
				for (int j = 0; j < tiles.size(); j++)
				{
					if(_t.Intersect(tiles[j]) && ind != j)
					{
						f = flag = true;
						break;
					}
				}
				if(!f)
				{
					tiles[i] = _t;
				}
			}
			else if(_dir == 3)
			{
				Tile _t = tiles[i];
				_t.OffseXY(0, -_sp);
				short ind = i;
				bool f = false;
				for (int j = 0; j < tiles.size(); j++)
				{
					if(_t.Intersect(tiles[j]) && ind != j)
					{
						f = flag = true;
						break;
					}
				}
				if(!f)
				{
					tiles[i] = _t;
				}
			}
			else if(_dir == 4)
			{
				Tile _t = tiles[i];
				_t.OffseXY(0, _sp);
				short ind = i;
				bool f = false;
				for (int j = 0; j < tiles.size(); j++)
				{
					if(_t.Intersect(tiles[j]) && ind != j)
					{
						f = flag = true;
						break;
					}
				}
				if(!f)
				{
					tiles[i] = _t;
				}
			}
		}
	}

	if(!flag)
		_pl = _p;
}

void Map::LoadLevel(const int _lev)
{
	char _c[9] = {GetChar(_lev), 'm', 'a', 'p', '.', 't', 'x', 't'};
	std::ifstream f(_c);
	tiles.clear();
	enemys.clear();
	e_patch.clear();
	
	for (int h = 0; h < 10; h++)
	{
		char _ch[16];
		f.getline(_ch, 16);

		for(int w = 0; w < 15; w++)
		{
			if(_ch[w] == '1')
			{
				Tile _t = Tile((w * (TILE_W + 1) - 300), (h * (TILE_H + 1) - 200), TILE_W, TILE_H, Color(0.0, 0.61, 0.01));
				tiles.push_back(_t);
			}
			else if(_ch[w] == '2')
			{
				Tile _t = Tile((w * (TILE_W + 1) - 300), (h * (TILE_H + 1) - 200), TILE_W, TILE_H, Color(0.94, 0.92, 0.45));
				_t.SetActive(true);

				tiles.push_back(_t);
			}
			else if(_ch[w] == '9')
			{
				Enemy _e;
				_e.initialize((w * (TILE_W + 1) - 300) + 5, (h * (TILE_H + 1) - 200) + 5, TILE_W / 2 + TILE_W / 4, TILE_H / 2 + TILE_H / 4, Color(0.24, 0.92, 0.65));
				enemys.push_back(_e);
			}
		}
	}

	for (int h = 0; h < 11; h++)
	{
		char _ch[16];
		f.getline(_ch, 16);

		for(int w = 0; w < 15; w++)
		{
			if(_ch[w] == '+')
			{
				Tile _t = Tile((w * (TILE_W + 1) - 300), ((h - 1) * (TILE_H + 1) - 200), TILE_W, TILE_H, Color(0.94, 0.92, 0.45));
				e_patch.push_back(_t.Center());
			}
		}
	}
}

char Map::GetChar(const int _count)
{
	char ch;

	switch(_count)
	{
	case 1:
		ch = '1';
		break;
	case 2:
		ch = '2';
		break;
	case 3:
		ch = '3';
		break;
	case 4:
		ch = '4';
		break;
	case 5:
		ch = '5';
		break;
	case 6:
		ch = '6';
		break;
	case 7:
		ch = '7';
		break;
	case 8:
		ch = '8';
		break;
	case 9:
		ch = '9';
		break;
	case 10:
		ch = '10';
		break;
	}

	return ch;
}
