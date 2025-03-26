#pragma once
#include "Pole.h"
#include "Character.h"
#include "EngineRect.h"
#include "AnimateExplode.h"
#include "GoldIn.h"

#define INTER_C 5
#define E_COUNT 5

class LogGame
{
private:
	Character inter_character[INTER_C];
	Character inter_char_enemy[E_COUNT];
	EngineRect rect[INTER_C];
	EngineRect mouse_rect;
	Pole pole;
	BulletA bull_tupe_explode;
	BulletA bull_tupe_explode_02;
	AnimateExplode a_explode;
	GoldIn golds;
	std::vector<BulletA> list_explode;
	std::vector<BulletA> list_explode_02;
	std::vector<Character> list_char;
	std::vector<Character> list_char_enemy;
	ui texture_explode;
	ui texture_explode_02;

	short cur_ind;
	short curr_enemy;
	short resp_enemy;
	short resp_add_enemy;
	short resp_end;
	short resp_curr;
	short resp_begin;
	short resp_time;
	short gold;
	short lives;
	short enemy_die;
	short mouse_y;
	bool isDown;
	bool isStopGame;
	bool imposible[COUNT_W][COUNT_H];
	const short lives_level;
	const short enemy_end;
public:
	LogGame(void);
	~LogGame(void);

	void Draw(Painter &_p);
	void Update();
	void Mouse(int _button, int _state, int _x, int _y);
	void PassMouse(const int _x, const int _y);
	void ActiveMouse(int _x, int _y);
	void Initialize(ui _texture[]);
	void ColisionEnemy();
	void SetCharacterEnemy(Character _character, const short _ind);
	void ShowString(const char *_string, const int _x, const int _y);
	void ShowString(int _intejer, const int _x, const int _y);
	void IsShootChar();
	Character GetCharacter(const short _ind) { return inter_char_enemy[_ind]; }
};
