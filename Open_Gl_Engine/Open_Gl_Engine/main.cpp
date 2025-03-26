#include <iostream>
#include <gl\glut.h>
#include <gl\glaux.h>
#include "GeneralProperty.h"
#include "Player.h"
#include "GameObject.h"
#include "Figure.h"
#include "RenderObject.h"
#include "StringRead.h"
#include <gl/audiere.h>

#pragma comment(lib, "glaux.lib")
#pragma comment(lib, "audiere.lib")

using namespace audiere;

Figure f[20];
RenderObject render;
Player pl;
SYSTEMTIME tm;
EditState e_state;

typedef unsigned int texture;
#define COUNT_TEXTURE 20

texture listTexture[COUNT_TEXTURE];
int count_fps = 0;
int count = 0;
int time_frame = 0;
int time_curren = 0;

void InitLoad2D();
void InitLoad3D();

void initTexture(int index, char *str)
{
	AUX_RGBImageRec *recText = auxDIBImageLoadA(str);
	glBindTexture(GL_TEXTURE_2D, listTexture[index - 1]);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexImage2D(GL_TEXTURE_2D, 0, 3, recText ->sizeX , recText ->sizeY, 0, GL_RGB, GL_UNSIGNED_BYTE, recText ->data);
}

void MenuMatrix(int _val)
{
	switch(_val)
	{
	case MATRIX_TOP:
		InitLoad2D();
		e_state = EDIT_MAP_2D;
		break;
	case MATRIX_PERSPECTIVE: 
		InitLoad3D();
		e_state = VIEW_3D_MODE;
		break;
	}
}
void Menu(int _val)
{

}
void InitMenu()
{
	int _menu_m = glutCreateMenu(MenuMatrix);
	int _menu = glutCreateMenu(Menu);
	glutSetMenu(_menu_m);
	glutAddMenuEntry("Matrix TOP", MATRIX_TOP);
	glutAddMenuEntry("Matrix PERSPECTIVE", MATRIX_PERSPECTIVE);
	glutSetMenu(_menu);
	glutAddSubMenu("Edit Matrix", _menu_m);
	glutAttachMenu(GLUT_RIGHT_BUTTON);
}

void init_l()
{
	glEnable(GL_LIGHTING);

	GLfloat material_diffuse[] = {0.4, 0.7, 0.2};
	float light0_direction[] = {0.0, 0.0, 1.0, 0.0};

    //glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material_diffuse);
	glLightfv(GL_LIGHT0, GL_DIFFUSE, material_diffuse);
	glLightfv(GL_LIGHT0, GL_POSITION, light0_direction);
	glLightf(GL_LIGHT0, GL_CONSTANT_ATTENUATION, 0.0);
    glLightf(GL_LIGHT0, GL_LINEAR_ATTENUATION, 0.2);
    glLightf(GL_LIGHT0, GL_QUADRATIC_ATTENUATION, 0.4);
}

void Render()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	//glEnable(GL_DEPTH_TEST);

	switch(e_state)
	{
	case VIEW_3D_MODE:
		glColor3ub(0, 200, 200);

		glPushMatrix();

			glRotatef(pl.GetRot(), 0.0, 1.0, 0.0);
			glTranslatef(pl.GetPos().x, pl.GetPos().y, pl.GetPos().z);
			glPushMatrix();
				glTranslatef(0, 20, 0);
				init_l();
				glDisable(GL_LIGHTING);
				glutWireCube(1);
				glEnable(GL_LIGHTING);
			glPopMatrix();

			glutSolidSphere(10, 20, 20);

			/*glColor3ub(0, 0, 255);
			glTranslatef(0, 0, -200);*/

			//рисовать с текстурай
			/*glEnable(GL_TEXTURE_2D);
			glBindTexture(GL_TEXTURE_2D, listTexture[0]);
			render.Render();
			glDisable(GL_TEXTURE_2D);*/
			glDisable(GL_LIGHTING);

		glPopMatrix();

		glColor3ub(255, 255, 255);
		StringRead::ShowString("render count : ", GP::width - 110, GP::height - 10);
		StringRead::ShowString(count, GP::width - 30, GP::height - 10);
		count_fps++;
		break;
	}
	//glDisable(GL_DEPTH_TEST);
	glutSwapBuffers();
}

void Update(int = 0)
{
	glutPostRedisplay();

	switch(e_state)
	{
	case VIEW_3D_MODE:
		render.Update();
		char str[15];
		itoa(count, str, 10);
		glutSetWindowTitle(str);

		GetSystemTime(&tm);
		time_curren = (int)tm.wSecond;

		if(time_frame < time_curren)
		{
			time_frame = time_curren;
			count = count_fps;
			count_fps = 0;
		}
		break;
	}

	glutTimerFunc(16, Update, 0);
}

void Keyboard(unsigned char _button, int _x, int _y)
{
	switch(e_state)
	{
	case VIEW_3D_MODE:
		pl.Keyboard(_button, _x, _y);
		break;
	}
}

void InitLoad2D()
{
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluOrtho2D(0, GP::width, GP::height, 0);
}
void InitLoad3D()
{
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(45, GP::width / GP::height, 1, 2000);

	glMatrixMode(GL_MODELVIEW);
    glLoadIdentity();
	pl.SetDir(90, 5);
}

void main(int argc, char** argv)
{
	//char *str_audio = {"Audio//sound.mp3"};
	//AudioDevicePtr device = OpenDevice();
	//OutputStreamPtr sound(OpenSound(device, str_audio, false));
	//sound->play();
	//sound->setRepeat(false);
	//sound->setVolume(0.3);

	glutInit(&argc , argv);
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE);
	glutInitWindowPosition(400, 100);
	glutInitWindowSize(GP::width, GP::height);
	glutCreateWindow("Engine");

	InitLoad3D();
	InitMenu();

	Update();

	glGenTextures(COUNT_TEXTURE, &listTexture[0]);
	initTexture(1, "texture//1.bmp");

	srand(time(0));
	for(int i = 0; i < 20; i++)
	{
		f[i].SetTranslation(Translation(rand() % 500, rand() % 200 - 100, rand() % 500));
		f[i].SetColor(Color(rand() % 300 - 45, rand() % 300 - 45, rand() % 300 - 45));
		render.AddObject(f[i]);
	}

	glEnable(GL_LIGHT0);
	glEnable(GL_NORMALIZE);
	glShadeModel(GL_SMOOTH);
	glEnable(GL_DEPTH_TEST);
	//glLightModelf(GL_LIGHT_MODEL_TWO_SIDE, GL_TRUE);
	glLightModeli(GL_LIGHT_MODEL_LOCAL_VIEWER, GL_TRUE);

	glutDisplayFunc(Render);
	glutKeyboardFunc(Keyboard);

	glClearColor(0, 0, 0, 1);

	glutMainLoop();
}