#include <iostream>
#include "ManagerEff.h"

using namespace graphic;

#define W 255
#define H 170

std::vector<ManagerEff> eff;

void Display()
{
	graphic::Painter::Start();

	for (int j = 0; j < eff.size(); j++)
	{
		for (int i = 0; i < eff[j].GetMass(); i++)
		{
			graphic::Circlef ccc = eff[j].GetCircls()[i].GetCircle();
			graphic::Painter::DrawCircle(ccc, eff[j].GetCircls()[i].GetColor());
		}
	}

	graphic::Painter::End();
}

void Update(int _t)
{
	for (int j = 0; j < eff.size(); j++)
	{
		for (int i = 0; i < eff[j].GetMass(); i++)
		{
			eff[j].Update(graphic::Vector2f(0.0001, 0.0004));
		}
	}
	for (int d = 0; d < eff.size(); d++)
	{
		if(!eff[d].IsAlive())
			eff.erase(eff.begin() + d);
	}
	graphic::Painter::ReDraw(50, Update);
}

void mouse(int button, int state, int x, int y)
{
	if(button == GLUT_LEFT_BUTTON && state == GLUT_UP)
	{
		eff.push_back(ManagerEff(30, 2300, graphic::Recti(graphic::Vector2i(x, y), 3, 3), 3));
	}
}

int main(int argc, char** argv)
{
	eff.push_back(ManagerEff(35, 3000, graphic::Recti(graphic::Vector2i(250, 250), 15, 15), 6));
	srand((int)__TIME__);

	graphic::Painter::InitUpdate(1, Update);
	graphic::Painter::Initialize2D(800, 600, argc, argv, "graphic window", Display, mouse);

	return 0;
}