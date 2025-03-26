#include "Enemy.h"
#include <ctime>


Enemy::Enemy(void)
{
	srand(time(0));
	dir = (direction)(rand() % 4 + 1);
	false_index = -1;
}


Enemy::~Enemy(void)
{
}

void Enemy::initialize(const int _x, const int _y, const int _wid, const int _hei, Color _col)
{
	i_x = _x;
	i_y = _y;
	init(_x, _y, _wid, _hei, _col);
	srand(_x + _y + time(0));
	dir = (direction)(rand() % 4 + 1);
}

bool Enemy::IsPassive(const short _dir, const std::vector<Tile> &_tiles, short &_negativ)
{
	bool _flag = false;
	Tile _temp;
	switch(_dir)
	{
	case 1: 
		_temp = Tile(Left() - GetWid(), Top(), GetWid(), GetHei(), Color());
		_negativ = 2;
		for (int i = 0; i < _tiles.size(); i++)
		{
			if(_temp.Intersect(_tiles[i]))
			{
				_flag = true;
				break;
			}
		}
		 break;
	case 2: 
		_temp = Tile(Right(), Top(), GetWid(), GetHei(), Color());
		_negativ = 1;
		for (int i = 0; i < _tiles.size(); i++)
		{
			if(_temp.Intersect(_tiles[i]))
			{
				_flag = true;
				break;
			}
		}
		 break;
	case 3: 
		_temp = Tile(Left(), Top() - GetHei(), GetWid(), GetHei(), Color());
		_negativ = 4;
		for (int i = 0; i < _tiles.size(); i++)
		{
			if(_temp.Intersect(_tiles[i]))
			{
				_flag = true;
				break;
			}
		}
		 break;
	case 4: 
		_temp = Tile(Left(), Bottom(), GetWid(), GetHei(), Color());
		_negativ = 3;
		for (int i = 0; i < _tiles.size(); i++)
		{
			if(_temp.Intersect(_tiles[i]))
			{
				_flag = true;
				break;
			}
		}
		 break;
	}
	return _flag;
}

void Enemy::Update(const std::vector<Tile> &_tiles, const std::vector<Vector2> &e_patch)
{
	Enemy &_e = Enemy();
	_e.SetX(x);
	_e.SetY(y);
	_e.SetWid(wid);
	_e.SetHei(hei);

	Vector2 &_pos = Vector2();
	Vector2 _point = Vector2(this->x + (this->wid / 2), this->y + (this->hei / 2));

	bool isTurn = false;
	float _s = 1.5;

	if(dir == L)
		_pos.x = -_s, _pos.y = 0;
	else if(dir == R)
		_pos.x = _s, _pos.y = 0;
	else if(dir == T)
		_pos.x = 0, _pos.y = -_s;
	else if(dir == B)
		_pos.x = 0, _pos.y = _s;

	_e.OffseXY(_pos.x, _pos.y);

	for (int i = 0; i < e_patch.size(); i++)
	{
		float _val = sqrt((_point.x - e_patch[i].x) * (_point.x - e_patch[i].x) + (_point.y - e_patch[i].y) * (_point.y - e_patch[i].y));

		if(_val < 4 && false_index != i)
		{
			isTurn = true;
			false_index = i;
			break;
		}
	}

	if(isTurn)
	{
		v_dir.clear();

		short _neg = -1;
		IsPassive(static_cast<int>(dir), _tiles, _neg);

		short _none = 0;
		if(!IsPassive((short)L, _tiles, _none))
			v_dir.push_back(static_cast<short>(L));
		if(!IsPassive((short)R, _tiles, _none))
			v_dir.push_back(static_cast<short>(R));
		if(!IsPassive((short)T, _tiles, _none))
			v_dir.push_back(static_cast<short>(T));
		if(!IsPassive((short)B, _tiles, _none))
			v_dir.push_back(static_cast<short>(B));

		if(v_dir.size() > 1)
		{
			for (int i = 0; i < v_dir.size(); i++)
				if(_neg == v_dir[i])
					v_dir.erase(v_dir.begin() + i);
		}
		srand(clock());
		dir = (direction)(v_dir[rand() % v_dir.size()]);
	}
	this->SetX(_e.GetX());
	this->SetY(_e.GetY());
}
