#pragma once
#include <gl/glut.h>
#include <ctime>
#include "Shapes.h"
#include "Maps.h"
#include "AI.h"

struct Buffer
{
	int cur_ind;
	int end_ind;
	bool turnPlayer;
	bool varible;
	bool isActive;
	bool updateOk;
	vovan::Vector2f endP;

	Buffer() : cur_ind(0), isActive(false), turnPlayer(true), varible(false), updateOk(false), endP(), end_ind(0)
	{

	}

	void BuffReset()
	{
		cur_ind = -1;
		end_ind = -1;
		turnPlayer = true;
		varible = false;
		isActive = false;
		updateOk = false;
		endP = vovan::Vector2f();
	}
};

class ManagerEditor
{
private:
	Maps map;
	AI ai;
	Buffer buf;
	Shapes figure;
	vovan::Recti tRect;
	vovan::Recti tRect2;
	void VarRect();
	vovan::Recti RectR1(const int _ind);
public:
	ManagerEditor(void);
	~ManagerEditor(void);

	void Render();
	void Init();
	void Mouse(int _b, int _s, int _x, int _y);
	void PassMouse(int _x, int _y);
	void Update();
};