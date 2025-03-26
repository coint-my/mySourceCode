#pragma once
#include <gl/glut.h>

class painter
{
public:
	painter(void);

	enum Color {RED, GREEN, BLUE, ELLOWY};

	void rect(int x1, int y1, int x2, int y2);

	void setColor(Color);

	~painter(void);
};
