#include <iostream>
#include <fstream>

class Position
{
private:
	float x, y, z;
public:
	Position(void) : x(5), y(5), z(10) {}
	~Position(void){}

	const inline Position& get_pos() const { return *this; }
};

struct Acc
{
	int ind;
	char name[10];
	double sum;
	Position pos;
	Acc(void): ind(0), sum(1.5), pos()
	{
		std::strcpy(name, "none");
	}
	~Acc(void){}
};

int main()
{
	Acc ac;
	ac.ind = 532;
	std::ofstream file;
	file.open("data.txt", std::ios::out | std::ios::binary);

	file.write(reinterpret_cast<const char*>(&ac), sizeof(Acc));

	file.close();

	Acc acc;

	std::ifstream rf;
	rf.open("data.txt", std::ios::in);

	while(!rf.eof())
	{
		rf.read(reinterpret_cast<char*>(&acc), sizeof(Acc));
		printf("name : %s \nind : %i\nsum : %d\n", acc.name, acc.ind, acc.sum);
	}

	rf.close();

	system("pause");
	return 1;
}