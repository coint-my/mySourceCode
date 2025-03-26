#pragma once
#include "VovanLib.h"

class Obj
{
private:
	vovan::Color col;
	vovan::Rectf rect;
	vovan::Vector2f dir;
public:
	Obj(void);
	virtual ~Obj(void);

	void SetRect(const vovan::Rectf &_r) { rect.Initialize(_r); }
	void SetColor(const vovan::Color &_c) { col = vovan::Color(_c); }
	void Init(const vovan::Rectf &_r, const vovan::Color &_c, const vovan::Vector2f &_dir);
	void Offset(vovan::Vector2f &_vel) { rect.Offset(_vel); }

	vovan::Rectf &GetRect() { return this->rect; }
	vovan::Color &GetColor() { return this->col; }
	vovan::Vector2f &GetDir() { return this->dir; }
};

