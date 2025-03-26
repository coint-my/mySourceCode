#include "utilites.h"

using namespace Vova;
Poligon pol;
Camera cam;

//---------------------------------------
int xt = 50 + (WINDOW_WIDTH / 2), yt = 50 + (WINDOW_HEIGHT / 2);
int mx, my;
//---------------------------------------

void Render()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	glColor4ub(pol.r, pol.g, pol.b, pol.a);

	glTranslatef(0, 0, -cam.z);

	static double tmp = 1.1;
	//tmp++;

	float xxx = 2.5;
	float yyy = 2.5;

	cam.Rotation(pol.points[0], Point(xxx, yyy, 0));
	cam.Rotation(pol.points[1], Point(xxx, yyy, 0));
	cam.Rotation(pol.points[2], Point(xxx, yyy, 0));
	cam.Rotation(pol.points[3], Point(xxx, yyy, 0));

	glBegin(GL_QUADS);
	glVertex3f(pol.points[0].x, pol.points[0].y, pol.points[0].z);
	glVertex3f(pol.points[1].x, pol.points[1].y, pol.points[1].z);
	glVertex3f(pol.points[2].x, pol.points[2].y, pol.points[2].z);
	glVertex3f(pol.points[3].x, pol.points[3].y, pol.points[3].z);
	glEnd();

	glTranslatef(0, 0, cam.z);

	glutSwapBuffers();
}

void Update(int = 0)
{
	cam.angleX += (xt - mx) / 4;
	cam.angleY += (yt - my) / 4;

	glutPostRedisplay();
	glutTimerFunc(GAME_UPDATE, Update, 0);
}

void Initialize()
{
	pol = Poligon(Point(0, 0, -10), Point(5, 0, -10), Point(5, 5, -10), Point(0, 5, -10));
	cam = Camera();
	cam.rotation.x = 0;
	cam.rotation.y = 0;
	cam.x = 0;
	cam.y = 0;
	cam.z = 100;
}

void mouse(int _button, int _state, int _x, int _y)
{
	mx = _x;
	my = _y;
}

int main(int argc, char** argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_SINGLE | GLUT_DOUBLE | GLUT_RGBA);
	glutInitWindowPosition(50, 50);
	glutInitWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);
	glutCreateWindow("Тест Freeglut 2017.г");

	Initialize();

	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(60, WINDOW_WIDTH / WINDOW_HEIGHT, 0.001, 10000);	

	/*gluLookAt(cam.x, cam.y, cam.z, cam.x - sin(cam.angleX / 180 * PI), cam.y + tan(cam.angleY / 180 * PI),
		cam.z - cos(cam.angleX / 180 * PI), 0, 1, 0);*/

	glClearColor(0, 0, 0, 1.0);

	glutDisplayFunc(Render);
	glutMouseFunc(mouse);
	glutTimerFunc(0, Update, 0);

	glutMainLoop();

	return 0;
}