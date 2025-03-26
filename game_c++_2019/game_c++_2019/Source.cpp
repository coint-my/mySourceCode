#include <iostream>
#include "EditGame.h"

ManagerLogick ml;
ed::EditGame eg;
GLfloat _t = 0, _tm = 0, _tmp = 0, sec_draw = 0;
int frame = 0, temp_frame = 0, upd_count = 0;
bool pressButton = false;

unsigned char key;

void render_sub()
{
	glutSetWindow(my::win_id[1]);
	glClearColor(0.3, 0.3, 0.3, 1.0);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glEnable(GL_TEXTURE_2D);
	glEnable(GL_BLEND);

	eg.draw(_tmp);

	glDisable(GL_BLEND);
	glDisable(GL_TEXTURE_2D);
	glutSwapBuffers();
}

void render()
{
	glutSetWindow(my::win_id[0]);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glEnable(GL_TEXTURE_2D);
	glEnable(GL_BLEND);
	
	ml.render(_tmp);

	temp_frame++;
	my::ShowStringFont("frame = " + std::to_string(frame) + " update = " + std::to_string(upd_count),
		7, my::precent(50, my::WID), 20, my::colori());

	glDisable(GL_BLEND);
	glDisable(GL_TEXTURE_2D);

	glutSwapBuffers();
}

void close();
void keyboard(unsigned char _key, int _x, int _y)
{
	//выход из игры
	if (_key == 27) { close(); }

	key = _key;

	pressButton = true;
}

void mouse(int button, int state, int _x, int _y)
{
	ml.mouse(button, state, _x, _y);
}

void mouseMotion(int _x, int _y)
{
	ml.mouseMotion(_x, _y);
}

void sub_mouse(int button, int state, int _x, int _y)
{
	eg.mouse(button, state, _x, _y);
}
void upKeyboard(unsigned char _key, int _x, int _y)
{
	pressButton = false;
}

void sub_upKeyboard(unsigned char _key, int _x, int _y)
{
	eg.keyboard_up(_key, _x, _y);
}

void mouseWheel(int button, int dir, int _x, int _y)
{
	eg.mouseWheel(button, dir, _x, _y);
}

void update_main(int = 0)
{
	glutSetWindow(my::win_id[0]);
	_t = clock();//glutGet(GLUT_ELAPSED_TIME);
	
	_tmp = (_t - _tm) / 1000.0f;
	_tm = _t;

	ml.update(_tmp);

	static GLint upd = 0;
	upd++;

	sec_draw += _tmp;
	if (sec_draw >= 1.0f)
	{
		//printf("update count = %i\nframe = %i\n", upd, temp_frame);
		upd_count = upd;
		upd = 0;
		sec_draw = 0;
		frame = temp_frame;
		temp_frame = 0;
	}

	if (pressButton)
		ml.pressKeyboard(_tmp, key);
	else
		ml.reliseKeyboard();

	eg.update(_tmp);

	glutPostRedisplay();
	glutTimerFunc(0, update_main, 0);
}

void update()
{
	update_main();
	//update_sub();
}

void renderSceneAll()
{
	render();
}

void close()
{
	glutDestroyWindow(my::win_id[1]);
	glutDestroyWindow(my::win_id[0]);

	exit(0);
}

int main(int argc, char** argv)
{
	setlocale(LC_ALL, "russian");
	time(0);
	my::initialize_main(argc, argv);
	ml.Initialize();

	glutMouseFunc(mouse);
	glutMotionFunc(mouseMotion);
	glutDisplayFunc(render);
	glutKeyboardFunc(keyboard);
	glutKeyboardUpFunc(upKeyboard);
	glutCloseFunc(close);

	//update();

	my::initialize_sub();
	glutMouseFunc(sub_mouse);
	glutDisplayFunc(render_sub);
	eg.init(&ml);
	glutKeyboardUpFunc(sub_upKeyboard);
	glutMouseWheelFunc(mouseWheel);

	update();
	
	//update_sub();

	while (1) 
	{
		glutMainLoopEvent();
	}

	return 0;
}