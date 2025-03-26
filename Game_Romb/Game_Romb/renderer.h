#pragma once
#include "figure.h"
#include "player.h"

//рисование графики class renderer
class renderer
{
	//нарисовать круг
public: static void draw_polygon(const data_figure& fig);
public: static void draw_Rect(const Rectangle& rec, const Color& col);
//вывод строки кирилица
public: static void ShowString(const wchar_t *_string, const int _x, const int _y);
//вывод целого числа
public: static void ShowString(int _intejer, const int _x, const int _y);
public:
	renderer();
	~renderer();
};

