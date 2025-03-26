#pragma once
#include "entity.h"
#include <vector>

class Rectangle;

typedef struct s_index
{
	int x, y;
	s_index() : x(0), y(0) {}
	s_index(int xx, int yy) : x(xx), y(yy) {}
	~s_index() {}
};

//структура лучших ходов
typedef struct best_ind
{
	int x1, y1, x2, y2;
	int value;
	bool isRang1;
	best_ind() : x1(0), y1(0), x2(0), y2(0), value(0), isRang1(true) {}
	best_ind(int _x1, int _y1, int _x2, int _y2, int _val, bool _isRang1) : x1(_x1), y1(_y1), x2(_x2), y2(_y2), value(_val), isRang1(_isRang1) {}
	~best_ind() {}
};

class enemy :	public entity
{
//игровой стол
private: table* _table;
//воображаемый игрок ипротивник
private: int pl;
private: int en;
//текущий ход компютера
best_ind curr_turn;

//имеющиеся фигуры
private: std::vector<s_index> init_ind;
//выбор компьютера вариантов ходов 
private: std::vector<best_ind> list_vars;
//фигура перемещения
private: data_figure *fig;

//инициализация начальной логики
private: void init();
//инициализация ходов ранга 1
private: void init_rang1(const int xx, const int yy, std::vector<best_ind>& _list);
//инициализация ходов ранга 2
private: void init_rang2(const int xx, const int yy, std::vector<best_ind>& _list);
private: inline const Rectangle& get_r1(const int xx, const int yy) const;
private: inline const Rectangle& get_r1(const int xx, const int yy, const int _size) const;
private: const best_ind& get_best_var() const;
private: const best_ind& empty_turn() const;
//перемещение фигуры с точки (а) в точку (в)
private: bool run_figure();
//управление противником
public: bool isHandle;

public: bool handle();
public:
	enemy(table &_t, int pla, int ene, const wchar_t* _nam);
	~enemy();
};

