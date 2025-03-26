#pragma once
#include <ctime>
#include "Tile.h"
#include "Painter.h"

#define POLE 7
#define width 800
#define height 600

struct Point
{
	short x, y;
	bool isEmpty;
	Point(void) : x(-100), y(-100), isEmpty(true) {}
	Point(short _x, short _y) : x(_x), y(_y), isEmpty(false) {}
};

class GameManager
{
private:
	float oreol_x;
	float oreol_y;
	float oreol_x_act;
	float oreol_y_act;

	int t;
	int t2;
	int temp_t;
	int temp_count_l;
	int temp_count_r;
	int temp_count_h;

	bool oldState;

	Point cur_p;
	Point sec_p;
	Point logic;
	Painter paint;
	Tile cur_tile;
	Tile sec_tile;
	Tile tiles[POLE][POLE];

public:
	GameManager(void);
	~GameManager(void);

	void InitPole();
	void Draw();
	void PassiveMouse(int _x, int _y);
	void ActiveMouse(int _x, int _y);
	void Mouse(int button, int state, int x, int y);
	void Timer();
};

