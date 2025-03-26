#include "programEffect.h"
#include "particle.h"
#include "Explode.h"
#include "Painter.h"
#include "Ball.h"
#include "plander.h"
#include "Blook.h"

#include <gl/glut.h>
#include <vector>
#include <cstdlib>
#include <ctime>

using namespace std;

int width = 1000;
int height = 700;
int countArr = -1;
int blookCount = 0;
int currentBlook = 0;
int widBlook = 15;
int heiBlook = 10;
char blooks[] = 
{
	'0','0','g','0','0','0','0','0','g','0','0','g','0','0','g',
	'0','0','0','0','0','0','0','0','0','0','0','0','0','0','0',
	'0','0','0','0','0','0','0','g','0','g','0','0','g','0','0',
	'0','0','0','0','g','g','0','0','0','0','0','0','0','0','g',
	'0','b','0','g','g','g','g','g','g','g','0','0','0','g','0',
	'0','0','0','0','g','g','0','0','0','0','0','0','0','0','g',
	'0','0','0','0','g','g','0','0','0','0','0','0','0','0','g',
	'0','0','0','0','g','g','0','0','0','0','0','0','0','0','g',
	'0','0','0','0','b','b','b','0','0','0','0','0','0','0','g',
	'0','0','0','g','g','g','0','0','0','0','0','b','b','b','g'
};
bool stopBlooks[] = 
{
	0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
	0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
	0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
	0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
	0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
	0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
	0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
	0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
	0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
	0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
};

bool isactive;
//float speed = 0.7f;
float refresh = 300;
float rotSphere = 0;
float rad = 45;

struct Pixel
{
	float x;
	float y;
	float z;
	Pixel(const float _x, const float _y, const float _z) : x(_x), y(_y), z(_z) {}
};

GLubyte foull[] = 
{
	0x00,0x0f,0x00,0x00,0x00,0x0f,0x00,0x00,
	0x00,0x0f,0x00,0x00,0x00,0x0f,0x00,0x00,
	0x00,0x0f,0x00,0x00,0x00,0x0f,0x00,0x00,
	0x00,0x0f,0x00,0x00,0x00,0x0f,0x00,0x00,
	0x00,0x0f,0x00,0x00,0x00,0x0f,0x00,0x00,
	0x00,0x0f,0x00,0x00,0x00,0x0f,0x00,0x00,
	0x00,0x0f,0x00,0x00,0x00,0x0f,0x00,0x00,
	0x00,0x0f,0x00,0x00,0x00,0x0f,0x00,0x00,
	0x00,0x00,0x00,0x00,0x00,0x0f,0x00,0x00,
	0x00,0x00,0x00,0x00,0x00,0x0f,0x00,0x00,
	0x00,0x00,0x00,0x00,0x00,0x0f,0x00,0x00,
	0x00,0x00,0x00,0x00,0x00,0x0f,0x00,0x00,
	0x00,0x00,0x00,0x00,0x00,0x0f,0x00,0x00,
	0x00,0x00,0x00,0x00,0x00,0x0f,0x00,0x00,
	0x00,0x00,0x00,0x00,0x00,0x0f,0x00,0x00
	
};

vector<Pixel> part;
vector<Pixel2> part2;
vector<Pixel2> part3;
vector<Explode> e(10, Explode(26, 1.5f));
Painter paint;
short Painter::wid = 20;
short Painter::hei = 20;
Ball ball = Ball(4, 16);
plander plane;
Blook *blook;
bool intersectBlooks(Ball &_b, const Blook *_bl);
//initPoint2(18, part3);

int radiusPlane(const Ball &b,const plander &p)
{
	int center = (p.getLenth() / 2);
	int temp = b.X() - p.getX() - center;


	return temp;

}
float RadiusArea(const float _radius, const char _coordinates)
{
	double alpha = ((_radius) * 3.1415 / 180);
	double rads = sin(alpha) * ball.getSpeed();
	double radc = cos(alpha) * ball.getSpeed();

	switch(_coordinates)
	{
	case 'x':
		return rads;
		break;
	case 'y':
		return radc;
		break;
	default:
		return 0;
		break;
	}
}

Ball angleIntersectionBall(const plander &p)
{
	Ball b = ball;

	if(b.X() + b.getSize() > width / 2)
	{
		b.vx = -b.vx;
	}
	else if(b.X() < 0)
	{
		b.vx = -b.vx;
	}
	if(b.Y() + b.getSize() > p.getY() && b.X() >= p.getX() - b.getSize()
			&& b.X() <= p.getX() + p.getLenth())
	{
		float r = radiusPlane(b, p);

		b.setRadian(b.getRad() + r - 40);
		
		b.vx = b.vx;

		b.vy = -b.vy;
	}
 	else if(b.Y() + b.getSize() > height / 2)
	{
		b.vy = -b.vy;
	}
	else if(b.Y() < 0)
	{
		b.vy = -b.vy;
	}

	b.UpdateBall();
	return b;
}

int currentBall(const int _bx, const int _by, const int _x, const int _y, const int _stepx, const int _stepy)
{
	for(int y = 0; y < _y; y++)
	{
		for(int x = 0; x < _x; x++)
		{
			if((_bx > x * _stepx) && (_bx < (x + 1) * _stepx) && (_by > y * _stepy) && (_by < (y + 1) * _stepy))
			{
				//paint.DrawRect(x * _stepx, y * _stepy, _stepx, _stepy, Painter::Blue);
				return (x + (y * _x));
			}
		}
	}
}

void UpdateDestroyBlook()
{
	if(ball.vx > 0 && ball.vy > 0)
	{
		currentBlook = currentBall(ball.X() + ball.getSize(), ball.Y() + ball.getSize(), 
							   widBlook, heiBlook, blook[0].getW() + 2, blook[0].getH() + 2);
	}
	else if(ball.vx < 0 && ball.vy < 0)
	{
		currentBlook = currentBall(ball.X(), ball.Y(), widBlook, heiBlook, blook[0].getW() + 2, blook[0].getH() + 2);
	}
	else if(ball.vx < 0 && ball.vy > 0)
	{
		currentBlook = currentBall(ball.X(), ball.Y() + ball.getSize(), 
			widBlook, heiBlook, blook[0].getW() + 2, blook[0].getH() + 2);
	}
	else if(ball.vx > 0 && ball.vy < 0)
	{
		currentBlook = currentBall(ball.X() + ball.getSize(), ball.Y(), 
			widBlook, heiBlook, blook[0].getW() + 2, blook[0].getH() + 2);
	}
	if(intersectBlooks(ball, blook))
	{
		for(int i = 0; i < blookCount; i++)
		{
			if(blook[i].Index() == currentBlook)
			{
				int cur_counter = blook[i].getCounter();
				cur_counter++;
				

				blook[i].setCounter(cur_counter);

				if(blook[i].getEndCounter() == blook[i].getCounter())
				{
					blook[i].setIsAlive(false);
					e[1].initExplodePosition(blook[i].getX() + (blook[i].getW() / 2 + 20), 
						blook[i].getY() + (blook[i].getH() / 2 + 15), 0, blook[i].getW());
				}

				break;
			}
		}

		ball.UpdateBall();

	}
	else
	{
		ball = angleIntersectionBall(plane);
	}
	for(int i = 0; i < blookCount; i++)
	{
		if(blook[i].getIsAlive())
			blook[i].draw(paint);
	}
}

void display()
{
	glClear(GL_COLOR_BUFFER_BIT);

	UpdateDestroyBlook();

	ball.Draw(paint);

	plane.Draw(paint);
	glColor3f(0.5, 0.5, 0.2);
	//glRotatef(rotSphere, 1.0, 0.5, 0.2);
	//glTranslatef(0.1,0.1, 0.0);
	//glutWireSphere(10, 20, 10);
	/*
	glColor3f(1.0, 1.0, 1.0);
	glPointSize(1);
	glBegin(GL_POINTS);
	for(int i = 0; i < part.size(); i++)
	{
		part[i].z += speed;
		glVertex3f(part[i].x, part[i].y, part[i].z);
		if(part[i].z > refresh)
		{
			Pixel &p = Pixel(rand() % width / 5, rand() % height / 4, rand() % 400 - 400);
			part[i] = p;
		}
	}
	glEnd();
	glPointSize(3);
	glBegin(GL_POINTS);
	for(int i = 0; i < part2.size(); i++)
	{
		glColor3f(part2[i].getColor()[0], part2[i].getColor()[1], part2[i].getColor()[2]);
		part2[i].z += speed;
		glVertex3f(part2[i].x, part2[i].y, part2[i].z);
		if(part2[i].z > refresh)
		{
			Pixel2 &p = Pixel2(rand() % width / 5, rand() % height / 4, rand() % 400 - 400, Pixel2::red);
			part2[i] = p;
		}
	}
	glEnd();
	glPointSize(5);
	glBegin(GL_POINTS);
	for(int i = 0; i < part3.size(); i++)
	{
		glColor3f(part3[i].getColor()[0], part3[i].getColor()[1], part3[i].getColor()[2]);
		part3[i].z += speed;
		glVertex3f(part3[i].x, part3[i].y, part3[i].z);
		if(part3[i].z > refresh)
		{
			Pixel2 &p = Pixel2(rand() % width / 5, rand() % height / 4, rand() % 400 - 400, Pixel2::green);
			part3[i] = p;
		}
	}
	glEnd();
	*/
	//glEnable(GL_POLYGON_STIPPLE);
	//glPolygonStipple(foull);
	//glRectf(0, 0, 5, 5);
	//glDisable(GL_POLYGON_STIPPLE);
	for(int i = 0; i < e.size(); i++)
	{
		e[i].display();
	}		

	glutSwapBuffers();
}

void timer(int = 0)
{
	glutPostRedisplay();
	glutTimerFunc(30, timer, 0);
}

void mouse(int x, int y)
{
	if(plane.getX() + plane.getLenth() / 2 > (x / 2) && plane.getX() > 0)
		plane.MovePlane(plander::Left);
	if(plane.getX() + plane.getLenth() / 2 < (x / 2) && plane.getX() < width / 2 - plane.getLenth())
		plane.MovePlane(plander::Right);
}

void m(int button, int state, int x, int y)
{
	if(state == GLUT_DOWN)
	{
		if(countArr > 8)
			countArr = -1;
		countArr++;
		//arr = arrayPoints(width, height);
		//arr.initPosition(vx, vy);
		//arr.isActive = true;
		e[countArr].initExplodePosition(x / 2, y / 2, 0, 8);
	}
}

void initPoint(const int count)
{
	for(int i = 0; i < count; i++)
	{
		Pixel &p = Pixel(rand() % width / 5, rand() % height / 4, rand() % 400 - 400);
		part.push_back(p);
	}
}
void initPoint2(const int count, vector<Pixel2> &p)
{
	for(int i = 0; i < count; i++)
	{
		Pixel2 p1 = Pixel2(rand() % width / 5, rand() % height / 4, rand() % 400 - 400, Pixel2::red);
		p.push_back(p1);
	}
}

bool intersectBlooks(Ball &_b, const Blook *_bl)
{
	bool tempFlag = false;
	int i = 0;
	Blook bb;

	for(int j = 0; j < widBlook * heiBlook; j++)
	{
		i = j;
		if(stopBlooks[j] && currentBlook == j)
		{
			tempFlag = true;
			break;
		}
	}

	for(int cout = 0; cout < blookCount; cout++)
	{
		if(_bl[cout].Index() == i)
		{
			bb = _bl[cout];
			break;
		}
	}

	if(tempFlag && bb.getIsAlive())
	{

		if(_b.Bottom() > bb.Top() && _b.Top() < bb.Top())
		{
			_b.vy = -_b.vy;
			paint.DrawRect(bb.getX() - 2, bb.getY() - 2, bb.getW() + 4, bb.getH() + 4, Painter::White);
		}
		else if(_b.Top() < bb.Bottom() && _b.Bottom() > bb.Bottom())
		{
			_b.vy = -_b.vy;
			paint.DrawRect(bb.getX() - 2, bb.getY() - 2, bb.getW() + 4, bb.getH() + 4, Painter::White);
		}
		else if(_b.Right() > bb.Left() && _b.Left() < bb.Left())
		{
			_b.vx = -_b.vx;
			paint.DrawRect(bb.getX() - 2, bb.getY() - 2, bb.getW() + 4, bb.getH() + 4, Painter::White);
		}
		else if(_b.Left() < bb.Right() && _b.Right() > bb.Right())
		{
			_b.vx = -_b.vx;
			paint.DrawRect(bb.getX() - 2, bb.getY() - 2, bb.getW() + 4, bb.getH() + 4, Painter::White);
		}
	}

	return tempFlag;
}
void initBlooks(int _cw, int _ch, const char *_char)
{
	int wid = (width / 2) / _cw;
	int hei = (height / 2 - 150) / _ch;

	int cout = 0;
	for(int i = 0; i < (_cw * _ch); i++)
	{
		if(_char[i] != '0')
			cout++;
	}
	blookCount = cout;
	blook = new Blook[cout];

	int ccout = 0;
	int ind = 0;

	for(int y = 0; y < _ch; y++)
	{
		for(int x = 0; x < _cw; x++)
		{
			switch(_char[x + (y * _cw)])
			{
			case 'b':
				blook[ccout] = Blook(x * wid, y * hei, wid -2, hei - 2, Painter::Blue, ind, 20);
				stopBlooks[x + (y * widBlook)] = true;
				ccout++;
				break;
			case 'g':
				blook[ccout] = Blook(x * wid, y * hei, wid - 2, hei - 2, Painter::Green, ind, 5);
				stopBlooks[x + (y * widBlook)] = true;
				ccout++;
				break;
			case 'r':
				blook[ccout] = Blook(x * wid, y * hei, wid - 2, hei - 2, Painter::Red, ind, 1);
				stopBlooks[x + (y * widBlook)] = true;
				ccout++;
				break;
			}
			ind++;
		}
	}
}
void initialize()
{
	glClearColor(0.0, 0.0, 0.0, 1.0);
	glMatrixMode(GL_PROJECTION);
	glMatrixMode(GL_MODELVIEW);

	glLoadIdentity();
	//gluOrtho2D(0, 20, 20, 0);
	glOrtho(0, width / 2, height / 2, 0, -100, 100);
	//gluPerspective(45, width / height, 0.1, 500);
	//glFrustum(-1, 1, -1, 1, 1.5, 300.0);
	//glLoadIdentity();
	//gluLookAt(0,0,100,0,0,0, 0,1,0);

	srand(time(0));
	//arr = arrayPoints(100, 0, 0, 8, 1);
	glTranslatef(0, 0, 0);
	plane = plander(((width / 2) / 2 - 50), (height / 2 - 20), 3, 0, 100);
	initBlooks(widBlook, heiBlook, blooks);
		
	initPoint(500);
	initPoint2(40, part2);
	for(int i = 0; i < e.size(); i++)
		e[i].SetGravity(true, 0.007);
}

int main(int argc, char **argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB | GLUT_DEPTH);
	glutInitWindowSize(width, height);
	glutInitWindowPosition(100, 100);
	glutCreateWindow("effects");

	initialize();
	
	glutDisplayFunc(display);
	glutMouseFunc(m);
	glutPassiveMotionFunc(mouse);
	timer();

	glutMainLoop();
}
