#pragma once
#include "Painter.h"
#include <cstdlib>
#include <ctime>
#include "MyRect.h"
#define WIDTH_POLE 10
#define HEIGHT_POLE 10

struct Gex
{
	short x, y;
	short value;
	short _lentch;
	bool active;
	char _ch;
};

class Pole
{
private:
	Gex gex[WIDTH_POLE][HEIGHT_POLE];
	void Intersect(const short _x, const short _y, const short _lenth, const short _rot);
	void RandomShutle(const short _num_shutle, const short _lenth_shutle);
	short count_timer;
public:
	short offset_x, offset_y;
	short count_shuttle;

	Pole(void);
	Pole(const short _x_start, const short _y_start);
	~Pole(void);

	void Initialize();
	void InitRandom();
	void Draw(Painter &_p, const unsigned int *_texture) const;
	void Draw(Painter &_p, const float _color_val) const;

	short GetCountShuttle() { return count_shuttle; }

	Gex GetGex(const short _x, const short _y) { return gex[_x][_y]; }
	void SetGex(const short _x, const short _y, Gex _gex) { gex[_x][_y] = _gex; }
};
