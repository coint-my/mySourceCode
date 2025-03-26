#pragma once
#include "EngineRect.h"
#include "Painter.h"

class ScrollBar : public EngineRect
{
private:
	Color color;
public:
	ScrollBar(void);

	void Offset(const int _x, const int _y);
	void SetColor(Color &_c) { color = _c; }
	void DrawBar(Painter &_p) const;

	~ScrollBar(void);
};
