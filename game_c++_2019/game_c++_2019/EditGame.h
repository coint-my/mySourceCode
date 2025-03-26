#pragma once
#include "ManagerLogick.h"
#include <experimental/filesystem>

namespace ed
{
	static int counter_panel = 0;
	static int counter_tile = 0;
	const static int offset_scroll_x = 8;

	void showAllTextures(std::vector<my::MyTexture*>& _texture_store)
	{
		namespace fs = std::experimental::filesystem;
		const char* path = "texture/ground";

		if (fs::exists(path))
		{
			std::cout << "directories is found 'textures'" << std::endl;

			for (const auto& entry : fs::directory_iterator(path))
			{
				std::string tmp = entry.path().string();

				if ((tmp.compare(tmp.length() - 3, 3, "jpg") == 0) ||
					(tmp.compare(tmp.length() - 3, 3, "png") == 0))
				{
					std::cout << tmp << std::endl;
					_texture_store.push_back(new my::MyTexture(tmp.c_str()));
				}
				else std::cout << "unknow format texture = " << tmp << std::endl;
			}
		}
		else
		{
			std::cout << "directories is not found 'textures'" << std::endl;
		}
	}

	class panel : public object
	{
	protected:
		my::colori col;	
		my::colori col_active;
	public:
		bool isShowName;
		panel() 
		{
			isShowName = true;
			initialize("panel" + std::to_string(counter_panel++), new my::vec2f(10, 10), new my::rectf(10, 10, 40, 40));
		}
		panel(my::colori _col, my::vec2f _pos)
		{
			isShowName = true;
			col = _col;
			col_active = my::colori(220, 0, 0);
			initialize("panel" + std::to_string(counter_panel++), new my::vec2f(_pos), new my::rectf(_pos.x, _pos.y, 40, 40));
		}
		panel(my::colori _col, my::vec2f _pos, my::vec2f _size)
		{
			isShowName = true;
			col = _col;
			col_active = my::colori(220, 0, 0);
			initialize("panel" + std::to_string(counter_panel++), new my::vec2f(_pos), new my::rectf(_pos.x, _pos.y, _size.x, _size.y));
		}
		panel(my::vec2f* _pos, my::vec2f _size, my::colori _col = my::colori(200, 200, 200))
		{
			isShowName = true;
			col = _col;
			col_active = my::colori(220, 0, 0);
			pos = _pos;
			initialize("panel" + std::to_string(counter_panel++), pos, new my::rectf(pos->x, pos->y, _size.x, _size.y));
		}
		virtual ~panel() { counter_panel--; std::cout << "destructor = " << name << "\n"; }
	protected:
		virtual void initialize(std::string _name, my::vec2f* _pos, my::rectf* _rect) override
		{
			name = _name; pos = _pos; rect = _rect; first_pos = *pos;
		}
		virtual void showName() const 
		{
			if (isShowName)
			{
				int temp = glutBitmapLength((void*)0x0007, (unsigned char*)name.c_str());
				int hei = glutBitmapHeight((void*)0x0007);
				my::ShowStringFont(name, 3, pos->x + ((rect->size.x / 2) - temp / 2), pos->y + hei, my::colori(255, 255, 255));
			}
		}
		virtual void draw(const float time) override
		{
			glDisable(GL_TEXTURE_2D);
			glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
			if (isActive)
				glColor4ub(col_active.r, col_active.g, col_active.b, 200);
			else
				glColor4ub(col.r, col.g, col.b, 200);
			glBegin(GL_QUADS);
			glVertex2f(rect->pos.x, rect->pos.y);
			glVertex2f(rect->pos.x + rect->size.x, rect->pos.y);
			glVertex2f(rect->size.x + rect->pos.x, rect->size.y + rect->pos.y);
			glVertex2f(rect->pos.x, rect->size.y + rect->pos.y);
			glEnd();
			glEnable(GL_TEXTURE_2D);
		}
		virtual void update(const float time) override
		{

		}
	};

	class texture_panel : public panel
	{
	private:
		short border;
		my::MyTexture* texture;
	public:
		texture_panel(my::MyTexture* _texture, my::vec2f _pos) :
			panel(new my::vec2f(_pos), my::vec2f(40, 40)), texture(_texture), border(2) {
			texture->initTexture();
		}
		~texture_panel() { }

	public:
		my::MyTexture* get_tex() { return texture; }

		virtual void showName() const
		{
			if (isShowName)
			{
				int temp = glutBitmapLength((void*)0x0007, (unsigned char*)name.c_str());
				int hei = glutBitmapHeight((void*)0x0007);
				my::ShowStringFont(name, 3, pos->x + ((rect->size.x / 2) - temp / 2), pos->y + hei, my::colori(255, 255, 255));
			}
		}
		void draw(const float time) override
		{
			panel::draw(time);

			glBindTexture(GL_TEXTURE_2D, texture->tex.id);
			glColor3ub(255, 255, 255);
			glBegin(GL_QUADS);
			glTexCoord2f(0, 0); glVertex2f(rect->pos.x + (float)border, rect->pos.y + (float)border);
			glTexCoord2f(1, 0); glVertex2f(rect->pos.x + rect->size.x - (float)border, rect->pos.y + (float)border);
			glTexCoord2f(1, 1); glVertex2f(rect->size.x + rect->pos.x - (float)border, rect->size.y + rect->pos.y - (float)border);
			glTexCoord2f(0, 1); glVertex2f(rect->pos.x + (float)border, rect->size.y + rect->pos.y - (float)border);
			glEnd();
		}
	};

	class textBox : public panel, public my::MyMessage
	{
	private:
		std::string text;
		int x, y;
		bool isFocuse;
	public:
		textBox(my::vec2f* _pos, my::vec2f _size) : panel(_pos, _size) 
		{
			isFocuse = false; 
			my::m_message.push_back(this);
		}
		textBox(my::vec2f _parent, my::vec2f _pos, my::vec2f _size) : panel(new my::vec2f(_parent + _pos), _size)
		{
			isFocuse = false;
			my::m_message.push_back(this);
		}
		virtual ~textBox() { /*std::cout << "destructor textBox = " << name << " " << text << "\n";*/ }

		inline void IsFocuse(const bool _flag) { isFocuse = _flag; }
		inline bool IsFocuse() { return isFocuse; }
		//событие изменение экрана формы
		void call()
		{

		}
		//присвоить новый текст
		void set_text(const char* _text) { text = _text; }
		void set_text(std::string _text) { text = _text; }
		//выровнить по центру плитки текст типа char*
		void setStringCenter(const unsigned char* _text, int& _xOut, int& _yOut)
		{
			int w = glutBitmapLength((void*)0x0006, _text);
			int h = glutBitmapWidth((void*)0x0006, 35);

			int xx = (rect->center().x - (w / 2));
			int yy = (rect->center().y + (h / 2));

			_xOut = xx;
			_yOut = yy;
		}
		//выровнить по центру плитки текст типа string
		void setStringCenter(std::string _text, int& _xOut, int& _yOut)
		{
			int w = glutBitmapLength((void*)0x0006, (unsigned char*)_text.c_str());
			int h = glutBitmapWidth((void*)0x0006, 35);

			int xx = (rect->center().x - (w / 2));
			int yy = (rect->center().y + (h / 2));

			_xOut = xx;
			_yOut = yy;
		}

		void mouse(int button, int state, int _x, int _y)
		{
			if ((button == GLUT_LEFT) && (state == GLUT_UP) && (isFocuse))
			{
				if (my::intersect_rect(*rect, my::rectf(_x, _y, 5, 5)))
					IsActive(true);
			}
			else IsActive(false);
		}

		void keyboard_up(unsigned char _key, int _x, int _y)
		{
			if (isActive && isFocuse)
			{
				//если ключь enter
				if (_key == 13)
					IsActive(false);
				else
				{
					//если ключь backspace
					if (_key == 8 && text.length() != 0)
						text.pop_back();
					else
						text += _key;
				}
				glutPostRedisplay();
			}
		}

		void draw(const float time) override
		{
			panel::draw(time);

			setStringCenter(text, x, y);

			my::ShowStringFont(text, 2, x, y);
		}

		void update(const float time) override 
		{ 

		}
	};

	class textStatus : public my::MyMessage
	{
	private:
		textBox *name;
		textBox *posX;
		textBox *posY;
		my::rectf* par;

		float hei_line;
		float wid_separate;
	public:
		textStatus(my::rectf* const _parent, int _heiLine) 
		{
			par = _parent;
			hei_line = _heiLine;
			wid_separate = _parent->size.x / 2;
			name = new textBox(_parent->pos, my::vec2f(), my::vec2f(_parent->size.x, hei_line));
			posX = new textBox(_parent->pos, my::vec2f(0, hei_line + 2), my::vec2f(wid_separate - 1, hei_line));
			posY = new textBox(_parent->pos, my::vec2f(wid_separate, hei_line + 2), my::vec2f(wid_separate - 1, hei_line));

			my::m_message.push_back(this);
		}
		~textStatus() 
		{ 
			delete name;
			delete posX;
			delete posY;
		}

		inline textBox* getName() const { return name; }
		inline textBox* getPosX() const { return posX; }
		inline textBox* getPosY() const { return posY; }

		void call()
		{
			wid_separate = par->pos.x + (par->size.x / 2);
			name->rect->size.x = par->size.x;
			posX->rect->size.x = par->size.x / 2 - 1;
			posY->set_pos(wid_separate, posY->pos->y);
			posY->rect->size.x = par->size.x / 2 - 1;
		}
	};

	class ScrollBarObject : public panel, public my::MyMessage
	{
	private:
		float scroll_val;
		std::vector<object*> item;
	public:
		enum status { QUAD, LINE, CUSTOM } stat;
	public:
		ScrollBarObject() : scroll_val(0) { stat = QUAD; }
		ScrollBarObject(my::rectf _rect, my::colori _col = my::colori(150, 100, 50)) : 
			panel(new my::vec2f(_rect.pos), my::vec2f(_rect.size), _col), scroll_val(0) 
		{
			my::m_message.push_back(this);
		}
		~ScrollBarObject() 
		{ 
			std::cout << "destructor = " << name << "\n";
			for(auto i : item)
				delete i;
			item.clear();
		}
		void setStatus(const status _val) { stat = _val; }
		void call()
		{
			//вычисл€ем ширину подокна, и задаЄм пр€моугольнику ширину
			GLint w = my::width_sub - offset_scroll_x * 2;
			rect->size.x = w;
			//если плиточна€ система отображени€
			if (stat == QUAD)
			{
				//вычисл€ем высоту подокна, и задаЄм высоту
				//rect->size.y = my::HEI / 2 + rect->pos.y;

				float _siz = (w / 2) + 1;

				//идет разположени€ позиции €чеек в пр€моугольнике
				int i = 0;
				int y = 0;
				for (auto obj : item)
				{
					obj->rect->size.x = _siz;
					obj->rect->size.y = _siz;
					int res_w = w / _siz;
					int x = i % (res_w + 1);

					my::vec2f _pos = my::vec2f(pos->x + (x * _siz), pos->y + (y * _siz));
					obj->set_pos(_pos);

					x == res_w ? y++ : y = y;
					i++;
				}
			}
			else if (stat == LINE)//если линейна€ система отображени€
			{
				//идет разположени€ позиции €чеек в пр€моугольнике
				int i = 0;
				for (auto obj : item)
				{
					float h = 30.0f;
					obj->rect->size.x = w;
					obj->rect->size.y = h;

					my::vec2f _pos = my::vec2f(pos->x, pos->y + (i * h));
					obj->set_pos(_pos);

					i++;
				}
			}
			else if (stat == CUSTOM)
			{

			}
		}

		object& getf() const
		{
			if (item.size() > 0)
			{		
				return *item[0];
			}
			else std::cout << "fail the object of scroll bar" << std::endl;
		}
		object& gete() const
		{
			if (item.size() > 0)
			{
				return *item[item.size() - 1];
			}
			else std::cout << "fail the object of scroll bar" << std::endl;
		}

		void AddItem(object* const _obj)
		{
			item.push_back(_obj);
		}

		bool visible(const float _y) const
		{
			return (_y > rect->pos.y && _y < rect->bottom());
		}

		void mouse(int button, int state, int _x, int _y)
		{
			if (state == GLUT_UP && button == GLUT_LEFT)
			{
				for (auto obj : item)
				{
					obj->IsActive(false);

					if (obj->isVisible)
					{
						my::rectf test = my::rectf(_x, _y, 5, 5);
						if (my::intersect_rect(*obj->rect, test))
						{
							std::cout << "ok" << std::endl;
							obj->IsActive(true);
							if (texture_panel * p = dynamic_cast<texture_panel*>(obj))
							{
								buff.tex = p->get_tex();
								buff.obj = obj;
							}
							else buff.tex = &tex_none;
						}
					}
				}
			}
		}

		void mouseWheel(int button, int dir, int _x, int _y)
		{
			if (my::intersect_rect(my::rectf(rect->pos.x, rect->pos.y, rect->size.x, rect->size.y),
				my::rectf(_x, _y, 2, 2)))
			{
				float _size = getf().rect->size.y;
				float top = getf().pos->y + (_size * 1.4f);
				float bottom = gete().rect->bottom() - (_size * 1.4f);
				if ((dir > 0) && (pos->y < bottom))
				{
					scroll_val += -getf().rect->size.y;
					glutPostRedisplay();
				}
				else if ((dir < 0) && (rect->bottom() > top))
				{
					scroll_val += getf().rect->size.y;
					glutPostRedisplay();
				}
			}
		}

		void draw(const float time) override
		{
			panel::draw(time);
			my::ShowStringFont(std::to_string(scroll_val), 2, rect->center().x, rect->center().y, my::colori(0, 0, 200));

			for (auto i : item)
			{
				if (i->isVisible)
				{
					i->draw(time);
					//i->showName();
				}
			}
		}

		void update(const float time) override
		{
			for (auto i : item)
			{
				i->set_offset_pos(0, scroll_val);
				if (visible(i->rect->center().y)) i->isVisible = true;
				else i->isVisible = false;
			}
		}
	};

	class EditGame : public my::MyMessage
	{
	public:
		ManagerLogick* man;
		ScrollBarObject* bar_texture;
		ScrollBarObject* bar_textBox;
		textStatus* tex_stat;
		/*textBox* textB;
		textBox* textBx;
		textBox* textBy;*/
		std::vector<object*> objects_list;

		EditGame() 
		{
			click_message_edit = this;
		}
		~EditGame() 
		{ 
			for (auto obj : objects_list)
				delete obj;
			objects_list.clear();

			for (auto obj : texture_store)
				delete obj;
			texture_store.clear();
		}

		void addTileScrollBar(object* _obj, object* _parent)
		{
			_obj->isScroll = true;

			ScrollBarObject* _par = dynamic_cast<ScrollBarObject*>(_parent);
			_par->AddItem(_obj);
			counter_tile++;
		}

		void init(ManagerLogick* _man)
		{
			man = _man;
			showAllTextures(texture_store);
			//скролинг панель дл€ текстур
			bar_texture = new ScrollBarObject(my::rectf(8, 10, my::width_sub - 16, my::HEI / 2));
			objects_list.push_back((object*)bar_texture);
			bar_texture->isShowName = false;
			//скролинг панель статуса
			bar_textBox = new ScrollBarObject(my::rectf(8, my::HEI / 2 + 80, my::width_sub - 16, my::HEI - 10));
			//bar_textBox->setStatus(ScrollBarObject::LINE);
			bar_textBox->setStatus(ScrollBarObject::CUSTOM);
			objects_list.push_back((object*)bar_textBox);
			//добовл€ю панель с текстурой
			for (size_t i = 0; i < texture_store.size(); i++)
			{
				texture_panel* p = new texture_panel(texture_store[i], my::vec2f());
				addTileScrollBar(p, bar_texture);
				if (p->get_tex()->tex.width <= 10) p->name = "Clear";
			}
			tex_stat = new textStatus(bar_textBox->rect, 30);
			addTileScrollBar(tex_stat->getName(), bar_textBox);
			addTileScrollBar(tex_stat->getPosX(), bar_textBox);
			addTileScrollBar(tex_stat->getPosY(), bar_textBox);
			////-----------------------Name
			//textB = new textBox(*bar_textBox->pos, my::vec2f(), my::vec2f(bar_textBox->rect->size.x, 20));
			//textB->set_text(buff.tex->name);
			//textB->IsFocuse(true);
			//addTileScrollBar(textB, bar_textBox);
			////-----------------------pos.X
			//textBx = new textBox(*bar_textBox->pos, my::vec2f(0, 22), my::vec2f(100, 20));
			//addTileScrollBar(textBx, bar_textBox);
			////-----------------------pos.Y
			//textBy = new textBox(*bar_textBox->pos, my::vec2f(0, 44), my::vec2f(100, 20));
			//addTileScrollBar(textBy, bar_textBox);

			printf("{%i}\n", counter_tile);
		}

		void click()
		{
			/*textB->set_text(buff.tex->name);
			textBx->set_text("x = " + std::to_string(buff.obj->pos->pos.x));
			textBy->set_text("y = " + std::to_string(buff.obj->pos->pos.y));*/
			presset_status();
			glutSetWindow(my::win_id[1]);
			glutPostRedisplay();
		}

		void presset_status()
		{
			object* ob1 = dynamic_cast<object*>(buff.obj);
			my::gameObject* ob2 = dynamic_cast<my::gameObject*>(buff.obj);

			tex_stat->getName()->set_text(buff.tex->name);
			//textB->set_text(buff.tex->name);

			if (ob1)
			{
				tex_stat->getPosX()->set_text("x = " + std::to_string((int)ob1->pos->x));
				tex_stat->getPosY()->set_text("y = " + std::to_string((int)ob1->pos->y));
				/*textBx->set_text("x = " + std::to_string(ob1->pos->x));
				textBy->set_text("y = " + std::to_string(ob1->pos->y));*/
			}
			else if (ob2)
			{
				tex_stat->getPosX()->set_text("x = " + std::to_string((int)ob2->pos->pos.x));
				tex_stat->getPosY()->set_text("y = " + std::to_string((int)ob2->pos->pos.y));
				/*textBx->set_text("x = " + std::to_string(ob2->pos->pos.x));
				textBy->set_text("y = " + std::to_string(ob2->pos->pos.y));*/
			}
		}

		void keyboard_up(unsigned char _key, int _x, int _y)
		{
			/*textB->keyboard_up(_key, _x, _y);*/
		}

		void mouse(int button, int state, int _x, int _y)
		{
			bar_texture->mouse(button, state, _x, _y);
			bar_textBox->mouse(button, state, _x, _y);
			click_message_edit->click();
		}

		void mouseWheel(int button, int dir, int _x, int _y)
		{
			bar_texture->mouseWheel(button, dir, _x, _y);
			bar_textBox->mouseWheel(button, dir, _x, _y);
		}

		void draw(const float time) const
		{
			for (auto obj : objects_list)
			{
				obj->draw(time);
			}
		}

		void update(const float time)
		{
			for (auto obj : objects_list)
			{
				obj->update(time);
			}
		}
	};
}