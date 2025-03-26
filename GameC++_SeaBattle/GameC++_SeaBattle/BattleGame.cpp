#include "BattleGame.h"

BattleGame::BattleGame(void)
{
	_count_destroy_enemy = _count_destroy_player = 0;
	_present = 1;
	_end_present = 10;
	_count_present = _present;
	is_passive_enemy = false;
}

BattleGame::~BattleGame(void)
{
}

void BattleGame::SetString(char *_ch, short _x, short _y)
{
	glColor3f(0.5, 0.0, 0.5);

	glRasterPos2s(_x, _y);
	while(*_ch)
		glutBitmapCharacter(GLUT_BITMAP_HELVETICA_18, *_ch++);
}
void BattleGame::SetStringLinear(char _ch, short _x, short _y)
{
	glColor3f(0.7, 0.5, 0.3);

	glRasterPos2s(_x, _y);

	glutBitmapCharacter(GLUT_BITMAP_HELVETICA_18, _ch);
}

bool BattleGame::Intersect(const short _x, const short _y, Pole &_p)
{
	short alpha_x = _p.offset_x;
	short alpha_y = _p.offset_y;
	if((_x > alpha_x && _y > alpha_y) && (alpha_x + ((Painter::wid + 1) * WIDTH_POLE) > _x)
		&& (alpha_y + ((Painter::hei + 1) * HEIGHT_POLE) > _y))
	{
		return true;
	}
	return false;
}

bool BattleGame::IsShuttleFullEnemy(Pole &_p, Pole &_pv ,const short _ind_x, const short _ind_y)
{
	short _count_x = 0;
	short _count_y = 0;
	short _index_x = 0;
	short _index_y = 0;
	bool is_x = false;
	bool is_y = false;

	for(short _x = 0; _x < WIDTH_POLE + 1; _x++)
	{
		if(_p.GetGex(_x ,_ind_y).active == true)
		{
			_count_x++;
		}
		else
		{
			if(_count_x > 0)
			{
				bool is_true = false;
				for(short _c = _x - 1; _c > (_x - 1) - _count_x; _c--)
				{
					if(_ind_x == _c)
						is_true = true;
				}
				if(is_true)
				{
					short _count_temp = 0;
					for(short _c = _x - 1; _c > (_x - 1) - _count_x; _c--)
					{
						if(_pv.GetGex(_c, _ind_y).value == 1)
							_count_temp++;
						_index_x = _c;
					}
					if(_count_x == _count_temp)
						is_x = true;

					break;
				}
				_count_x = 0;
			}
		}
	}
	for(short _y = 0; _y < HEIGHT_POLE + 1; _y++)
	{
		if(_p.GetGex(_ind_x ,_y).active == true)
		{
			_count_y++;
		}
		else
		{
			if(_count_y > 0)
			{
				bool is_true = false;
				for(short _c = _y - 1; _c > (_y - 1) - _count_y; _c--)
				{
					if(_ind_y == _c)
						is_true = true;
				}
				if(is_true)
				{
					short _count_temp = 0;
					for(short _c = _y - 1; _c > (_y - 1) - _count_y; _c--)
					{
						if(_pv.GetGex(_ind_x, _c).value == 1)
							_count_temp++;
						_index_y = _c;
					}
					if(_count_y == _count_temp)
						is_y = true;

					break;
				}
				_count_y = 0;
			}
		}
	}
	if((_count_x > _count_y) && (is_x == true) && (is_y == true))
	{
		for(short _c = _index_x; _c < _count_x + _index_x; _c++)
		{
			Gex _g = _pv.GetGex(_c, _ind_y);
			_g.value = 4;
			_pv.SetGex(_c, _ind_y, _g);
		}
		for(short _c = _index_y; _c < _count_y + _index_y; _c++)
		{
			Gex _g = _pv.GetGex(_ind_x, _c);
			_g.value = 4;
			_pv.SetGex(_ind_x, _c, _g);
		}

		return true;
	}
	else if((_count_x < _count_y) && (is_x == true) && (is_y == true))
	{
		for(short _c = _index_x; _c < _count_x + _index_x; _c++)
		{
			Gex _g = _pv.GetGex(_c, _ind_y);
			_g.value = 4;
			_pv.SetGex(_c, _ind_y, _g);
		}
		for(short _c = _index_y; _c < _count_y + _index_y; _c++)
		{
			Gex _g = _pv.GetGex(_ind_x, _c);
			_g.value = 4;
			_pv.SetGex(_ind_x, _c, _g);
		}

		return true;
	}
	else if((_count_x == _count_y) && (is_x == true) && (is_y == true))
	{
		for(short _c = _index_x; _c < _count_x + _index_x; _c++)
		{
			Gex _g = _pv.GetGex(_c, _ind_y);
			_g.value = 4;
			_pv.SetGex(_c, _ind_y, _g);
		}
		for(short _c = _index_y; _c < _count_y + _index_y; _c++)
		{
			Gex _g = _pv.GetGex(_ind_x, _c);
			_g.value = 4;
			_pv.SetGex(_ind_x, _c, _g);
		}

		return true;
	}
	else
	{
		return false;
	}
}

bool BattleGame::Shoot_Down_Player(Pole &enemy, Pole &enemyVisible, const short _x, const short _y)
{
	if(Intersect(_x, _y, enemy))
	{
		short wid = Painter::wid + 1;
		short hei = Painter::hei + 1;
		short index_x = 0;
		short index_y = 0;

		for(short j = 0; j < HEIGHT_POLE; j++)
		{	
			for(short i = 0; i < WIDTH_POLE; i++)
			{
				short x_e = enemy.GetGex(i, j).x;
				short y_e = enemy.GetGex(i, j).y;

				if((_x > x_e) && (_y > y_e) && (_x < x_e + wid) && (_y < y_e + hei))
				{
					index_x = i;
					index_y = j;
				}
			}
		}

		//выбор цвета гекса
		//проверка на соответствие поподания в цель
		if(enemy.GetGex(index_x, index_y).value == 3 && enemyVisible.GetGex(index_x, index_y).value != 1)
		{
			Gex _gex = enemyVisible.GetGex(index_x, index_y);
			_gex.value = 1;
			_gex.active = true;
			enemyVisible.SetGex(index_x, index_y, _gex);

			//проверка полного уничтожения коробля
			if(IsShuttleFullEnemy(enemy, enemyVisible, index_x, index_y))
				_count_destroy_enemy++;

			return true;
		}
		else if(enemy.GetGex(index_x, index_y).active == false)
		{
			Gex _gex = enemyVisible.GetGex(index_x, index_y);
			_gex.value = 2;
			enemyVisible.SetGex(index_x, index_y, _gex);
			return false;
		}
	}
	return true;
}

void BattleGame::SetVarible(const short _set_varible, const short _set_logick, Pole &_pv)
{
	Gex _g;
	switch (_set_logick)
	{
		case 0:
			_g = _pv.GetGex(previous_index_x - 1, previous_index_y);
			_g.value = _set_varible;
			_pv.SetGex(previous_index_x - 1, previous_index_y, _g);
			break;
		case 1:
			_g = _pv.GetGex(previous_index_x, previous_index_y - 1);
			_g.value = _set_varible;
			_pv.SetGex(previous_index_x, previous_index_y - 1, _g);
			break;
		case 2:
			_g = _pv.GetGex(previous_index_x + 1, previous_index_y);
			_g.value = _set_varible;
			_pv.SetGex(previous_index_x + 1, previous_index_y, _g);
			break;
		case 3:
			_g = _pv.GetGex(previous_index_x, previous_index_y + 1);
			_g.value = _set_varible;
			_pv.SetGex(previous_index_x, previous_index_y + 1, _g);
			break;
	}
}
bool BattleGame::Shoot_Down_Enemy(Pole &_player, Pole &_playerVisible, Gex &_g)
{
	//случайный выстрел по player
	if(!is_passive_enemy)
	{
		Gex _gex = Gex_obj_random(_playerVisible);
		short x_pos = ((_gex.x - _playerVisible.offset_x) / (Painter::wid + 1));
		short y_pos = ((_gex.y - _playerVisible.offset_y) / (Painter::hei + 1));

		if(_player.GetGex(x_pos, y_pos).value == 3)
		{
			_gex.value = 1;
			_playerVisible.SetGex(x_pos, y_pos, _gex);
			previous_index_x = x_pos;
			previous_index_y = y_pos;
			is_passive_enemy = true;
			_count_present = _present;
			_g = _playerVisible.GetGex(x_pos, y_pos);
			return true;
		}
		else if(_player.GetGex(x_pos, y_pos).value == 0)
		{
			_gex.value = 2;
			_playerVisible.SetGex(x_pos, y_pos, _gex);

			return false;
		}
	}
	else
		return NextTurn(_playerVisible, _player, _g); //не случайный выстрел по player
}

bool BattleGame::NextTurn(Pole &_pv, Pole &_p, Gex &_g)
{
	srand(time(0));
	short begin_x = 0;
	short begin_y = 0;
	short x_cout = 0;
	short y_cout = 0;
	
	for(short x = previous_index_x; x >= 0; x--)
	{
		if(_p.GetGex(x, previous_index_y).value != 3)
		{
			begin_x = (x + 1);
			break;
		}

	}
	for(short y = previous_index_y; y >= 0; y--)
	{
		if(_p.GetGex(previous_index_x, y).value != 3)
		{
			begin_y = (y + 1);
			break;
		}

	}
	char _ch = _p.GetGex(previous_index_x, previous_index_y)._ch;
	short x_lenth = _ch == 'w' ? _p.GetGex(previous_index_x, previous_index_y)._lentch : 1;
	short y_lenth = _ch == 'h' ? _p.GetGex(previous_index_x, previous_index_y)._lentch : 1;

	for(short x = begin_x; x < begin_x + x_lenth; x++)
	{
		if(_pv.GetGex(x, previous_index_y).value == 1)
			x_cout++;
	}
	for(short y = begin_y; y < begin_y + y_lenth; y++)
	{
		if(_pv.GetGex(previous_index_x, y).value == 1)
			y_cout++;
	}
	if(x_cout > y_cout)
	{
		if(DeadShip(_pv, _p))
		{
			is_passive_enemy = false;
			IntersectRect(begin_x, begin_y, _pv);
			_count_destroy_player++;
			_g = _pv.GetGex(previous_index_x, previous_index_y);
			return true;
		}

		bool _flag = false;
		short _rand = rand() % 2;

		if(_pv.GetGex(previous_index_x - 1, previous_index_y).value != 1 &&
			_pv.GetGex(previous_index_x - 1, previous_index_y).value != 2 && _rand == 0)
		{
			_g = _pv.GetGex(previous_index_x - 1, previous_index_y);
			_flag = Set_value_shoot(_pv, _p, _rand, 'w');
		}
		else if(_pv.GetGex(previous_index_x + 1, previous_index_y).value != 1 &&
			_pv.GetGex(previous_index_x + 1, previous_index_y).value != 2 && _rand == 1)
		{
			_g = _pv.GetGex(previous_index_x + 1, previous_index_y);
			_flag = Set_value_shoot(_pv, _p, _rand, 'w');
		}
		else
		{
			for(short x = begin_x; x < begin_x + _pv.GetGex(x, previous_index_y)._lentch; x++)
			{
				if(_pv.GetGex(x, previous_index_y).value == 3)
				{
					Set_val(_pv, x, previous_index_y, 1);
					_g = _pv.GetGex(x, previous_index_y);
					_flag = true;
					break;
				}
				else
					_flag = false;
			}
		}
		return _flag;
	}
	else if(x_cout < y_cout)
	{
		if(DeadShip(_pv, _p))
		{
			is_passive_enemy = false;
			IntersectRect(begin_x, begin_y, _pv);
			_count_destroy_player++;
			_g = _pv.GetGex(previous_index_x, previous_index_y);
			return true;
		}

		bool _flag = false;
		short _rand = 0;

		if(_pv.GetGex(previous_index_x, previous_index_y - 1).value != 1 &&
			_pv.GetGex(previous_index_x, previous_index_y - 1).value != 2 && _rand == 0)
		{
			_g = _pv.GetGex(previous_index_x, previous_index_y - 1);
			_flag = Set_value_shoot(_pv, _p, _rand, 'h');
		}
		else if(_pv.GetGex(previous_index_x, previous_index_y + 1).value != 1 &&
			_pv.GetGex(previous_index_x, previous_index_y + 1).value != 2 && _rand == 1)
		{
			_g = _pv.GetGex(previous_index_x, previous_index_y + 1);
			_flag = Set_value_shoot(_pv, _p, _rand, 'h');
		}
		else
		{
			for(short y = begin_y; y < begin_y + _pv.GetGex(previous_index_x, y)._lentch; y++)
			{
				if(_pv.GetGex(previous_index_x, y).value == 3)
				{
					Set_val(_pv, previous_index_x, y, 1);
					_g = _pv.GetGex(previous_index_x, y);
					_flag = true;
					break;
				}
				else
					_flag = false;
			}
		}
		return _flag;
	}
	else
	{
		if(DeadShip(_pv, _p))
		{
			is_passive_enemy = false;
			IntersectRect(previous_index_x, previous_index_y, _pv);
			_count_destroy_player++;
			return true;
		}

		short _rand = 0;
		bool _flag = false;
		bool _isok = false;

		while(!_flag)
		{
			_rand = rand() % 4;
			_flag = Set_value_shoot(_pv, _rand, _isok);
		}
		return _isok;
	}
}
void BattleGame::Set_val(Pole &_pv, const short _x, const short _y, const short _val)
{
	Gex _gex = _pv.GetGex(_x, _y);
	_gex.value = _val;
	_pv.SetGex(_x, _y, _gex);
}
bool BattleGame::Set_value_shoot(Pole &_pv, const short _r, bool &_isok)
{
	Gex _gex;
	bool is_ok = false;
	switch(_r)
	{
		case 0:
			if(previous_index_x - 1 >= 0 && _pv.GetGex(previous_index_x - 1, previous_index_y).value != 1
				&& _pv.GetGex(previous_index_x - 1, previous_index_y).value != 2)
			{
				if(_pv.GetGex(previous_index_x - 1, previous_index_y).value == 3)
				{
					_gex = _pv.GetGex(previous_index_x - 1, previous_index_y);
					_gex.value = 1;
					_pv.SetGex(previous_index_x - 1, previous_index_y, _gex);
					is_ok = true;
					_isok = true;
					previous_index_x -= 1;
				}
				else
				{
					_gex = _pv.GetGex(previous_index_x - 1, previous_index_y);
					_gex.value = 2;
					_pv.SetGex(previous_index_x - 1, previous_index_y, _gex);
					is_ok = true;
					_isok = false;
				}
			}
			else
			{
				_isok = false;
				is_ok = false;
			}
			break;
		case 1:
			if(previous_index_y - 1 >= 0 && _pv.GetGex(previous_index_x, previous_index_y - 1).value != 1
				&& _pv.GetGex(previous_index_x, previous_index_y - 1).value != 2)
			{
				if(_pv.GetGex(previous_index_x, previous_index_y - 1).value == 3)
				{
					_gex = _pv.GetGex(previous_index_x, previous_index_y - 1);
					_gex.value = 1;
					_pv.SetGex(previous_index_x, previous_index_y - 1, _gex);
					is_ok = true;
					_isok = true;
					previous_index_y -= 1;
				}
				else
				{
					_gex = _pv.GetGex(previous_index_x, previous_index_y - 1);
					_gex.value = 2;
					_pv.SetGex(previous_index_x, previous_index_y - 1, _gex);
					is_ok = true;
					_isok = false;
				}
			}
			else
			{
				_isok = false;
				is_ok = false;
			}
			break;
		case 2:
			if(previous_index_x + 1 < WIDTH_POLE && _pv.GetGex(previous_index_x + 1, previous_index_y).value != 1
				&& _pv.GetGex(previous_index_x + 1, previous_index_y).value != 2)
			{
				if(_pv.GetGex(previous_index_x + 1, previous_index_y).value == 3)
				{
					_gex = _pv.GetGex(previous_index_x + 1, previous_index_y);
					_gex.value = 1;
					_pv.SetGex(previous_index_x + 1, previous_index_y, _gex);
					is_ok = true;
					_isok = true;
					previous_index_x + 1;
				}
				else
				{
					_gex = _pv.GetGex(previous_index_x + 1, previous_index_y);
					_gex.value = 2;
					_pv.SetGex(previous_index_x + 1, previous_index_y, _gex);
					is_ok = true;
					_isok = false;
				}
			}
			else
			{
				_isok = false;
				is_ok = false;
			}
			break;
		case 3:
			if(previous_index_y + 1 < HEIGHT_POLE && _pv.GetGex(previous_index_x, previous_index_y + 1).value != 1
				&& _pv.GetGex(previous_index_x, previous_index_y + 1).value != 2)
			{
				if(_pv.GetGex(previous_index_x, previous_index_y + 1).value == 3)
				{
					_gex = _pv.GetGex(previous_index_x, previous_index_y + 1);
					_gex.value = 1;
					_pv.SetGex(previous_index_x, previous_index_y + 1, _gex);
					is_ok = true;
					_isok = true;
					previous_index_y += 1;
				}
				else
				{
					_gex = _pv.GetGex(previous_index_x, previous_index_y + 1);
					_gex.value = 2;
					_pv.SetGex(previous_index_x, previous_index_y + 1, _gex);
					is_ok = true;
					_isok = false;
				}
			}
			else
			{
				_isok = false;
				is_ok = false;
			}
			break;
	}
	return is_ok;
}

bool BattleGame::Set_value_shoot(Pole &_pv, Pole &_p, const short _r, const char _ch)
{
	Gex _gex;

	if(_r == 0 && _ch == 'w')
	{
		if(_pv.GetGex(previous_index_x - 1, previous_index_y).value == 3)
		{
			_gex = _pv.GetGex(previous_index_x - 1, previous_index_y);
			_gex.value = 1;
			_pv.SetGex(previous_index_x - 1, previous_index_y, _gex);
			previous_index_x -= 1;
			return true;
		}
		else if(_pv.GetGex(previous_index_x - 1, previous_index_y).value == 0)
		{
			_gex = _pv.GetGex(previous_index_x - 1, previous_index_y);
			_gex.value = 2;
			_pv.SetGex(previous_index_x - 1, previous_index_y, _gex);
			return false;
		}
		else
			return false;
	}
	else if(_r == 1 && _ch == 'w')
	{
		if(_pv.GetGex(previous_index_x + 1, previous_index_y).value == 3)
		{
			_gex = _pv.GetGex(previous_index_x + 1, previous_index_y);
			_gex.value = 1;
			_pv.SetGex(previous_index_x + 1, previous_index_y, _gex);
			previous_index_x += 1;
			return true;
		}
		else if(_pv.GetGex(previous_index_x + 1, previous_index_y).value == 0)
		{
			_gex = _pv.GetGex(previous_index_x + 1, previous_index_y);
			_gex.value = 2;
			_pv.SetGex(previous_index_x + 1, previous_index_y, _gex);
			return false;
		}
		else
			return false;
	}
	else if(_r == 0 && _ch == 'h')
	{
		if(_pv.GetGex(previous_index_x, previous_index_y - 1).value == 3)
		{
			_gex = _pv.GetGex(previous_index_x, previous_index_y - 1);
			_gex.value = 1;
			_pv.SetGex(previous_index_x, previous_index_y - 1, _gex);
			previous_index_y -= 1;
			return true;
		}
		else if(_pv.GetGex(previous_index_x, previous_index_y - 1).value == 0)
		{
			_gex = _pv.GetGex(previous_index_x, previous_index_y - 1);
			_gex.value = 2;
			_pv.SetGex(previous_index_x, previous_index_y - 1, _gex);
			return false;
		}
		else
			return false;
	}
	else if(_r == 1 && _ch == 'h')
	{
		if(_pv.GetGex(previous_index_x, previous_index_y + 1).value == 3)
		{
			_gex = _pv.GetGex(previous_index_x, previous_index_y + 1);
			_gex.value = 1;
			_pv.SetGex(previous_index_x, previous_index_y + 1, _gex);
			previous_index_y += 1;
			return true;
		}
		else if(_pv.GetGex(previous_index_x, previous_index_y + 1).value == 0)
		{
			_gex = _pv.GetGex(previous_index_x, previous_index_y + 1);
			_gex.value = 2;
			_pv.SetGex(previous_index_x, previous_index_y + 1, _gex);
			return false;
		}
		else
			return false;
	}
}
bool BattleGame::DeadShip(Pole &_pv, Pole &_p)
{
	short begin_x = 0;
	short begin_y = 0;
	short end_number_x = 0;
	short end_number_y = 0;
	short active_x = 0;
	short active_y = 0;

	for(short x = previous_index_x; x > previous_index_x - 5; x--)
	{
		if(_p.GetGex(x, previous_index_y).value == 3)
		{

		}
		else
		{
			begin_x = x + 1;
			break;
		}
	}
	for(short y = previous_index_y; y > previous_index_y - 5; y--)
	{
		if(_p.GetGex(previous_index_x, y).value == 3)
		{

		}
		else
		{
			begin_y = y + 1;
			break;
		}
	}

	char _ch = _p.GetGex(previous_index_x, previous_index_y)._ch;
	short x_lenth = _ch == 'w' ? _p.GetGex(previous_index_x, previous_index_y)._lentch : 1;
	short y_lenth = _ch == 'h' ? _p.GetGex(previous_index_x, previous_index_y)._lentch : 1;

	for(short x = begin_x; x < begin_x + x_lenth; x++)
	{
		if(_p.GetGex(x, previous_index_y).value == 3)
		{
			end_number_x++;
		}
	}
	for(short y = begin_y; y < begin_y + y_lenth; y++)
	{
		if(_p.GetGex(previous_index_x, y).value == 3)
		{
			end_number_y++;
		}
	}
	for(short x = begin_x; x < begin_x + x_lenth; x++)
	{
		if(_pv.GetGex(x, previous_index_y).value == 1)
		{
			active_x++;
		}
	}
	for(short y = begin_y; y < begin_y + y_lenth; y++)
	{
		if(_pv.GetGex(previous_index_x, y).value == 1)
		{
			active_y++;
		}
	}
	
	if(end_number_x == active_x && end_number_y == active_y)
		return true;

	return false;
}

void BattleGame::IntersectRect(const short _ind_x, const short _ind_y, Pole &_pv)
{
	Gex gex_b = _pv.GetGex(_ind_x, _ind_y);

	MyRect _rect;

	short _bx = (gex_b.x) - (Painter::wid + 1);
	short _by = (gex_b.y) - (Painter::hei + 1);

	if(gex_b._ch == 'w')
		_rect = MyRect(_bx, _by, _bx + ((Painter::wid + 1) * (gex_b._lentch + 2)), _by + ((Painter::hei + 1) * 3));
	else
		_rect = MyRect(_bx, _by, _bx + ((Painter::wid + 1) * 3), _by + ((Painter::hei + 1) * (gex_b._lentch + 2)));
	
	for(short j = 0; j < HEIGHT_POLE; j++)
	{
		for(short i = 0; i < WIDTH_POLE; i++)
		{
			short p_x = _pv.GetGex(i, j).x + (Painter::wid / 2);
			short p_y = _pv.GetGex(i, j).y + (Painter::hei / 2);

			if((_rect.Right() > p_x) && (_rect.Left() < p_x) && 
				(_rect.Bottom() > p_y) && (_rect.Top() < p_y))
			{
				if(_pv.GetGex(i, j).value != 1 && _pv.GetGex(i, j).value != 3)
				{
					Gex _gex = _pv.GetGex(i, j);
					_gex.value = 2;
					_pv.SetGex(i, j, _gex);
				}
			}
		}
	}
}
Gex BattleGame::Gex_obj_random(Pole &_pv)
{
	Gex _gex;
	std::vector<Gex> _array;
	//std::vector<Gex> _passive;
	for(short y = 0; y < HEIGHT_POLE; y++)
	{
		for(short x = 0; x < WIDTH_POLE; x++)
		{
			if(_pv.GetGex(x, y).value == 0 || _pv.GetGex(x, y).value == 3)
				_array.push_back(_pv.GetGex(x, y));
			//else if(_pv.GetGex(x, y).value == 3)
			//	_passive.push_back(_pv.GetGex(x, y));
		}
	}

	srand(time(0));

	if(_array.size() > 0)
	{
		short _rand = rand() % _array.size();
		_gex = _array[_rand];
		_count_present = _present;
	}

	_array.clear();
	//_passive.clear();
	return _gex;
}
Gex BattleGame::Get_Object(Pole &_p, const short _x, const short _y) const
{
	short st_x = _p.offset_x;
	short st_y = _p.offset_y;

	for(short y = 0; y < HEIGHT_POLE; y++)
	{
		for(short x = 0; x < WIDTH_POLE; x++)
		{
			Gex _gex = _p.GetGex(x, y);

			if((_gex.x < _x) && (_gex.y < _y) && (_gex.x + (Painter::wid + 1) > _x) && (_gex.y + (Painter::hei + 1) > _y))
			{
				return _gex;
			}
		}
	}
}