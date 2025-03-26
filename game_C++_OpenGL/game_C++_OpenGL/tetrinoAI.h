#pragma once
#include "unity.h"
#include "tetrino.h"
#include <cstdlib>

class tetrinoAI
{
public:
	int x;
	int rotation;
	bool isRotation;
	bool isGoNext;

	tetrinoAI(void);
	~tetrinoAI(void);

	bool Init(unity &u);

	bool easy(unity &u);

	void update(unity &u);
};
