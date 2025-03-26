#include <iostream>
#include <fstream>

#include "gl/glut.h"
#include <gl/GLU.h>
#include <math.h>

#include "Blook.h"
#include "player.h"
#include "Trigger.h"
#include "Light.h"
#include "SaveBuffer.h"
#include "StringRead.h"

//#pragma comment(lib, "opengl32.lib")
#pragma comment(lib, "glut32.lib")

std::vector<GameObject*> obj;
std::vector<Light> lights;

Blook data_blooks[COUNT_BLOOKS];
Blook data_keys[MAX_KEYS];
Trigger data_trigger[MAX_TRIGGER];

Player player;
SaveBuffer s_buff;

bool isRight = false;
bool isLeft = false;
bool isBottom = false;
bool isTop = false;
bool isMap[9] = {0, 0, 0, 0, 0, 0, 0, 0, 0 };

int currentLevel = 3;
int counter_fps = 0;
int fps = 0;

void collision(std::vector<GameObject*> &_obj, Player* _ui);
void scripts_trigger(Trigger *_tr, const GameObject* _pl);
void LoadLevel(const char* _fileName, const int _w, const int _h);
void LoadTempLevel(const char* _fileName);
void load_text(const char* _fileName);
bool saveTempLevel(const char* _fileName);
void save_text(const char* _fileName);
std::string getNameLevel(const int _curr);
bool isSqrt(const Point2f& _pos1, const Point2f& _pos2, const float _dist);
void update_doors();
void update_keys();

void reshape(int _w, int _h)
{
	glViewport(0, 0, (GLsizei)_w, (GLsizei)_h);

	glShadeModel(GL_SMOOTH);

	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(0, WIDTH, HEIGHT, 0, -1000, 1000);
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();

	glClearColor(0, 0, 0, 1);
}

void render()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	static float r = 0;
	float r_light = sin(r * (180 / 3.141592)) * 0.01;
	r += 0.007;

	glEnable(GL_LIGHTING);
	glEnable(GL_LIGHT0);
	lights.size() > 0 ? glEnable(GL_LIGHT1) : 0;
	glEnable(GL_DEPTH_TEST);

	const float pos_light[] = { player.get_pos().x, player.get_pos().y, 50.0, 1.0 };
	const float difuse_light[] = { r_light, r_light, r_light, 1.0 };
	const float dif_light[] = { 0.0001, 0.0001, 0.0001, 1.0 };

	glLightModelfv(GL_LIGHT_MODEL_AMBIENT, dif_light);

	glLightfv(GL_LIGHT0, GL_POSITION, pos_light);
	glLightfv(GL_LIGHT0, GL_AMBIENT, difuse_light);
	/*const float d[] = { 1.0001, 1.0001, 1.0001, 1.0 };
	glMaterialfv(GL_FRONT, GL_DIFFUSE, d);*/

	for(std::vector<GameObject*>::const_iterator i = obj.begin(); i != obj.end(); i++)
	{
		(*i)->draw();

		for(int j = 0; j < lights.size(); j++)
			lights[j].draw();
	}

	for(int i = 0; i < CURRENT_KEYS; ++i)
		data_keys[i].draw();

	for(int i = 0; i < CURRENT_TRIGGER; i++)
	{
		data_trigger[i].render();
	}

	glDisable(GL_LIGHTING);
	glDisable(GL_LIGHT0);
	glDisable(GL_LIGHT1);
	glDisable(GL_DEPTH_TEST);

	player.draw();

	glColor3ub(255, 255, 255);
	StringRead::ShowString("FPS : ", 20, 20);
	StringRead::ShowString("Location : ", 20, 40);
	StringRead::ShowString(fps, 80, 20);
	StringRead::ShowString(currentLevel + 1, 80, 40);

	++counter_fps;

	glFlush();
}

void update(int = 0)
{
#if 1
	if(isRight)
	{
		player.offset(player.get_speed(), 0);
	}
	else if(isLeft)
	{
		player.offset(-player.get_speed(), 0);
	}
	if(isBottom)
	{
		player.offset(0, player.get_speed());
	}
	else if(isTop)
	{
		player.offset(0, -player.get_speed());
	}


#endif

	collision(obj, &player);

	scripts_trigger(data_trigger, &player);

	update_keys();
	update_doors();

	glutPostRedisplay();
	glutTimerFunc(15, update, 0);
}
void update_fps(int = 0)
{
	fps = counter_fps;
	counter_fps = 0;
	glutTimerFunc(1000, update_fps, 0);
}
void update_keys()
{
	for(int ind = 0; ind < CURRENT_KEYS; ind++)
	{
		if(std::strcmp(data_keys[ind].get_label(), "key 1") == 0)
			if(isSqrt(data_keys[ind].get_center(), player.get_pos(), 10))
			{
				player.add_keys(1, 0);
				data_keys[ind] = Blook();
			}
		if(std::strcmp(data_keys[ind].get_label(), "key 2") == 0)
			if(isSqrt(data_keys[ind].get_center(), player.get_pos(), 10))
			{
				player.add_keys(1, 1);
				data_keys[ind] = Blook();
			}
		if(std::strcmp(data_keys[ind].get_label(), "key 3") == 0)
			if(isSqrt(data_keys[ind].get_center(), player.get_pos(), 10))
			{
				player.add_keys(1, 2);
				data_keys[ind] = Blook();
			}
		if(std::strcmp(data_keys[ind].get_label(), "key 4") == 0)
			if(isSqrt(data_keys[ind].get_center(), player.get_pos(), 10))
			{
				player.add_keys(1, 3);
				data_keys[ind] = Blook();
			}
		if(std::strcmp(data_keys[ind].get_label(), "key 5") == 0)
			if(isSqrt(data_keys[ind].get_center(), player.get_pos(), 10))
			{
				player.add_keys(1, 4);
				data_keys[ind] = Blook();
			}
		if(std::strcmp(data_keys[ind].get_label(), "key 6") == 0)
			if(isSqrt(data_keys[ind].get_center(), player.get_pos(), 10))
			{
				player.add_keys(1, 5);
				data_keys[ind] = Blook();
			}
		if(std::strcmp(data_keys[ind].get_label(), "key 7") == 0)
			if(isSqrt(data_keys[ind].get_center(), player.get_pos(), 10))
			{
				player.add_keys(1, 6);
				data_keys[ind] = Blook();
			}
		if(std::strcmp(data_keys[ind].get_label(), "key 8") == 0)
			if(isSqrt(data_keys[ind].get_center(), player.get_pos(), 10))
			{
				player.add_keys(1, 7);
				data_keys[ind] = Blook();
			}
	}
}
void update_doors()
{
	for(int ind = 0; ind < CURRENT_OBJECT; ind++)
	{
		if(std::strcmp(data_blooks[ind].get_label(), "dor 1") == 0)
		{
			if((isSqrt(player.get_pos(), data_blooks[ind].get_center(), H_CELL)) &&	player.get_keys().key[0] != 0)
			{
				player.add_keys(-1, 0);
				data_blooks[ind] = Blook();
			}
		}
		if(std::strcmp(data_blooks[ind].get_label(), "dor 2") == 0)
		{
			if((isSqrt(player.get_pos(), data_blooks[ind].get_center(), H_CELL)) &&	player.get_keys().key[1] != 0)
			{
				player.add_keys(-1, 1);
				data_blooks[ind] = Blook();
			}
		}
		if(std::strcmp(data_blooks[ind].get_label(), "dor 3") == 0)
		{
			if((isSqrt(player.get_pos(), data_blooks[ind].get_center(), H_CELL)) &&	player.get_keys().key[2] != 0)
			{
				player.add_keys(-1, 2);
				data_blooks[ind] = Blook();
			}
		}
		if(std::strcmp(data_blooks[ind].get_label(), "dor 4") == 0)
		{
			if((isSqrt(player.get_pos(), data_blooks[ind].get_center(), H_CELL)) &&	player.get_keys().key[3] != 0)
			{
				player.add_keys(-1, 3);
				data_blooks[ind] = Blook();
			}
		}
		if(std::strcmp(data_blooks[ind].get_label(), "dor 5") == 0)
		{
			if((isSqrt(player.get_pos(), data_blooks[ind].get_center(), H_CELL)) &&	player.get_keys().key[4] != 0)
			{
				player.add_keys(-1, 4);
				data_blooks[ind] = Blook();
			}
		}
		if(std::strcmp(data_blooks[ind].get_label(), "dor 6") == 0)
		{
			if((isSqrt(player.get_pos(), data_blooks[ind].get_center(), H_CELL)) &&	player.get_keys().key[5] != 0)
			{
				player.add_keys(-1, 5);
				data_blooks[ind] = Blook();
			}
		}
		if(std::strcmp(data_blooks[ind].get_label(), "dor 7") == 0)
		{
			if((isSqrt(player.get_pos(), data_blooks[ind].get_center(), H_CELL)) &&	player.get_keys().key[6] != 0)
			{
				player.add_keys(-1, 6);
				data_blooks[ind] = Blook();
			}
		}
		if(std::strcmp(data_blooks[ind].get_label(), "dor 8") == 0)
		{
			if((isSqrt(player.get_pos(), data_blooks[ind].get_center(), H_CELL)) &&	player.get_keys().key[7] != 0)
			{
				player.add_keys(-1, 7);
				data_blooks[ind] = Blook();
			}
		}
	}
}
void trigger_level(Trigger *_tr, const int _count)
{
	if(_tr[_count].get_level() == 8)
	{
		_tr[_count].set_active(false);
		printf("--> enter a triger \"%s\"\n", _tr[_count].t_name());
		printf("--> Load Level (\"%i\")\n", _tr[_count].get_level());

		saveTempLevel(getNameLevel(currentLevel).c_str());
				
		if(isMap[8])
			LoadTempLevel(getNameLevel(8).c_str());
		else
			LoadLevel(_tr[_count].t_name(), 20, 15);

		currentLevel = 8;

		lights.push_back(Light(Color4f(400, 300, 70.0, 1.0), Color4f(0.2, 0.2, 0.2, 1.0)));

		return;
	}
	else if(_tr[_count].get_level() == 7)
	{
		_tr[_count].set_active(false);
		printf("--> enter a triger \"%s\"\n", _tr[_count].t_name());
		printf("--> Load Level (\"%i\")\n", _tr[_count].get_level());

		saveTempLevel(getNameLevel(currentLevel).c_str());
				
		if(isMap[7])
			LoadTempLevel(getNameLevel(7).c_str());
		else
			LoadLevel(_tr[_count].t_name(), 20, 15);

		currentLevel = 7;

		lights.push_back(Light(Color4f(400, 300, 70.0, 1.0), Color4f(0.2, 0.2, 0.2, 1.0)));

		return;
	}
	else if(_tr[_count].get_level() == 6)
	{
		_tr[_count].set_active(false);
		printf("--> enter a triger \"%s\"\n", _tr[_count].t_name());
		printf("--> Load Level (\"%i\")\n", _tr[_count].get_level());

		saveTempLevel(getNameLevel(currentLevel).c_str());
				
		if(isMap[6])
			LoadTempLevel(getNameLevel(6).c_str());
		else
			LoadLevel(_tr[_count].t_name(), 20, 15);

		currentLevel = 6;

		lights.push_back(Light(Color4f(400, 300, 70.0, 1.0), Color4f(0.2, 0.2, 0.2, 1.0)));

		return;
	}
	else if(_tr[_count].get_level() == 5)
	{
		_tr[_count].set_active(false);
		printf("--> enter a triger \"%s\"\n", _tr[_count].t_name());
		printf("--> Load Level (\"%i\")\n", _tr[_count].get_level());

		saveTempLevel(getNameLevel(currentLevel).c_str());
				
		if(isMap[5])
			LoadTempLevel(getNameLevel(5).c_str());
		else
			LoadLevel(_tr[_count].t_name(), 20, 15);

		currentLevel = 5;

		lights.push_back(Light(Color4f(400, 300, 70.0, 1.0), Color4f(0.05, 0.05, 0.05, 1.0)));

		return;
	}
	else if(_tr[_count].get_level() == 4)
	{
		_tr[_count].set_active(false);
		printf("--> enter a triger \"%s\"\n", _tr[_count].t_name());
		printf("--> Load Level (\"%i\")\n", _tr[_count].get_level());

		saveTempLevel(getNameLevel(currentLevel).c_str());
				
		if(isMap[4])
			LoadTempLevel(getNameLevel(4).c_str());
		else
			LoadLevel(_tr[_count].t_name(), 20, 15);

		currentLevel = 4;

		lights.push_back(Light(Color4f(400, 300, 70.0, 1.0), Color4f(0.3, 0.3, 0.3, 1.0)));

		return;
	}
	else if(_tr[_count].get_level() == 3)
	{
		_tr[_count].set_active(false);
		printf("--> enter a triger \"%s\"\n", _tr[_count].t_name());
		printf("--> Load Level (\"%i\")\n", _tr[_count].get_level());

		saveTempLevel(getNameLevel(currentLevel).c_str());

		if(isMap[3])
			LoadTempLevel(getNameLevel(3).c_str());
		else
			LoadLevel(_tr[_count].t_name(), 20, 15);

		currentLevel = 3;

		return;
	}
	else if(_tr[_count].get_level() == 2)
	{
		_tr[_count].set_active(false);
		printf("--> enter a triger \"%s\"\n", _tr[_count].t_name());
		printf("--> Load Level (\"%i\")\n", _tr[_count].get_level());

		saveTempLevel(getNameLevel(currentLevel).c_str());

		if(isMap[2])
			LoadTempLevel(getNameLevel(2).c_str());
		else
			LoadLevel(_tr[_count].t_name(), 20, 15);

		currentLevel = 2;

		lights.push_back(Light(Color4f(400, 300, 70.0, 1.0), Color4f(0.1, 0.1, 0.1, 1.0)));

		return;
	}
	else if(_tr[_count].get_level() == 1)
	{
		_tr[_count].set_active(false);
		printf("--> enter a triger \"%s\"\n", _tr[_count].t_name());
		printf("--> Load Level (\"%i\")\n", _tr[_count].get_level());

		saveTempLevel(getNameLevel(currentLevel).c_str());

		if(isMap[1])
			LoadTempLevel(getNameLevel(1).c_str());
		else
			LoadLevel(_tr[_count].t_name(), 20, 15);

		currentLevel = 1;

		lights.push_back(Light(Color4f(400, 300, 70.0, 1.0), Color4f(0.2, 0.2, 0.2, 1.0)));

		return;
	}
	else if(_tr[_count].get_level() == 0)
	{
		_tr[_count].set_active(false);
		printf("--> enter a triger \"%s\"\n", _tr[_count].t_name());
		printf("--> Load Level (\"%i\")\n", _tr[_count].get_level());

		saveTempLevel(getNameLevel(currentLevel).c_str());

		if(isMap[0])
			LoadTempLevel(getNameLevel(0).c_str());
		else
			LoadLevel(_tr[_count].t_name(), 20, 15);

		currentLevel = 0;

		lights.push_back(Light(Color4f(400, 300, 70.0, 1.0), Color4f(0.2, 0.2, 0.27, 1.0)));

		return;
	}
	else
	{
		printf("--> enter a triger \"%s\"\n", _tr[_count].t_name());
		_tr[_count].set_active(false);
	}
}
void scripts_trigger(Trigger *_tr, const GameObject* _pl)
{
	for(int i = 0; i < CURRENT_TRIGGER; i++)
	{
		E_Rect _pr((_pl->get_pos().x - W_CELL / 2) + 2.5, (_pl->get_pos().y - H_CELL / 2) + 2.5, W_CELL - 5, H_CELL - 5);

		if(E_Rect::IsIntersect(_tr[i].get_rect(), _pr) &&_tr[i].IsActive())
		{
			trigger_level(_tr, i);
		}
	}
}
std::string getNameLevel(const int _curr)
{
	switch(_curr)
	{
	case 0: return "temp_level\\l_left_top.dat";
	case 1: return "temp_level\\l_midle_top.dat";
	case 2: return "temp_level\\l_right_top.dat";
	case 3: return "temp_level\\l_left_midle.dat";
	case 4: return "temp_level\\l_midle.dat";
	case 5: return "temp_level\\l_right_midle.dat";
	case 6: return "temp_level\\l_left_bottom.dat";
	case 7: return "temp_level\\l_midle_bottom.dat";
	case 8: return "temp_level\\l_right_bottom.dat";
	default: return "none_name";
	}
}
bool saveTempLevel(const char* _fileName)
{
	save_text(_fileName);
	std::ofstream _save(_fileName, std::ios::out | std::ios::binary);

	if(!_save)
	{
		printf("error saveFile : %s\n", _fileName);
		return false;
	}

	int cx = player.get_pos().x / W_CELL;
	int cy = player.get_pos().y / H_CELL;
	float ccx = (cx * W_CELL) + ((W_CELL / 2) + 2.5);
	float ccy = (cy * H_CELL) + (H_CELL / 2);
	player.set_pos(Point2f(ccx, ccy));
	s_buff.player = player;
	s_buff.count_trigger = CURRENT_TRIGGER;
	s_buff.count_keys = CURRENT_KEYS;

	_save.write(reinterpret_cast<const char*>(&s_buff), sizeof(SaveBuffer));

	for(int i = 0; i < CURRENT_TRIGGER; i++)
	{
		data_trigger[i].set_active(true);
		_save.write(reinterpret_cast<const char*>(&data_trigger[i]), sizeof(Trigger));
	}
	
	//for(int i = 0; i < CURRENT_KEYS; i++)
	//	_save.write(reinterpret_cast<const char*>(&keys[i]), sizeof(KeyObject));

	for(int i = 0; i < CURRENT_OBJECT; i++)
		_save.write(reinterpret_cast<const char*>(&data_blooks[i]), (sizeof(Blook)));

	_save.close();
	printf("save is (ok) : %s\n", _fileName);
	return true;
}
void save_text(const char* _fileName)
{
	std::string _ffileName(_fileName);
	_ffileName.replace(strlen(_fileName) - 3, 3, "txt");

	std::ofstream file(_ffileName, std::ios::out | std::ios::binary);
	file.write((char*) &CURRENT_KEYS, 4);

	if(file)
	{
		for(int ind = 0; ind < CURRENT_KEYS; ++ind)
			file.write((char*) &data_keys[ind], sizeof(Blook));
	}
	file.close();
}
void load_text(const char* _fileName)
{
	std::string _ffileName(_fileName);
	_ffileName.replace(strlen(_fileName) - 3, 3, "txt");

	for(int i = 0; i < MAX_KEYS - 1; i++)
		data_keys[i] = Blook();

	std::ifstream file;
	file.open(_ffileName, std::ios::in);
	file.read((char*) &CURRENT_KEYS, 4);
	int i = 0;

	while(!file.eof() && !file.fail())
	{
		file.read((char*) &data_keys[i++], sizeof(Blook));
	}

	file.close();
}
void LoadTempLevel(const char* _fileName)
{
	load_text(_fileName);
	std::ifstream in_str;
	in_str.open(_fileName, std::ios::in);

	CURRENT_OBJECT = 0;
	CURRENT_TRIGGER = 0;

	obj.clear();
	lights.clear();

	for(int i = 0; i < COUNT_BLOOKS - 1; i++)
		data_blooks[i] = Blook();

	if(in_str)
	{
		in_str.read(reinterpret_cast<char*>(&s_buff), sizeof(SaveBuffer));
		player.set_pos(s_buff.player.get_pos());

		for(int i = 0; i < s_buff.count_trigger; i++)
			in_str.read(reinterpret_cast<char*>(&data_trigger[CURRENT_TRIGGER++]), sizeof(Trigger));


		while(!in_str.eof())
		{
			in_str.read(reinterpret_cast<char*>(&data_blooks[CURRENT_OBJECT]), (sizeof(Blook)));

			if(data_blooks[CURRENT_OBJECT].get_wid() != 0)
				obj.push_back(&data_blooks[CURRENT_OBJECT++]);
		}

		in_str.close();
	}
	else
	{
		printf("error_Open_File: (\"%s\") file not found? \n", _fileName);
		return;
	}

	isRight = 0;
	isLeft = 0;
	isBottom = 0;
	isTop = 0;
	printf("Load_map_current_object = [ %i ] blooks.\n", CURRENT_OBJECT);
	printf("Load_map_current_trigger = [ %i ] trigger.\n", CURRENT_TRIGGER);
	printf("Load keys = [ %i ] keys.\n", CURRENT_KEYS);
	isMap[currentLevel] = true;
}
void LoadLevel(const char* _fileName, const int _w, const int _h)
{
	std::ifstream in_str;
	in_str.open(_fileName, std::ios::in);

	std::string _mass[15];
	if(in_str)
	{
		int i = 0;
		char _ch[21];
		while(!in_str.eof() && i < 15)
		{
			in_str.getline(_ch, _w + 1);
			_mass[i] = _ch;
			++i;
		}
		in_str.close();
	}
	else
	{
		printf("error_Open_File: (\"%s\") file not found? \n", _fileName);
		return;
	}

	CURRENT_OBJECT = 0;
	CURRENT_TRIGGER = 0;
	CURRENT_KEYS = 0;

	obj.clear();
	lights.clear();

	W_CELL = WIDTH / _w;
	H_CELL = HEIGHT / _h;
	bool is_player = false;

	for (int i = 0; i < _h; i++)
	{
		for (int j = 0; j < _w; j++)
		{
			if(_mass[i].at(j) == 'b')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook(p, W_CELL, H_CELL);
				_bb.set_color(Colorf(0.3, 0.3, 0.3));

				data_blooks[CURRENT_OBJECT] = _bb;

				if(CURRENT_OBJECT < MAX_OBJECT)
					obj.push_back(&data_blooks[CURRENT_OBJECT++]);
			}
			else if(_mass[i].at(j) == 'w')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook(10, p, W_CELL, H_CELL);
				_bb.set_color(Colorf(0.3, 0.3, 0.3));

				data_blooks[CURRENT_OBJECT] = _bb;

				if(CURRENT_OBJECT < MAX_OBJECT)
					obj.push_back(&data_blooks[CURRENT_OBJECT++]);
			}
			else if(_mass[i].at(j) == 'a')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook("dor 1", p, W_CELL, H_CELL);
				_bb.init_wall();
				_bb.set_color(Colorf(0.7, 0.7, 0.7));

				data_blooks[CURRENT_OBJECT] = _bb;

				if(CURRENT_OBJECT < MAX_OBJECT)
					obj.push_back(&data_blooks[CURRENT_OBJECT++]);
			}
			else if(_mass[i].at(j) == 'd')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook("dor 2", p, W_CELL, H_CELL);
				_bb.init_wall();
				_bb.set_color(Colorf(0.7, 0.7, 0.7));

				data_blooks[CURRENT_OBJECT] = _bb;

				if(CURRENT_OBJECT < MAX_OBJECT)
					obj.push_back(&data_blooks[CURRENT_OBJECT++]);
			}
			else if(_mass[i].at(j) == 'f')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook("dor 3", p, W_CELL, H_CELL);
				_bb.init_wall();
				_bb.set_color(Colorf(0.7, 0.7, 0.7));

				data_blooks[CURRENT_OBJECT] = _bb;

				if(CURRENT_OBJECT < MAX_OBJECT)
					obj.push_back(&data_blooks[CURRENT_OBJECT++]);
			}
			else if(_mass[i].at(j) == 'g')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook("dor 4", p, W_CELL, H_CELL);
				_bb.init_wall();
				_bb.set_color(Colorf(0.7, 0.7, 0.7));

				data_blooks[CURRENT_OBJECT] = _bb;

				if(CURRENT_OBJECT < MAX_OBJECT)
					obj.push_back(&data_blooks[CURRENT_OBJECT++]);
			}
			else if(_mass[i].at(j) == 'h')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook("dor 5", p, W_CELL, H_CELL);
				_bb.init_wall();
				_bb.set_color(Colorf(0.7, 0.7, 0.7));

				data_blooks[CURRENT_OBJECT] = _bb;

				if(CURRENT_OBJECT < MAX_OBJECT)
					obj.push_back(&data_blooks[CURRENT_OBJECT++]);
			}
			else if(_mass[i].at(j) == 'j')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook("dor 6", p, W_CELL, H_CELL);
				_bb.init_wall();
				_bb.set_color(Colorf(0.7, 0.7, 0.7));

				data_blooks[CURRENT_OBJECT] = _bb;

				if(CURRENT_OBJECT < MAX_OBJECT)
					obj.push_back(&data_blooks[CURRENT_OBJECT++]);
			}
			else if(_mass[i].at(j) == 'k')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook("dor 7", p, W_CELL, H_CELL);
				_bb.init_wall();
				_bb.set_color(Colorf(0.7, 0.7, 0.7));

				data_blooks[CURRENT_OBJECT] = _bb;

				if(CURRENT_OBJECT < MAX_OBJECT)
					obj.push_back(&data_blooks[CURRENT_OBJECT++]);
			}
			else if(_mass[i].at(j) == 'l')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook("dor 8", p, W_CELL, H_CELL);
				_bb.init_wall();
				_bb.set_color(Colorf(0.7, 0.7, 0.7));

				data_blooks[CURRENT_OBJECT] = _bb;

				if(CURRENT_OBJECT < MAX_OBJECT)
					obj.push_back(&data_blooks[CURRENT_OBJECT++]);
			}
			else if(_mass[i].at(j) == 'q')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook("key 1", p, W_CELL, H_CELL, Colorf(0.3, 0.3, 0.3));

				if(CURRENT_KEYS < MAX_KEYS)
					data_keys[CURRENT_KEYS++] = _bb;
			}
			else if(_mass[i].at(j) == 'e')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook("key 2", p, W_CELL, H_CELL, Colorf(0.3, 0.3, 0.3));

				if(CURRENT_KEYS < MAX_KEYS)
					data_keys[CURRENT_KEYS++] = _bb;
			}
			else if(_mass[i].at(j) == 'r')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook("key 3", p, W_CELL, H_CELL, Colorf(0.3, 0.3, 0.3));

				if(CURRENT_KEYS < MAX_KEYS)
					data_keys[CURRENT_KEYS++] = _bb;
			}
			else if(_mass[i].at(j) == 't')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook("key 4", p, W_CELL, H_CELL, Colorf(0.3, 0.3, 0.3));

				if(CURRENT_KEYS < MAX_KEYS)
					data_keys[CURRENT_KEYS++] = _bb;
			}
			else if(_mass[i].at(j) == 'y')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook("key 5", p, W_CELL, H_CELL, Colorf(0.3, 0.3, 0.3));

				if(CURRENT_KEYS < MAX_KEYS)
					data_keys[CURRENT_KEYS++] = _bb;
			}
			else if(_mass[i].at(j) == 'u')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook("key 6", p, W_CELL, H_CELL, Colorf(0.3, 0.3, 0.3));

				if(CURRENT_KEYS < MAX_KEYS)
					data_keys[CURRENT_KEYS++] = _bb;
			}
			else if(_mass[i].at(j) == 'i')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook("key 7", p, W_CELL, H_CELL, Colorf(0.3, 0.3, 0.3));

				if(CURRENT_KEYS < MAX_KEYS)
					data_keys[CURRENT_KEYS++] = _bb;
			}
			else if(_mass[i].at(j) == 'o')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook("key 8", p, W_CELL, H_CELL, Colorf(0.3, 0.3, 0.3));

				if(CURRENT_KEYS < MAX_KEYS)
					data_keys[CURRENT_KEYS++] = _bb;
			}
			else if(_mass[i].at(j) == 'z')
			{
				Point2f p(j * W_CELL, i * H_CELL);
				Blook _bb = Blook(1.2, p, W_CELL, H_CELL);
				_bb.set_color(Colorf(1.0, 0.3, 0.3));

				data_blooks[CURRENT_OBJECT] = _bb;

				if(CURRENT_OBJECT < MAX_OBJECT)
					obj.push_back(&data_blooks[CURRENT_OBJECT++]);
			}
			else if(_mass[i].at(j) == 'p' && !is_player)
			{
				player.set_pos(Point2f(j * W_CELL + 20, i * H_CELL + 20));

				is_player = true;
			}
			else if(_mass[i].at(j) == '9')
			{
				Point2f _p(j * W_CELL + 10, i * H_CELL + 10);
				Trigger _t("levels\\l_9.txt", E_Rect(0, 0, 20, 20), Colorf(0.7, 0.7, 0.7));
				_t.set_pos(_p);
				_t.set_level(8);
				data_trigger[CURRENT_TRIGGER++] = _t;
			}
			else if(_mass[i].at(j) == '8')
			{
				Point2f _p(j * W_CELL + 10, i * H_CELL + 10);
				Trigger _t("levels\\l_8.txt", E_Rect(0, 0, 20, 20), Colorf(0.7, 0.7, 0.7));
				_t.set_pos(_p);
				_t.set_level(7);
				data_trigger[CURRENT_TRIGGER++] = _t;
			}
			else if(_mass[i].at(j) == '7')
			{
				Point2f _p(j * W_CELL + 10, i * H_CELL + 10);
				Trigger _t("levels\\l_7.txt", E_Rect(0, 0, 20, 20), Colorf(0.7, 0.7, 0.7));
				_t.set_pos(_p);
				_t.set_level(6);
				data_trigger[CURRENT_TRIGGER++] = _t;
			}
			else if(_mass[i].at(j) == '6')
			{
				Point2f _p(j * W_CELL + 10, i * H_CELL + 10);
				Trigger _t("levels\\l_6.txt", E_Rect(0, 0, 20, 20), Colorf(0.7, 0.7, 0.7));
				_t.set_pos(_p);
				_t.set_level(5);
				data_trigger[CURRENT_TRIGGER++] = _t;
			}
			else if(_mass[i].at(j) == '5')
			{
				Point2f _p(j * W_CELL + 10, i * H_CELL + 10);
				Trigger _t("levels\\l_5.txt", E_Rect(0, 0, 20, 20), Colorf(0.7, 0.7, 0.7));
				_t.set_pos(_p);
				_t.set_level(4);
				data_trigger[CURRENT_TRIGGER++] = _t;
			}
			else if(_mass[i].at(j) == '4')
			{
				Point2f _p(j * W_CELL + 10, i * H_CELL + 10);
				Trigger _t("levels\\l_4.txt", E_Rect(0, 0, 20, 20), Colorf(0.7, 0.7, 0.7));
				_t.set_pos(_p);
				_t.set_level(3);
				data_trigger[CURRENT_TRIGGER++] = _t;
			}
			else if(_mass[i].at(j) == '3')
			{
				Point2f _p(j * W_CELL + 10, i * H_CELL + 10);
				Trigger _t("levels\\l_3.txt", E_Rect(0, 0, 20, 20), Colorf(0.7, 0.7, 0.7));
				_t.set_pos(_p);
				_t.set_level(2);
				data_trigger[CURRENT_TRIGGER++] = _t;
			}
			else if(_mass[i].at(j) == '2')
			{
				Point2f _p(j * W_CELL + 10, i * H_CELL + 10);
				Trigger _t("levels\\l_2.txt", E_Rect(0, 0, 20, 20), Colorf(0.7, 0.7, 0.7));
				_t.set_pos(_p);
				_t.set_level(1);
				data_trigger[CURRENT_TRIGGER++] = _t;
			}
			else if(_mass[i].at(j) == '1')
			{
				Point2f _p(j * W_CELL + 10, i * H_CELL + 10);
				Trigger _t("levels\\l_1.txt", E_Rect(0, 0, 20, 20), Colorf(0.7, 0.7, 0.7));
				_t.set_pos(_p);
				_t.set_level(0);
				data_trigger[CURRENT_TRIGGER++] = _t;
			}
		}
	}

	isMap[currentLevel] = true;

	printf("Load_map_current_object = [ %i ] blooks.\n", CURRENT_OBJECT);
	printf("Load_map_current_trigger = [ %i ] trigger.\n", CURRENT_TRIGGER);
	printf("Load keys = [ %i ] keys.\n", CURRENT_KEYS);
}
bool isSqrt(const Point2f& _pos1, const Point2f& _pos2, const float _dist)
{
	Point2f _p1(_pos1.x + (W_CELL / 2), _pos1.y + (H_CELL / 2));
	Point2f _p2(_pos2.x + (W_CELL / 2), _pos2.y + (H_CELL / 2));

	float d = sqrt((_p1.x - _p2.x) * (_p1.x - _p2.x) + (_p1.y - _p2.y) * (_p1.y - _p2.y));

	if(d < _dist)
		return true;
	return false;
}
bool detection(std::vector<GameObject*> &_obj, const float _massa, const int _c, const int _sp)
{
	bool isT = false;
	E_Rect _r2(_obj[_c]->get_pos().x + 1, _obj[_c]->get_pos().y + 1, W_CELL - 2, H_CELL - 2);
	int _count = 0;
	int _count2 = 0;
	for(std::vector<GameObject*>::iterator i = _obj.begin(); i != _obj.end(); ++i)
	{
		E_Rect _ri((*i)->get_pos().x, (*i)->get_pos().y, W_CELL, H_CELL);
		
		if((_count != _c) && (E_Rect::IsIntersect(_r2, _ri)))
		{
			const float _m = ((*i)->get_weight() + _obj[_c]->get_weight()); 

			if(isRight && (_m < _massa))
			{
				E_Rect _tmp(_ri);
				_tmp.offset(_sp, 0);

				for(std::vector<GameObject*>::iterator j = _obj.begin(); j != _obj.end(); ++j)
				{
					E_Rect _t((*j)->get_pos().x, (*j)->get_pos().y,  W_CELL, H_CELL);

					if(E_Rect::IsIntersect(_tmp, _t) && (_count != _count2) && (_count2 != _c))
						isT = true;

					_count2++;
				}

				if(!isT)
				{
					(*i)->set_offset(_sp + 0.5, 0);
				}
			}
			else if(isLeft && (_m < _massa))
			{
				E_Rect _tmp(_ri);
				_tmp.offset(-_sp, 0);

				for(std::vector<GameObject*>::iterator j = _obj.begin(); j != _obj.end(); ++j)
				{
					E_Rect _t((*j)->get_pos().x, (*j)->get_pos().y,  W_CELL, H_CELL);

					if(E_Rect::IsIntersect(_tmp, _t) && (_count != _count2) && (_count2 != _c))
						isT = true;

					_count2++;
				}

				if(!isT)
				{
					(*i)->set_offset(-(_sp + 0.5), 0);
				}
			}
			else if(isTop && (_m < _massa))
			{
				E_Rect _tmp(_ri);
				_tmp.offset(0, -_sp);

				for(std::vector<GameObject*>::iterator j = _obj.begin(); j != _obj.end(); ++j)
				{
					E_Rect _t((*j)->get_pos().x, (*j)->get_pos().y,  W_CELL, H_CELL);

					if(E_Rect::IsIntersect(_tmp, _t) && (_count != _count2) && (_count2 != _c))
						isT = true;

					_count2++;
				}

				if(!isT)
				{
					(*i)->set_offset(0, -(_sp + 0.5));
				}
			}
			else if(isBottom && (_m < _massa))
			{
				E_Rect _tmp(_ri);
				_tmp.offset(0, _sp);

				for(std::vector<GameObject*>::iterator j = _obj.begin(); j != _obj.end(); ++j)
				{
					E_Rect _t((*j)->get_pos().x, (*j)->get_pos().y,  W_CELL, H_CELL);

					if(E_Rect::IsIntersect(_tmp, _t) && (_count != _count2) && (_count2 != _c))
						isT = true;

					_count2++;
				}

				if(!isT)
				{
					(*i)->set_offset(0, (_sp + 0.5));
				}
			}
			else isT = true;
		}
		_count++;
	}
	return isT;
}
void collision(std::vector<GameObject*> &_obj, Player* _ui)
{
	int _count = 0;
	for(std::vector<GameObject*>::iterator i = _obj.begin(); i != _obj.end(); ++i)
	{
		Point2f _p1((*i)->get_pos().x, (*i)->get_pos().y);
		Point2f _p2(_ui->get_pos().x - W_CELL / 2, _ui->get_pos().y - H_CELL / 2);

		E_Rect _r1(_p1.x, _p1.y, W_CELL, H_CELL);
		E_Rect _r2(_p2.x + 2.5, _p2.y + 2.5, W_CELL - 5, H_CELL - 5);

		if(E_Rect::IsIntersect(_r1, _r2) /*&& _count != 300*/)
		{
			const float _sp = _ui->get_speed();

			if(isRight)
			{
				if(!isTop && !isBottom)
				{
					if(_sp > (*i)->get_weight())
						(*i)->set_offset(_sp - (*i)->get_weight(), 0);

					if(detection(_obj, _sp, _count, (_sp - (*i)->get_weight())))
						(*i)->set_offset(-(_sp - (*i)->get_weight()), 0);
				}
				_ui->offset(-_sp ,0);
			}
			else if(isLeft)
			{
				if(!isTop && !isBottom)
				{
					if(_sp > (*i)->get_weight())
						(*i)->set_offset(-(_sp - (*i)->get_weight()), 0);

					if(detection(_obj, _sp, _count, (_sp - (*i)->get_weight())))
						(*i)->set_offset((_sp - (*i)->get_weight()), 0);
				}

				_ui->offset(_sp, 0);
			}
			if(isTop)
			{
				if(!isLeft && !isRight)
				{
					if(_sp > (*i)->get_weight())
						(*i)->set_offset(0, -(_sp - (*i)->get_weight()));

					if(detection(_obj, _sp, _count, (_sp - (*i)->get_weight())))
						(*i)->set_offset(0, (_sp - (*i)->get_weight()));
				}

				_ui->offset(0, _sp);
			}
			else if(isBottom)
			{
				if(!isLeft && !isRight)
				{
					if(_sp > (*i)->get_weight())
						(*i)->set_offset(0, (_sp - (*i)->get_weight()));

					if(detection(_obj, _sp, _count, (_sp - (*i)->get_weight())))
						(*i)->set_offset(0, -(_sp - (*i)->get_weight()));
				}

				_ui->offset(0, -_sp);
			}
		}
		_count++;
	}
}
void special(int _key, int x, int y)
{
	switch(_key)
	{
	case GLUT_KEY_RIGHT:
		isRight = true;
		break;
	case GLUT_KEY_LEFT:
		isLeft = true;
		break;
	case GLUT_KEY_DOWN:
		isBottom = true;
		break;
	case GLUT_KEY_UP:
		isTop = true;
		break;
	}
}
void special_up(int _key, int x, int y)
{
	switch(_key)
	{
	case GLUT_KEY_RIGHT:
		isRight = false;
		break;
	case GLUT_KEY_LEFT:
		isLeft = false;
		break;
	case GLUT_KEY_DOWN:
		isBottom = false;
		break;
	case GLUT_KEY_UP:
		isTop = false;
		break;
	}
}

int main(int argc, char** argv)
{
	LoadLevel("levels\\l_4.txt", 20, 15);

	/*Blook _m[2];
	Blook g1 = Blook(Point2f(), W_CELL, H_CELL);
	Blook g2 = Blook(0, Point2f(), W_CELL, H_CELL);
	_m[0] = g1;
	_m[1] = g2;

	int _t[2] = {0};
	int _t2[2] = {0};

	_t[0] = sizeof(Blook);
	_t[1] = sizeof(Blook);

	_t2[0] = _m[0].get_memory();
	_t2[1] = _m[1].get_memory();*/

	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_RGB | GLUT_SINGLE | GLUT_DEPTH);
	glutInitWindowPosition(200, 200);
	glutInitWindowSize(WIDTH, HEIGHT);
	glutCreateWindow("GameCircle");

	glutReshapeFunc(reshape);
	glutDisplayFunc(render);
	glutSpecialFunc(special);
	glutSpecialUpFunc(special_up);
	update();
	update_fps();

	glutMainLoop();

	return 0;
}