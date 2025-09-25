#include <iostream>
#include "freeglut.h"
#include "glut.h"

#pragma comment(lib, "freeglut.lib")

int WID = 1280, HEI = 720;

int myWorldMap[8][8] =
{
	1,1,1,1,1,1,1,1,
	1,0,0,0,0,0,0,1,
	1,0,0,0,0,0,0,1,
	1,0,0,0,0,0,0,1,
	1,0,0,0,0,0,0,1,
	1,0,1,0,0,0,0,1,
	1,0,0,0,0,0,0,1,
	1,1,1,1,1,1,1,1
};

struct Vector2
{
	float x, y;
};

struct MyPlayer
{
	Vector2 myPosition{ 4, 4 };//позиция игрока
	Vector2 myDirection{ -1, 0 };//направление игрока
	Vector2 myPlane{ 0, 0.70f };//угол обзора игрока
}myPlayer;

Vector2 myRay{ 0, 0 };
Vector2 myDeltaDistance{ 0, 0 };

enum MyKeyDown { LEFT, RIGHT, UP, DOWN };
bool isKeyDown[4] = { false, false, false, false };
unsigned int lastMouseX = WID / 2;  // Начальная позиция курсора (центр экрана)
const float myMoveSpeed = 0.1; //скорость перемещения
float moveSpeed = myMoveSpeed; // Скорость движения
float mouseSensitivity = 0.003;  // Чувствительность мыши

GLuint textures[2];

GLuint myLoadTexture(const char* _data, int _wid, int _hei)
{
	GLuint textureID;
	glGenTextures(1, &textureID);
	glBindTexture(GL_TEXTURE_2D, textureID);

	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, _wid, _hei, 0, GL_RGB, GL_UNSIGNED_BYTE, _data);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);

	return textureID;
}

void initOpenGL()
{
	//создаю пиксели из чисел
	const char data[192] = {
							254,1,1,254,1,1,254,1,1,254,1,1,254,1,1,254,1,1,254,1,1,254,1,1,
							254,1,1,1,1,1,1,1,254,1,254,1,1,1,1,254,1,1,1,1,1,254,1,1,
							254,1,1,1,1,1,1,1,254,1,254,1,1,1,1,254,1,1,1,1,1,254,1,1,
							254,1,1,1,1,1,1,1,254,1,254,1,1,1,1,254,1,1,1,1,1,254,1,1,
							254,1,1,254,1,1,254,1,1,254,1,1,254,1,1,254,1,1,254,1,1,254,1,1,
							254,1,1,1,1,1,1,1,254,1,254,1,1,1,1,254,1,1,1,1,1,254,1,1,
							254,1,1,1,1,1,1,1,254,1,254,1,1,1,1,254,1,1,1,1,1,254,1,1,
							254,1,1,254,1,1,254,1,1,254,1,1,254,1,1,254,1,1,254,1,1,254,1,254
	};
	//загружаю в память OpenGL
	textures[0] = myLoadTexture(data, 8, 8);

	glutSetCursor(GLUT_CURSOR_NONE);  // Скрываем курсор
	glutWarpPointer(WID / 2, HEI / 2);  // Центрируем курсор
}

void processNormalKeys(unsigned char key, int x, int y)
{
	if (key == 27) { exit(0); }//выход из приложения кнопка ESC

	if (key == 'a') { isKeyDown[MyKeyDown::LEFT] = true; }
	if (key == 'd') { isKeyDown[MyKeyDown::RIGHT] = true; }
	if (key == 'w') { isKeyDown[MyKeyDown::UP] = true; }
	if (key == 's') { isKeyDown[MyKeyDown::DOWN] = true; }
}

void releaseNormalKeys(unsigned char key, int x, int y)//когда отпускаем клавишу
{
	if (key == 'a') { isKeyDown[MyKeyDown::LEFT] = false; }
	if (key == 'd') { isKeyDown[MyKeyDown::RIGHT] = false; }
	if (key == 'w') { isKeyDown[MyKeyDown::UP] = false; }
	if (key == 's') { isKeyDown[MyKeyDown::DOWN] = false; }
}

void preesKeys(int key, int x, int y)
{
	if (key == GLUT_KEY_SHIFT_L) { moveSpeed = myMoveSpeed * 2; }
}

void releaseKey(int key, int x, int y)
{
	if (key == GLUT_KEY_SHIFT_L) { moveSpeed = myMoveSpeed; }
}

// Проверка столкновения
bool myIsWall(float x, float y)
{
	int mapX = (int)x;
	int mapY = (int)y;
	return myWorldMap[mapX][mapY] > 0;
}

void myUpdate(int _time)
{
	if (isKeyDown[MyKeyDown::UP])
	{
		float nextX = myPlayer.myPosition.x + myPlayer.myDirection.x * moveSpeed;//вычисляем направление игрока по X
		float nextY = myPlayer.myPosition.y + myPlayer.myDirection.y * moveSpeed;//вычисляем направление игрока по Y

		Vector2 myNewPosition = myPlayer.myPosition;
		if (!myIsWall(nextX, myPlayer.myPosition.y)) myNewPosition.x = nextX;//если мы не выходим за стену по x
		if (!myIsWall(myPlayer.myPosition.x, nextY)) myNewPosition.y = nextY;//если мы не выходим за стену по y
		myPlayer.myPosition = myNewPosition;
	}
	if (isKeyDown[MyKeyDown::DOWN])
	{
		float nextX = myPlayer.myPosition.x - myPlayer.myDirection.x * moveSpeed;//вычисляем направление игрока по X
		float nextY = myPlayer.myPosition.y - myPlayer.myDirection.y * moveSpeed;//вычисляем направление игрока по Y

		Vector2 myNewPosition = myPlayer.myPosition;
		if (!myIsWall(nextX, myPlayer.myPosition.y)) myNewPosition.x = nextX;//если мы не выходим за стену по x
		if (!myIsWall(myPlayer.myPosition.x, nextY)) myNewPosition.y = nextY;//если мы не выходим за стену по y
		myPlayer.myPosition = myNewPosition;
	}
	if (isKeyDown[MyKeyDown::LEFT])
	{
		float nextX = myPlayer.myPosition.x - myPlayer.myPlane.x * moveSpeed;//вычисляем направление игрока по X
		float nextY = myPlayer.myPosition.y - myPlayer.myPlane.y * moveSpeed;//вычисляем направление игрока по Y

		Vector2 myNewPosition = myPlayer.myPosition;
		if (!myIsWall(nextX, myPlayer.myPosition.y)) myNewPosition.x = nextX;//если мы не выходим за стену по x
		if (!myIsWall(myPlayer.myPosition.x, nextY)) myNewPosition.y = nextY;//если мы не выходим за стену по y
		myPlayer.myPosition = myNewPosition;
	}
	if (isKeyDown[MyKeyDown::RIGHT])
	{
		float nextX = myPlayer.myPosition.x + myPlayer.myPlane.x * moveSpeed;//вычисляем направление игрока по X
		float nextY = myPlayer.myPosition.y + myPlayer.myPlane.y * moveSpeed;//вычисляем направление игрока по Y

		Vector2 myNewPosition = myPlayer.myPosition;
		if (!myIsWall(nextX, myPlayer.myPosition.y)) myNewPosition.x = nextX;//если мы не выходим за стену по x
		if (!myIsWall(myPlayer.myPosition.x, nextY)) myNewPosition.y = nextY;//если мы не выходим за стену по y
		myPlayer.myPosition = myNewPosition;
	}

	glutTimerFunc(_time, myUpdate, _time);
}

void mouseMotion(int _x, int _y)
{
	int deltaX = _x - lastMouseX;
	lastMouseX = WID / 2;  // Центрируем мышь после обработки

	float rotSpeed = deltaX * mouseSensitivity;  // Угол поворота

	float oldDirX = myPlayer.myDirection.x;
	Vector2 myNewDir = { myPlayer.myDirection.x * cos(-rotSpeed) - myPlayer.myDirection.y * sin(-rotSpeed),
		oldDirX * sin(-rotSpeed) + myPlayer.myDirection.y * cos(-rotSpeed) };//вычисляем поворот направления игрока
	myPlayer.myDirection = myNewDir;

	float oldPlaneX = myPlayer.myPlane.x;
	Vector2 myNewPlane = { myPlayer.myPlane.x * cos(-rotSpeed) - myPlayer.myPlane.y * sin(-rotSpeed),
		oldPlaneX * sin(-rotSpeed) + myPlayer.myPlane.y * cos(-rotSpeed) };//вычисляем поворот угла обзора игрока
	myPlayer.myPlane = myNewPlane;

	glutWarpPointer(WID / 2, HEI / 2);  // Возвращаем курсор в центр экрана
}

void myDrawScene(unsigned short _quality)
{
	glEnable(GL_TEXTURE_2D);//включаем текстурирование
	for (int x = 0; x < WID; x += _quality)// Проходим по каждому пикселю экрана (по горизонтали)
	{
		float cameraX = 2 * x / (float)WID - 1; // Преобразование в нормализованное пространство
		myRay.x = myPlayer.myDirection.x + myPlayer.myPlane.x * cameraX;//направление куда будет лететь луч x
		myRay.y = myPlayer.myDirection.y + myPlayer.myPlane.y * cameraX;//направление куда будет лететь луч y

		int mapX = int(myPlayer.myPosition.x);//на каком крадрате находится сейчас игрок по x
		int mapY = int(myPlayer.myPosition.y);//на каком крадрате находится сейчас игрок по y

		myDeltaDistance.x = (myRay.x == 0) ? 1e30 : std::abs(1 / myRay.x);//дистанция дельта от 0 до 1 по x
		myDeltaDistance.y = (myRay.y == 0) ? 1e30 : std::abs(1 / myRay.y);//дистанция дельта от 0 до 1 по y

		int stepX, stepY;//шаг сдвига влево или в право
		float sideDistX, sideDistY;//сторона стены

		if (myRay.x < 0)
		{
			stepX = -1;
			sideDistX = (myPlayer.myPosition.x - mapX) * myDeltaDistance.x;
		}
		else
		{
			stepX = 1;
			sideDistX = (mapX + 1.0 - myPlayer.myPosition.x) * myDeltaDistance.x;
		}
		if (myRay.y < 0)
		{
			stepY = -1;
			sideDistY = (myPlayer.myPosition.y - mapY) * myDeltaDistance.y;
		}
		else
		{
			stepY = 1;
			sideDistY = (mapY + 1.0 - myPlayer.myPosition.y) * myDeltaDistance.y;
		}

		bool hit = false;//попали мы в стену или нет
		int side; // Вертикальная или горизонтальная стена

		while (!hit) // Запускаем "DDA алгоритм" для поиска столкновения
		{
			if (sideDistX < sideDistY)
			{
				sideDistX += myDeltaDistance.x;
				mapX += stepX;
				side = 0;
			}
			else
			{
				sideDistY += myDeltaDistance.y;
				mapY += stepY;
				side = 1;
			}
			if (myWorldMap[mapX][mapY] > 0) hit = true;
		}

		float perpWallDist;//растояние до стены

		if (side == 0)
			perpWallDist = (mapX - myPlayer.myPosition.x + (1 - stepX) / 2) / myRay.x;
		else
			perpWallDist = (mapY - myPlayer.myPosition.y + (1 - stepY) / 2) / myRay.y;

		int lineHeight = (int)(HEI / perpWallDist);//высота стены
		int drawStart = -lineHeight / 2 + (HEI / 2);//начало рисование стены
		int drawEnd = lineHeight / 2 + (HEI / 2);//конец рисование стены

		float wallX;
		if (side == 0)
		{
			wallX = myPlayer.myPosition.y + perpWallDist * myRay.y;//смотрим откуда начинаеться стена по Y
			glColor3ub(50, 50, 50);
		}
		else
		{
			wallX = myPlayer.myPosition.x + perpWallDist * myRay.x;//смотрим откуда начинаеться стена по X
			glColor3ub(150, 150, 150);
		}
		wallX -= floor(wallX);

		glBindTexture(GL_TEXTURE_2D, textures[myWorldMap[mapX][mapY] - 1]);
		glBegin(GL_QUADS);
		glTexCoord2f(wallX, 0); glVertex2i(x - _quality, drawStart);
		glTexCoord2f(wallX, 1); glVertex2i(x - _quality, drawEnd);
		glTexCoord2f(wallX + 0.01, 1); glVertex2i(x, drawEnd);
		glTexCoord2f(wallX + 0.01, 0); glVertex2i(x, drawStart);
		glEnd();
	}
	glDisable(GL_TEXTURE_2D);//отключаем текстурирование
}

void myRender()
{
	glClear(GL_COLOR_BUFFER_BIT);//очистим экран предыдущего цвета
	myDrawScene(2);//выбираем качество отрисовки текстур

	Sleep(30);//задержка
	glutPostRedisplay();//заставить перерисовать экран
	glutSwapBuffers();//меняем буфер старый на новый
}

void myChangeSize(int w, int h)
{
	// предупредим деление на ноль
	if (h == 0)
		h = 1;

	// определяем окно просмотра
	glViewport(0, 0, w, h);
}

int main(int argc, char** argv)
{
    glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE | GLUT_DEPTH);
	glutInitWindowPosition(200, 100);
	glutInitWindowSize(WID, HEI);
	glutCreateWindow("test raycasting");
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glViewport(0, 0, WID, HEI);
	gluOrtho2D(0, WID, HEI, 0);
	glClearColor(0, 0, 0, 1);

	initOpenGL();

	glutDisplayFunc(myRender);

	myUpdate(16);
	glutIgnoreKeyRepeat(1);//игнорировать задержку клавишь
	glutKeyboardFunc(processNormalKeys);//события нажатия клавиши
	glutSpecialFunc(preesKeys);//события нажатия специальной клавиши
	glutSpecialUpFunc(releaseKey);//события отпускания клавиши
	glutKeyboardUpFunc(releaseNormalKeys);//события отпускания клавиши
	glutPassiveMotionFunc(mouseMotion);//события поворота мыши

	glutReshapeFunc(myChangeSize);

	glutMainLoop();

    return 0;
}
