#include <iostream>
#include <cmath>
#include <vector>
#include "gl/glut.h"

#define WID 800
#define HEI 600

int mousex = WID / 2;
int mousey = HEI / 2;

struct vec2
{
	float x, y;

	vec2() { x = 0; y = 0; }
	vec2(float _x, float _y) : x(_x), y(_y) { }

	float inline distance(vec2 &_v)
	{
		float t = abs((x - _v.x) * (x - _v.x) + (y - _v.y) * (y - _v.y));
		return t;
	}
	static float inline dist_sqrt(vec2 &_v1, vec2 &_v2)
	{
		return sqrt((_v1.x - _v2.x) * (_v1.x - _v2.x) + (_v1.y - _v2.y) * (_v1.y - _v2.y));
	}
};

struct element
{
	vec2 pos;
	vec2 vel;
};

struct roupe
{
	int count;
	std::vector<element> el;
	roupe(int _count) : count(_count)
	{
		for (size_t i = 0; i < count; i++)
		{
			element part = { vec2(WID/2+i*30, HEI/2+i*20), vec2() };
			el.push_back(part);
		}
	}
	virtual void draw()
	{
		glBegin(GL_LINE_STRIP);

		for (size_t i = 0; i < count; i++)
		{
			glVertex2f(el[i].pos.x, el[i].pos.y);
		}

		glEnd();
	}

	virtual void update()
	{
		float speed = 30;

		for (size_t i = 1; i < count; i++)
		{
			element &_p1 = el[i-1];
			element &_p2 = el[i];

			float dis = vec2::dist_sqrt(_p1.pos, _p2.pos);

			float a = 0;
			float rast = 1;
			float ax = 0;
			float ay = 0;

			if (dis > rast)
			{
				a = 0.0005 * (dis - rast);
				//a = 1;
			}
			else if(dis < 0.01)
			{
				a = 0.0005 * (0.01 - dis);
				//a = -1;
			}

			/*ax = a * (_p1.pos.x - _p2.pos.x) / dis;
			ay = a * (_p1.pos.y - _p2.pos.y) / dis;*/

			ax = a * ((_p1.pos.x - _p2.pos.x)) / dis;
			ay = a * ((_p1.pos.y - _p2.pos.y)) / dis;

			_p2.vel.x += ax * speed;
			_p2.vel.y += ay * speed;

			if (i != 1)
			{
				_p1.vel.x -= ax * speed;
				_p1.vel.y -= ay * speed;
			}
		}
		el[count - 1].vel.y += 0.01;
		el[0].pos.x = mousex;
		el[0].pos.y = mousey;

		for (size_t i = 1; i < count; i++)
		{
			el[i].vel.x *= 0.97;
			el[i].vel.y *= 0.97;
			el[i].pos.x += el[i].vel.x * speed;
			el[i].pos.y += el[i].vel.y * speed;
		}
	}

	virtual ~roupe()
	{
		el.clear();
	}
};

struct myRoupe : public roupe
{
	myRoupe(int _count) : roupe(_count) { }

	virtual void update() override 
	{
		for (size_t i = 1; i < count; i++)
		{
			element &_e1 = el[i - 1];
			element &_e2 = el[i];

			float xx = 0;
			float yy = 0;
			float dis = vec2::dist_sqrt(_e1.pos, _e2.pos);
			float a = 0;

			if (dis > 50)
			{
				_e2.vel.x = (_e1.pos.x - _e2.pos.x) / 3;
				_e2.vel.y = (_e1.pos.y - _e2.pos.y) / 3;
			}
			else if (dis < 50)
			{
				_e2.vel.x = 0;
				_e2.vel.y = 0;
			}
			el[i].vel.y += 5;
		}

		//el[count - 1].vel.y += 0.3;

		el[0].pos.x = mousex;
		el[0].pos.y = mousey;

		for (size_t i = 0; i < count; i++)
		{
			el[i].pos.x += el[i].vel.x;
			el[i].pos.y += el[i].vel.y;
		}
	}
};

//roupe *rr = new roupe(10);
myRoupe* r = new myRoupe(10);

void render()
{
	glClear(GL_COLOR_BUFFER_BIT);

	r->draw();

	glutSwapBuffers();
}

void update(int = 0)
{
	r->update();
	glutPostRedisplay();
	glutTimerFunc(1, update, 0);
}

void mouse(int _b, int _s, int _x, int _y)
{
	if (_b == GLUT_LEFT_BUTTON && _s == GLUT_DOWN)
	{
		mousex = _x;
		mousey = _y;
	}
}

void mousepress(int _x, int _y)
{
	mousex = _x;
	mousey = _y;
}

int main(int argc, char** argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA);
	glutInitWindowPosition(100, 100);
	glutInitWindowSize(WID, HEI);
	glutCreateWindow("test_pointer");

	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluOrtho2D(0, WID, HEI, 0);
	glClearColor(0, 0, 0, 1);

	glutDisplayFunc(render);
	glutMouseFunc(mouse);
	glutMotionFunc(mousepress);
	glutTimerFunc(1, update, 0);

	glutMainLoop();

	return 0;
}