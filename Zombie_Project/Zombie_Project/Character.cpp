#include "Character.h"

Character::Character(void)
{

}

Character::Character(const float _frameT, const float _frame, const int _speed, const int _live, const TupeBullet _bull,
					 const ui _texture_1, const ui _texture_2, const int _gold_cost)
{
	anim = Animation(WIDTH - 20, 1 * (HEIGHT / COUNT_H) + 10, WIDTH / COUNT_W, HEIGHT / COUNT_H);
	x_cout = 0;
	c_shoot = 0;
	counter = 0;
	frameT = _frameT;
	frame = _frame;
	speed = _speed;
	live = baseLive = _live;
	cost_gold = _gold_cost;
	isBullAnim = false;
	switch(_bull)
	{
	case BULLET:
		bullet = Bullet(anim.Get_X() + 85, anim.Get_Y() + 35, 7.3f, frameT * frame, 10, _bull);
		break;
	case BULLET_FREZE:
		bullet = Bullet(anim.Get_X() + 85, anim.Get_Y() + 35, 11.3f, frameT * frame + 3, 16, _bull);
		break;
	case BULLET_BOMB:
		b_anim = BulletA(2, 10, 11, 21, false, BULLET_BOMB);
		break;
	case BULLET_BOMB_2X:
		bullet = Bullet(0, 0, 14.6f, frameT * frame + 7, 25, _bull);
		bullet.SetDirection(false);
		break;
	case BULLET_FIRE:
		break;
	case MINE:
		bullet = Bullet(0, 0, 0, 0, 0, _bull);
		break;
	}
	texture[0] = _texture_1;
	texture[1] = _texture_2;
	isShoot = false;
	bar_live.Set_Wid(WIDTH / COUNT_W);
}

Character::~Character(void)
{

}

void Character::DrawPassive(Painter &_p) const
{
	if(!isShoot)
		anim.Draw(_p, texture[0], -(1.0 / frame) + x_cout, x_cout);

	if(isBullAnim)
	{
		for(int i = 0; i < bullet_anim.size(); i++)
		{
			bullet_anim[i].DrawBulletAnim(_p, texture_bull_a);
		}
	}
	else if(bullet.GetHorizontal())
	{
		for(int i = 0; i < bullets.size(); i++)
		{
			bullets[i].DrawBullet(_p);
		}
	}
	else if(!bullet.GetHorizontal())
	{
		for(int i = 0; i < bullets.size(); i++)
		{
			bullets[i].DrawBullet(_p);
		}
	}

	bar_live.DrawBar(_p);
}

void Character::DrawShoot(Painter &_p) const
{
	if(isShoot)
		anim.Draw(_p, texture[1], -(1.0 / frame) + x_cout, x_cout);

	if(isBullAnim)
	{
		for(int i = 0; i < bullet_anim.size(); i++)
		{
			bullet_anim[i].DrawBulletAnim(_p, texture_bull_a);
		}
	}
	else if(bullet.GetHorizontal())
	{
		for(int i = 0; i < bullets.size(); i++)
		{
			bullets[i].DrawBullet(_p);
		}
	}
	else if(!bullet.GetHorizontal())
	{
		for(int i = 0; i < bullets.size(); i++)
		{
			bullets[i].DrawBullet(_p);
		}
	}

	bar_live.DrawBar(_p);
}

void Character::SetDamage(const int _damage)
{
	live -= _damage;

	if(live > 0)
		bar_live.Set_Wid((WIDTH / COUNT_W) * live / baseLive);
}

void Character::Update()
{
	if(counter > frameT)
	{
		x_cout += (1.0 / frame);

		c_shoot++;

		if(isShoot && c_shoot >= frame && bullet.Tupe() != MINE)
		{
			c_shoot = 0;
			if(isBullAnim)
			{
				Animation &_a = b_anim.GetAnim();
				_a.Initialize(anim.Get_X() + anim.Width() - 5, anim.Get_Y() + anim.Height() / 2 - 5);
				b_anim.SetAnim(_a);
				bullet_anim.push_back(b_anim);
			}
			else if(bullet.GetHorizontal())
			{
				bullet.X_Y(anim.Get_X() + anim.Width() - 5, anim.Get_Y() + anim.Height() / 2 - 5);
				bullets.push_back(bullet);
			}
			else if(!bullet.GetHorizontal())
			{
				bullet.X_Y(x_vertical_bull, 0);
				bullets.push_back(bullet);
			}
		}
		counter = 0;
	}

	if(isBullAnim && bullet.Tupe() != MINE)
	{
		for(int i = 0; i < bullet_anim.size(); i++)
		{
			bullet_anim[i].SetOffset(bullet_anim[i].GetSpeed());
			bullet_anim[i].UpdateBulletAnim();
		}

		if(bullet_anim.size() > 0)
		{
			for(int i = 0; i < bullet_anim.size(); i++)
			{
				if(bullet_anim[i].GetAnim().Get_X() > WIDTH)
					bullet_anim.erase(bullet_anim.begin() + i);
			}
		}
	}
	else if(bullet.GetHorizontal() && bullet.Tupe() != MINE)
	{
		for(int i = 0; i < bullets.size(); i++)
		{
			if(bullets[i].Tupe() == BULLET)
			{
				//int t = clock() / 120;
				float _angle = sin((360 / 3.1415) * counter) * 9;
				bullets[i].Set_X_Y(bullets[0].Get_Speed(), _angle);
			}
			else
				bullets[i].Set_X_Y(bullets[0].Get_Speed(), 0);
		}

		if(bullets.size() > 0)
		{
			for(int i = 0; i < bullets.size(); i++)
			{
				if(bullets[i].Get_X() > WIDTH)
					bullets.erase(bullets.begin() + i);
			}
		}
	}
	else if(!bullet.GetHorizontal() && bullet.Tupe() != MINE)
	{
		for(int i = 0; i < bullets.size(); i++)
		{
			bullets[i].Set_X_Y(0, bullets[i].Get_Speed());
		}

		if(bullets.size() > 0)
		{
			for(int i = 0; i < bullets.size(); i++)
			{
				if(bullets[i].Get_Y() > HEIGHT)
					bullets.erase(bullets.begin() + i);
			}
		}
	}
	anim.Offset(-speed);
	bar_live.Offset(anim.Get_X(), anim.Get_Y());
	counter++;
}

void Character::InitPositiion(const int _x, const int _y)
{
	anim = Animation(_x, _y, WIDTH / COUNT_W, HEIGHT / COUNT_H);
}

void Character::SetBulletPosition(const int _x)
{
	x_vertical_bull = _x;
}

void Bullet::DrawBullet(const Painter &_p) const
{
	Color &_c = Color();
	switch(tupe)
	{
	case BULLET:
		_c = Color(0.5f, 0.5f, 0.5f);
		_p.DrawCircle(x, y, rad, _c);
		break;
	case BULLET_FREZE:
		_c = Color(0.0f, 0.2f, 1.0f);
		_p.DrawCircle(x, y, rad, _c);
		break;
	case BULLET_BOMB_2X:
		_c = Color(0.8f, 0.8f, 0.0f);
		_p.DrawCircle(x, y, rad, _c);
		break;
	case BULLET_FIRE:
		break;
	}
}

void Character::DelBullet(const short _del)
{
	bullets.erase(bullets.begin() + _del);
}

void Character::DelBulletA(const short _del)
{
	bullet_anim.erase(bullet_anim.begin() + _del);
}
