#include "BulletA.h"

BulletA::BulletA(void)
{
}

BulletA::BulletA(const int _frame_t, const int _frame, const float _speed, const int _damage, bool _only, TupeBullet _tupe)
{
	frame_t = _frame_t;
	frame = _frame;
	x_count = 1.0 / frame;
	speed = _speed;
	counter = 0;
	damage = _damage;
	tupe = _tupe;
	anim = Animation(0, 0, 30, 30);
	isOnly = _only;
}

BulletA::~BulletA(void)
{
}

void BulletA::DrawBulletAnim(Painter &_p, unsigned int _texture) const
{
	_p.DrawRectTexture(anim.Get_X(), anim.Get_Y(), anim.Width(), anim.Height(), x_count, 1.0, x_count - (1.0 / frame), 0.0, _texture);
}

void BulletA::UpdateBulletAnim()
{
	counter++;

	if(counter > frame_t)
	{
		counter = 0;

		if(x_count > 1.0)
		{
			x_count = 1.0 / frame;
			isOnly = false;
		}

		x_count += 1.0 / frame;
	}
}
