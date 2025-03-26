#pragma once
#include "initialize_gl.h"
#include <iostream>
#include <fstream>

static int val_layer = 0;
const char* nameFileData = "data.txt";
my::MyMessage* save_message = NULL;
my::MyMessage* click_message_edit = NULL;
static std::vector<my::MyTexture*> texture_store;
static my::MyTexture tex_none;

void menu_event(int option)
{
	if (option == 1)
		val_layer = 0;
	else if (option == 2)
		val_layer = 1;
	else if (option == 3)
		save_message->save();
	else if (option == 4)
		save_message->load();
}

struct object : public my::Edit_Status
{
	std::string name;
	my::vec2f* pos;
	my::vec2f first_pos;
	my::rectf* rect;
	bool isScroll;
	bool isVisible;
	bool isActive;

	object() { pos = NULL; rect = NULL; name = "none"; isScroll = false; isVisible = true; isActive = false; }
	virtual ~object() { std::cout << "destructor object = " << name << "\n"; name.clear(); delete pos; delete rect; }

	virtual void initialize(std::string _name, my::vec2f* _pos, my::rectf* _rect) = 0;
	virtual void draw(const float time) = 0;
	virtual void update(const float time) = 0;
	virtual void showName() const {}

	inline void IsActive(const bool _val) { isActive = _val; }
	inline void set_pos(const my::vec2f _p)
	{
		pos->x = _p.x;
		pos->y = _p.y;
		first_pos = _p;
		rect->pos.x = pos->x;
		rect->pos.y = pos->y;
	}
	inline void set_pos(const float _x, const float _y)
	{
		pos->x = _x;
		pos->y = _y;
		first_pos = *pos;
		rect->pos.x = pos->x;
		rect->pos.y = pos->y;
	}
	inline void set_offset_pos(const float _x, const float _y)
	{
		pos->x = first_pos.x + _x;
		pos->y = first_pos.y + _y;
		rect->pos.x = pos->x;
		rect->pos.y = pos->y;
	}
	inline void offset_pos(const float _x, const float _y)
	{
		pos->x += _x;
		pos->y += _y;
		rect->pos.x = pos->x;
		rect->pos.y = pos->y;
	}
};

struct buffer_edit
{
	my::MyTexture* tex = &tex_none;
	my::Edit_Status* obj = NULL;
}buff;

class sav_and_load
{
public:
	//сохранение текстур id
	static bool save(const char* _fileName, std::vector<my::renderLayerObjects>& _ob)
	{
		std::ofstream _save(_fileName, std::ios::binary);
		if(_save)
		{
			for (size_t i = 0; i < _ob.size() - 1; i++)
			{
				for (size_t j = 0; j < _ob[i].list.size(); j++)
				{
					my::cube* temp = dynamic_cast<my::cube*>(_ob[i].list[j]);

					_save.write(reinterpret_cast<char*>(&temp->tex->name), (my::len(temp->tex->name) + 1));
				}
			}
			
			_save.close();
			return true;
		}
		std::cout << "file not found (" << _fileName << ")" << std::endl;
		_save.close();
		return false;
	}
	//загрузка текстур id
	static bool load(const char* _fileName, std::vector<my::renderLayerObjects>& _ob)
	{
		std::ifstream _load(_fileName, std::ios::binary);
		if (_load)
		{	
			int beck = 0;
			for (size_t i = 0; i < _ob.size() - 1; i++)
			{
				for (size_t j = 0; j < _ob[i].list.size(); j++)
				{
					char temp[100];
					
					_load.read(reinterpret_cast<char*>(&temp), 100);
					beck += (my::len(temp) + 1);
					_load.seekg(beck, _load.beg);
					my::cube* cub = dynamic_cast<my::cube*>(_ob[i].list[j]);
					for (auto& obj : texture_store)
					{
						//if (obj->name == temp)
						if(strcmp(obj->name, temp) == 0)
						{
							cub->tex = obj;
							cub->tex->initTexture();							
							break;
						}
					}
				}
			}
			_load.close();
			return true;
		}
		std::cout << "file not found (" << _fileName << ")" << std::endl;
		_load.close();
		return false;
	}
};

class ManagerLogick : public my::MyMessage
{
	std::vector<my::renderLayerObjects> listLayer;
	std::vector<my::gameObject*> takeD;
	std::vector<my::gameObject*> giveD;
	std::vector<my::my_polygon> vecShapes;

	my::renderLayerObjects layer;
	my::renderLayerObjects layer2;
	my::renderLayerObjects layer3;
public:
	my::MyTexture tex_hero_01;
	my::MyTexture tex_goblin_01;
	my::MyTexture tex_cat_01;
	my::MyTexture tex_human_01;
	my::MyTexture tex_explode_01;
	my::MyTexture tex_fire_01;
	my::MyTexture tex_unit_01;
	my::MyTexture tex_coin_01;

	my::unitGoblin* goblin;
	my::menu_gl* menu;

	my::myTime my_time;

	const float size_bg = 100;
	int state, button;

public:
	ManagerLogick() 
	{ 
		save_message = this;
	}
	~ManagerLogick() 
	{ 
		for (size_t i = 0; i < listLayer.size(); i++)
		{
			for (size_t j = 0; j < listLayer[i].list.size(); j++)
			{
				my::gameObject* obj = listLayer[i].list[j];
				if (obj) { delete[] obj; }
			}
		}

		delete menu;
		listLayer.clear(); 
		takeD.clear(); 
		giveD.clear(); 
	}

	void save()
	{
		sav_and_load::save(nameFileData, listLayer);

		std::cout << "save ok name file = " << nameFileData << std::endl;
	}

	void load()
	{
		sav_and_load::load(nameFileData, listLayer);

		std::cout << "load ok name file = " << nameFileData << std::endl;
	}

	void Initialize()
	{
		tex_none.LoadTexture("texture/ground/none.png");
		tex_hero_01.LoadTexture("texture/new_hero.png");
		tex_goblin_01.LoadTexture("texture/goblin_04.png");
		tex_cat_01.LoadTexture("texture/cat_01.png");
		tex_human_01.LoadTexture("texture/human_01.png");
		tex_explode_01.LoadTexture("texture/explode_01.png");
		tex_fire_01.LoadTexture("texture/fire_01.png");
		tex_unit_01.LoadTexture("texture/unit_01.png");
		tex_coin_01.LoadTexture("texture/coin_02.png");

		//добавить в список слоев слой
		listLayer.push_back(layer);
		//добавить еще слой в список слоев
		listLayer.push_back(layer2);
		//добавить еще слой в список слоев
		listLayer.push_back(layer3);
		/*my::cube_font* cub_f = new my::cube_font(my::vec2f(100, 100), 100, &tex_hero_01);
		listLayer[2].Add(cub_f);
		cub_f->set_string("heroes ");*/
		//добавить новый тип в слой 2
		//добавляю анимацию прямоугольника

		goblin = new my::unitGoblin(my::rectf(my::vec2f(200, 400), my::vec2f(100, 100)), &tex_goblin_01, 2);
		listLayer[2].Add(goblin);

		my::unitCat* cat = new my::unitCat(my::rectf(my::WID - 200, my::HEI - 200, 200, 200), &tex_cat_01);
		listLayer[2].Add(cat);

		my::unitHuman* human = new my::unitHuman(my::rectf(700, 300, 200, 200), &tex_human_01);
		listLayer[2].Add(human);

		my::unitGuard* guard = new my::unitGuard(my::rectf(400, 400, 100, 100), &tex_unit_01);
		listLayer[2].Add(guard);

		my::animSingle* coin = new my::animSingle(my::rectf(250, 50, 50, 50), my::rectf(0, 0, 100, 100),
			&tex_coin_01, 9, 0);
		listLayer[2].Add(coin);
		coin->isSingle = false;
		coin->set_aliveTime(30);
		coin->anim.set_speed_animation(0.02f);

		//добавляю меню glut
		menu = new my::menu_gl(menu_event);

		//заполнение тайлов слой 1
		for (size_t j = 0; j < 10; j++)
		{
			for (size_t i = 0; i < 10; i++)
			{
				my::vec2f p = my::vec2f((i * size_bg + my::width_sub), (j * size_bg));
				my::cube* cub = new my::cube(p, size_bg, &tex_none);
				listLayer[0].Add(cub);
			}
		}
		//заполнение тайлов слой 2
		for (size_t j = 0; j < 10; j++)
		{
			for (size_t i = 0; i < 10; i++)
			{
				my::vec2f p = my::vec2f((i * size_bg + my::width_sub), (j * size_bg));
				my::cube* cub = new my::cube(p, size_bg, &tex_none);
				listLayer[1].Add(cub);
			}
		}

		my::my_polygon poly1;
		float fTheta = 3.14159f * 2.0f / 5.0f;
		poly1.pos = { 100, 100 };
		poly1.angle = 0.0f;
		for (int i = 0; i < 5; i++)
		{
			poly1.o.push_back({ 30.0f * cosf(fTheta * i), 30.0f * sinf(fTheta * i) });
			poly1.p.push_back({ 30.0f * cosf(fTheta * i), 30.0f * sinf(fTheta * i) });
		}
		my::my_polygon poly2;
		fTheta = 3.14159f * 2.0f / 3.0f;
		poly2.pos = { 200, 150 };
		poly2.angle = 0.0f;
		for (int i = 0; i < 3; i++)
		{
			poly2.o.push_back({ 20.0f * cosf(fTheta * i), 20.0f * sinf(fTheta * i) });
			poly2.p.push_back({ 20.0f * cosf(fTheta * i), 20.0f * sinf(fTheta * i) });
		}
		my::my_polygon poly3;
		poly3.pos = { 50, 200 };
		poly3.angle = 0.0f;
		poly3.o.push_back({ -30, -30 });
		poly3.o.push_back({ -30, +30 });
		poly3.o.push_back({ +30, +30 });
		poly3.o.push_back({ +30, -30 });
		poly3.p.resize(4);

		vecShapes.push_back(poly1);
		vecShapes.push_back(poly2);
		vecShapes.push_back(poly3);
	}

	void mouse(int _button, int _state, int _x, int _y)
	{
		state = _state;
		button = _button;
		if (state == GLUT_UP && button == GLUT_LEFT_BUTTON)
		{
			for (auto obj : listLayer[val_layer].list)
			{			
				my::rectf* r = obj->pos;
				my::rectf r2 = my::rectf(_x - my::width_sub, _y, 2, 2);
				if (my::intersect_rect(*r, r2))
				{
					my::cube* c = dynamic_cast<my::cube*>(obj);
					c->tex = buff.tex;
					if(buff.tex) c->tex->initTexture();
					buff.obj = obj;
					if(click_message_edit)
						click_message_edit->click();
				}
			}
		}
	}

	void mouseMotion(int _x, int _y)
	{
		if (state == GLUT_DOWN && button == GLUT_LEFT_BUTTON)
		{
			for (auto obj : listLayer[val_layer].list)
			{
				my::rectf* r = obj->pos;
				my::rectf r2 = my::rectf(_x - my::width_sub, _y, 2, 2);
				if (my::intersect_rect(*r, r2))
				{
					my::cube* c = dynamic_cast<my::cube*>(obj);
					c->tex = buff.tex;
				}
			}
		}
	}

	void pressKeyboard(float time, unsigned char key)
	{
		//if (key == 'w')
		//{
		//	/*goblin->set_dir(0);
		//	goblin->set_offset_pos(0, -100 * time);*/
		//	goblin->pos->pos.x += cosf(goblin->poly.angle) * 60.0f * time;
		//	goblin->pos->pos.y += sinf(goblin->poly.angle) * 60.0f * time;
		//}
		//else if (key == 's')
		//{
		//	/*goblin->set_dir(2);
		//	goblin->set_offset_pos(0, 100 * time);*/
		//	goblin->pos->pos.x -= cosf(goblin->poly.angle) * 60.0f * time;
		//	goblin->pos->pos.y -= sinf(goblin->poly.angle) * 60.0f * time;
		//}
		//if (key == 'a')
		//{
		//	/*goblin->set_dir(3);
		//	goblin->set_offset_pos(-100 * time, 0);*/
		//	goblin->poly.angle -= 2.0f * time;
		//}
		//else if (key == 'd')
		//{
		//	/*goblin->set_dir(1);
		//	goblin->set_offset_pos(100 * time, 0);*/
		//	goblin->poly.angle += 2.0f * time;
		//}
		if (key == 'w')
		{
			vecShapes[2].pos.x += cosf(vecShapes[2].angle) * 60.0f * time;
			vecShapes[2].pos.y += sinf(vecShapes[2].angle) * 60.0f * time;
		}
		else if (key == 's')
		{
			vecShapes[2].pos.x -= cosf(vecShapes[2].angle) * 60.0f * time;
			vecShapes[2].pos.y -= sinf(vecShapes[2].angle) * 60.0f * time;
		}
		if (key == 'a')
		{
			vecShapes[2].angle -= 2.0f * time;
		}
		else if (key == 'd')
		{
			vecShapes[2].angle += 2.0f * time;
		}
	}

	void reliseKeyboard()
	{
		goblin->isPlay = false;
	}

	void render(float time)
	{
		for (size_t indLayer = 0; indLayer < listLayer.size(); indLayer++)
		{
			listLayer[indLayer].render(time);
		}

		glDisable(GL_TEXTURE_2D);
		glColor3ub(255, 0, 0);
		for (auto& r : vecShapes)
		{
			// Draw Boundary
			for (int i = 0; i < r.p.size(); i++)
			{
				glBegin(GL_LINE_STRIP);
				glVertex2f(r.p[i].x, r.p[i].y/*, r.p[(i + 1) % r.p.size()].x, r.p[(i + 1) % r.p.size()].y*/);
				glVertex2f(r.p[(i + 1) % r.p.size()].x, r.p[(i + 1) % r.p.size()].y);
				glEnd();
			}

			// Draw Direction
			glBegin(GL_LINES);
			glVertex2f(r.p[0].x, r.p[0].y);
			glVertex2f(r.pos.x, r.pos.y);
			glEnd();
		}
		glEnable(GL_TEXTURE_2D);
		//my::ShowStringFont("Time = " + std::to_string((int)_time), 7, 0, 20, my::colori());
		my::ShowStringFont("Time = " + std::to_string(my_time.hour) + ":" + std::to_string(my_time.min) + 
			":" + std::to_string(my_time.sec), 7, 0, 20, my::colori());
		my::ShowStringFont("Layer (" + std::to_string(val_layer + 1) + ")", 7, my::WID / 3, 20);
	}

	void update(float time)
	{
		my_time.update(time);

		takeD.clear();
		giveD.clear();
		for (size_t indLayer = 0; indLayer < listLayer.size(); indLayer++)
		{
			listLayer[indLayer].update(time);

			for (size_t j = 0; j < listLayer[indLayer].list.size(); j++)
			{
				//ссылка на текущий обьект
				my::gameObject* ob = listLayer[indLayer].list[j];
				//кокой обьект может получить урон
				if (dynamic_cast<my::takeDamage*>(ob))
					takeD.push_back(ob);
				//какой обьект может дать урон
				if (dynamic_cast<my::giveDamage*>(ob))
					giveD.push_back(ob);
				//надо удалить анимацию у которой вышло время
				if (dynamic_cast<my::animSingle*>(ob))
				{
					my::animSingle* ani = dynamic_cast<my::animSingle*>(ob);
					if (ani->isDead)
					{
						delete[] ani;
						listLayer[indLayer].list.erase(listLayer[indLayer].list.begin() + j);
					}
				}
			}
		}
		for (auto take : takeD)
		{
			my::takeDamage& td = *dynamic_cast<my::takeDamage*>(take);
			for (auto give : giveD)
			{
				my::giveDamage& gd = *dynamic_cast<my::giveDamage*>(give);
				for (size_t i = 0; i < gd.bullet.m_bullet.size(); i++)
				{
					my::rectf rf = my::rectf(gd.bullet.m_bullet[i].pos, my::vec2f(10, 10));
					if (my::intersect(*take->pos, rf) && td.live > 0)
					{
						std::cout << "пересекается " << std::endl;

						my::rectf& tr = *take->pos;
						
						my::rectf r = my::rectf(my::vec2f(rf.pos.x - tr.size.x / 2, rf.pos.y - tr.size.y / 2), my::vec2f());
						my::animSingle* anim = new my::animSingle(my::rectf(r.pos, my::vec2f(100, 100)),
							my::rectf(my::vec2f(), my::vec2f(112.5, 113)), &tex_explode_01, 7, 5, 0, 0);
						listLayer[2].Add(anim);
						anim->anim.set_speed_animation(0.01f);

						/*my::animSingle* an = new my::animSingle(my::rectf(rf.pos.x - tr.size.x / 2, rf.pos.y - tr.size.y,
							100, 100), my::rectf(my::vec2f(), my::vec2f(160, 160)), &tex_fire_01, 3, 3, 0, 0);
						listLayer[1].Add(an);
						an->anim.set_speed_animation(0.1f);*/

						td.take_damage(gd.give_damage());
						gd.bullet.m_bullet.erase(gd.bullet.m_bullet.begin() + i);
					}
				}
			}
		}

		for (auto& r : vecShapes)
		{
			for (int i = 0; i < r.o.size(); i++)
				r.p[i] =
			{	// 2D Rotation Transform + 2D Translation
				(r.o[i].x * cosf(r.angle)) - (r.o[i].y * sinf(r.angle)) + r.pos.x,
				(r.o[i].x * sinf(r.angle)) + (r.o[i].y * cosf(r.angle)) + r.pos.y,
			};

			r.overlap = false;
		}

		for (int m = 0; m < vecShapes.size(); m++)
			for (int n = m + 1; n < vecShapes.size(); n++)
				//vecShapes[m].overlap |= ShapeOverlap_SAT(vecShapes[m], vecShapes[n]);
				//vecShapes[m].overlap |= ShapeOverlap_SAT_STATIC(vecShapes[m], vecShapes[n]);
				//vecShapes[m].overlap |= ShapeOverlap_DIAGS(vecShapes[m], vecShapes[n]);
				vecShapes[m].overlap |= ShapeOverlap_DIAGS_STATIC(vecShapes[m], vecShapes[n]);
	}
};