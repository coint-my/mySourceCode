#pragma once
#include <vector>
#include <gl/glut.h>

struct point
{
	float x, z;
	float dx, dz;
};

class EditGrid
{
private:
	std::vector<point> list;
	int width, step;
	int xcord, zcord;
	int vx, vy;
	int scale;
	bool isactive;
public:
	void drawEdit();

	void InitEdit();

	void editMouse(int button, int state, int x, int y);

	void lineEdit();

	int delIntersect(const int x, const int y);

	void tempLine(const int x, const int y, int dx, int dy);

	int getScale();

	int stepGrid(const int step, const int val);

	void visible(int x, int y);

	std::vector<point> getListPoints();

	EditGrid(void);
	~EditGrid(void);
};
