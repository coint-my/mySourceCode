#include "openglMain.h"

#include <gl/glut.h>
#include <gl/glaux.h>
#include <windows.h>
#include <stdlib.h>
#include <stdio.h>
#include <math.h>
#include "sector.h"
#include "EditGrid.h"
#include <vector>

int const width = 600;
int const height = 600;

int const widLen = 64;
bool isactive = false;
bool run = false;
int xedit, zedit;

float turnX = 0;
int rot = 0, speed = 10;
double rad = 0;
float dx = 0, dy = 0, dz = 0;

sector sec;
EditGrid edit;
std::vector<point> lstPoint;

unsigned int textName[10];

void InitPoint(float x, float z, float xe, float ze);
void Initialize();
void keyboard(unsigned char key, int x, int y)
{
	if(key == 'p')
	{
		if(!isactive)
		{
			Initialize();
			isactive = true;
		}
		else
		{
			edit.InitEdit();
			isactive = false;
		}
		glutPostRedisplay();
	}
	if(isactive)
	{
		switch(key)
		{
			case 'a':
				rad = (rot - 90) * (3.1415 / 180);
				dz -= sin(rad) * speed;
				turnX -= cos(rad) * speed;
				glutPostRedisplay();
			break;
			case 'd':
				rad = (rot - 90) * (3.1415 / 180);
				dz += sin(rad) * speed;
				turnX += cos(rad) * speed;
				glutPostRedisplay();
			break;
			case 'w':
				rad = rot * (3.1415 / 180);
				dz -= (float)sin(rad) * speed;
				turnX -= (float)cos(rad) * speed;

				glutPostRedisplay();
			break;
			case 's':
				rad = rot * (3.1415 / 180);
				dz += (float)sin(rad) * speed;
				turnX += (float)cos(rad) * speed;

				glutPostRedisplay();
			break;
			default:
			break;
		}
	}
}

void initTexGame(int index, char *str)
{
	AUX_RGBImageRec *recText = auxDIBImageLoadA(str);
	glGenTextures(index + 1, &textName[index]);
	glBindTexture(GL_TEXTURE_2D, textName[index]);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexImage2D(GL_TEXTURE_2D, 0, 3, recText ->sizeX , recText ->sizeY, 0, GL_RGB, GL_UNSIGNED_BYTE, recText ->data);
}

void display()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	//glEnable(GL_DEPTH_TEST);

	glEnable(GL_DEPTH_TEST);
	//glTexEnvf(GL_TEXTURE_ENV,GL_TEXTURE_ENV_MODE,GL_REPLACE);
	//glBindTexture(GL_TEXTURE_2D, textName);
	if(!isactive)
	{
		edit.drawEdit();
	}
	else
	{
		//glLoadIdentity();
		glColor3f(1.0, 1.0, 1.0);

		glPushMatrix();
		glRotatef(rot, 0.0, 1.0, 0.0);
		glTranslatef(turnX, dy, dz);

		glBindTexture(GL_TEXTURE_2D, textName[0]);
		sec.draw();
		glPopMatrix();
	}
	glDisable(GL_DEPTH_TEST);

	glutSwapBuffers();
}

void Initialize()
{
	glClearColor(0.1, 0.1, 0.1, 1.0);
	glEnable(GL_TEXTURE_2D);
	//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
	//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
	//glPixelStorei(GL_UNPACK_ALIGNMENT,1);
	initTexGame(0, "1.bmp");
	//InitPoint(0, 0, 100, 100);
	
	sec = sector(edit.getListPoints());
	sec.Scale(edit.getScale());
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
	//gluLookAt(0, 0, 0, -5, 10, 0, -10, 10, 0);
	//glOrtho(-width / 2, width / 2, -height / 2, height / 2, 0, 600);
	//glFrustum(-1, 1, -1, 1, 1.5, 500);
	gluPerspective(45.0, width / height, 0.1f, 2000);
	glTranslatef(0, 0, 0);
	glRotatef(90, 0.0, 1.0, 0.0);
}
void editMouse(int button, int state, int x, int y)
{
	if(!isactive)
	{
		edit.editMouse(button, state, x, y);
		glutPostRedisplay();
	}
}
void move(int x, int y)
{
	if(isactive)
	{
		rot = x * 0.7;

		//turnY = (y - height / 2);
		//gluLookAt(0, 0, -100, 0, 0, 0, 0, 1, 0);
		glutPostRedisplay();
	}
	else
	{
		edit.visible(x, y);
		glutPostRedisplay();
	}
}

void InitPoint(float x, float z, float xe, float ze)
{
	point p;
	p.x = x;
	p.z = z;
	p.dx = xe;
	p.dz = ze;
	lstPoint.push_back(p);
}

void main(int argc, char **argv)
{	
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB);
	glutInitWindowSize(width, height);
	glutInitWindowPosition(200, 200);
	glutCreateWindow("My OpenGL (Vovan)");
	
	glutDisplayFunc(display);
	edit.InitEdit();
	glutKeyboardFunc(keyboard);
	glutPassiveMotionFunc(move);
	//glutMotionFunc(edit);
	glutMouseFunc(editMouse);
	//Initialize();

	glutMainLoop();
}
