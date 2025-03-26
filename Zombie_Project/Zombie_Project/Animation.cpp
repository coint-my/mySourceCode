#include "Animation.h"

Animation::Animation(void)
{
}

Animation::~Animation(void)
{
}

void Animation::Draw(Painter &_p, const float _text_wid_s, const float _text_hei_s, const unsigned int _text,
					 const float _text_wid_e, const float _text_hei_e) const
{
	_p.SetColor(Color(color.r, color.g, color.b));
	_p.DrawRectTexture(x, y, wid, hei, _text_wid_e, _text_hei_e, _text_wid_s, _text_hei_s, _text);
}
void Animation::Draw(Painter &_p, const unsigned int _text, const float _lenth_b, const float _lenth_e) const
{
	_p.DrawRectTexture(x, y, wid, hei, _lenth_e, 1.0, _lenth_b, 0.0, _text);
}

void Animation::SetColor(Color &_col)
{
	color = _col;
}

void Animation::Offset(const int _x)
{
	x += _x;
}

void Animation::Initialize(const int _x, const int _y)
{
	x = _x;
	y = _y;
}
