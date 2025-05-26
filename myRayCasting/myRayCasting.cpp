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
float myRotation = 0;

void myDrawScene()
{
	for (int x = 0; x < WID; x++)// Проходим по каждому пикселю экрана (по горизонтали)
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

		if (side == 0)
			glColor3ub(50, 50, 50);
		else
			glColor3ub(150, 150, 150);

		glBegin(GL_LINES);
		glVertex2i(x, drawStart);
		glVertex2i(x, drawEnd);
		glEnd();
	}
}

void myRender()
{
	glClear(GL_COLOR_BUFFER_BIT);//очистим экран предыдущего цвета
	myDrawScene();

	myRotation += 0.0001f;//тест для поворота
	float oldDirX = myPlayer.myDirection.x;
	myPlayer.myDirection.x = myPlayer.myDirection.x * cos(-myRotation) - myPlayer.myDirection.y * sin(-myRotation);
	myPlayer.myDirection.y = oldDirX * sin(-myRotation) + myPlayer.myDirection.y * cos(-myRotation);

	float oldPlaneX = myPlayer.myPlane.x;
	myPlayer.myPlane.x = myPlayer.myPlane.x * cos(-myRotation) - myPlayer.myPlane.y * sin(-myRotation);
	myPlayer.myPlane.y = oldPlaneX * sin(-myRotation) + myPlayer.myPlane.y * cos(-myRotation);

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
	glutCreateWindow("test GPT raycasting");
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glViewport(0, 0, WID, HEI);
	gluOrtho2D(0, WID, HEI, 0);
	glClearColor(0, 0, 0, 1);
	glutDisplayFunc(myRender);
	glutReshapeFunc(myChangeSize);

	glutMainLoop();

    return 0;
}
