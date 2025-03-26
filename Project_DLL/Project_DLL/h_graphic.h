#ifndef H_GRAPHIC_H
#define H_GRAPHIC_H

#include <gl/glut.h>
#include <cmath>
#pragma comment(lib, "opengl32.lib")
#pragma comment(lib, "glut32.lib")

namespace graphic
{
	#define exp __declspec(dllexport)
	#define PI 3.14159

	struct Vector2i
	{
		int x, y;
		exp Vector2i() : x(0), y(0) { }
		exp Vector2i(int _x, int _y) : x(_x), y(_y) { }
	};

	struct Vector2f
	{
		float x, y;
		exp Vector2f() : x(0), y(0) { }
		exp Vector2f(float _x, float _y) : x(_x), y(_y) { }
	};

	struct Color
	{
		short r, g, b, a;
		exp Color() : r(255), g(255), b(255), a(0) { }
		exp Color(short _r, short _g, short _b) : r(_r), g(_g), b(_b), a(0) { }
		exp Color(short _r, short _g, short _b, short _a) : r(_r), g(_g), b(_b), a(_a) { }
	};

	class Recti
	{
	private:
		int wid, hei;
		Vector2i pos;
	public:
		//конструктор поумолчанию
		exp Recti() : pos(0, 0), wid(5), hei(5) { }
		//конструктор с параметрами
		exp Recti(Vector2i &_pos, int _wid, int _hei) : pos(_pos.x, _pos.y), wid(_wid), hei(_hei) { }
		//получить X
		exp int GetX() { return pos.x; }
		//получить Y
		exp int GetY() { return pos.y; }
		//получить ширину
		exp int GetWid() { return wid; }
		//получить висоту
		exp int GetHei() { return hei; }
		//получить вектор2
		exp Vector2i GetVect() { return this->pos; }
		//здвинуть пр€моугольник
		exp void Offset(Vector2i &_p) { pos.x += _p.x; pos.y += _p.y; }
		//проверка пересечени€ пр€моугольников
		exp bool Intersect(Recti &_rect);
		//права€ часть пр€моугольника
		exp int Right() { return pos.x + wid; }
		//лева€ часть пр€моугольника
		exp int Left() { return pos.x; }
		//верхн€€ часть пр€моугольника
		exp int Top() { return pos.y; }
		//нижн€€ часть пр€моугольника
		exp int Bottom() { return pos.y + hei; }
		//оператор присваивани€
		exp const Recti &operator=(const Recti &_r);
	};

	class Circlef
	{
	private:
		Vector2f pos;
		int rad;
		int seg;
	public:
		//конструктор поумолчанию
		Circlef() : rad(1), seg(4) {}
		//конструктор с параметрами
		Circlef(Vector2f &_pos, const int _rad, int _seg) : pos(_pos.x, _pos.y), rad(_rad), seg(_seg) { }
		//здвинуть обьект
		void Offset(const Vector2f &_pos) { pos.x += _pos.x; pos.y += _pos.y; }
		//инициализаци€ обьекта
		exp void Init(const Vector2f &_pos, const int _rad, const int _seg);
		//получить радиус
		int GetRad() { return rad; }
		//получить сколько сегментов
		int GetSegment() { return seg; }
		//получить позицию
		Vector2f GetPos() { return pos; }
	};

	class Painter
	{
	public:
		//обновление функции
		static exp void InitUpdate(int _time, void(*update)(int _t));
		//перерисовка функции рисовани€
		static exp void ReDraw(int _time, void (*update)(int _t));
		//инициализировать графическое окно
		static exp void Initialize2D(int _wid, int _hei, int &_argc, char** _argv, char* _nameWindow, void(*render)(), void(*mouse)(int _b, int _s, int _x, int _y));
		//нарисовать пр€моугольник
		static exp void DrawRect(Recti &_r, Color &_c);
		//нарисовать круг
		static exp void DrawCircle(Circlef &_circl, Color &_c);
		//начать рисовать
		static exp void Start();
		//закончить рисовать
		static exp void End();
	};
}
#endif