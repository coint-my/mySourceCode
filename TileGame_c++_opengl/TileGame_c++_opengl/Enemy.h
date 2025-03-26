#pragma once
#include "Tile.h"
#include <vector>
class Enemy : public Tile
{
private:
	enum direction{L = 1,R,T,B}dir;
	short false_index;
	short i_x, i_y;
	std::vector<short> v_dir;
public:
	Enemy(void);
	~Enemy(void);

	void initialize(const int _x, const int _y, const int _wid, const int _hei, Color _col);
	void Update(const std::vector<Tile> &_tiles, const std::vector<Vector2> &e_patch);

	bool IsPassive(const short _dir, const std::vector<Tile> &_tiles, short &_negativ);

	int GetDirection() { return (int)dir; }
};

