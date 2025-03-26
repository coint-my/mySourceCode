#pragma once
#include "player.h"

#define PUNCT_MENU 5

typedef enum { PLAYER_COMP, PLAYER_PLAYER, COMP_COMP, RESET_POLE, EXIT_GAME, EMPTY};

typedef struct menu_buff
{
	Color col;
	Color b_col;
	Color n_col;
	Rectangle rect;

	menu_buff() : col(), b_col(), n_col(), rect() {}
	menu_buff(Rectangle& r, Color c, Color new_color) : rect(r), col(c), b_col(c), n_col(new_color) {}

	void set_color(const bool val) { col = val ? n_col : b_col; }
};

class Menu
{
private: Rectangle r_b_g;
private: menu_buff buff[PUNCT_MENU];

public: void draw_menu() const;

public: menu_buff& get_menu(const int _ind);

public:
	Menu();
	~Menu();
};

