#include "Player.h"
#include "StringRead.h"
#include "gl/glut.h"

Player::Player(void) : speed(2.4)
{
	color = Colorf(0.0, 0.4, 0.0);
	//keys.key[0] = 1;
}


Player::~Player(void)
{
}

void Player::show_keys() const
{
	for(int i = 0; i < 9; i++)
	{
		glColor3ub(255, 255, 255);
		StringRead::ShowString("Key_", i + 1, " :", 60 * i, HEIGHT - 10);
		glColor3ub(20, 255, 20);
		StringRead::ShowString(keys.key[i], 45 + 60 * i, HEIGHT - 10);
	}
}

void Player::draw() const
{
	const float amb_light[] = { color.r, color.g, color.b, 1.0 };
	glLightModelfv(GL_LIGHT_MODEL_AMBIENT, amb_light);
	glLightfv(GL_LIGHT0, GL_DIFFUSE, amb_light);

	glPushMatrix();
		glTranslatef(pos.x, pos.y, 0);

		glEnable(GL_LIGHTING);
		glEnable(GL_LIGHT0);
		glEnable(GL_DEPTH_TEST);

		glutSolidSphere(20, 15, 15);

		glDisable(GL_LIGHTING);
		glDisable(GL_LIGHT0);
		glDisable(GL_DEPTH_TEST);

	glPopMatrix();

	show_keys();
}
