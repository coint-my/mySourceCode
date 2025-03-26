#pragma once
#include "Painter.h"
#include "Tile.h"
#include "Enemy.h"
#include <fstream>
#include <vector>

#define TILE_W 40
#define TILE_H 40

class Map
{
private:
	std::vector<Tile> tiles;
	std::vector<Enemy> enemys;
	std::vector<Vector2> e_patch;
public:
	Map(void);
	~Map(void);

	void Init(short _val[], const short _cw, const short _ch);
	void Draw();
	void Update(Tile &_pl, const short _dir);
	void LoadLevel(const int _lev);

	char GetChar(const int _count);
};

