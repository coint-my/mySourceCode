#pragma once
#include <iostream>
#include "gl/freeglut.h"

#pragma comment(lib, "freeglut.lib")

#define STB_IMAGE_IMPLEMENTATION // Загружаем текстуры
#include"stb-master/stb_image.h"
#include <string>

int WID = 1280, HEI = 720;
//int WID = 640, HEI = 480;

int worldMap[32][32] = 
{
    { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,2,2,2,2,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,3,3,3,0,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,3,3,0,3,0,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,3,0,0,3,0,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,3,0,0,3,0,0,0,1 },
    { 1,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,2,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,0,2,2,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
    { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
    { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }
};

enum MyKeyDown { LEFT, RIGHT, UP, DOWN };

// Позиция и направление игрока
float playerX = 10.5, playerY = 10.5;
float dirX = -1, dirY = 0;
float planeX = 0, planeY = 0.70; // "камера" для FOV

bool isKeyDown[4] = { false, false, false, false };


std::string myFpsStr = "GPT4 Raycasting ";   //счетчик кадров
int frame = 0;          
int frame_current = 0;
double time_c = 0.0;
double time_e = 0.0;
double time_temp = 0.0;
double time_sec = 0.0;

// Параметры мыши
int lastMouseX = WID / 2;  // Начальная позиция курсора (центр экрана)
const unsigned int myMoveSpeed = 3;
float moveSpeed = myMoveSpeed; // Скорость движения
float mouseSensitivity = 0.065;  // Чувствительность мыши
const unsigned short myQuality = 2;//качество игры чем меньше тем лучше

// Текстуры
GLuint textures[3];
GLuint floorTexture;

// Функция загрузки текстуры
GLuint loadTexture(const char* filename)
{
    int width, height, nrChannels;
    unsigned char* data = stbi_load(filename, &width, &height, &nrChannels, 0);
    if (!data) {
        std::cerr << "Ошибка загрузки текстуры: " << filename << std::endl;
        return 0;
    }

    GLuint textureID;
    glGenTextures(1, &textureID);
    glBindTexture(GL_TEXTURE_2D, textureID);

    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, data);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

    stbi_image_free(data);
    return textureID;
}

void myFrameCount()
{
    time_c = (double)clock();
    time_temp = (time_c - time_e) / 1000.0;
    time_e = time_c;
    frame++;
    time_sec += time_temp;
    if (time_sec >= 1.0)
    {
        time_sec = 0.0;
        frame_current = frame;
        frame = 0;
        glutSetWindowTitle((myFpsStr + std::to_string(frame_current) + std::string(" FPS")).c_str());
    }
}

// Проверка столкновения
bool isWall(float x, float y) {
    int mapX = (int)x;
    int mapY = (int)y;
    return worldMap[mapX][mapY] > 0;
}

// Инициализация OpenGL
void initOpenGL() 
{
    glEnable(GL_TEXTURE_2D);
    textures[0] = loadTexture("texture/wall1.bmp"); // Загружаем текстуры
    textures[1] = loadTexture("texture/wall2.bmp");
    textures[2] = loadTexture("texture/floor.bmp");
    floorTexture = loadTexture("texture/floor3.bmp");

    glutSetCursor(GLUT_CURSOR_NONE);  // Скрываем курсор
    glutWarpPointer(WID / 2, HEI / 2);  // Центрируем курсор
}

void drawScene() 
{
    glEnable(GL_TEXTURE_2D);
    for (int x = 0; x < WID; x++) { // Проходим по каждому пикселю экрана (по горизонтали)
        float cameraX = 2 * x / (float)WID - 1; // Преобразование в нормализованное пространство
        float rayDirX = dirX + planeX * cameraX;
        float rayDirY = dirY + planeY * cameraX;

        int mapX = int(playerX);
        int mapY = int(playerY);

        float sideDistX, sideDistY;
        float deltaDistX = (rayDirX == 0) ? 1e30 : std::abs(1 / rayDirX);
        float deltaDistY = (rayDirY == 0) ? 1e30 : std::abs(1 / rayDirY);
        float perpWallDist;

        int stepX, stepY;
        bool hit = false;
        int side; // Вертикальная или горизонтальная стена?

        if (rayDirX < 0) {
            stepX = -1;
            sideDistX = (playerX - mapX) * deltaDistX;
        }
        else {
            stepX = 1;
            sideDistX = (mapX + 1.0 - playerX) * deltaDistX;
        }
        if (rayDirY < 0) {
            stepY = -1;
            sideDistY = (playerY - mapY) * deltaDistY;
        }
        else {
            stepY = 1;
            sideDistY = (mapY + 1.0 - playerY) * deltaDistY;
        }

        while (!hit) { // Запускаем "DDA алгоритм" для поиска столкновения
            if (sideDistX < sideDistY) {
                sideDistX += deltaDistX;
                mapX += stepX;
                side = 0;
            }
            else {
                sideDistY += deltaDistY;
                mapY += stepY;
                side = 1;
            }
            if (worldMap[mapX][mapY] > 0) hit = true;
        }

        // Вычисляем расстояние до стены
        if (side == 0) perpWallDist = (mapX - playerX + (1 - stepX) / 2) / rayDirX;
        else          perpWallDist = (mapY - playerY + (1 - stepY) / 2) / rayDirY;

        int lineHeight = (int)(HEI / perpWallDist);
        int drawStart = -lineHeight / 2 + (HEI / 2);
        int drawEnd = lineHeight / 2 + (HEI / 2);

        float wallX;
        if (side == 0) { wallX = playerY + perpWallDist * rayDirY; glColor3ub(255, 255, 255); }
        else           { wallX = playerX + perpWallDist * rayDirX; glColor3ub(150, 150, 150); }
        wallX -= floor(wallX);

        glBindTexture(GL_TEXTURE_2D, textures[worldMap[mapX][mapY] - 1]);
        glBegin(GL_QUADS);
        glTexCoord2f(wallX, 0); glVertex2i(x, drawStart);
        glTexCoord2f(wallX, 1); glVertex2i(x, drawEnd);
        glTexCoord2f(wallX + 0.01, 1); glVertex2i(x + 1, drawEnd);
        glTexCoord2f(wallX + 0.01, 0); glVertex2i(x + 1, drawStart);
        glEnd();
    }
    glDisable(GL_TEXTURE_2D);
}

void myDrawScene(unsigned short _quality)
{
    glEnable(GL_TEXTURE_2D);
    for (int x = 0; x < WID; x += _quality) { // Проходим по каждому пикселю экрана (по горизонтали)
        float cameraX = 2 * x / (float)WID - 1; // Преобразование в нормализованное пространство
        float rayDirX = dirX + planeX * cameraX;
        float rayDirY = dirY + planeY * cameraX;

        int mapX = int(playerX);
        int mapY = int(playerY);

        float sideDistX, sideDistY;
        float deltaDistX = (rayDirX == 0) ? 1e30 : std::abs(1 / rayDirX);
        float deltaDistY = (rayDirY == 0) ? 1e30 : std::abs(1 / rayDirY);
        float perpWallDist;

        int stepX, stepY;
        bool hit = false;
        int side; // Вертикальная или горизонтальная стена?

        if (rayDirX < 0) {
            stepX = -1;
            sideDistX = (playerX - mapX) * deltaDistX;
        }
        else {
            stepX = 1;
            sideDistX = (mapX + 1.0 - playerX) * deltaDistX;
        }
        if (rayDirY < 0) {
            stepY = -1;
            sideDistY = (playerY - mapY) * deltaDistY;
        }
        else {
            stepY = 1;
            sideDistY = (mapY + 1.0 - playerY) * deltaDistY;
        }

        while (!hit) { // Запускаем "DDA алгоритм" для поиска столкновения
            if (sideDistX < sideDistY) {
                sideDistX += deltaDistX;
                mapX += stepX;
                side = 0;
            }
            else {
                sideDistY += deltaDistY;
                mapY += stepY;
                side = 1;
            }
            if (worldMap[mapX][mapY] > 0) hit = true;
        }

        // Вычисляем расстояние до стены
        if (side == 0) perpWallDist = (mapX - playerX + (1 - stepX) / 2) / rayDirX;
        else          perpWallDist = (mapY - playerY + (1 - stepY) / 2) / rayDirY;

        int lineHeight = (int)(HEI / perpWallDist);
        int drawStart = -lineHeight / 2 + (HEI / 2);
        int drawEnd = lineHeight / 2 + (HEI / 2);

        float wallX;
        if (side == 0) { wallX = playerY + perpWallDist * rayDirY; glColor3ub(255, 255, 255); }
        else { wallX = playerX + perpWallDist * rayDirX; glColor3ub(150, 150, 150); }
        wallX -= floor(wallX);

        glBindTexture(GL_TEXTURE_2D, textures[worldMap[mapX][mapY] - 1]);
        glBegin(GL_QUADS);
        glTexCoord2f(wallX, 0); glVertex2i(x - _quality, drawStart);
        glTexCoord2f(wallX, 1); glVertex2i(x - _quality, drawEnd);
        glTexCoord2f(wallX + 0.01, 1); glVertex2i(x, drawEnd);
        glTexCoord2f(wallX + 0.01, 0); glVertex2i(x, drawStart);
        glEnd();
    }
    glDisable(GL_TEXTURE_2D);
}

// Отрисовка пола и потолка
void drawFloor() 
{
    glEnable(GL_TEXTURE_2D);

    glBindTexture(GL_TEXTURE_2D, floorTexture);

    for (int y = HEI / 2; y < HEI; y++) 
    {
        float rowDistance = HEI / (2.0f * y - HEI); // Правильная перспектива

        float rayDirX0 = dirX - planeX;
        float rayDirY0 = dirY - planeY;
        float rayDirX1 = dirX + planeX;
        float rayDirY1 = dirY + planeY;

        float floorStepX = rowDistance * (rayDirX1 - rayDirX0) / WID;
        float floorStepY = rowDistance * (rayDirY1 - rayDirY0) / WID;
        float floorX = playerX + rowDistance * rayDirX0;
        float floorY = playerY + rowDistance * rayDirY0;

        glBegin(GL_TRIANGLE_STRIP);
        for (int x = 0; x < WID; x++) {
            glTexCoord2f(floorX * 0.5, floorY * 0.5);
            glVertex2i(x, y);

            glTexCoord2f(floorX * 0.5, floorY * 0.5);
            glVertex2i(x, y + 1);

            floorX += floorStepX;
            floorY += floorStepY;
        }
        glEnd();
    }

    glDisable(GL_TEXTURE_2D);
}
// оптимизирован
void myDrawFloor(unsigned short _quality)
{
    glEnable(GL_TEXTURE_2D);

    glBindTexture(GL_TEXTURE_2D, floorTexture);

    float scale = 0.5f;

    for (int y = HEI / 2; y < HEI; y += _quality)
    {
        float rowDistance = HEI / (2.0f * y - HEI); // Правильная перспектива

        float rayDirX0 = dirX - planeX;
        float rayDirY0 = dirY - planeY;
        float rayDirX1 = dirX + planeX;
        float rayDirY1 = dirY + planeY;

        float floorStepX = rowDistance * (rayDirX1 - rayDirX0) / WID;
        float floorStepY = rowDistance * (rayDirY1 - rayDirY0) / WID;
        float floorX = playerX + rowDistance * rayDirX0;
        float floorY = playerY + rowDistance * rayDirY0;

        glPointSize(_quality);
        glBegin(GL_POINTS);
        glColor3ub(255, 255, 255);
        for (int x = 0; x < WID; x++) 
        {
            if (x % _quality == 0)
            {
                glTexCoord2f(floorX * scale, floorY * scale);
                glVertex2i(x, y);
            }

            floorX += floorStepX;
            floorY += floorStepY;
        }
        glEnd();
    }

    glDisable(GL_TEXTURE_2D);
}

void render()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

    //drawFloor();//ресурсно затратный вариант пола с текстурой
    myDrawFloor(myQuality);

    glColor3ub(190, 190, 240); //небо
    glBegin(GL_QUADS);
    glVertex2f(0, 0);
    glVertex2f(WID, 0);
    glVertex2f(WID, HEI / 2);
    glVertex2f(0, HEI / 2);
    glEnd();

    myFrameCount();
    myDrawScene(myQuality);

	glutSwapBuffers();
    Sleep(30);
    glutPostRedisplay(); // Перерисовываем сцену
    //glFlush();
}

void processNormalKeys(unsigned char key, int x, int y)
{
    if (key == 27) { exit(0); }

    if (key == 'a') { isKeyDown[MyKeyDown::LEFT] = true; }
    if (key == 'd') { isKeyDown[MyKeyDown::RIGHT] = true; }
    if (key == 'w') { isKeyDown[MyKeyDown::UP] = true; }
    if (key == 's') { isKeyDown[MyKeyDown::DOWN] = true; }
}
void releaseNormalKeys(unsigned char key, int x, int y)
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

void changeSize(int w, int h)
{
	// предупредим деление на ноль
	// если окно сильно перетянуто будет
	if (h == 0)
		h = 1;

	// определяем окно просмотра
	glViewport(0, 0, w, h);
}

void myUpdate(int _time)
{
    if (isKeyDown[MyKeyDown::UP])
        //movePlayer(dirX * moveSpeed, dirY * moveSpeed);
    {
        float nextX = playerX + dirX * moveSpeed * time_temp;
        float nextY = playerY + dirY * moveSpeed * time_temp;
        if (!isWall(nextX, playerY)) playerX = nextX;
        if (!isWall(playerX, nextY)) playerY = nextY;
    }
    if (isKeyDown[MyKeyDown::DOWN])
        //movePlayer(-dirX * moveSpeed, -dirY * moveSpeed);
    {
        float nextX = playerX - dirX * moveSpeed * time_temp;
        float nextY = playerY - dirY * moveSpeed * time_temp;
        if (!isWall(nextX, playerY)) playerX = nextX;
        if (!isWall(playerX, nextY)) playerY = nextY;
    }
    if (isKeyDown[MyKeyDown::LEFT])
        //movePlayer(-planeX * moveSpeed, -planeY * moveSpeed);  // Strafe влево
    {
        float nextX = playerX - planeX * moveSpeed * time_temp;
        float nextY = playerY - planeY * moveSpeed * time_temp;
        if (!isWall(nextX, playerY)) playerX = nextX;
        if (!isWall(playerX, nextY)) playerY = nextY;
    }
    if (isKeyDown[MyKeyDown::RIGHT])
        //movePlayer(planeX * moveSpeed, planeY * moveSpeed);   // Strafe вправо
    {
        float nextX = playerX + planeX * moveSpeed * time_temp;
        float nextY = playerY + planeY * moveSpeed * time_temp;
        if (!isWall(nextX, playerY)) playerX = nextX;
        if (!isWall(playerX, nextY)) playerY = nextY;
    }

    //glutPostRedisplay(); // Перерисовываем сцену
    
    glutTimerFunc(_time, myUpdate, _time);
}

// Обработка движения мыши
void mouseMotion(int _x, int _y)
{
    int deltaX = _x - lastMouseX;
    lastMouseX = WID / 2;  // Центрируем мышь после обработки

    float rotSpeed = deltaX * mouseSensitivity * time_temp;  // Угол поворота

    float oldDirX = dirX;
    dirX = dirX * cos(-rotSpeed) - dirY * sin(-rotSpeed);
    dirY = oldDirX * sin(-rotSpeed) + dirY * cos(-rotSpeed);

    float oldPlaneX = planeX;
    planeX = planeX * cos(-rotSpeed) - planeY * sin(-rotSpeed);
    planeY = oldPlaneX * sin(-rotSpeed) + planeY * cos(-rotSpeed);

    glutWarpPointer(WID / 2, HEI / 2);  // Возвращаем курсор в центр экрана
}
 
int main(int argc, char** argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE | GLUT_DEPTH);
	glutInitWindowPosition(200, 100);
	glutInitWindowSize(WID, HEI);
	glutCreateWindow("test GPT raycasting" );

	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glViewport(0, 0, WID, HEI);
	gluOrtho2D(0, WID, HEI, 0);
	//gluPerspective(45, WID / HEI, 0.1, 1000);
	//glMatrixMode(GL_MODELVIEW);
	//glLoadIdentity();

	glClearColor(0, 0, 0, 1);

    initOpenGL();

	glutDisplayFunc(render);

    myUpdate(16);

	glutIgnoreKeyRepeat(1);
	glutKeyboardFunc(processNormalKeys);
	glutSpecialFunc(preesKeys);
    glutSpecialUpFunc(releaseKey);
	glutKeyboardUpFunc(releaseNormalKeys);

	/*glutMouseFunc(mouseButton);
	glutMotionFunc(mouseMove);*/
	glutPassiveMotionFunc(mouseMotion);

	// Новая функция изменения размеров окна
	glutReshapeFunc(changeSize);
	/*update();*/

	glutMainLoop();

	return 0;
}
