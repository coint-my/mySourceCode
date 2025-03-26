#include "StringRead.h"
#include "GeneralProperty.h"
#include <gl/glut.h>
#include <stdlib.h>

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