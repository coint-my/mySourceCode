#pragma once
#include <string>
#include "E_Rect.h"
#include "GameObject.h"

class Rectf;

class Trigger
{
private:
	bool isActive;
	int level;
	char name[30];
	Colorf color;
	E_Rect rect;
public:
	Trigger(void);
	Trigger(const char* _name, E_Rect& _rect, const Colorf& _color);
	~Trigger(void);

	inline void set_active(const bool _active) { isActive = _active; }
	inline void set_pos(const Point2f& _pos) { rect.set_xy(_pos.x, _pos.y); }
	inline void set_level(const int _l) { level = _l; }

	void render() const;

	inline const char* t_name() const { return name; }
	inline E_Rect& get_rect() { return rect; }
	inline bool IsActive() const { return isActive; }
	inline int get_level() { return level; }
};

