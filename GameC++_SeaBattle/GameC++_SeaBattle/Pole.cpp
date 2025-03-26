#include "Pole.h"

Pole::Pole(void)
{
	offset_x = 20;
	offset_y = 20;
	Initialize();
	count_timer = 0;
}

Pole::Pole(const short _x_start, const short _y_start)
{
	offset_x = _x_start;
	offset_y = _y_start;
	Initialize();
}

Pole::~Pole(void)
{
}

void Pole::Initialize()
{
	short wid = Painter::wid;
	short hei = Painter::hei;

	for(short j = 0; j < HEIGHT_POLE; j++)
	{
		for(short i = 0; i < WIDTH_POLE; i++)
		{
			gex[i][j].value = 0;
			gex[i][j].active = false;
			gex[i][j]._lentch = 1;
			gex[i][j].x = offset_x + (wid * i) + i;
			gex[i][j].y = offset_y + (hei * j) + j;
		}
	}
}

void Pole::Intersect(const short _x, const short _y, const short _lenth, const short _rot)
{
	short wid = Painter::wid;
	short hei = Painter::hei;

	short val_x = gex[_x][_y].x;
	short val_y = gex[_x][_y].y;

	MyRect rect;

	if(_rot == 1)
		rect = MyRect(val_x - (wid * 2), val_y - (hei * 2), val_x + ((_lenth + 2) * wid), val_y + (hei * 3));
	else
		rect = MyRect(val_x - (wid * 2), val_y - (hei * 2), val_x + (wid * 3), val_y + ((_lenth + 2) * hei));

	for(short j = 0; j < HEIGHT_POLE; j++)
	{
		for(short i = 0; i < WIDTH_POLE; i++)
		{
			short t_x = gex[i][j].x;
			short t_y = gex[i][j].y;
			if((rect.Right() > t_x + wid) && (rect.Left() < t_x) &&
				(rect.Top() < t_y) && (rect.Bottom() > t_y + hei))
			{
				gex[i][j].value = 2;
				gex[i][j].active = true;
			}
		}
	}
}

void Pole::RandomShutle(const short _num_shutle, const short _lenth_shutle)
{
	srand(count_timer + time(0));
	bool flag = true;
	//корабль _num_shutle
	for(short _count = 0; _count < _num_shutle; _count++)
	{
		flag = true;
		while(flag)
		{
			short rand_numb_x = rand() % WIDTH_POLE;
			short rand_numb_y = rand() % HEIGHT_POLE;
			bool rand_rotate = rand() % 2;

			if((rand_rotate == true) && (!gex[rand_numb_x][rand_numb_y].active) && (rand_numb_x + _lenth_shutle) <= WIDTH_POLE)
			{
				bool is_not_problem = true;
				for(short i = rand_numb_x; i < rand_numb_x + _lenth_shutle; i++)
				{
					if(!gex[i][rand_numb_y].active)
						is_not_problem = true;
					else
						is_not_problem = false;
				}

				if(is_not_problem)
				{
					Intersect(rand_numb_x, rand_numb_y, _lenth_shutle, rand_rotate);
					for(short i = rand_numb_x; i < rand_numb_x + _lenth_shutle; i++)
					{
						gex[i][rand_numb_y].value = 3;
						gex[i][rand_numb_y]._ch = 'w';
						gex[i][rand_numb_y].active = true;
						gex[i][rand_numb_y]._lentch = _lenth_shutle;
						flag = false;
					}
				}
				else if(count_timer > 1000)
				{
					flag = false;
				}
				else
				{
					count_timer++;
				}
			}
			else if((rand_rotate == false) && (!gex[rand_numb_x][rand_numb_y].active) && (rand_numb_y + _lenth_shutle) <= HEIGHT_POLE)
			{
				bool is_not_problem = true;
				for(short i = rand_numb_y; i < rand_numb_y + _lenth_shutle; i++)
				{
					if(!gex[rand_numb_x][i].active)
						is_not_problem = true;
					else
						is_not_problem = false;
				}

				if(is_not_problem)
				{
					Intersect(rand_numb_x, rand_numb_y, _lenth_shutle, rand_rotate);
					for(short i = rand_numb_y; i < rand_numb_y + _lenth_shutle; i++)
					{
						gex[rand_numb_x][i].value = 3;
						gex[rand_numb_x][i]._ch = 'h';
						gex[rand_numb_x][i].active = true;
						gex[rand_numb_x][i]._lentch = _lenth_shutle;
						flag = false;
					}
				}
				else if(count_timer > 1000)
				{
					flag = false;
				}
				else
				{
					count_timer++;
				}
			}
		}
	}
}

void Pole::InitRandom()
{
	Initialize();

	//случайное расположение кораблей на поле боя(gex)

	RandomShutle(1, 4);
	RandomShutle(3, 3);
	RandomShutle(2, 2);
	RandomShutle(3, 1);

	count_shuttle = 9;

	for(short j = 0; j < HEIGHT_POLE; j++)
	{
		for(short i = 0; i < WIDTH_POLE; i++)
		{
			if(gex[i][j].value == 3)
			{

			}
			else
			{
				gex[i][j].value = 0;
				gex[i][j].active = false;
			}
		}
	}
}

void Pole::Draw(Painter &_p, const unsigned int *_texture) const
{
	for(short j = 0; j < HEIGHT_POLE; j++)
	{
		for(short i = 0; i < WIDTH_POLE; i++)
		{
			switch(gex[i][j].value)
			{
				case 1:
					glColor3f(1.0, 1.0, 1.0);
					_p.DrawRect(gex[i][j].x, gex[i][j].y, _texture[1]);
					break;
				case 2:
					glColor3f(1.0, 1.0, 1.0);
					_p.DrawRect(gex[i][j].x, gex[i][j].y, _texture[2]);
					break;
				case 3:
					glColor3f(1.0, 1.0, 1.0);
					_p.DrawRect(gex[i][j].x, gex[i][j].y, _texture[3]);
					break;
				case 4:
					glColor3f(0.5, 0.5, 0.5);
					_p.DrawRect(gex[i][j].x, gex[i][j].y, _texture[1]);
					break;
				default:
					glColor3f(1.0, 1.0, 1.0);
					_p.DrawRect(gex[i][j].x, gex[i][j].y, _texture[0]);
					break;
			}
		}
	}
}

void Pole::Draw(Painter &_p, const float _color_val) const
{
	for(short j = 0; j < HEIGHT_POLE; j++)
	{
		for(short i = 0; i < WIDTH_POLE; i++)
		{
			switch(gex[i][j].value)
			{
				case 1:
					_p.SetColor(1.0 + _color_val, 0.0, 0.0);
					_p.DrawRect(gex[i][j].x, gex[i][j].y);
					break;
				case 2:
					_p.SetColor(0.5 + _color_val, 0.5 + _color_val, 0.5 + _color_val);
					_p.DrawRect(gex[i][j].x, gex[i][j].y);
					break;
				case 3:
					_p.SetColor(0.0, 0.0, 1.0 + _color_val);
					_p.DrawRect(gex[i][j].x, gex[i][j].y);
					break;
				case 4:
					_p.SetColor(0.3 + _color_val, 0.3 + _color_val, 0.0);
					_p.DrawRect(gex[i][j].x, gex[i][j].y);
					break;
				default:
					_p.SetColor(0.9 + _color_val, 0.9 + _color_val, 0.9 + _color_val);
					_p.DrawRect(gex[i][j].x, gex[i][j].y);
					break;
			}
		}
	}
}