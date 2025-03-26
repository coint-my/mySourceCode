#pragma once
#include "GameObject.h"

class Mesh : public GameObject
{
public:
	Mesh(void);
	virtual ~Mesh(void);

	void initialize(const Point2i *_pp, const int _count);

	inline void draw() const;
	//inline void set_pos(const Point2f &_pos) { pos = _pos; }
	//inline const Point2f& get_pos() { return pos; }
};

