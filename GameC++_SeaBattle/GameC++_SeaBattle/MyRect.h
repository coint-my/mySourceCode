#pragma once

class MyRect
{
private:
	short x, y, x2, y2;
public:
	MyRect(void);
	MyRect(const short _x, const short _y, const short _x2, const short _y2);

	short Left();
	short Right();
	short Top();
	short Bottom();

	~MyRect(void);
};
