#pragma once
#include "table.h"
#include "player.h"
#include "enemy.h"
#include "Menu.h"

typedef enum {PL1, PL2, GAME_OVER, GAME_MENU};

typedef struct gameBuffer
{
	int round;
	bool isActive;
	int count;

	gameBuffer() : round(GAME_MENU), isActive(true), count(0) {}

}game_buff;

class manager_game
{
//меню
private: Menu men;
//состояние меню
private: int state_menu;
//счетчик секунд
private: int _counter;
//игровой стол
private: table *_table;
//класс противника
private: enemy* _enemy;
private: enemy* _enemy2;
//класс игрока
private: entity *_player;
private: entity *_player2;
//класс игрока родитель
private: player * _p;
private: player * _p2;

//буфер игрового процесса
private: gameBuffer g_buff;

//общее рисование
public: void render();
//апдейт здесь надо писать код
public: void update();
//обновление секунд
public: void up_second();
//управление мышкой
public: void mouse(int b, int s, int x, int y);
//управление перемищение мышки
public: void m_mouse(int x, int y);
//подсчет очков
private: void calculate_score(const int _tupe_figure, int& score);
//устанавливает игроков игрок и противник
private: void player_vs_comp();
//устанавливает игроков игрок и игрок
private: void player_vs_player();
//устанавливает игроков противник и противник
private: void comp_vs_comp();

private: bool Is_OVER();

public:
	manager_game();
	~manager_game();
};

