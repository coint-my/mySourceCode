#include <iostream>
#include <cmath>
#include <gl/glut.h>

#pragma comment(lib, "opengl32.lib")
#pragma comment(lib, "glut32.lib")

GLuint lst;

typedef struct Point
{
	double x, y;
}point;

GLubyte raster_H[24] = 
{
	0xC0, 0xC0,
	0xC0, 0xC0,
	0xC0, 0xC0,
	0xC0, 0xC0,
	0xC0, 0xC0,
	0xFF, 0xC0,
	0xFF, 0xC0,
	0xC0, 0xC0,
	0xC0, 0xC0,
	0xC0, 0xC0,
	0xC0, 0xC0,
	0xC0, 0xC0
};
GLubyte raster_E[24] = 
{
	0xFF, 0xC0,
	0xFF, 0xC0,
	0xC0, 0x00,
	0xC0, 0x00,
	0xC0, 0x00,
	0xFF, 0x00,
	0xFF, 0x00,
	0xC0, 0x00,
	0xC0, 0x00,
	0xC0, 0x00,
	0xFF, 0xC0,
	0xFF, 0xC0
};
GLubyte raster_T[13] = 
{
	0x00,0x00,0xfe,0xc7,0xc3,0xc3,0xc7,0xfe,0xc7,0xc3,0xc3,0xc7,0xfe
};

void DrawCircle(const int _rad, const int _seg, const int _curr)
{
	glNewList(lst + _curr, GL_COMPILE);
		glLineWidth(2.0);
		glBegin(GL_LINE_LOOP);
			for (int i = 0; i < _seg; i++)
			{
				const double r = i * (3.141592 * 2) / _seg;
				double x = sin(r) * _rad;
				double y = cos(r) * _rad;
				glVertex2d(x, y);
			}
		glEnd();
	glEndList();
}

void initialize()
{
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(0, 600, 400, 0, -100, 100);
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
	glClearColor(0.0, 0.0, 0.0, 1.0);

	lst = glGenLists(5);

	DrawCircle(50, 4, 0);
	DrawCircle(50, 6, 1);
	DrawCircle(50, 10, 2);
	DrawCircle(50, 16, 3);
	DrawCircle(50, 32, 4);

	glPixelStorei(GL_UNPACK_ALIGNMENT, 1);
}

void display()
{
	glClear(GL_COLOR_BUFFER_BIT);

	glColor3ub(0, 50, 200);

	glTranslatef(10, 10, 0);
	glRecti(0, 0, 50, 30);
	glTranslatef(-10, -10, 0);

	glColor3ub(255, 0, 0);
	glRasterPos2i(20, 20);
	glBitmap(12, 12, 0.0, 12.0, 13.0, 0.0, raster_H);
	glBitmap(12, 12, 0.0, 12.0, 13.0, 0.0, raster_E);
	glBitmap(8, 13, 9.0, 0.0, 0.0, 0.0, raster_T);

	glTranslated(100.0, 100.0, 0.0);
	glCallList(lst);
	glTranslated(-100.0, -100.0, 0.0);
	glTranslated(200.0, 100.0, 0.0);
	glCallList(lst+1);
	glTranslated(-200.0, -100.0, 0.0);
	glTranslated(300.0, 100.0, 0.0);
	glCallList(lst+2);
	glTranslated(-300.0, -100.0, 0.0);
	glTranslated(400.0, 100.0, 0.0);
	glCallList(lst+3);
	glTranslated(-400.0, -100.0, 0.0);
	glTranslated(500.0, 100.0, 0.0);
	glCallList(lst+4);
	glTranslated(-500.0, -100.0, 0.0);

	glFlush();
}

void update(int = 0)
{
	glutPostRedisplay();
	glutTimerFunc(50, update, 0);
}

void reshape(int _w, int _h)
{
	glViewport(0, 0, (GLsizei)_w, (GLsizei)_h);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(0, _w, _h, 0, -100, 100);
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
}

int main(int argv, char** argc)
{
	glutInit(&argv, argc);
	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);
	glutInitWindowSize(600, 400);
	glutInitWindowPosition(200, 200);
	glutCreateWindow("poligons_test");

	initialize();

	update();

	glutDisplayFunc(display);
	glutReshapeFunc(reshape);

	glutMainLoop();

	return 0;
}