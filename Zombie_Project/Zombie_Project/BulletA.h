#pragma once
#include "Painter.h"
#include "Animation.h"

class BulletA
{
private:
	bool isOnly;
	int counter;
	int frame_t;
	int frame;
	int damage;
	float x_count;
	float speed;
	Animation anim;
	TupeBullet tupe;
public:
	BulletA(void);
	BulletA(const int _frame_t, const int _frame, const float _speed, const int _damage, bool _only, TupeBullet _tupe);
	~BulletA(void);

	void DrawBulletAnim(Painter &_p, unsigned int _texture) const;
	void UpdateBulletAnim();
	void SetOffset(const int _x) { anim.Offset(_x); }
	void SetAnim(Animation &_anim) { anim = _anim; }

	bool GetOnly() { return isOnly; }
	int GetDamage() { return damage;}
	float GetSpeed() { return speed; }
	TupeBullet GetTupe() { return tupe; }
	Animation GetAnim() { return anim; }
};
