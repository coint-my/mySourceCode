#pragma once
#include "Shapes.h"
#include "VovanLib.h"
using vovan::Vector2f;
using vovan::Color;

class Maps
{
private:
	std::vector<Shapes> map;
	std::vector<bool> imposible;
	std::vector<int> r1;
	Shapes baseShape;
	int cur_cursor;
public:
	Maps(void);
	~Maps(void);

	void Update();
	void InitMatrix(const int _wid, const int _hei);
	void SetImp(const int _ind, const bool _val) { imposible[_ind] = _val; }
	void SetCursor(const int _cursor) { cur_cursor = _cursor; }
	void SetR1R2(vovan::Recti &_r1, Color &_col);

	bool IsColor(const Color &_col, const Color &_col2);

	std::vector<Vector2f> &GetPoints(const int _ind) { return map.at(_ind).GetPoints(); }
	std::vector<Shapes> &GetMap() { return map; }
	std::vector<bool> GetImp() { return imposible; }
	std::vector<int> GetR1() { return r1; }
};

