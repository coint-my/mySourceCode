#pragma once
#include "GameObject.h"

class Blook : public GameObject
{
protected:
	int wid, hei;
	char label[10];
	bool is_show;
public:
	Blook(void);
	Blook(const Point2f &_pos, const int _wid, const int _hei);
	Blook(const char _name[10], const Point2f &_pos, const int _wid, const int _hei);
	Blook(const char _name[10], const Point2f &_pos, const int _wid, const int _hei, const Colorf& _color);
	Blook(float _m, const Point2f &_pos, const int _wid, const int _hei);
	/*Blook(const Blook& _bl);*/
	~Blook(void);

	virtual void draw() const;

	void draw(const float _z, const Point3f& _normal) const;
	void init_wall();

	//Blook& operator=(const Blook& _bl);

	inline int get_wid() { return wid; }
	inline int get_hei() { return hei; }
	inline const Point2f get_center() const { return Point2f(pos.x + (wid / 2), pos.y + (hei / 2)); }
	inline char* get_label() { return label; }

	inline void show_lable() const;
};

