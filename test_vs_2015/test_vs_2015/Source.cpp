//#include <GL/glut.h>
#include <iostream>
#include <time.h>

typedef struct Card
{
	char val;
};

void print(const Card* _c)
{
	for (size_t i = 0; i < 10; i++)
	{
		printf("val : %i\n", (int)_c[i].val);
	}
}

int main()
{
	Card cards[10];

	srand(time(NULL));

	for (size_t i = 0; i < 10; i++)
		cards[i].val = i;

	print(cards);

	for (size_t i = 0; i < 10; i++)
	{
		int j = rand() % 10;
		Card temp = cards[i];
		cards[i] = cards[j];
		cards[j] = temp;
	}

	std::cout << "--------------------------------------------\n";

	print(cards);

	system("PAUSE");

	return 0;
}