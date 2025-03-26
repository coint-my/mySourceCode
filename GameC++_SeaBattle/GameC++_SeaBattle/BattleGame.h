#pragma once
#include "Pole.h"
#include <vector>

class BattleGame
{
private:
	bool is_passive_enemy;
	bool is_turn_false;
	short previous_index_x;
	short previous_index_y;
	short _count_destroy_enemy;
	short _count_destroy_player;
	short _present;
	short _count_present;
	short _end_present;
	bool Intersect(const short _x, const short _y, Pole &_p);
	bool IsShuttleFullEnemy(Pole &_p, Pole &_pv ,const short _ind_x, const short _ind_y);
	bool NextTurn(Pole &pv, Pole &_p, Gex &_g);
	bool DeadShip(Pole &_pv, Pole & _p);
	void IntersectRect(const short _ind_x, const short _ind_y, Pole &_pv);
	Gex Gex_obj_random(Pole & _pv);
	bool Set_value_shoot(Pole &_pv, const short _r, bool &_isok);
	bool Set_value_shoot(Pole &_pv, Pole &_p, const short _r, const char _ch);
	void Set_val(Pole &_pv, const short _x, const short _y, const short _val);
public:
	BattleGame(void);
	~BattleGame(void);

	bool Shoot_Down_Player(Pole &enemy, Pole &enemyVisible, const short _x, const short _y);
	bool Shoot_Down_Enemy(Pole &_player, Pole &_playerVisible, Gex &_g);
	void SetVarible(const short _set_varible, const short _set_logick, Pole &_pv);
	void SetString(char *_ch, short _x, short _y);
	void SetStringLinear(char _ch, short _x, short _y);
	short GetCountEnemy() const { return _count_destroy_enemy; }
	short GetCountPlayer() const { return _count_destroy_player; }
	Gex Get_Object(Pole &_p, const short _x, const short _y) const;
};
