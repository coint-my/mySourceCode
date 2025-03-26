#pragma once
#include <vector>
#include "BulletA.h"

class AnimateExplode
{
private:
	unsigned int texture;
	std::vector<BulletA> bullets;
	Animation anim;
	BulletA bull;
public:
	AnimateExplode(void);
	~AnimateExplode(void);

	//--------------------

	void Initialize(const Animation &_anim, const BulletA &_bull_a, unsigned int _texture);
	void Draw(Painter &_p) const;
	void Update();
	void AddEffect(const int _x, const int _y);

	std::vector<BulletA> GetBullet() { return bullets; }
};
