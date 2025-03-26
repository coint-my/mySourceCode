#ifndef VOVANLIB_H
#define VOVANLIB_H
namespace vovan
{
	#define expd __declspec(dllexport)
	#define PI 3.14159

	struct Vector2i
	{
		int x, y;
		expd Vector2i() : x(0), y(0) { }
		expd Vector2i(const int _x, const int _y) : x(_x), y(_y) { }
		expd Vector2i(const Vector2i &_v) : x(_v.x), y(_v.y) { }
	};

	struct Vector2f
	{
		float x, y;
		expd Vector2f() : x(0), y(0) { }
		expd Vector2f(const float _x, const float _y) : x(_x), y(_y) { }
		expd Vector2f(const Vector2f &_v) : x(_v.x), y(_v.y) { }
	};
	//÷ветова€ байт схема RGBA
	//(0 - 255)
	struct Color
	{
		short r, g, b, a;
		expd Color() : r(255), g(255), b(255), a(0) { }
		expd Color(short _r, short _g, short _b) : r(_r), g(_g), b(_b), a(0) { }
		expd Color(short _r, short _g, short _b, short _a) : r(_r), g(_g), b(_b), a(_a) { }
		expd bool operator==(Color &_col);
		expd bool operator!=(Color &_col);
	};

	class Recti
	{
	private:
		int wid, hei;
		Vector2i pos;
	public:
		//конструктор поумолчанию
		expd Recti() : pos(0, 0), wid(5), hei(5) { }
		//конструктор с параметрами
		expd Recti(Vector2i &_pos, int _wid, int _hei) : pos(_pos.x, _pos.y), wid(_wid), hei(_hei) { }
		//конструктор копировани€
		expd Recti(const Recti &_r) : pos(_r.pos), wid(_r.wid), hei(_r.hei) { }
		//инициализировать пр€моугольник
		expd void Initialize(const Recti &_rect) { pos = _rect.pos; wid = _rect.wid; hei = _rect.hei; }
		//получить ширину
		expd int GetWid() { return wid; }
		//получить висоту
		expd int GetHei() { return hei; }
		//получить позицию вектор2
		expd Vector2i GetVect() { return this->pos; }
		//здвинуть пр€моугольник
		void Offset(Vector2i &_p) { pos.x += _p.x; pos.y += _p.y; }
		//проверка пересечени€ пр€моугольников
		expd bool Intersect(Recti &_rect);
		//права€ часть пр€моугольника
		int Right() { return pos.x + wid; }
		//лева€ часть пр€моугольника
		int Left() { return pos.x; }
		//верхн€€ часть пр€моугольника
		int Top() { return pos.y; }
		//нижн€€ часть пр€моугольника
		int Bottom() { return pos.y + hei; }
		//оператор присваивани€
		expd const Recti &operator=(const Recti &_r);
	};

	class Rectf
	{
	private:
		float wid, hei;
		Vector2f pos;
	public:
		//конструктор поумолчанию
		expd Rectf() : pos(0, 0), wid(5), hei(5) { }
		//конструктор с параметрами
		expd Rectf(Vector2f &_pos, float _wid, float _hei) : pos(_pos.x, _pos.y), wid(_wid), hei(_hei) { }
		//конструктор копировани€
		expd Rectf(const Rectf &_r) : pos(_r.pos), wid(_r.wid), hei(_r.hei) { }
		//инициализировать пр€моугольник
		expd void Initialize(const Rectf &_rect) { pos = _rect.pos; wid = _rect.wid; hei = _rect.hei; }
		//получить ширину
		expd float GetWid() { return wid; }
		//получить висоту
		expd float GetHei() { return hei; }
		//получить позицию вектор2
		expd Vector2f GetVect() { return this->pos; }
		//здвинуть пр€моугольник
		void Offset(Vector2f &_p) { pos.x += _p.x; pos.y += _p.y; }
		//проверка пересечени€ пр€моугольников
		expd bool Intersect(Rectf &_rect);
		//права€ часть пр€моугольника
		float Right() { return pos.x + wid; }
		//лева€ часть пр€моугольника
		float Left() { return pos.x; }
		//верхн€€ часть пр€моугольника
		float Top() { return pos.y; }
		//нижн€€ часть пр€моугольника
		float Bottom() { return pos.y + hei; }
		//оператор присваивани€
		//expd const Rectf &operator=(const Rectf &_r);
	};

	class Circlef
	{
	private:
		Vector2f pos;
		int rad;
		int seg;
	public:
		//конструктор поумолчанию
		expd Circlef() : rad(1), seg(4) {}
		//конструктор с параметрами
		expd Circlef(Vector2f &_pos, const int _rad, int _seg) : pos(_pos.x, _pos.y), rad(_rad), seg(_seg) { }
		//конструктор копировани€
		expd Circlef(const Circlef &_c) : pos(_c.pos), rad(_c.rad), seg(_c.seg) { }
		//здвинуть обьект
		void Offset(const Vector2f &_pos) { pos.x += _pos.x; pos.y += _pos.y; }
		//инициализаци€ обьекта
		expd void Init(const Vector2f &_pos, const int _rad, const int _seg);
		//получить радиус
		int GetRad() { return rad; }
		//получить сколько сегментов
		int GetSegment() { return seg; }
		//получить позицию
		Vector2f GetPos() { return pos; }
	};
}
#endif