#include "Menu.h"
#include "renderer.h"
#include <GL\glut.h>

void Menu::draw_menu() const
{
	renderer::draw_Rect(r_b_g, Color(80, 80, 80));

	for (size_t i = 0; i < PUNCT_MENU; i++)
		renderer::draw_Rect(buff[i].rect, buff[i].col);

	glColor3ub(200, 200, 200);
	renderer::ShowString(L"Player VS COMP", buff[0].rect.get_x(), buff[0].rect.get_y() + buff[0].rect.get_hei() / 2);
	renderer::ShowString(L"Player VS Player", buff[1].rect.get_x(), buff[1].rect.get_y() + buff[1].rect.get_hei() / 2);
	renderer::ShowString(L"COMP VS COMP", buff[2].rect.get_x(), buff[2].rect.get_y() + buff[2].rect.get_hei() / 2);
	renderer::ShowString(L"RESET POLE", buff[3].rect.get_x(), buff[3].rect.get_y() + buff[3].rect.get_hei() / 2);
	renderer::ShowString(L"EXIT", buff[4].rect.get_x(), buff[4].rect.get_y() + buff[4].rect.get_hei() / 2);
}

menu_buff & Menu::get_menu(const int _ind)
{
	return this->buff[_ind];
}

Menu::Menu()
{
	r_b_g = Rectangle(300, 100, 200, 400);

	for (size_t i = 0; i < PUNCT_MENU; i++)
	{
		buff[i] = menu_buff(Rectangle(r_b_g.get_x(), r_b_g.get_y() + (i * (r_b_g.get_hei() / PUNCT_MENU)),
			r_b_g.get_wid(), r_b_g.get_hei() / PUNCT_MENU), Color(80 + (30 * i), 50, 50), Color(50, 50, 50));
	}
}


Menu::~Menu()
{
}
