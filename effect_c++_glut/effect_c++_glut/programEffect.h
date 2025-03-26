#pragma once

struct Pixel2
{
	float x;
	float y;
	float z;
	enum Color {red = 0, green = 1, blue = 2, white = 3};
	Pixel2(const float _x, const float _y, const float _z, Color c) : x(_x), y(_y), z(_z), colorTupe(c)
	{
		switch(c)
		{
		case red:
			color[0] = 1.0f; color[1] = 0.0f; color[2] = 0.0f;
			break;
		case green:
			color[0] = 0.0f; color[1] = 1.0f; color[2] = 0.0f;
			break;
		case blue:
			color[0] = 0.0f; color[1] = 0.0f; color[2] = 1.0f;
			break;
		default:
			color[0] = 0.0f; color[1] = 0.0f; color[2] = 0.0f;
			break;
		}
	}
	float* getColor() {return color;}

private:
	float color[3];
	Color colorTupe;
};

struct Pixel3
{
	float x;
	float y;
	float z;

	float vx;
	float vy;
	float vz;

	enum Color {red = 0, green = 1, blue = 2, white = 3};
	Pixel3(const float _x, const float _y, const float _z, Color c) : x(_x), y(_y), z(_z), colorTupe(c)
	{
		vx = 0;
		vy = 0;
		vz = 0;
		switch(c)
		{
		case red:
			color[0] = 1.0f; color[1] = 0.0f; color[2] = 0.0f;
			break;
		case green:
			color[0] = 0.0f; color[1] = 1.0f; color[2] = 0.0f;
			break;
		case blue:
			color[0] = 0.0f; color[1] = 0.0f; color[2] = 1.0f;
			break;
		default:
			color[0] = 0.0f; color[1] = 0.0f; color[2] = 0.0f;
			break;
		}
	}
	float* getColor() {return color;}

private:
	float color[3];
	Color colorTupe;
};

class programEffect
{
public:
	programEffect(void);
	~programEffect(void);
};
