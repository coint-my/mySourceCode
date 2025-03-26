#pragma once
#include <iostream>
#include <wchar.h>
#include "setting.h"
//позиция обьекта
typedef struct Position
{
	float x, y;
	Position() { x = 0; y = 0; }
	Position(float x, float y) { this->x = x; this->y = y; }
	void Init(const float x, const float y) { this->x = x; this->y = y; }
	~Position(void) {}
};
//структура цвета
typedef struct Color
{
	int r, g, b;
	Color() { r = 0; g = 0; b = 0; }
	Color(int r, int g, int b) : r(r), g(g), b(b) {}
	void Init(const int r, const int g, const int b) { this->r = r; this->g = g; this->b = b; }
	~Color(void) {}
};
//данные класса (Figure) этого обьекта
class data_figure
{
protected: float _segment;
protected: float _radius;
protected: wchar_t* _name;
protected: Position* _position;
protected: Color* _color;

public: float get_seg() const;
public: float get_rad() const;
public: wchar_t* get_name() const;
public: Position& get_pos() const;
public: Color& get_color() const;

public: void offset(const float x, const float y);
public: void set_seg(const float seg);
public: void set_rad(const float rad);
public: void set_pos(const Position& pos);
public: void set_col(const Color& col);
public: void initialize(const float seg, const float rad, const Position& pos, const Color& col, const wchar_t* name);

public:	data_figure();
	virtual ~data_figure();
};