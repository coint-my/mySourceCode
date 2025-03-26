#include "entity.h"

wchar_t * entity::get_name() const
{
	return _name;
}

int entity::get_score() const
{
	return _score;
}

void entity::add_score(const int val)
{
	_score += val;
}

void entity::set_score(const int val)
{
	_score = val;
}

bool entity::handle()
{
	return false;
}

entity::entity()
{
	_name = new wchar_t[MAX_NAME_FIGURE];
	_name = L"нет имени";
	_score = 0;
	isHandleMouse = false;
}

entity::entity(const wchar_t * name)
{
	_name = new wchar_t[MAX_NAME_FIGURE];
	try
	{
		if(sizeof(name) > 14)
			throw(name);

		wcscpy(_name, name);
	}
	catch (const wchar_t* exept)
	{
		wprintf(L"ошибка индекса имени: (%ls) максимум букв = (%i)\n", exept, MAX_NAME_FIGURE);
	}
	_score = 0;
	isHandleMouse = false;
}


entity::~entity()
{
	delete[] _name;
}
