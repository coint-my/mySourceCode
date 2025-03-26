#pragma once
#include <wchar.h>
#include <iostream>
#include "table.h"

#ifndef MAX_NAME_FIGURE
#define MAX_NAME_FIGURE 15
#endif

class entity
{
private: wchar_t* _name;
//очки
private: int _score;
//инициализация переменная bool
public: bool isHandleMouse;

//взять имя
public: wchar_t* get_name() const;
//взять очки
public: int get_score() const;
//добавить очки
public: void add_score(const int val);
//присвоить очки
public: void set_score(const int val);
//дать управление
public: virtual bool handle();

public:
	entity();
	entity(const wchar_t *name);
	virtual ~entity();
};

