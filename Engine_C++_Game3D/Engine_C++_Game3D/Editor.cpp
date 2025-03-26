#include "Editor.h"

Editor::Editor(void) : list_sector(100)
{
	grid = Grid(50);
	sect = 0;
	index = 0;
}

Editor::~Editor(void)
{
}
void Editor::activeSector()
{

}
void defColorSect()
{
	for(int sec = 0; sec < sectors.size(); sec++)
	{
		for(int i = 0; i < sectors[sec].getPoints().size(); i++)
		{
			Point p = sectors[sec].getPoints()[i];
			p.col = ColorRGB(0.1, 0.1, 0.1);
			sectors[sec].setPoints(i, p);
		}
	}
}
void Editor::initArrPoint(Rect &_r)
{
	tempArrPoint.clear();
	isActive = false;

	for(int i = 0; i < 100; i++)
	{
		for(int j = 0; j < 100; j++)
		{
			list_curr_sector[i][j] = false;
		}
	}

	for(int sec = 0; sec < sectors.size(); sec++)
	{
		for(int i = 0; i < sectors[sec].getPoints().size(); i++)
		{
			Point p = sectors[sec].getPoints()[i];

			if(((p.x * lentchGrid) + OffsetX > _r.left()) && ((p.x * lentchGrid) + OffsetX < _r.right()) &&
				(p.y * lentchGrid) + OffsetY > _r.top() && ((p.y * lentchGrid) + OffsetY < _r.bottom()))
			{
				list_curr_sector[sec][i] = true;			
				isActive = true;
				isArrPointMode = false;
				p.col = ColorRGB(0.5, 0.5, 0.5);
				sectors[sec].setPoints(i, p);
			}
		}
	}
}
void drawChar(char *_text)
{
	while(*_text)
		glutBitmapCharacter(GLUT_BITMAP_HELVETICA_12, *_text++);
}

void InitMenu(int i)
{
	switch(i)
	{
	case 0:
		tempPoints.clear();
		break;
	case 1:
		isArrPointMode = true;
		isActive = false;
		defColorSect();
		break;
	case 2:
		isArrPointMode = false;
		isActive = false;
		defColorSect();
		break;
	case 3:
		if(tempPoints.size() > 2)
		{
			Sector *sect = new Sector();
			sect->LoadSector(tempPoints);
			sectors.push_back(*sect);
			delete sect;
			tempPoints.clear();
		}
		break;
	}
}

void Editor::mouse(int button, int state, int _x, int _y)
{
	if(isArrPointMode)
	{
		if(state == GLUT_UP)
		{
			initArrPoint(rect);
			rect = Rect(0,0,0,0);
		}
	}
	else
	{
		if(!isActive)
		{
			if(button == GLUT_LEFT_BUTTON && state == GLUT_UP)
			{
				tempPoints.push_back(Point((mx - OffsetX) / lentchGrid, (my - OffsetY) / lentchGrid));
			}
		}
	}
	isButtonDown = (state == GLUT_DOWN);
}
void Editor::move(int _x, int _y)
{
	mouseX = _x - OffsetX;
	mouseY = _y - OffsetY;

	if(isArrPointMode)
	{
		rect.x2 = _x;
		rect.y2 = _y;
		isVisible = true;
	}
	if(isActive && isButtonDown)
	{
		for(int sec = 0; sec < sectors.size(); sec++)
		{
			for(int i = 0; i < sectors[sec].getPoints().size(); i++)
			{
				if(list_curr_sector[sec][i])
				{
					Point p = sectors[sec].getPoints()[i];
					p.x = ((_x / lentchGrid) - p.x);
					p.y = ((_y / lentchGrid) - p.y);
					sectors[sec].setPoints(i, p);
					//p.y = ((_y - OffsetY) - (p.y * lentchGrid)) / lentchGrid;
				}
			}
		}
	}
}
void Editor::passMove(int _x, int _y)
{
	mouseX = _x - OffsetX;
	mouseY = _y - OffsetY;

	if(isArrPointMode)
	{
		rect.x = _x;
		rect.y = _y;
		isVisible = false;
	}
}
void Editor::keyboard(unsigned char _key, int _x, int _y)
{
	switch(_key)
	{
		case '=':
			if(ScaleGrid < 20)
			{
				ScaleGrid++;
				OffsetX -= _x;
				OffsetY -= _y;
			}
			break;
		case '-':
			if(ScaleGrid > 1)
			{
				ScaleGrid--;
				OffsetX += _x;
				OffsetY += _y;
			}
			break;
	}
}

void Editor::update()
{
	lentchGrid = (ScaleGrid * ScaleStep);
	mx = ((mouseX + lentchGrid / 2) / lentchGrid) * lentchGrid + OffsetX;
	my = ((mouseY + lentchGrid / 2) / lentchGrid) * lentchGrid + OffsetY;
}

void Editor::key(int _key, int _x, int _y)
{
	switch(_key)
	{
	case GLUT_KEY_LEFT:
		OffsetX += 30;
		break;
	case GLUT_KEY_RIGHT:
		OffsetX -= 30;
		break;
	case GLUT_KEY_UP:
		OffsetY += 30;
		break;
	case GLUT_KEY_DOWN:
		OffsetY -= 30;
		break;
	}
}

void Editor::Initialize()
{
	glLoadIdentity();
	glMatrixMode(GL_PROJECTION);
	glOrtho(0, DisplayWidth, DisplayHeight, 0, -10, 10);

	int m = glutCreateMenu(InitMenu);
	glutSetMenu(m);
	glutAddMenuEntry("Очистить буфер точек", 0);
	glutAddMenuEntry("Выбор точек", 1);
	glutAddMenuEntry("Добавление точек", 2);
	glutAddMenuEntry("Добавить сектор", 3);
	glutAttachMenu(GLUT_RIGHT_BUTTON);
}

void drawText()
{
	char str0[8];
	char str1[8];
	char str2[8];
	char str3[8];
	itoa(ScaleGrid, str0, 10);
	itoa(mouseX, str1, 10);
	itoa(mouseY, str2, 10);
	itoa(sectors.size(), str3, 10);

	glColor3f(0.1, 0.7, 0.0);

	glRasterPos2i(10, 20);
	drawChar("gridScale : ");
	glRasterPos2i(10 + (6 * 12), 20);
	drawChar(str0);
	glRasterPos2i(10, 40);
	drawChar("Mouse.X : ");
	glRasterPos2i(10 + (6 * 12), 40);
	drawChar(str1);
	glRasterPos2i(10, 60);
	drawChar("Mouse.Y : ");
	glRasterPos2i(10 + (6 * 12), 60);
	drawChar(str2);
	glRasterPos2i(10, 80);
	drawChar("Sector cout: ");
	glRasterPos2i(15 + (6 * 12), 80);
	drawChar(str3);
}

void Editor::draw() const
{
	glColor3f(0.0, 0.2, 0.1);
	glBegin(GL_QUADS);
	glVertex2i(0, 0);
	glVertex2i(DisplayWidth, 0);
	glVertex2i(DisplayWidth, DisplayHeight);
	glVertex2i(0, DisplayHeight);
	glEnd();

	grid.draw(OffsetX, OffsetY, ScaleGrid, ScaleStep);

	glColor3f(1.0, 1.0, 1.0);

	drawText();

	glBegin(GL_LINE_LOOP);
	for(int i = 0; i < tempPoints.size(); i++)
	{
		glVertex2i((tempPoints[i].x * lentchGrid) + OffsetX, (tempPoints[i].y * lentchGrid) + OffsetY);
	}
	glEnd();
	glPointSize(5);
	glBegin(GL_POINTS);
	for(int i = 0; i < tempPoints.size(); i++)
	{
		glVertex2i((tempPoints[i].x * lentchGrid) + OffsetX, (tempPoints[i].y * lentchGrid) + OffsetY);
	}
	for(int i = 0; i < tempPoints.size(); i++)
	{
		for(int j(0); j < tempArrPoint.size(); j++)
		{
			if(tempArrPoint[j] == i)
			{
				glColor3f(0.0, 0.0, 0.0);
				glVertex2i((tempPoints[i].x * lentchGrid) + OffsetX, (tempPoints[i].y * lentchGrid) + OffsetY);
			}
		}
	}
	if(!isArrPointMode)
	{
		glPointSize(6);
		glColor3f(0.0, 0.5, 0.5);
		glVertex2i(mx, my);
	}
	glEnd();
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	glEnable(GL_BLEND);
	if(isVisible)
		rect.draw();
	for(int i = 0; i < sectors.size(); i++)
	{
		sectors[i].DrawSector(OffsetX, OffsetY, lentchGrid);
	}
	glDisable(GL_BLEND);
	glFinish();
}
