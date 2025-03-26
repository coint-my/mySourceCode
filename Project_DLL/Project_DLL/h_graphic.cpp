#include "h_graphic.h"

namespace graphic
{
	void Painter::Initialize2D(int _wid, int _hei, int &_argc, char** _argv, char* _nameWindow, void(*render)(), void(*mouse)(int _b, int _s, int _x, int _y))
	{
		glutInit(&_argc, _argv);
		glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA);
		glutInitWindowPosition(100, 100);
		glutInitWindowSize(_wid, _hei);
		glutCreateWindow(_nameWindow);

		glClearColor(0.0, 0.0, 0.0, 1.0);
		glMatrixMode(GL_PROJECTION);
		glLoadIdentity();
		glOrtho(0, _wid, _hei, 0, -1, 1);
		
		glutDisplayFunc(render);
		glutMouseFunc(mouse);

		glutMainLoop();
	}

	void Painter::InitUpdate(int _time, void(*update)(int _t))
	{
		glutTimerFunc(_time, update, 1);
	}

	void Painter::ReDraw(int _time, void(*update)(int _t))
	{
		glutPostRedisplay();
		glutTimerFunc(_time, update, 1);
	}

	void Painter::DrawRect(Recti &_r, Color &_c)
	{
		glColor3ub(_c.r, _c.g, _c.b);
		glRecti(_r.GetX(), _r.GetY(), _r.GetX() + _r.GetWid(), _r.GetY() + _r.GetHei());
	}

	void Painter::DrawCircle(Circlef &_c, Color &_col)
	{
		glColor3ub(_col.r, _col.g, _col.b);

		glBegin(GL_POLYGON);
		float xx = 0, yy = 0;
		for (int i = 0; i < _c.GetSegment(); i++)
		{
			xx = sin(i * (PI * 2) / _c.GetSegment()) * _c.GetRad();
			yy = cos(i * (PI * 2) / _c.GetSegment()) * _c.GetRad();

			glVertex2f(xx + _c.GetPos().x, yy + _c.GetPos().y);
		}
		glEnd();
	}

	void Painter::Start()
	{
		glClear(GL_COLOR_BUFFER_BIT);
	}

	void Painter::End()
	{
		glutSwapBuffers();
	}

	void Circlef::Init(const Vector2f &_pos, const int _rad, const int _seg)
	{
		pos.x = _pos.x;
		pos.y = _pos.y;
		rad = _rad;
		seg = _seg;
	}

	bool Recti::Intersect(Recti &_r)
	{
		if((Right() > _r.Left()) && (Left() < _r.Right()) && (Bottom() > _r.Top()) && (Top() < _r.Bottom()))
			return true;
		else
			return false;
	}

	const Recti &Recti::operator=(const Recti &_r)
	{
		pos.x = _r.pos.x;
		pos.y = _r.pos.y;
		wid = _r.wid;
		hei = _r.hei;
		return *this;
	}
}