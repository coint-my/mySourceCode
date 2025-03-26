#include "StringRead.h"
#include "gl/glut.h"
#include <cstdlib>
#include <cstring>

StringRead::StringRead(void)
{
}


StringRead::~StringRead(void)
{
}

void StringRead::ShowString(const char *_string, const int _x, const int _y)
{
	glRasterPos2i(_x, _y);

	while(*_string)
	{
		glutBitmapCharacter(GLUT_BITMAP_HELVETICA_12, *_string++);
	}
}

void StringRead::ShowString(const char _string, const int _x, const int _y)
{
	glRasterPos2i(_x, _y);

	glutBitmapCharacter(GLUT_BITMAP_HELVETICA_12, _string);
}

void StringRead::ShowString(const char *_string, const int _intejer, const char* _strend, const int _x, const int _y)
{
	char _str[20];
	std::strcpy(_str, _string);
	char _temp[15];
	itoa(_intejer, _temp, 10);
	std::strcat(_str, _temp);
	std::strcat(_str, _strend);

	glRasterPos2i(_x, _y);

	int i = 0;
	while(i < 20)
	{
		glutBitmapCharacter(GLUT_BITMAP_HELVETICA_12, _str[i]);
		i++;
	}
}

void StringRead::ShowString(int _intejer, const int _x, const int _y)
{
	char _str[15];
	itoa(_intejer, _str, 10);

	glRasterPos2i(_x, _y);

	int i = 0;
	while(i < 20)
	{
		glutBitmapCharacter(GLUT_BITMAP_HELVETICA_12, _str[i]);
		i++;
	}
}