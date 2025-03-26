#include "AnimateExplode.h"

AnimateExplode::AnimateExplode(void)
{
}

AnimateExplode::~AnimateExplode(void)
{
}

void AnimateExplode::Initialize(const Animation &_anim, const BulletA &_bull_a, unsigned int _texture)
{
	anim = _anim;
	bull = _bull_a;
	texture = _texture;
}

void AnimateExplode::Draw(Painter &_p) const
{
	for(int i = 0; i < bullets.size(); i++)
	{
		bullets[i].DrawBulletAnim(_p, texture);
	}
}

void AnimateExplode::Update()
{
	for(int i = 0; i < bullets.size(); i++)
	{
		bullets[i].UpdateBulletAnim();

		if(!bullets[i].GetOnly())
			bullets.erase(bullets.begin() + i);
	}
}

void AnimateExplode::AddEffect(const int _x, const int _y)
{
	bull.SetAnim(Animation(_x, _y, anim.Width(), anim.Height()));
	bullets.push_back(bull);
}
