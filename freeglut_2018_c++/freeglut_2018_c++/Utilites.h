#pragma once
#include "gl/freeglut.h"
#include "Settings.h"
#include <iostream>
#include <cmath>

//#pragma comment(lib, "gl/freeglut.lib")

struct color
{
	int r, g, b;
	color() : r(255), g(255), b(255) {}
	color(int _r, int _g, int _b) : r(_r), g(_g), b(_b) {}
};

struct point4
{
	float x, y, z, w;
	point4() : x(0), y(0), z(0), w(1) {}
	point4(float _x, float _y, float _z, float _w = 1) : x(_x), y(_y), z(_z), w(_w) {}
	~point4() {}
};

struct point3
{
	float x, y, z;
	point3() : x(0), y(0), z(0) {}
	point3(float _x, float _y, float _z) : x(_x), y(_y), z(_z) {}
	~point3() {}
};

struct point
{
	float x, y;
	point() : x(0), y(0) {}
	point(float _x, float _y) : x(_x), y(_y) {}
	~point(){}
};

class quaternion
{
public: double r, i, j, k;

public: quaternion() : r(0), i(0), j(0), k(0) { }
public: quaternion(double _r, double _i, double _j, double _k) : r(_r), i(_i), j(_j), k(_k) { }

public: double mag() { return sqrt(r * r + i * i + j * j + k * k); }
public: quaternion inverse()
{
	double _mag = mag();
	return quaternion(r / _mag, -i / _mag, -j / _mag, -k / _mag);
}
public: quaternion unit()
{
	double _mag = mag();
	return quaternion(r, i / _mag, j / _mag, k / _mag);
}
public: quaternion mul(quaternion q) {
	double _r = r*q.r - i*q.i - j*q.j - k*q.k;
	double _i = r*q.i + i*q.r + j*q.k - k*q.j;
	double _j = r*q.j - i*q.k + j*q.r + k*q.i;
	double _k = r*q.k + i*q.j - j*q.i + k*q.r;
	return quaternion(_r, _i, _j, _k);
}
};

#define RAD_IN_DEG 0.017453292519943295769
#define PI 3.14159265359

class polygon : public point4
{
private: point4 p[4];
private: int size;

public : polygon() : size(100)
{
	p[0] = point4(x, y, z, w);			        p[1] = point4(x + size, y, z, w);
	p[2] = point4(x + size, y + size, z, w);	p[3] = point4(x, y + size, z, w);
}

public: polygon(point4 _p, int _size) : point4(_p.x, _p.y, _p.z, _p.w), size(_size)
{
	p[0] = point4(x, y, z, w);			        p[1] = point4(x + size, y, z, w);
	p[2] = point4(x + size, y + size, z, w);	p[3] = point4(x, y + size, z, w);
}

public:polygon(point4 _points1, point4 _points2, point4 _points3, point4 _points4) : size(100)
{
	p[0] = _points1;
	p[1] = _points2;
	p[2] = _points3;
	p[3] = _points4;
}

public: void rotQuaternion(double _ang, double _x, double _y, double _z)
{
	/*кватернионы*/
	double phi = 3.141592653589793238462643 / 180.0 * _ang;
	double cosphi = cos(phi / 2.0);
	double sinphi = sin(phi / 2.0);
	quaternion _q = quaternion(cosphi, _x*sinphi, _y*sinphi, _z*sinphi).unit();
	////quaternion _q = quaternion(cosphi, i*sinphi, j*sinphi, k*sinphi).unit();
	quaternion _p;
	/*кватернионы*/

	for (size_t ind = 0; ind < 4; ind++)
	{
		_p = quaternion(0.0, p[ind].x, p[ind].y, p[ind].z);
		_p = _q.mul(_p.mul(_q.inverse()));
		p[ind].x = (float)_p.i;
		p[ind].y = (float)_p.j;
		p[ind].z = (float)_p.k;
	}
}

public: inline void DrawQuad(color _c)
{
	glColor3ub(_c.r, _c.g, _c.b);
	glBegin(GL_QUADS);
	glVertex3f(p[0].x, p[0].y, p[0].z); glVertex3f(p[1].x, p[1].y, p[1].z);
	glVertex3f(p[2].x, p[2].y, p[2].z); glVertex3f(p[3].x, p[3].y, p[3].z);
	glEnd();
}
public: inline void DrawQuad()
{	
	glBegin(GL_QUADS);
	glColor3ub(190, 50, 100);
	glVertex3f(p[0].x, p[0].y, p[0].z); 
	glColor3ub(90, 150, 150);
	glVertex3f(p[1].x, p[1].y, p[1].z);
	glColor3ub(220, 255, 100);
	glVertex3f(p[2].x, p[2].y, p[2].z);
	glColor3ub(40, 50, 250);
	glVertex3f(p[3].x, p[3].y, p[3].z);
	glEnd();
}
public: inline void rotY(double _rad)
{
	float cos_a = cos(_rad * RAD_IN_DEG);
	float sin_a = sin(_rad * RAD_IN_DEG);

	for (size_t i = 0; i < 4; i++)
	{
		p[i] = point4(p[i].x * cos_a + p[i].z * sin_a,
					  p[i].y,
					  -p[i].x * sin_a + p[i].z * cos_a,
					  0);
	}
}
public: inline void rotX(double _rad)
{
	float cos_a = cos(_rad * RAD_IN_DEG);
	float sin_a = sin(_rad * RAD_IN_DEG);

	for (size_t i = 0; i < 4; i++)
	{
		p[i] = point4(p[i].x,
					  p[i].y * cos_a + p[i].z * sin_a,
					  -p[i].y * sin_a + p[i].z * cos_a,
					  0);
	}
}
public: inline void rotZ(double _rad)
{
	float cos_a = cos(_rad * RAD_IN_DEG);
	float sin_a = sin(_rad * RAD_IN_DEG);

	for (size_t i = 0; i < 4; i++)
	{
		p[i] = point4(p[i].x * cos_a - p[i].y * sin_a,
					  p[i].x * sin_a + p[i].y * cos_a,
					  p[i].z,
					  0);
	}
}

	 ~polygon() {}
};

class polygonGl
{
private:	point3 points[4];
private:	point3 pos;
public:		point3 g_pos;
private:	point4 rot;

public: polygonGl() : pos()
{
	float _size = 1;
	points[0] = point3(pos.x, pos.y, pos.z);					points[1] = point3(pos.x + _size, pos.y, pos.z);
	points[2] = point3(pos.x + _size, pos.y + _size, pos.z);	points[3] = point3(pos.x, pos.y + _size, pos.z);
}

public: polygonGl(point3 _pos, float _size) : pos(_pos)
{
	points[0] = point3(pos.x, pos.y, pos.z);					points[1] = point3(pos.x + _size, pos.y, pos.z);
	points[2] = point3(pos.x + _size, pos.y + _size, pos.z);	points[3] = point3(pos.x, pos.y + _size, pos.z);
}
public:polygonGl(point3 _points1, point3 _points2, point3 _points3, point3 _points4)
{
	points[0] = _points1;
	points[1] = _points2;
	points[2] = _points3;
	points[3] = _points4;
}

		//поворот по оси _vect на угол _ang
public: inline void Rot(point4 _r) 
{ 
	rot = point4(_r);
}

public: inline void Draw(color _col)
{
	glColor3ub(_col.r, _col.g, _col.b);
	glPushMatrix();
	glTranslatef(g_pos.x, g_pos.y, g_pos.z);
	glRotatef(rot.x, 1, 0, 0);
	glRotatef(rot.y, 0, 1, 0);
	glRotatef(rot.z, 0, 0, 1);
	glBegin(GL_QUADS);
	glVertex3f(points[0].x, points[0].y, points[0].z);	
	glVertex3f(points[1].x, points[1].y, points[1].z);
	glVertex3f(points[2].x, points[2].y, points[2].z);
	glVertex3f(points[3].x, points[3].y, points[3].z);
	glEnd();	
	glPopMatrix();
}
};

class camera : public point3
{
		 // координаты вектора направления движения камеры
private: float dir_x, dir_y, dir_z;
		 // угол поворота камеры
private: float anglex, angley;
		 //скорость поворота камеры и перемещения
private: float rot_Speed, move_Speed;
		 //нажата клавиша или нет
private: float deltaMove, delta_x, delta_y;
		 //старые координаты x и y
private: int old_x, old_y;
		 //стрейф в сторону
private: float streffe_x, streffe_speed;
		 //активировать блокировку мыши
private: bool isActiveMouseCrosshair;

public: camera() : anglex(0.0f), angley(0.0f), rot_Speed(0.005f), move_Speed(0.5), point3(0, 0, -1)
{
	dir_x = 0;
	dir_y = 0;
	dir_z = 0;
	delta_x = 0;
	delta_y = 0;
	deltaMove = 0;
	old_x = -1;
	old_y = -1;
	streffe_speed = 0.1f;

	HWND window_header = GetConsoleWindow();
	SetWindowPos(window_header, HWND_TOP, 0, 0, 450, 300, NULL);

	isActiveMouseCrosshair = false;
}
public: camera(point3 _pos) : anglex(0.0f), angley(0.0f), rot_Speed(0.005f), move_Speed(0.5), point3(_pos)
{
	dir_x = -0.3;
	dir_y = -0.3;
	dir_z = 10;
	delta_x = 0;
	delta_y = 0;
	deltaMove = 0;
	old_x = -1;
	old_y = -1;
	streffe_speed = 0.1f;

	HWND window_header = GetConsoleWindow();
	SetWindowPos(window_header, HWND_TOP, 0, 0, 450, 300, NULL);

	isActiveMouseCrosshair = false;
}

public: void calcCam()
{
	dir_x = sin(anglex);
	dir_y = tan(angley);
	dir_z = cos(anglex);

	x += (deltaMove * dir_x) * 0.1;
	y += (deltaMove * dir_y) * 0.1;
	z += (deltaMove * dir_z) * 0.1;

	x += streffe_x * sin(anglex + (90 * PI / 180));
	z += streffe_x * cos(anglex + (90 * PI / 180));
}

public: void draw()
{
	calcCam();
	
	if (isActiveMouseCrosshair)
	{
		glutSetCursor(GLUT_CURSOR_CROSSHAIR);
		glutWarpPointer(Wid / 2, Hei / 2);
	}
	else
	{
		glutSetCursor(GLUT_CURSOR_INHERIT);
	}
	glLoadIdentity();
	// установить камеру
	gluLookAt(x, y, z, x + dir_x, y + dir_y, z + dir_z, 0, 1, 0);
}

public: void processNormalKeys(unsigned char key, int xx, int yy) 
{
	if (key == 27)
		exit(0);

	if (key == 'w')
		deltaMove = move_Speed;
	if (key == 's')
		deltaMove = -move_Speed;
	if (key == 'a')
		streffe_x = streffe_speed;
	if (key == 'd')
		streffe_x = -streffe_speed;
}
public: void releaseNormalKeys(unsigned char key, int xx, int yy)
{
	deltaMove = 0;
	streffe_x = 0;
}

public: void pressKey(int key, int xx, int yy) 
{
	switch (key) 
	{
	case GLUT_KEY_UP:
		deltaMove = move_Speed;
		break;
	case GLUT_KEY_DOWN:
		deltaMove = -move_Speed;
		break;
	}
}

public: void releaseKey(int key, int xx, int yy)
{
	switch (key) 
	{
	case GLUT_KEY_UP:
	case GLUT_KEY_DOWN:
		deltaMove = 0;
		break;
	}
}
public:	void mouseMove(int xx, int yy)
{
	if (isActiveMouseCrosshair)
	{
		////////////////////////////
		if ((xx - old_x) > 0)
			delta_x = -rot_Speed;
		else if ((xx - old_x) < 0)
			delta_x = rot_Speed;
		else delta_x = 0;
		////////////////////////////
		if ((yy - old_y) > 0)
			delta_y = -rot_Speed;
		else if ((yy - old_y) < 0)
			delta_y = rot_Speed;
		else delta_y = 0;
		////////////////////////////
		anglex += delta_x;
		angley += delta_y;

		old_x = xx;
		old_y = yy;
	}
}
public:	void mouseButton(int button, int state, int xx, int yy)
{
	if (button == GLUT_LEFT_BUTTON)
	{
		if (state == GLUT_UP)
		{
			isActiveMouseCrosshair = false;
			printf("x = (%.3f) y = (%.3f) z = (%.3f)\n", x, y, z);
		}
		else
		{
			isActiveMouseCrosshair = true;
		}
	}
}
public: void passMouse(int _x, int _y)
{

}
};