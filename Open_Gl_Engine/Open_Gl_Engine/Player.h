#pragma once
#include "EngineBase.h"

class Player
{
private:
	float dir;
	const float speed;
	Translation pos;
public:
	Player(void);
	~Player(void);

	Translation GetPos() const { return pos; } //позиция игрока

	float GetRot() const { return dir; } //взять направление

	void SetPos(const Translation &_pos); //задать позицию
	void SetDir(const float _newDir, const float _speed); //задать направление

	void Keyboard(unsigned char _button, int _x, int _y); //обработка клавиш
};

