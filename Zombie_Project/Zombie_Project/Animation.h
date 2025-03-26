#pragma once
#include "Painter.h"

enum TupeBullet
{
	BULLET,
	BULLET_FREZE,
	BULLET_BOMB,
	BULLET_BOMB_2X,
	BULLET_FIRE,
	MINE
};

class Animation
{
private:
	int x, y, wid, hei;
	Color color;
public:
	Animation(void);
	Animation(const int _x, const int _y, const int _wid, const int _hei) : x(_x), y(_y), wid(_wid), hei(_hei) {};
	~Animation(void);

	void Offset(const int _x);
	void Initialize(const int _x, const int _y);
	int Get_X() const { return x; }
	int Get_Y() const { return y; }
	int Width() const { return wid; }
	int Height() const { return hei; }
	void SetColor(Color &_col);

	void Draw(Painter &_p, const float _text_wid_s, const float _text_hei_s, const unsigned int _text,
				const float _text_wid_e, const float _text_hei_e) const;
	void Draw(Painter &_p, const unsigned int _text, const float _lenth_b, const float _lenth_e) const;
};
