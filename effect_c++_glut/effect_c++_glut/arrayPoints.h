#pragma once

#include <gl/glut.h>
#include <math.h>
#include <cstdlib>

struct PointA
{
	float x, y;

	int lenth;
	int _y, _x;
};

class arrayPoints
{
private:
	int count;
	int angle;
	int speed;
	int counter;
	float posy;
	float angSpray;
	PointA *arr;

	float radius(const float x, const float y, const float endx, const float endy);
	void init(const int yInit, const int xInit, const int ind);

public:
	bool isActive;
	arrayPoints(void);
	arrayPoints(const int ar, const int _angle, const int _spray, const int lenth, const int lenthLive);
	arrayPoints(const int width, const int height);
	~arrayPoints(void);

	void cor(const int x, const int y);

	void drawArr();
	void initPosition(const int vx, const int vy);
	void updateOreol();
	void gravityOreol(const double alpha);
	void update(const int x, const int y);
	void updateAB(const int x_x, const int y_y, const int dx, const int dy);
};
