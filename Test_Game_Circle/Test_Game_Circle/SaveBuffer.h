#pragma once
#include "Player.h"

struct SaveBuffer
{
	int count_trigger;
	int count_keys;

	Player player;

	SaveBuffer(void) : player(), count_trigger(0), count_keys(0) {}

	~SaveBuffer(void) {}

	SaveBuffer& operator=(const SaveBuffer& _buff)
	{
		count_trigger = _buff.count_trigger;
		count_keys = _buff.count_keys;

		player = _buff.player;

		return *this;
	}
};