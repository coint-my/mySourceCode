#include "GameManager.h"


GameManager::GameManager(void)
{
	oldState = true;
	t = POLE - 2;
	t2 = POLE - 1;
	temp_t = 0;
	temp_count_l = 0;
	temp_count_r = 0;
	temp_count_h = 0;
}


GameManager::~GameManager(void)
{
}

void GameManager::InitPole()
{
	int rad = 39;
	srand(time(0));

	for (int i = 0; i < POLE; i++)
	{
		for (int j = 0; j < POLE; j++)
		{
			int r = rand() % 5;

			if(i % 2 == 0)
				tiles[i][j].Initialize(60 + (i * (rad * 2 - 10)), 50 + (j * (rad * 2)), rad, 6, static_cast<TupeColor>(r));

			else if(i % 2 != 0)
				tiles[i][j].Initialize(60 + (i * (rad * 2 - 10)), (50 + rad) + (j * (rad * 2)), rad, 6, static_cast<TupeColor>(r));
		}
	}
}

void GameManager::Draw()
{
	paint.DrawCircle(tiles[0][0].GetRad() + 4, oreol_x, oreol_y, Color(1.0, 0.0, 0.0));
	paint.DrawCircle(tiles[0][0].GetRad() + 4, oreol_x_act, oreol_y_act, Color(0.0, 1.0, 0.0));
	paint.DrawCircle(tiles[0][0].GetRad() + 4, logic.x, logic.y, Color(0.1, 0.7, 0.1));

	for (int i = 0; i < POLE; i++)
	{
		for (int j = 0; j < POLE; j++)
		{
			paint.DrawTile(tiles[i][j].GetRad(), tiles[i][j].GetSegment(), tiles[i][j].GetX(), tiles[i][j].GetY(), tiles[i][j].GetColor());
		}
	}
}

void GameManager::PassiveMouse(int _x, int _y)
{
	for (int y = 0; y < POLE; y++)
	{
		for (int x = 0; x < POLE; x++)
		{
			int xp = tiles[x][y].GetX();
			int yp = tiles[x][y].GetY();

			float d = sqrt((xp - _x) * (xp - _x) + (yp - _y) * (yp - _y));

			if(d < tiles[x][y].GetRad() - 3)
			{
				oreol_x = tiles[x][y].GetX();
				oreol_y = tiles[x][y].GetY();
				cur_p = Point(x, y);
				goto end;
			}
			else
			{
				oreol_x = -100;
				oreol_y = -100;
				cur_p = Point();
				oreol_x_act = -100;
				oreol_y_act = -100;
				sec_p = Point();
			}
		}
	}
	end:;
}

void GameManager::ActiveMouse(int _x, int _y)
{
	if(!cur_p.isEmpty)
	{
		for (int y = 0; y < POLE; y++)
		{
			for (int x = 0; x < POLE; x++)
			{
				int xp = tiles[x][y].GetX();
				int yp = tiles[x][y].GetY();

				int cpx = tiles[cur_p.x][cur_p.y].GetX();
				int cpy = tiles[cur_p.x][cur_p.y].GetY();

				float d = sqrt((xp - _x) * (xp - _x) + (yp - _y) * (yp - _y));
				float dp = sqrt((cpx - _x) * (cpx - _x) + (cpy - _y) * (cpy - _y));

				if(dp < tiles[cur_p.x][cur_p.y].GetRad() * 2.5)
				{
					if(d < tiles[x][y].GetRad() - 3)
					{
						oreol_x_act = tiles[x][y].GetX();
						oreol_y_act = tiles[x][y].GetY();
						sec_p = Point(x, y);
						goto end;
					}
				}
			}
		}
	}
	end:;
}

void GameManager::Mouse(int button, int state, int x, int y)
{
	if(state == GLUT_DOWN && oldState && button == GLUT_LEFT_BUTTON && !cur_p.isEmpty)
	{
		cur_tile = tiles[cur_p.x][cur_p.y];
	}
	else if(state == GLUT_UP && button == GLUT_LEFT_BUTTON && !sec_p.isEmpty)
	{
		sec_tile = tiles[sec_p.x][sec_p.y];
		tiles[cur_p.x][cur_p.y].SetTupe(sec_tile.GetTupe());
		tiles[sec_p.x][sec_p.y].SetTupe(cur_tile.GetTupe());
	}

	oldState = state;
}

void GameManager::Timer()
{
	if(temp_count_l < POLE)
	{
		logic.x = tiles[t][temp_t].GetX();
		logic.y = tiles[t][temp_t].GetY();

		t++;
		if(t >= POLE)
		{
			temp_count_l++;
			temp_t -= t / 2;
			t = 0;
		}
		if(t % 2 == 0)
			temp_t++;
		if(temp_t >= POLE)
		{
			temp_t -= t / 2 - 1;
			t = 0;
		}
	}
	else if(temp_count_r < POLE)
	{
		logic.x = tiles[t2][temp_t].GetX();
		logic.y = tiles[t2][temp_t].GetY();

		t2--;
		if(t2 < 0)
		{
			temp_count_r++;
			t2 = POLE - 1;
		}
		if(t2 % 2 == 0)
			temp_t++;
		if(temp_t >= POLE)
			temp_t = 0;
	}
	else 
	{
		if(temp_count_h < POLE)
		{
			logic.x = tiles[t][temp_t].GetX();
			logic.y = tiles[t][temp_t].GetY();

			temp_t++;
			if(temp_t >= POLE)
			{
				temp_t = 0;
				temp_count_h++;
				t++;
			}
		}
	}
}
