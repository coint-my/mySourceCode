#pragma once
#include "GameObject.h"

class Figure : public GameObject
{
public:
	Figure(void);
	virtual ~Figure(void);

	void InitializeTexture(const Translation *_tr, const int _count);

	virtual void Initialize(const Translation *_tr, const int _count);
	virtual void Render() {}
	virtual void Update() {}
};

