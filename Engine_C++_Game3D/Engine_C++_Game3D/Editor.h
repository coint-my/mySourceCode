#pragma once
#include <gl/glut.h>
#include "DisplayEditor.h"
#include "Grid.h"
#include "Sector.h"
#include "Rect.h"

static int mouseX;
static int mouseY;

static Rect rect = Rect(0,0,0,0);
static std::vector<Point> tempPoints;
static std::vector<int> tempArrPoint;

static std::vector<Sector> sectors;

static bool isArrPointMode;
static bool isActive;

class Editor
{
public:
	bool isButtonDown;
	Grid grid;
	int mx;
	int my;
	int lentchGrid;
	int sect, index;
	const int list_sector;

	bool list_curr_sector[100][100];

	void draw() const;

	void Initialize();

	void keyboard(unsigned char _key, int _x, int _y);
	void mouse(int _button, int _state, int _x, int _y);
	void move(int _x, int _y);
	void passMove(int _x, int _y);
	void key(int _key, int _x, int _y);
	void initArrPoint(Rect &_r);
	void activeSector();
	void update();

	Editor(void);
	~Editor(void);
};
