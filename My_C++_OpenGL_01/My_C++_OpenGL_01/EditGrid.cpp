#include "EditGrid.h"

EditGrid::EditGrid(void)
{
	scale = 3;
	width = 30;
	step = 30;
	isactive = false;
	vx = 0;
	vy = 0;
}

EditGrid::~EditGrid(void)
{
}
void EditGrid::visible(int x, int y)
{
	vx = stepGrid(step, x);
	vy = 600 - stepGrid(step, y);
}
void EditGrid::drawEdit()
{
	glColor3f(1.0, 0.0, 0.0);
	glPointSize(6);
	glBegin(GL_POINTS);
	glVertex2i(vx, vy);
	glEnd();

	glColor3f(1.0, 1.0, 1.0);
	glEnable(GL_LINE_STIPPLE);
	glLineStipple(1, 0x1111);
	glBegin(GL_LINES);
	for(int i = 0; i < width; i++)
	{
		glVertex2i(0, i * step);
		glVertex2i(step * width - step, i * step);
	}
	for(int j = 0; j < width; j++)
	{
		glVertex2i(j * step, 0);
		glVertex2i(j * step, step * width - step);
	}
	glEnd();
	glDisable(GL_LINE_STIPPLE);
	lineEdit();
}

void EditGrid::editMouse(int button, int state, int x, int y)
{
	if(button == GLUT_LEFT_BUTTON && state == GLUT_DOWN)
	{
		if(!isactive)
		{
			isactive = true;
			xcord = stepGrid(step, x);
			zcord = 600 - stepGrid(step, y);
		}
		else
		{
			isactive = false;
			point p;
			p.x = xcord;
			p.z = zcord;
			p.dx = stepGrid(step, x);
			p.dz = 600 - stepGrid(step, y);
			list.push_back(p);
		}
	}
	if(button == GLUT_RIGHT_BUTTON)
	{
		isactive = false;
		
		int t = delIntersect(stepGrid(step, x), 600 - stepGrid(step, y));

		if(t != -1)
			list.erase(list.begin() + t);
	}
}

int EditGrid::stepGrid(const int st, const int val)
{
	int i = 0;
	bool flag = false;
	int temp = val - st / 2;

	if(temp < 0)
		return 0;

	while(true)
	{
		temp = temp - st;

		i++;

		if(temp <= 0)
			break;
	}
	
	return st * i;
}
void EditGrid::lineEdit()
{
	glColor3f(1.0, 0.0, 0.0);
	glBegin(GL_POINTS);
	for(int i = 0; i < list.size(); i++)
	{
		//glVertex2f(list[i].x, list[i].z);
		glVertex2f(list[i].dx, list[i].dz);
	}
	glEnd();

	glLineWidth(3);
	glColor3f(1.0, 1.0, 1.0);
	glBegin(GL_LINES);
	
	for(int i = 0; i < list.size(); i++)
	{
		glVertex2f(list[i].x, list[i].z);
		glVertex2f(list[i].dx, list[i].dz);
	}

	if(isactive)
		tempLine(xcord, zcord, vx, vy);

	glEnd();

	glLineWidth(1);
}

void EditGrid::tempLine(const int x, const int y, int dx, int dy)
{
	glVertex2i(x, y);
	glVertex2i(dx, dy);
}
int EditGrid::delIntersect(const int x, const int y)
{
	for(int i = 0; i < list.size(); i++)
	{
		if(x == list[i].dx && y == list[i].dz)
			return i;
	}

	return -1;
}
void EditGrid::InitEdit()
{
	glClearColor(0.0, 0.0, 0.0, 1.0);
	glLoadIdentity();
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(0, 600, 0, 600, -100, 100);
}


std::vector<point> EditGrid::getListPoints()
{
	return list;
}
int EditGrid::getScale()
{
	return scale;
}