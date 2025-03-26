#include "Player.h"
#include <iostream>

Player::Player(void) : speed(5.5f), dir(0)
{
}


Player::~Player(void)
{
}

void Player::SetPos(const Translation &_pos)
{
	pos = _pos;
}

void Player::SetDir(const float _newDir, const float _speed)
{
	float temp = _newDir + _speed;

	dir = temp;
}

void Player::Keyboard(unsigned char _button, int _x, int _y)
{
	const float rad = (dir + 90) * (3.14159 / 180);

	switch(_button)
	{
	case 'd':
		SetDir(dir, speed);
		//std::cout << "position : x = " << pos.x << " dir = " << dir << std::endl;
		break;
	case 'a':
		SetDir(dir, -speed);
		//std::cout << "position : x = " << pos.x << " dir = " << dir << std::endl;
		break;
	case 'w':
		pos.z += sin(rad) * speed;
		pos.x += cos(rad) * speed;
		//std::cout << "position : z = " << pos.z << " dir = " << dir <<  std::endl;
		break;
	case 's':
		pos.z -= sin(rad) * speed;
		pos.x -= cos(rad) * speed;
		//std::cout << "position : z = " << pos.z << " dir = " << dir <<  std::endl;
		break;
	}
}