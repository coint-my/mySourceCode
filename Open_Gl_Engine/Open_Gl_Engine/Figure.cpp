#include "Figure.h"


Figure::Figure(void)
{
	float size = 20;
	Translation p[24];
	//1
	p[0] = Translation(0, 0, 0);
	p[1] = Translation(size, 0, 0);
	p[2] = Translation(size, size, 0);
	p[3] = Translation(0, size, 0);
	//2 right
	p[4] = Translation(0, 0, 0);
	p[5] = Translation(0, 0, size);
	p[6] = Translation(0, size, size);
	p[7] = Translation(0, size, 0);
	//3 left
	p[8] = Translation(size, 0, 0);
	p[9] = Translation(size, 0, size);
	p[10] = Translation(size, size, size);
	p[11] = Translation(size, size, 0);
	//4 top
	p[12] = Translation(0, 0, size);
	p[13] = Translation(size, 0, size);
	p[14] = Translation(size, size, size);
	p[15] = Translation(0, size, size);
	//5 top
	p[16] = Translation(0, size, 0);
	p[17] = Translation(size, size, 0);
	p[18] = Translation(size, size, size);
	p[19] = Translation(0, size, size);
	//6 top
	p[20] = Translation(0, 0, 0);
	p[21] = Translation(size, 0, 0);
	p[22] = Translation(size, 0, size);
	p[23] = Translation(0, 0, size);

	Translation f[24];
	//1
	f[0] = Translation(0.0, 0.0, 0.0);
	f[1] = Translation(1.0, 0.0, 0.0);
	f[2] = Translation(1.0, 1.0, 0.0);
	f[3] = Translation(0.0, 1.0, 0.0);
	//2
	f[4] = Translation(0.0, 0.0, 0.0);
	f[5] = Translation(1.0, 0.0, 0.0);
	f[6] = Translation(1.0, 1.0, 0.0);
	f[7] = Translation(0.0, 1.0, 0.0);
	//3
	f[8] = Translation(0.0, 0.0, 0.0);
	f[9] = Translation(1.0, 0.0, 0.0);
	f[10] = Translation(1.0, 1.0, 0.0);
	f[11] = Translation(0.0, 1.0, 0.0);
	//4
	f[12] = Translation(0.0, 0.0, 0.0);
	f[13] = Translation(1.0, 0.0, 0.0);
	f[14] = Translation(1.0, 1.0, 0.0);
	f[15] = Translation(0.0, 1.0, 0.0);
	//5
	f[16] = Translation(0.0, 0.0, 0.0);
	f[17] = Translation(1.0, 0.0, 0.0);
	f[18] = Translation(1.0, 1.0, 0.0);
	f[19] = Translation(0.0, 1.0, 0.0);
	//6
	f[20] = Translation(0.0, 0.0, 0.0);
	f[21] = Translation(1.0, 0.0, 0.0);
	f[22] = Translation(1.0, 1.0, 0.0);
	f[23] = Translation(0.0, 1.0, 0.0);

	InitializeTexture(f, 24);
	Initialize(p, 24);

	static int countFigure = 0;
	countFigure++;
	std::cout << "ClassFigure 'Call' " << countFigure << std::endl;
}

Figure::~Figure(void)
{

}

void Figure::InitializeTexture(const Translation *_tr, const int _count)
{
	textPoints.clear();
	isTexture = true;
	std::cout << "LoadTexture->Figure class" << std::endl;

	for (int i = 0; i < _count; i++)
	{
		textPoints.push_back(_tr[i]);
	}
}

void Figure::Initialize(const Translation *_tr, const int _count)
{
	points.clear();

	for (int i = 0; i < _count; i++)
	{
		points.push_back(_tr[i]);
	}
}
