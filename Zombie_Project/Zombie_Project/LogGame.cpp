#include "LogGame.h"

LogGame::LogGame(void) : resp_begin(135), resp_time(400), resp_add_enemy(0), resp_end(1), resp_curr(5), lives_level(20), enemy_end(120)
{
	isDown = false;
	isStopGame = false;
	cur_ind = -1;
	curr_enemy = 0;
	gold = 460;
	lives = 0;
	enemy_die = 0;

	for(int j = 0; j < COUNT_H; j++)
	{
		for(int i = 0; i < COUNT_W; i++)
		{
			imposible[i][j] = true;

			if(i > 4)
				imposible[i][j] = false;
		}
	}
}

LogGame::~LogGame(void)
{
	list_char.clear();
	list_char_enemy.clear();
}

void LogGame::Mouse(int _mouse, int _state, int _x, int _y)
{
	if(_state == GLUT_UP && isDown && (cur_ind >= 0))
	{
		int ind_x = ((_x - 10) / (WIDTH / COUNT_W));
		int ind_y = ((_y - 10) / (HEIGHT / COUNT_H));

		//проверка на пересечения разрешонного поля
		if((_x > 10 && _x < WIDTH) && (_y > 10 && _y < HEIGHT))
		{
			if(imposible[ind_x][ind_y])
			{
				//добавить растение в список
				Character _character = inter_character[cur_ind];
				_character.InitPositiion(pole.GetTile(ind_x, ind_y).X(), pole.GetTile(ind_x, ind_y).Y());
				list_char.push_back(_character);
				gold -= _character.GetCostGold();
				//блокировать поле
				imposible[ind_x][ind_y] = false;
			}
		}

		rect[cur_ind].Reset();
	}
	else if(_state == GLUT_DOWN && _mouse == GLUT_LEFT)
	{
		for(int i = 0; i < INTER_C; i++)
			if(rect[i].IsIntersect(mouse_rect) && gold >= inter_character[i].GetCostGold())
				cur_ind = i;
	}

	isDown = GLUT_DOWN == _state;
}

void LogGame::ActiveMouse(int _x, int _y)
{
	mouse_rect = EngineRect(_x, _y, 1, 1);

	//показать активное полe
	pole.SetTile(imposible);
	mouse_y = _y;

	//залипать интерфейс
	if((cur_ind >= 0) && rect[cur_ind].IsIntersect(mouse_rect))
	{
		rect[cur_ind].Set_X(_x - rect[cur_ind].Get_Width() / 2);
		rect[cur_ind].Set_Y(_y - rect[cur_ind].Get_Height() / 2);
	}
}

void LogGame::PassMouse(const int _x, const int _y)
{
	pole.IntersectVisible(_x, _y);

	//проверить пересечения gold и добовления золота
	golds.Intersect(_x, _y, gold);
	mouse_y = -1;

	mouse_rect = EngineRect(_x, _y, 1, 1);
	cur_ind = -1;
}

void LogGame::ShowString(const char *_string, const int _x, const int _y)
{
	glRasterPos2i(_x, _y);

	while(*_string)
	{
		glutBitmapCharacter(GLUT_BITMAP_TIMES_ROMAN_24, *_string++);
	}
}

void LogGame::ShowString(int _intejer, const int _x, const int _y)
{
	char _str[15];
	itoa(_intejer, _str, 10);

	glRasterPos2i(_x, _y);

	int i = 0;
	while(i < 20)
	{
		glutBitmapCharacter(GLUT_BITMAP_TIMES_ROMAN_24, _str[i]);
		i++;
	}
}

void LogGame::SetCharacterEnemy(Character _character, const short _ind)
{
	inter_char_enemy[_ind] = _character;
}

void LogGame::Initialize(ui _texture[])
{
	srand(time(0));
	//инициализация поля
	pole.Initialize(_texture[0]);

	//добовление противника
	SetCharacterEnemy(Character(5, 4.0, 3, 110, BULLET, _texture[1], _texture[1], 60), 0);
	SetCharacterEnemy(Character(5, 3.0, 3, 165, BULLET, _texture[6], _texture[6], 90), 1);
	SetCharacterEnemy(Character(2, 3.0, 5, 90, BULLET, _texture[7], _texture[7], 80), 2);
	SetCharacterEnemy(Character(3, 8.0, 2, 335, BULLET, _texture[8], _texture[8], 120), 3);

	//инициализация характер и прямоугольник интерфейса
	inter_character[0] = Character(4, 4.0, 0, 100, BULLET, _texture[2], _texture[3], 90);
	rect[0] = EngineRect(10, HEIGHT + 10, 50, 50);

	inter_character[1] = Character(5, 4.0, 0, 100, BULLET_FREZE, _texture[4], _texture[5], 160);
	rect[1] = EngineRect(10 + (1 * 51), HEIGHT + 10, 50, 50);

	inter_character[2] = Character(4, 5.0, 0, 100, BULLET_BOMB, _texture[9], _texture[10], 240);
	inter_character[2].LoadTextureBullA(_texture[11]);
	inter_character[2].SetBullAnim(true);
	rect[2] = EngineRect(10 + (2 * 51), HEIGHT + 10, 50, 50);

	inter_character[3] = Character(3, 6.0, 0, 100, BULLET_BOMB_2X, _texture[13], _texture[14], 380);
	rect[3] = EngineRect(10 + (3 * 51), HEIGHT + 10, 50, 50);

	inter_character[4] = Character(3, 4.0, 0, 100, MINE, _texture[17], _texture[17], 460);
	rect[4] = EngineRect(10 + (4 * 51), HEIGHT + 10, 50, 50);

	bull_tupe_explode = BulletA(1, 4, 1, 5, true, BULLET_BOMB);
	texture_explode = _texture[12];
	bull_tupe_explode_02 = BulletA(1, 4, 1, 5, true, BULLET_BOMB_2X);
	bull_tupe_explode_02.SetAnim(Animation(0, 0, 60, 30));
	texture_explode_02 = _texture[15];
	a_explode.Initialize(Animation(0, 0, 60, 60), BulletA(1, 12, 1, 1, true, BULLET), _texture[16]);
}

void LogGame::Draw(Painter &_p)
{
	//рисовать поле
	pole.Draw_T(_p, mouse_y);
	//рисовать противник
	for(int en = 0; en < list_char_enemy.size(); en++)
	{
		list_char_enemy[en].DrawPassive(_p);
	}
	//рисовать растения
	for(int i = 0; i < list_char.size(); i++)
	{
		list_char[i].DrawPassive(_p);
		list_char[i].DrawShoot(_p);
	}
	//рисуем еффект взрыва
	for(int i = 0; i < list_explode.size(); i++)
	{
		list_explode.at(i).DrawBulletAnim(_p, texture_explode);
	}
	//рисуем еффект взрыва02
	for(int i = 0; i < list_explode_02.size(); i++)
	{
		list_explode_02.at(i).DrawBulletAnim(_p, texture_explode_02);
	}
	//рисовать gold после удаления противника
	golds.Draw();
	//рисовать класс взрывов удаления противника
	a_explode.Draw(_p);
	//нарисовать интерфейс
	for(int i = 0; i < INTER_C; i++)
	{
		if(rect[i].IsIntersect(mouse_rect))
		{
			_p.SetColor(Color(0.0, 0.7, 0.7));
			_p.DrawRectTexture(rect[i].Get_X(), rect[i].Get_Y(), rect[i].Get_Width(), rect[i].Get_Height(), 1.0 / inter_character[i].GetFrame(),
								1.0, 0.0, 0.0, inter_character[i].Texture_1());
			glColor3f(1.0, 1.0, 1.0);
			ShowString(inter_character[i].GetCostGold(), rect[i].Get_X(), 
						rect[i].Get_Y() + inter_character[i].GetAnim().Height());
		}
		else
		{
			_p.SetColor(Color(1.0, 1.0, 1.0));
			_p.DrawRectTexture(rect[i].Get_X(), rect[i].Get_Y(), rect[i].Get_Width(), rect[i].Get_Height(), 1.0 / inter_character[i].GetFrame(),
								1.0, 0.0, 0.0, inter_character[i].Texture_1());
			ShowString(inter_character[i].GetCostGold(), rect[i].Get_X(), 
						rect[i].Get_Y() + inter_character[i].GetAnim().Height());
		}
	}
	ShowString("GOLD : ", WIDTH / 2 - 50, HEIGHT + 110);
	ShowString(gold, WIDTH / 2 + 50, HEIGHT + 110);
	ShowString("LIVES : ", (WIDTH / 3) * 2, HEIGHT + 110);
	ShowString(lives, (WIDTH / 3) * 2 + 90, HEIGHT + 110);
	ShowString(" / ", (WIDTH / 3) * 2 + 120, HEIGHT + 110);
	ShowString(lives_level, (WIDTH / 3) * 2 + 145, HEIGHT + 110);
	ShowString("ENEMY DIE: ", 10, HEIGHT + 110);
	ShowString(enemy_die, 10 + 11 * 13, HEIGHT + 110);
	ShowString(" / ", 10 + 13 * 13, HEIGHT + 110);
	ShowString(enemy_end, 10 + 15 * 13, HEIGHT + 110);

	if(lives > lives_level)
		ShowString("Game Over", WIDTH / 2, HEIGHT / 2);
	if(enemy_die > enemy_end)
		ShowString("WINS", WIDTH / 2, HEIGHT / 2);
}

void LogGame::IsShootChar()
{
	for (int i = 0; i < list_char.size(); i++)
	{
		for(int en = 0; en < list_char_enemy.size(); en++)
		{
			Character &_en = list_char_enemy[en];
			Character &_pl = list_char[i];
			EngineRect &_renemy = EngineRect(_en.GetAnim().Get_X(), _en.GetAnim().Get_Y(), _en.GetAnim().Width(), _en.GetAnim().Height());
			EngineRect &_rflayer = EngineRect(_pl.GetAnim().Get_X(), _pl.GetAnim().Get_Y(), WIDTH - _pl.GetAnim().Get_X(), _pl.GetAnim().Height());

			if(_rflayer.IsIntersect(_renemy))
			{
				_pl.SetShoot(true);
				_pl.SetBulletPosition(_en.GetAnim().Get_X());
				list_char[i] = _pl;
				list_char_enemy[en] = _en;
				break;
			}
			else
			{
				_pl.SetShoot(false);
				list_char[i] = _pl;
				list_char_enemy[en] = _en;
			}
		}
	}
}

void LogGame::Update()
{
	//проверка проиграл или нет
	if(lives <= lives_level && !isStopGame)
	{
		//стрелять по противнику
		IsShootChar();
		//добовление противника
		resp_enemy++;
		if(resp_enemy > rand() % resp_time + resp_begin)
		{
			resp_enemy = 0;
			resp_add_enemy++;

			short _ra = rand() % COUNT_H;

			if((resp_add_enemy > 5 + resp_curr) && (resp_end < 5))
			{
				resp_add_enemy = 0;
				resp_curr += 5;
				if(resp_begin > 20)
					resp_begin -= 30;
				if(resp_time > 100)
					resp_time -= 40;
				resp_end++;
			}
			Character &_char = GetCharacter(rand() % resp_end);

			_char.InitPositiion(WIDTH + 10, 10 + _ra * pole.GetTile(0, 0).Height());
			list_char_enemy.push_back(_char);
		}
		//обновлять противника
		for(int en = 0; en < list_char_enemy.size(); en++)
		{
			//удаляем противника по индексу
			if(list_char_enemy[en].GetLive() <= 0)
			{
				Animation an = list_char_enemy[en].GetAnim();
				a_explode.AddEffect(an.Get_X() + 5, an.Get_Y() + 5);
				golds.Add(EngineRect(an.Get_X() + 10, an.Get_Y() + an.Height() - 24, 24, 24), list_char_enemy[en].GetCostGold());
				list_char_enemy.erase(list_char_enemy.begin() + en);
				enemy_die++;
			}
			//проверка на выход за границу противника
			else if(list_char_enemy[en].GetAnim().Get_X() < 0)
			{
				lives++;
				list_char_enemy.erase(list_char_enemy.begin() + en);
			}
			else
				list_char_enemy[en].Update();
		}
		//обновить элемент взрыва
		for(int i = 0; i < list_explode.size(); i++)
		{
			list_explode.at(i).UpdateBulletAnim();
		}
		//обновить элемент взрыва02
		for(int i = 0; i < list_explode_02.size(); i++)
		{
			list_explode_02.at(i).UpdateBulletAnim();
		}
		//обновлять класс взрывов
		a_explode.Update();
		//включить столкновения противника с пулями
		ColisionEnemy();
		//обновлять растения
		for(int i = 0; i < list_char.size(); i++)
		{
			list_char[i].Update();
		}
	}
	if(enemy_die > enemy_end)
	{
		isStopGame = true;
	}
}

void LogGame::ColisionEnemy()
{
	for(int en = 0; en < list_char_enemy.size(); en++)
	{
		//проверка столкновения пуль BULLET и BULLET_FREZE
		for(int i = 0; i < list_char.size(); i++)
		{
			for(int j = 0; j < list_char[i].GetBullet().size(); j++)
			{
				EngineRect &_renemy = EngineRect(list_char_enemy[en].GetAnim().Get_X(), list_char_enemy[en].GetAnim().Get_Y(),
												list_char_enemy[en].GetAnim().Width(), list_char_enemy[en].GetAnim().Height());
				EngineRect &_rbull = EngineRect(list_char[i].GetBullet()[j].Get_X(), list_char[i].GetBullet()[j].Get_Y(), 1, 1);

				if(_rbull.IsIntersect(_renemy))
				{
					//если пули синие отталкиваем противника
					if(list_char.at(i).GetBullet().at(j).Tupe() == BULLET_FREZE)
						list_char_enemy.at(en).SetOffset(list_char_enemy.at(en).GetSpeed() * 2);

					if(list_char.at(i).GetBullet().at(j).Tupe() == BULLET_BOMB_2X)
					{
						Animation &_a = bull_tupe_explode_02.GetAnim();
						_a.Initialize(list_char_enemy[en].GetAnim().Get_X() + 5, list_char_enemy[en].GetAnim().Get_Y() + 40);
						bull_tupe_explode_02.SetAnim(_a);
						list_explode_02.push_back(bull_tupe_explode_02);

						list_char_enemy[en].SetDamage(list_char[i].GetBullet()[j].GetDamage());
						list_char[i].DelBullet(j);
					}
					else
					{
						list_char_enemy[en].SetDamage(list_char[i].GetBullet()[j].GetDamage());
						list_char[i].DelBullet(j);
					}
					return;
				}
			}

			//проверка на пуля анимированная или нет и тип пули
			if(list_char[i].GetBullAnim() && bull_tupe_explode.GetTupe() == BULLET_BOMB)
			{
				for(int j = 0; j < list_char[i].GetBulletA().size(); j++)
				{
					EngineRect &_renemy = EngineRect(list_char_enemy[en].GetAnim().Get_X(), list_char_enemy[en].GetAnim().Get_Y(),
													list_char_enemy[en].GetAnim().Width(), list_char_enemy[en].GetAnim().Height());
					EngineRect &_rbull = EngineRect(list_char[i].GetBulletA().at(j).GetAnim().Get_X(), 
													list_char[i].GetBulletA().at(j).GetAnim().Get_Y(), 1, 1);

					if(_rbull.IsIntersect(_renemy))
					{
						Animation &_a = bull_tupe_explode.GetAnim();
						_a.Initialize(list_char_enemy[en].GetAnim().Get_X() + 25, list_char_enemy[en].GetAnim().Get_Y() + 25);
						bull_tupe_explode.SetAnim(_a);
						list_explode.push_back(bull_tupe_explode);

						list_char_enemy[en].SetDamage(list_char[i].GetBulletA()[j].GetDamage());
						list_char[i].DelBulletA(j);
						return;
					}
				}
			}
			if(list_char[i].GetBulletTupe().Tupe() == MINE)
			{
				EngineRect &_renemy = EngineRect(list_char_enemy[en].GetAnim().Get_X(), list_char_enemy[en].GetAnim().Get_Y(),
													list_char_enemy[en].GetAnim().Width(), list_char_enemy[en].GetAnim().Height());
				EngineRect &_rbull = EngineRect(list_char[i].GetAnim().Get_X(), list_char[i].GetAnim().Get_Y(),
													list_char[i].GetAnim().Width(), list_char[i].GetAnim().Height());
				//если мина то убить врага
				if(_rbull.IsIntersect(_renemy))
				{
					Animation an2 = list_char[i].GetAnim();
					Animation an = list_char_enemy[en].GetAnim();
					imposible[((an2.Get_X() - 10) / (WIDTH / COUNT_W))][((an2.Get_Y() - 10) / (HEIGHT / COUNT_H))] = true;
					a_explode.AddEffect(an.Get_X() + 5, an.Get_Y() + 5);
					a_explode.AddEffect(an2.Get_X() + 5, an2.Get_Y() + 5);
					list_char_enemy.erase(list_char_enemy.begin() + en);
					list_char.erase(list_char.begin() + i);
				}
			}
		} 
	}
	//удаление еффект взрыва
	for(int i = 0; i < list_explode.size(); i++)
	{
		if(!list_explode.at(i).GetOnly())
		{
			list_explode.erase(list_explode.begin() + i);
		}
	}
	//удаление еффект взрыва02
	for(int i = 0; i < list_explode_02.size(); i++)
	{
		if(!list_explode_02.at(i).GetOnly())
		{
			list_explode_02.erase(list_explode_02.begin() + i);
		}
	}
}
