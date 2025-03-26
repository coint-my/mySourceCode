#pragma once
#include "Mesh.h"

struct P_Keys
{
	int key[9];

	P_Keys()
	{
		for (int i = 0; i < 9; i++)
			key[i] = 0;
	}
	~P_Keys(){}
};

class Player : public Mesh
{
private:
	float speed;
	P_Keys keys;
public:
	Player(void);
	~Player(void);

	void show_keys() const;

	inline void draw() const;
	inline void offset(const float _x, const float _y) { pos.x += _x; pos.y += _y; }
	inline void add_keys(const int _key, const int _ind) { keys.key[_ind] += _key; }

	inline float get_speed() const { return speed; }
	inline const P_Keys& get_keys() const { return keys; }
};

