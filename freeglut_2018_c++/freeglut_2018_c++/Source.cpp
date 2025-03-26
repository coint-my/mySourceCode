#include "Utilites.h"
#include "Settings.h"

//polygon pol = polygon(point4(-0.5, -0.5, 0, 1), 1);
polygonGl pol[6];

polygonGl pol2[6];

camera cam;

template <typename T>
void initPolygonQuad(point4 _pos, float _size, T* _pol)
{	
	_pol[0] = T(point3(-_size + _pos.x, -_size + _pos.y, -_size + _pos.z), point3(_size + _pos.x, -_size + _pos.y, -_size + _pos.z),
				point3(_size + _pos.x, _size +_pos.y, -_size + _pos.z),	   point3(-_size + _pos.x, _size + _pos.y, -_size + _pos.z));
					 															   
	_pol[1] = T(point3(-_size + _pos.x, -_size + _pos.y, _size + _pos.z),  point3(_size + _pos.x, -_size + _pos.y, _size + _pos.z),
				point3(_size + _pos.x, _size + _pos.y, _size + _pos.z),    point3(-_size + _pos.x, _size + _pos.y, _size + _pos.z));
					 															   
	_pol[2] = T(point3(-_size + _pos.x, -_size + _pos.y, -_size + _pos.z), point3(-_size + _pos.x, -_size + _pos.y, _size + _pos.z),
				point3(-_size + _pos.x, _size + _pos.y, _size + _pos.z),   point3(-_size + _pos.x, _size + _pos.y, -_size + _pos.z));
					 															   
	_pol[3] = T(point3(_size + _pos.x, -_size + _pos.y, -_size + _pos.z),  point3(_size + _pos.x, -_size + _pos.y, _size + _pos.z),
			    point3(_size + _pos.x, _size + _pos.y, _size + _pos.z),	   point3(_size + _pos.x, _size + _pos.y, -_size + _pos.z));
					 															   
	_pol[4] = T(point3(-_size + _pos.x, -_size + _pos.y, -_size + _pos.z), point3(-_size + _pos.x, -_size + _pos.y, _size + _pos.z),
				point3(_size + _pos.x, -_size + _pos.y, _size + _pos.z),   point3(_size + _pos.x, -_size + _pos.y, -_size + _pos.z));
					 															   
	_pol[5] = T(point3(-_size + _pos.x, _size + _pos.y, -_size + _pos.z),  point3(-_size + _pos.x, _size + _pos.y, _size + _pos.z),
				point3(_size + _pos.x, _size + _pos.y, _size + _pos.z),	   point3(_size + _pos.x, _size + _pos.y, -_size + _pos.z));
}
void drawQuad(polygonGl* const _pol)
{
	_pol[0].Draw(color(50, 150, 200));
	_pol[1].Draw(color(50, 50, 170));
	_pol[2].Draw(color(50, 150, 50));
	_pol[3].Draw(color(50, 0, 200));
	_pol[4].Draw(color(50, 0, 50));
	_pol[5].Draw(color(50, 70, 20));
}

void render()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);	
	glEnable(GL_DEPTH_TEST);
	cam.draw();

	drawQuad(pol);
	drawQuad(pol2);

	glDisable(GL_DEPTH_TEST);
	glutSwapBuffers();
}

void update(int = 0)
{
	glutPostRedisplay();
	glutTimerFunc(Tic, update, 0);
}

void changeSize(int w, int h)
{
	// предупредим деление на ноль
	// если окно сильно перет€нуто будет
	if (h == 0)
		h = 1;
	float ratio = w / h;

	Wid = w;
	Hei = h;

	// Reset матрицы
	glLoadIdentity();

	// определ€ем окно просмотра
	glViewport(0, 0, w, h);

	// установить корректную перспективу.
	gluPerspective(45, ratio, 0.1, 1000);

	glLoadIdentity();
	// вернутьс€ к модели
	glMatrixMode(GL_MODELVIEW);
}

void processNormalKeys(unsigned char key, int x, int y) 
{
	cam.processNormalKeys(key, x, y);
}
void releaseNormalKeys(unsigned char key, int x, int y)
{
	cam.releaseNormalKeys(key, x, y);
}
void preesKeys(int key, int x, int y)
{
	cam.pressKey(key, x, y);
}
void releaseKey(int key, int x, int y)
{
	cam.releaseKey(key, x, y);
}
void mouseButton(int button, int state, int x, int y)
{
	cam.mouseButton(button, state, x, y);
}
void mouseMove(int x, int y)
{
	cam.mouseMove(x, y);
}
void passMouse(int x, int y)
{
	//cam.passMouse(x, y);
}

int main(int argc, char** argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE | GLUT_DEPTH);
	glutInitWindowPosition(WinPosX, WinPosY);
	glutInitWindowSize(Wid, Hei);
	glutCreateWindow("test translation and rotation for matrix quad");
	
	initPolygonQuad(point4(-0.25f, -0.25f, 0.25f, 1.0f), 0.3f, pol);
	initPolygonQuad(point4(1, 0, 0.25f, 1.0f), 0.5f, pol2);

	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glViewport(0, 0, Wid, Hei);
	gluPerspective(45, Wid / Hei, 0.1, 1000);
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();

	glClearColor(0, 0, 0, 1);

	glutDisplayFunc(render);

	glutIgnoreKeyRepeat(1);
	glutKeyboardFunc(processNormalKeys);
	glutSpecialFunc(preesKeys);
	glutSpecialUpFunc(releaseKey);
	glutKeyboardUpFunc(releaseNormalKeys);

	glutMouseFunc(mouseButton);
	glutMotionFunc(mouseMove);
	glutPassiveMotionFunc(passMouse);

	// Ќова€ функци€ изменени€ размеров окна
	glutReshapeFunc(changeSize);
	update();

	glutMainLoop();

	return 0;
}