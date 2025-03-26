#pragma once
#include "VovanLib.h"
#include "Maps.h"
#include <ctime>

struct AIBuffer
{
	int cur_ind;
	int end_ind;

	bool isActive;
	bool isCapture;
	bool turn_AI;

	bool range_one;

	vovan::Vector2f endP;
	
	AIBuffer() : isActive(false), isCapture(true), cur_ind(0), end_ind(1), endP(), turn_AI(false), range_one(true) {}
	void Reset()
	{
		cur_ind = 0;
		end_ind = 1;
		isActive = false;
		isCapture = true;
		turn_AI = false;
		range_one = true;
		endP = vovan::Vector2f();
	}
};

class AI
{
private:
	std::vector<int> rang1;
	std::vector<int> rang2;

	AIBuffer buf;

	bool isClear(Maps &_map, const int _ind, int &_index);
	bool isCap(Maps &_map, const int _ind, int &_count);
	void InitRang(Maps &_map);
	void InitRangTurn(Maps &_map);
	void SetRang(Maps &_map, const int _ind);
	vovan::Recti RectR1(const int _ind, Maps &_map);
	vovan::Recti RectR2(const int _ind, Maps &_map);
public:
	AI(void);
	~AI(void);

	void Update(Maps &_map, Shapes &_fig);

	AIBuffer &GetBufferAI() { return buf; }
};

