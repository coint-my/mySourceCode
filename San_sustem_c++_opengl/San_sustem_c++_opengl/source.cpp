#include <iostream>
#include <cmath>
//#include <gl/glut.h>
#include "gl/freeglut.h"
#include "gl/freeglut_ext.h"
#include "gl/freeglut_std.h"
//#include "gl/glut.h"

#define M_PI2 3.14159 * 2

//#pragma comment(lib, "opengl32.lib")
//#pragma comment(lib, "freeglut.dll")
#pragma comment(lib, "gl/freeglut.lib")

const int width = 600;
const int height = 400;

float z_buffer = 4000;
float rot_earth = 0.01;
float rot_luna = 0.01;
float rot_san = 0.01;
float rot_mars = 0.01;
float rot_ypiter = 0.01;
float rot_saturn = 0.01;
float rot_neptun = 0.01;
float rot_venera = 0.01;
float rot_mercury = 0.01;
float rot_cam_x = 0.0;

struct Star
{
	float x, y;
	Star(float xx = 0, float yy = 0)
	{};
};

Star star[100];

void keyboard(unsigned char _key, int _x, int _y)
{
	if(_key == 'w')
	{
		z_buffer -= 10;
	}
	else if(_key == 's')
	{
		z_buffer += 10;
	}
	else if(_key == 'e')
	{
		rot_cam_x += 90;
	}
	else if(_key == 'd')
	{
		rot_cam_x -= 90;
	}

	glutPostRedisplay();
}

void display()
{
	
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	glEnable(GL_DEPTH_TEST);

	glPushMatrix();
	glTranslatef(0, 0, -z_buffer);
	glRotatef(rot_cam_x, 1.0, 0, 0);
		//---------------------------------------------
		glPushMatrix();
			glColor3f(1.0, 0.7, 0.7);
			glRotatef((rot_san += 0.1), 0.0, 1.0, 1.0);
			//солнце
			glutSolidSphere(100, 20, 10);

		glPopMatrix();
		//---------------------------------------------
		glPushMatrix();
			glColor3ub(255, 255, 255);
			//glPointSize(10);

			for (int i = 0; i < 100; i++)
			{
				glBegin(GL_POINTS);
					glVertex3f(star[i].x, star[i].y, -3000);
				glEnd();
			}

		glPopMatrix();
		//----------------------------------------------
		glPushMatrix();
			glColor3ub(130, 130, 130);
			glRotatef((rot_venera += 0.45), 1.0, 1.0, 0.0);

			glTranslatef(0, 0, -150);
			//венера
			glutSolidSphere(15, 10, 10);
		glPopMatrix();
		//----------------------------------------------
		glPushMatrix();
			glColor3ub(200, 110, 110);
			glRotatef((rot_mercury -= 0.75), 1.0, 1.0, 0.0);

			glTranslatef(0, 0, -250);
			//меркурий
			glutSolidSphere(18, 10, 10);
		glPopMatrix();
		//----------------------------------------------
		glPushMatrix();
			glColor3ub(0, 65, 205);
			glRotatef((rot_earth += 0.5), 1.0, 1.0, 0.0);

			glTranslatef(0, 0, -400);
			//земля
			glutSolidSphere(20, 10, 10);

			glRotatef((rot_luna += 2.0), 1.0, 1.0, 0.0);
			glTranslatef(0, 0, -80);
			glColor3ub(200, 190, 210);
			//луна
			glutSolidSphere(10, 8, 8);
		glPopMatrix();
		//----------------------------------------------
		glPushMatrix();
			glColor3ub(200, 55, 50);
			glRotatef((rot_mars += 0.7), 1.0, 1.0, 0.0);

			glTranslatef(0, 0, -650);
			//марс
			glutSolidSphere(18, 10, 10);
		glPopMatrix();
		//----------------------------------------------
		glPushMatrix();
			glColor3ub(180, 150, 5);
			glRotatef((rot_ypiter += 0.2), 1.0, 1.0, 0.0);
			
			glTranslatef(0, 0, -1000);
			//юпитер
			glutSolidSphere(80, 20, 20);
		glPopMatrix();
		//----------------------------------------------
		glPushMatrix();
			glColor3ub(180, 150, 75);
			glRotatef((rot_saturn += 0.15), 1.0, 1.0, 0.0);
			
			glTranslatef(0, 0, -1300);
			//сатурн
			glutSolidSphere(60, 20, 20);
			glColor3ub(50, 110, 75);
			glutSolidTorus(7, 85, 20, 20);
			
		glPopMatrix();
		//----------------------------------------------
		glPushMatrix();
			glColor3ub(0, 0, 255);
			glRotatef((rot_neptun += 0.11), 1.0, 1.0, 0.0);
			
			glTranslatef(0, 0, -2500);
			//нептун
			glutSolidSphere(65, 20, 20);
			
		glPopMatrix();

	glPopMatrix();

	glDisable(GL_DEPTH_TEST);

	glFlush();
}

void Initialize()
{
	glutInitWindowPosition(10, 10);
	glutInitWindowSize(width, height);
	glutCreateWindow("san_system");
	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
	gluPerspective(45, width / height, 1, 7000);
	glClearColor(0.0, 0.0, 0.0, 1.0);

	//glutDisplayFunc(display);

	/*for (int i = 0; i < 100; i++)
	{
		float xx = rand() % 6000 - 3000.0;
		star[i].x = xx;
		star[i].y = rand() % 6000 - 3000.0;
	}*/
}

void timer(int = 0)
{
	display();
	glutTimerFunc(30, timer, 0);
}

int main(int argc, char** argv)
{
	glutInit(&argc, argv);

	Initialize();

	//glutKeyboardFunc(keyboard);

	timer();

	glutMainLoop();

	return 0;
}