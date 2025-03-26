#pragma once
#include "figure.h"
#include "setting.h"

//перечисления состояний ячеек
enum { IMPOSIBLE = 1, POSIBLE, PLAYER, ENEMY, ACTIVE };

//нгровой стол прорисовки
class table
{
private: int _field[DESC_W][DESC_H];
private: Position pos[DESC_W][DESC_H];
private: data_figure _data[MAX_STATE];

//нарисовать стол
public: void draw_field();
//массив поля значений
public: int get_field(const int x, const int y) const;
//состояния фигур
public: const data_figure& get_figure(const int val) const;
//задать значение полю значений
public: bool set_field(const int x, const int y, const int _val);
//взять позицию фигуры
public: const Position& get_pos(const int x, const int y) const;
//сбросить игровой стол
public: void reset_pole();

public:
	table();
	~table();
};

