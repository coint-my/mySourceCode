#ifndef MANAGEREFF_H
#define MANAGEREFF_H

#include "ObjCir.h"
#include <vector>

class ManagerEff
{
private:
	int mass;
	int alive;
	int curr;
	std::vector<ObjCir> cir;
	graphic::Recti initRect;
public:
	ManagerEff(void);
	ManagerEff(const int _mass, const int _alive, graphic::Recti &_initRect, int _mSize);
	~ManagerEff(void);

	void Init(const graphic::Recti _rect, int _mass, int _maxSize);
	void Update(graphic::Vector2f _gravity);

	bool IsAlive() { return (alive > curr); }
	int GetMass() { return cir.size(); }
	std::vector<ObjCir> GetCircls() { return cir; }
};
#endif
