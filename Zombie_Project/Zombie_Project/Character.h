#pragma once
#include "Pole.h"
#include "Animation.h"
#include "Painter.h"
#include "ScrollBar.h"
#include "BulletA.h"
#include <vector>

#define ui unsigned int

class Bullet
{
protected:
	bool isHorisontal;
	float x, y;
	int damage;
	int speed;
	float rad;
	TupeBullet tupe;
public:
	Bullet(void) : x(0), y(0), speed(5), rad(15.5), tupe(BULLET), isHorisontal(true) {};
	Bullet(const float _x, const float _y, const float _rad, const int _speed, const int _damage, const TupeBullet _tupe)
			: x(_x), y(_y), rad(_rad), speed(_speed), damage(_damage), tupe(_tupe), isHorisontal(true) {};
	~Bullet(void) {};

	void Set_X_Y(const float _x, const float _y) { x += _x, y += _y; }
	void SetDirection(const bool _horisontal) { isHorisontal = _horisontal; }
	void X_Y(const int _x, const int _y) { x = _x, y = _y; }
	bool GetHorizontal() const { return isHorisontal; }
	int GetDamage() { return damage; }
	int Get_X() { return x; }
	int Get_Y() { return y; }
	int Get_Speed() { return speed; }
	TupeBullet Tupe() { return tupe; }

	void DrawBullet(const Painter &_p) const;
};

class Character
{
private:
	float x_cout;
	float frame;
	float frameT;
	int speed;
	int x_vertical_bull;
	int counter;
	int c_shoot;
	int live, baseLive;
	int cost_gold;
	bool isShoot;
	bool isBullAnim;
	ui texture[2];
	ui texture_bull_a;
	ScrollBar bar_live;
	Bullet bullet;
	BulletA b_anim;
	Animation anim;
	std::vector<Bullet> bullets;
	std::vector<BulletA> bullet_anim;
public:
	Character(void);
	Character(const float _frameT, const float _frame, const int _speed, const int _live, const TupeBullet _bull, const ui _texture_1,
		const ui _texture_2, const int _gold_cost);
	~Character(void);

	void DrawPassive(Painter &_p) const;
	void DrawShoot(Painter &_p) const;
	void Update();
	void InitPositiion(const int _x, const int _y);
	void SetShoot(const bool _shoot) { isShoot = _shoot; }
	void SetOffset(const int _x) { anim.Offset(_x); }
	void SetDamage(const int _damage);
	void DelBullet(const short _del);
	void DelBulletA(const short _del);
	void SetAnimColor(Color &_c) { anim.SetColor(_c); }
	void SetBullAnim(const bool _isA) { isBullAnim = _isA; }
	void LoadTextureBullA(const ui _texture) { texture_bull_a = _texture; }
	void SetBulletPosition(const int _x);
	bool GetBullAnim() { return isBullAnim; }
	int GetSpeed() { return speed; }
	int GetLive() { return live; }
	int GetCostGold() { return cost_gold; }
	float GetFrame() { return frame; }
	Animation GetAnim() { return anim; }
	ui Texture_1() { return texture[0]; }
	Bullet GetBulletTupe() { return bullet; }
	std::vector<Bullet> GetBullet() { return bullets; }
	std::vector<BulletA> GetBulletA() { return bullet_anim; }
};
