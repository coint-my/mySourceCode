#pragma once
#include <ctime>
#include <string>
#include <vector>
#include <cstdlib>
#include <cmath>
#include <algorithm>
#include "gl/freeglut.h"
#include "gl/src/SOIL.h"

namespace my 
{
	static int WID = 1024;
	static int HEI = 600;

	const static int width_sub_precent = 20;
	static GLint width_sub = 0;

	static GLuint _texture = 0;
#pragma region Misc
	class Edit_Status
	{
		virtual void dds() { }
	};
	class MyMessage
	{
	public:
		virtual void call() { }
		virtual void save() { }
		virtual void load() { }
		virtual void click() { }
	};

	std::vector<MyMessage*> m_message;

	size_t len(const char* _val)
	{
		size_t t = 0;
		while (*_val++ != '\0') { t++; }
		return t;
	}
#pragma endregion

#pragma region Texture structure
	struct texture
	{
		int chanel;
		unsigned char* data;
		GLint width, height;
		GLuint id;

		bool LoadTexture(const char* _fileName)
		{
			data = SOIL_load_image(_fileName, &width, &height, &chanel, SOIL_LOAD_AUTO);
			if (data == 0)
				return false;
			_texture++;
			return true;
		}

		void freeImage()
		{
			_texture--;
		}
		~texture()
		{
			std::cout << "destructor texture" << "\n";
			freeImage();
			glDeleteTextures(1, &id);
			SOIL_free_image_data(data);
			data = 0;
		}
	};
	class MyTexture
	{
	public: texture tex;
	public: char name[100];
	
	public: inline void LoadTexture(const char* _fn)
	{
		if (!tex.LoadTexture(_fn))
			printf("texture failure! file path(%s)\n", _fn);

		memcpy(name, _fn, len(_fn));
		name[len(_fn)] = '\0';
		//glGenTextures(2, &tex.id);
		tex.id = _texture;
		glBindTexture(GL_TEXTURE_2D, tex.id);

		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);

		if (tex.chanel == 3)
			glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, tex.width, tex.height, 0,
				GL_RGB, GL_UNSIGNED_BYTE, tex.data);
		if (tex.chanel == 4)
		{
			glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
			//glDepthMask(GL_FALSE);

			glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, tex.width, tex.height, 0,
				GL_RGBA, GL_UNSIGNED_BYTE, tex.data);
		}
	}
	public: inline void initTexture()
	{
		//glGenTextures(2, &tex.id);
		//tex.id = _texture;
		glBindTexture(GL_TEXTURE_2D, tex.id);

		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);

		if (tex.chanel == 3)
			glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, tex.width, tex.height, 0,
				GL_RGB, GL_UNSIGNED_BYTE, tex.data);
		if (tex.chanel == 4)
		{
			glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
			//glDepthMask(GL_FALSE);

			glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, tex.width, tex.height, 0,
				GL_RGBA, GL_UNSIGNED_BYTE, tex.data);
		}
	}

	public: MyTexture() { }
	public: MyTexture(const char* fileName) { LoadTexture(fileName); }
	public: ~MyTexture()
	{
		std::cout << "destructor myTexture" << "\n";
	}
	};
#pragma endregion

#pragma region Game Objects

	struct vec2f
	{
		float x, y;
		vec2f() : x(0), y(0) {}
		vec2f(float _x, float _y) : x(_x), y(_y) { }
		const vec2f& operator+(const vec2f& _val)
		{
			return vec2f(x + _val.x, y + _val.y);
		}
	};
	struct rectf
	{
		vec2f pos;
		vec2f size;
		rectf() : pos(), size(10, 10) { }
		rectf(vec2f _pos, vec2f _size) : pos(_pos), size(_size) { }
		rectf(float _x, float _y, float _wid, float _hei) : pos(vec2f(_x, _y)), size(vec2f(_wid, _hei)) { }
		float right() { return pos.x + size.x; }
		float bottom() { return pos.y + size.y; }
		inline const vec2f center() { return vec2f(pos.x + size.x / 2, pos.y + size.y / 2); }
		void set_pos(vec2f _pos) { pos = _pos; }
		rectf operator-(const rectf& r) { return rectf(vec2f(pos.x - r.pos.x, pos.y - r.pos.y), vec2f(r.size.x, r.size.y)); }
	};
	struct myTime
	{
		unsigned short sec;
		unsigned short min;
		unsigned short hour;
		float msec;
		myTime() : sec(0), min(0), hour(0), msec(0) { }
		myTime(const unsigned short _sec, const unsigned short _min, const unsigned short _hour) :
			sec(_sec), min(_min), hour(_hour), msec(0) { }

		virtual ~myTime() { }

		void update(float _msec)
		{
			msec += _msec;

			if (msec >= 1.0f)
			{
				sec++;
				if (sec == 60) { sec = 0; min++; }
				if (min == 60) { min = 0; hour++; }
				msec = 0;
			}
		}
	};
	struct my_polygon
	{
		std::vector<my::vec2f> p;
		my::vec2f pos;
		float angle;
		std::vector<my::vec2f> o;
		bool overlap;
	};

	static bool ShapeOverlap_SAT(my_polygon& r1, my_polygon& r2)
	{
		my_polygon* poly1 = &r1;
		my_polygon* poly2 = &r2;

		for (int shape = 0; shape < 2; shape++)
		{
			if (shape == 1)
			{
				poly1 = &r2;
				poly2 = &r1;
			}

			for (int a = 0; a < poly1->p.size(); a++)
			{
				int b = (a + 1) % poly1->p.size();
				my::vec2f axisProj = { -(poly1->p[b].y - poly1->p[a].y), poly1->p[b].x - poly1->p[a].x };
				float d = sqrtf(axisProj.x * axisProj.x + axisProj.y * axisProj.y);
				axisProj = { axisProj.x / d, axisProj.y / d };

				// Work out min and max 1D points for r1
				float min_r1 = INFINITY, max_r1 = -INFINITY;
				for (int p = 0; p < poly1->p.size(); p++)
				{
					float q = (poly1->p[p].x * axisProj.x + poly1->p[p].y * axisProj.y);
					min_r1 = std::min(min_r1, q);
					max_r1 = std::max(max_r1, q);
				}

				// Work out min and max 1D points for r2
				float min_r2 = INFINITY, max_r2 = -INFINITY;
				for (int p = 0; p < poly2->p.size(); p++)
				{
					float q = (poly2->p[p].x * axisProj.x + poly2->p[p].y * axisProj.y);
					min_r2 = std::min(min_r2, q);
					max_r2 = std::max(max_r2, q);
				}

				if (!(max_r2 >= min_r1 && max_r1 >= min_r2))
					return false;
			}
		}

		return true;
	}

	static bool ShapeOverlap_SAT_STATIC(my_polygon& r1, my_polygon& r2)
	{
		my_polygon* poly1 = &r1;
		my_polygon* poly2 = &r2;

		float overlap = INFINITY;

		for (int shape = 0; shape < 2; shape++)
		{
			if (shape == 1)
			{
				poly1 = &r2;
				poly2 = &r1;
			}

			for (int a = 0; a < poly1->p.size(); a++)
			{
				int b = (a + 1) % poly1->p.size();
				my::vec2f axisProj = { -(poly1->p[b].y - poly1->p[a].y), poly1->p[b].x - poly1->p[a].x };

				// Optional normalisation of projection axis enhances stability slightly
				//float d = sqrtf(axisProj.x * axisProj.x + axisProj.y * axisProj.y);
				//axisProj = { axisProj.x / d, axisProj.y / d };

				// Work out min and max 1D points for r1
				float min_r1 = INFINITY, max_r1 = -INFINITY;
				for (int p = 0; p < poly1->p.size(); p++)
				{
					float q = (poly1->p[p].x * axisProj.x + poly1->p[p].y * axisProj.y);
					min_r1 = std::min(min_r1, q);
					max_r1 = std::max(max_r1, q);
				}

				// Work out min and max 1D points for r2
				float min_r2 = INFINITY, max_r2 = -INFINITY;
				for (int p = 0; p < poly2->p.size(); p++)
				{
					float q = (poly2->p[p].x * axisProj.x + poly2->p[p].y * axisProj.y);
					min_r2 = std::min(min_r2, q);
					max_r2 = std::max(max_r2, q);
				}

				// Calculate actual overlap along projected axis, and store the minimum
				overlap = std::min(std::min(max_r1, max_r2) - std::max(min_r1, min_r2), overlap);

				if (!(max_r2 >= min_r1 && max_r1 >= min_r2))
					return false;
			}
		}

		// If we got here, the objects have collided, we will displace r1
		// by overlap along the vector between the two object centers
		my::vec2f d = { r2.pos.x - r1.pos.x, r2.pos.y - r1.pos.y };
		float s = sqrtf(d.x * d.x + d.y * d.y);
		r1.pos.x -= overlap * d.x / s;
		r1.pos.y -= overlap * d.y / s;
		return false;
	}

	static bool ShapeOverlap_DIAGS_STATIC(my_polygon& r1, my_polygon& r2)
	{
		my_polygon* poly1 = &r1;
		my_polygon* poly2 = &r2;

		for (int shape = 0; shape < 2; shape++)
		{
			if (shape == 1)
			{
				poly1 = &r2;
				poly2 = &r1;
			}

			// Check diagonals of this polygon...
			for (int p = 0; p < poly1->p.size(); p++)
			{
				my::vec2f line_r1s = poly1->pos;
				my::vec2f line_r1e = poly1->p[p];

				my::vec2f displacement = { 0,0 };

				// ...against edges of this polygon
				for (int q = 0; q < poly2->p.size(); q++)
				{
					my::vec2f line_r2s = poly2->p[q];
					my::vec2f line_r2e = poly2->p[(q + 1) % poly2->p.size()];

					// Standard "off the shelf" line segment intersection
					float h = (line_r2e.x - line_r2s.x) * (line_r1s.y - line_r1e.y) - (line_r1s.x - line_r1e.x) * (line_r2e.y - line_r2s.y);
					float t1 = ((line_r2s.y - line_r2e.y) * (line_r1s.x - line_r2s.x) + (line_r2e.x - line_r2s.x) * (line_r1s.y - line_r2s.y)) / h;
					float t2 = ((line_r1s.y - line_r1e.y) * (line_r1s.x - line_r2s.x) + (line_r1e.x - line_r1s.x) * (line_r1s.y - line_r2s.y)) / h;

					if (t1 >= 0.0f && t1 < 1.0f && t2 >= 0.0f && t2 < 1.0f)
					{
						displacement.x += (1.0f - t1) * (line_r1e.x - line_r1s.x);
						displacement.y += (1.0f - t1) * (line_r1e.y - line_r1s.y);
					}
				}

				r1.pos.x += displacement.x * (shape == 0 ? -1 : +1);
				r1.pos.y += displacement.y * (shape == 0 ? -1 : +1);
			}
		}

		// Cant overlap if static collision is resolved
		return false;
	}

	static bool ShapeOverlap_DIAGS(my_polygon& r1, my_polygon& r2)
	{
		my_polygon* poly1 = &r1;
		my_polygon* poly2 = &r2;

		for (int shape = 0; shape < 2; shape++)
		{
			if (shape == 1)
			{
				poly1 = &r2;
				poly2 = &r1;
			}

			// Check diagonals of polygon...
			for (int p = 0; p < poly1->p.size(); p++)
			{
				my::vec2f line_r1s = poly1->pos;
				my::vec2f line_r1e = poly1->p[p];

				// ...against edges of the other
				for (int q = 0; q < poly2->p.size(); q++)
				{
					my::vec2f line_r2s = poly2->p[q];
					my::vec2f line_r2e = poly2->p[(q + 1) % poly2->p.size()];

					// Standard "off the shelf" line segment intersection
					float h = (line_r2e.x - line_r2s.x) * (line_r1s.y - line_r1e.y) - (line_r1s.x - line_r1e.x) * (line_r2e.y - line_r2s.y);
					float t1 = ((line_r2s.y - line_r2e.y) * (line_r1s.x - line_r2s.x) + (line_r2e.x - line_r2s.x) * (line_r1s.y - line_r2s.y)) / h;
					float t2 = ((line_r1s.y - line_r1e.y) * (line_r1s.x - line_r2s.x) + (line_r1e.x - line_r1s.x) * (line_r1s.y - line_r2s.y)) / h;

					if (t1 >= 0.0f && t1 < 1.0f && t2 >= 0.0f && t2 < 1.0f)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	bool intersect(rectf& const r1, rectf& const r2)
	{
		float dis = (sqrt(pow((r1.center().x - r2.center().x), 2) + pow(r1.center().y - r2.center().y, 2)));

		return dis < ((r1.size.x + r1.size.y) / 4 + (r2.size.x + r2.size.y) / 4);
	}
	bool intersect_rect(rectf _r1, rectf _r2)
	{
		if ((_r1.pos.x < _r2.pos.x) && (_r1.pos.y < _r2.pos.y) && (_r1.right() > _r2.right()) && (_r1.bottom() > _r2.bottom()))
			return true;
		else return false;
	}
	
	struct colori
	{
		int b, g, r;
		colori() : r(255), g(255), b(255) {}
		colori(int _r, int _g, int _b) : r(_r), g(_g), b(_b) {}
	};
	struct bullet
	{
		short rad;
		short seg;
		int btime;
		float t; 
		colori col;
		vec2f pos;

		bullet() {}
		bullet(vec2f _pos, colori _col = colori(), short _rad = 10, short _seg = 10) : 
			pos(_pos), col(_col), rad(_rad), seg(_seg)
		{
			t = 0;
			btime = 0;
		}

		void draw(float time)
		{
			glColor3ub(col.r, col.g, col.b);
			glDisable(GL_TEXTURE_2D);
			glBegin(GL_POLYGON);
			for (size_t i = 0; i < seg; i++)
			{
				float angle = (3.1415 * 2) / seg;
				float x = cos(angle * i) * rad + pos.x;
				float y = sin(angle * i) * rad + pos.y;
				glVertex2f(x, y);
			}
			glEnd();
			glEnable(GL_TEXTURE_2D);
		}

		void update(float time)
		{
			t += time;
			if (t >= 1.0f)
			{
				btime++;
				t = 0;
			}
		}

		virtual ~bullet() { }
	};

	class sourceBullet
	{
	private:
		int liveBullet;
		vec2f pos;
		colori col;
	public:
		std::vector<bullet> m_bullet;

		float speed;

		sourceBullet() : col(colori()), liveBullet(10) { }
		sourceBullet(const vec2f& _pos, const colori _col = colori()) : pos(_pos), col(_col), speed(10), liveBullet(10) { }
		sourceBullet(const vec2f& _pos, float _speed, const colori _col = colori()) : col(_col),
			pos(_pos), speed(_speed), liveBullet(10) { }

		void makeBullet() { m_bullet.push_back(bullet(vec2f(pos), col)); }

		void draw(float time)
		{
			for (auto &i : m_bullet)
				i.draw(time);
		}

		void update(float time)
		{
			for (size_t i = 0; i < m_bullet.size(); i++)
			{
				m_bullet[i].update(time);
				bullet& bul = m_bullet[i];
				bul.pos.x -= speed * time;
				bul.pos.y += (cos((3.1415 * 2 / 5) * bul.btime) * 20) * time;
				if (m_bullet[i].btime >= liveBullet)
					m_bullet.erase(m_bullet.begin() + i);
			}
		}

		virtual ~sourceBullet() { } 
	};

	struct DrawRect
	{
		vec2f sizeMax;
		vec2f size;
		vec2f* pos;
		colori col;
		DrawRect() : pos(NULL), sizeMax(vec2f(10, 10)), col(), size(sizeMax) { }
		DrawRect(vec2f* _pos, vec2f _sizeMax, colori _col) : col(_col), sizeMax(_sizeMax), size(_sizeMax)
		{
			pos = _pos;
		}

		void set_pos(vec2f* _pos) { pos = _pos; }

		inline void offset_val(const float _val) { size.x += _val; }
		inline void set_val(const float _val) { size.x = _val; }

		void draw(float time)
		{
			glColor3ub(col.r, col.g, col.b);
			glDisable(GL_TEXTURE_2D);
			glBegin(GL_QUADS);
			glVertex2f(pos->x,			pos->y);
			glVertex2f(pos->x + size.x, pos->y);
			glVertex2f(pos->x + size.x, pos->y + size.y);
			glVertex2f(pos->x,			pos->y + size.y);
			glEnd();
			glEnable(GL_TEXTURE_2D);
		}

		virtual~DrawRect() { }
	};

	struct takeDamage
	{
		float live;
		float maxLive;
		DrawRect healBar;
		takeDamage() : live(110), maxLive(live) { }
		takeDamage(rectf* _pos, const float _maxLive) : live(_maxLive), maxLive(_maxLive),
			healBar(&_pos->pos, vec2f(_pos->size.x, 10), colori(255, 0, 0)) { }
		virtual void take_damage(float _val)
		{
			float div_precent = maxLive / healBar.sizeMax.x;
			float tmp = _val / div_precent;
			live -= tmp;
			
			if (live <= 0)
			{
				live = 0;
				healBar.set_val(live);
			}
			else healBar.offset_val(-(tmp / div_precent));
		}
		virtual ~takeDamage() { }
	};
	struct giveDamage
	{
		sourceBullet bullet;
		giveDamage() { }
		virtual float give_damage() { return 10; }
		virtual ~giveDamage() { }
	};

	class gameObject : public Edit_Status
	{
	public:
		rectf* pos;
		gameObject()
		{
			pos = NULL;
		}
		gameObject(rectf* _pos)
		{
			pos = _pos;
		}
		virtual void render(float time) = 0;
		virtual void update(float time) = 0;
		virtual ~gameObject() { }
	};

	struct cube : public gameObject
	{
		vec2f point[4];
		MyTexture* tex;
		float size;

		cube() : gameObject(NULL) { tex = 0; size = 10; }
		cube(vec2f _pos, float size) : gameObject(new rectf(_pos, vec2f(size, size))), tex(0), size(10)
		{
			point[0].x = pos->pos.x;		    point[0].y = pos->pos.y;
			point[1].x = pos->pos.x + size;		point[1].y = pos->pos.y;
			point[2].x = pos->pos.x + size;		point[2].y = pos->pos.y + size;
			point[3].x = pos->pos.x;		    point[3].y = pos->pos.y + size;
		}
		cube(vec2f _pos, float _size, MyTexture* const _tex) : cube(_pos, _size)
		{
			size = _size;
			tex = _tex;
		}
		virtual ~cube()
		{
			if (pos) { delete[] pos; pos = NULL; std::cout << "вызван деструктор cube\n"; }
		}
		void init()
		{
			point[0].x = pos->pos.x;		    point[0].y = pos->pos.y;
			point[1].x = pos->pos.x + size;		point[1].y = pos->pos.y;
			point[2].x = pos->pos.x + size;		point[2].y = pos->pos.y + size;
			point[3].x = pos->pos.x;		    point[3].y = pos->pos.y + size;
		}

		virtual void render(float time) override
		{
			if (tex)
			{
				glBindTexture(GL_TEXTURE_2D, tex->tex.id);
				glColor3ub(255, 255, 255);
				glBegin(GL_QUADS);
				glTexCoord2f(0, 0); glVertex2f(point[0].x, point[0].y);
				glTexCoord2f(1, 0); glVertex2f(point[1].x, point[1].y);
				glTexCoord2f(1, 1); glVertex2f(point[2].x, point[2].y);
				glTexCoord2f(0, 1); glVertex2f(point[3].x, point[3].y);
				glEnd();
			}
		}
		virtual void update(float time) override { }
	};

	struct BoundRect : public gameObject
	{
		rectf r_tex;
		MyTexture* tex;

		BoundRect() : gameObject(NULL) { tex = 0; pos = 0; }
		BoundRect(rectf* _rpos, rectf _rtex, MyTexture* _tex) : gameObject(_rpos),
			r_tex(_rtex), tex(_tex)	{ }

		virtual void render(float time) override
		{
			glBindTexture(GL_TEXTURE_2D, tex->tex.id);
			glColor3ub(255, 255, 255);
			glBegin(GL_QUADS);
			glTexCoord2f(r_tex.pos.x / tex->tex.width, r_tex.pos.y / tex->tex.height);
			glVertex2f(pos->pos.x, pos->pos.y);
			glTexCoord2f(r_tex.size.x / tex->tex.width, r_tex.pos.y / tex->tex.height);
			glVertex2f(pos->pos.x + pos->size.x, pos->pos.y);
			glTexCoord2f(r_tex.size.x / tex->tex.width, r_tex.size.y / tex->tex.height);
			glVertex2f(pos->pos.x + pos->size.x, pos->pos.y + pos->size.y);
			glTexCoord2f(r_tex.pos.x / tex->tex.width, r_tex.size.y / tex->tex.height);
			glVertex2f(pos->pos.x, pos->pos.y + pos->size.y);
			glEnd();
		}

		virtual void update(float time) { }

		virtual ~BoundRect() { }
	};

#pragma endregion

#pragma region Render Font String

	void ShowStringFont(const char* _string, int _font, const int _px, const int _py, colori _col = colori())
	{
		glColor3ub(_col.r, _col.g, _col.b);
		glRasterPos2i(_px, _py);
		glDisable(GL_TEXTURE_2D);
		switch (_font)
		{
		case 1:
			glutBitmapString(GLUT_BITMAP_TIMES_ROMAN_10, (unsigned char*)_string); break;
		case 2:
			glutBitmapString(GLUT_BITMAP_HELVETICA_10, (unsigned char*)_string); break;
		case 3:
			glutBitmapString(GLUT_BITMAP_HELVETICA_12, (unsigned char*)_string); break;
		case 4:
			glutBitmapString(GLUT_BITMAP_8_BY_13, (unsigned char*)_string); break;
		case 5:
			glutBitmapString(GLUT_BITMAP_9_BY_15, (unsigned char*)_string); break;
		case 6:
			glutBitmapString(GLUT_BITMAP_HELVETICA_18, (unsigned char*)_string); break;
		case 7:
			glutBitmapString(GLUT_BITMAP_TIMES_ROMAN_24, (unsigned char*)_string); break;
		default:
			std::cout << "not found font = " << _font << std::endl;
			break;
		}
		glEnable(GL_TEXTURE_2D);
	}
	void ShowStringFont(std::string _string, int _font, const int _px, const int _py, colori _col = colori())
	{
		glColor3ub(_col.r, _col.g, _col.b);
		glRasterPos2i(_px, _py);
		glDisable(GL_TEXTURE_2D);
		switch (_font)
		{
		case 1:
			glutBitmapString(GLUT_BITMAP_TIMES_ROMAN_10, (unsigned char*)_string.c_str());break;
		case 2:
			glutBitmapString(GLUT_BITMAP_HELVETICA_10, (unsigned char*)_string.c_str()); break;
		case 3:
			glutBitmapString(GLUT_BITMAP_HELVETICA_12, (unsigned char*)_string.c_str()); break;
		case 4:
			glutBitmapString(GLUT_BITMAP_8_BY_13, (unsigned char*)_string.c_str()); break;
		case 5:
			glutBitmapString(GLUT_BITMAP_9_BY_15, (unsigned char*)_string.c_str()); break;
		case 6:
			glutBitmapString(GLUT_BITMAP_HELVETICA_18, (unsigned char*)_string.c_str()); break;
		case 7:
			glutBitmapString(GLUT_BITMAP_TIMES_ROMAN_24, (unsigned char*)_string.c_str()); break;
		default:
			std::cout << "not fount font = " << _font << std::endl;
			break;
		}
		glEnable(GL_TEXTURE_2D);
	}
	void ShowString(std::string _string, const int _px, const int _py, float _sizeL = 3.0f, float _scal = 0.2f)
	{
		////////not working
		glColor3ub(255, 0, 0);
		glPushMatrix();
		glTranslatef(_px, _py, 0);
		glScalef(_scal, -_scal, 1);
		glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
		//glEnable(GL_BLEND);
		glEnable(GL_LINE_SMOOTH);
		glLineWidth(_sizeL);

		glutStrokeString(GLUT_STROKE_ROMAN, (unsigned char*)_string.c_str());

		//glDisable(GL_BLEND);
		glDisable(GL_LINE_SMOOTH);
		glPopMatrix();
	}
#pragma endregion

#pragma region Render Objects
	class renderLayerObjects
	{
	public:
		std::vector<gameObject*> list;

		void Add(gameObject* obj)
		{
			list.push_back(obj);
		}
		void render(float time)
		{
			for (auto i = list.begin(); i < list.end(); i++)
				(*i)->render(time);
		}
		void update(float time)
		{
			for (auto i = list.begin(); i < list.end(); i++)
				(*i)->update(time);
		}

		renderLayerObjects() { }
		~renderLayerObjects() { list.clear(); }
	};

	struct cube_font : public cube
	{
		std::string font;

		cube_font() : cube() { }
		cube_font(vec2f _pos, float size, MyTexture* const _tex) : cube(_pos, size, _tex) {}

		void set_string(const char* _name) { font = _name; }

		virtual void render(float time) override
		{
			cube::render(time);
			ShowStringFont(font + (std::to_string(time)), 5, pos->pos.x, pos->pos.y, colori());
		}

		virtual void update(float time) override { }

		virtual ~cube_font() { std::cout << "вызван деструктор для cube_font\n"; }
	};

	struct animBoundRect : public BoundRect
	{
		bool isEnd;
		int cw, ch;
		int bw, bh, ew, eh;
		float _t, speedAnimation;
		vec2f start_off_pos;
		animBoundRect() : BoundRect(), cw(0), ch(0), ew(0), eh(0), _t(0), bw(0), bh(0) 
		{
			speedAnimation = 0.1f; 
			isEnd = false;
		}
		animBoundRect(rectf* _rpos, rectf _rtex, MyTexture* _tex, int _ew, int _eh) :
			BoundRect(_rpos, _rtex, _tex), ew(_ew), eh(_eh), _t(0), cw(0), ch(0), bw(0), bh(0)
		{
			speedAnimation = 0.1f;
			start_off_pos = _rtex.pos;
			isEnd = false;
		}
		animBoundRect(rectf* _rpos, rectf _rtex, MyTexture* _tex, int _ew, int _eh, int _bw, int _bh) :
			BoundRect(_rpos, _rtex, _tex), ew(_ew), eh(_eh), _t(0), cw(_bw), ch(_bh), bw(_bw), bh(_bh)
		{
			speedAnimation = 0.1f;
			start_off_pos = _rtex.pos;
			isEnd = false;
		}

		void set_speed_animation(float _time) { speedAnimation = _time; }

		virtual void render(float time) override
		{
			glBindTexture(GL_TEXTURE_2D, tex->tex.id);
			glColor3ub(255, 255, 255);
			glBegin(GL_QUADS);
			glTexCoord2f(r_tex.pos.x / tex->tex.width, r_tex.pos.y / tex->tex.height);
			glVertex2f(pos->pos.x, pos->pos.y);
			glTexCoord2f(r_tex.right() / tex->tex.width, r_tex.pos.y / tex->tex.height);
			glVertex2f(pos->pos.x + pos->size.x, pos->pos.y);
			glTexCoord2f(r_tex.right() / tex->tex.width, r_tex.bottom() / tex->tex.height);
			glVertex2f(pos->pos.x + pos->size.x, pos->pos.y + pos->size.y);
			glTexCoord2f(r_tex.pos.x / tex->tex.width, r_tex.bottom() / tex->tex.height);
			glVertex2f(pos->pos.x, pos->pos.y + pos->size.y);
			glEnd();
		}

		virtual void update(float time) override 
		{
			_t += time;
			isEnd = false;
			
			if (_t > speedAnimation)
			{
				_t = 0;
				//printf("time %f addTime %f ", time, _t);
				r_tex.set_pos(vec2f((cw * r_tex.size.x) + start_off_pos.x,
					(ch * r_tex.size.y) + start_off_pos.y));

				cw++;
				if (cw > ew)
				{
					cw = bw;
					ch++;
					if (ch > eh)
					{
						ch = bh;
						isEnd = true;
					}
				}
			}
		}

		virtual ~animBoundRect() { }
	};

	class animSingle : public gameObject
	{
	protected:
		float aliveTime;
		float _time;
	public:
		bool isDead;
		bool isSingle;
		animBoundRect anim;
	public:
		animSingle() : gameObject(NULL), isDead(false), aliveTime(2), _time(0) { isSingle = true; }
		animSingle(rectf _rpos, rectf _rtex, MyTexture* _tex, int _ew, int _eh) : isDead(false), aliveTime(2), _time(0),
			gameObject(new rectf(_rpos)), anim(pos, _rtex, _tex, _ew, _eh) { isSingle = true; }
		animSingle(rectf _rpos, rectf _rtex, MyTexture* _tex, int _ew, int _eh, int _bw, int _bh) : isDead(false), aliveTime(2), _time(0),
			gameObject(new rectf(_rpos)), anim(pos, _rtex, _tex, _ew, _eh, _bw, _bh) { isSingle = true; }

		virtual ~animSingle() { if (pos) { delete[] pos; pos = 0; std::cout << "деструктор animSingle\n"; } }

		void set_aliveTime(const float time) { aliveTime = time; }

		void render(float time) override { anim.render(time); }
		void update(float time) override 
		{
			_time += time;

			if (isSingle)
			{
				anim.isEnd ? isDead = true : isDead = false;
			}
			else
			{
				if (_time >= aliveTime)
				{
					_time = 0;
					isDead = true;
				}
			}

			anim.update(time);
		}
	};

	class unitGoblin : public gameObject, public takeDamage
	{
	public:
		bool isPlay;
	protected:
		int dir;
		animBoundRect a_down;
		animBoundRect a_up;
		animBoundRect a_right;
		animBoundRect a_left;
	public:
		unitGoblin() : gameObject(NULL) { }
		unitGoblin(rectf _p, MyTexture* _tex, int _dir) : gameObject(new rectf(_p)), 
			takeDamage(pos, 100), dir(0), isPlay(false)
		{
			a_down = animBoundRect(pos, rectf(vec2f(), vec2f(80, 97)), _tex, 3, 0);
			a_left = animBoundRect(pos, rectf(vec2f(), vec2f(80, 97)), _tex, 3, 1, 0, 1);
			a_right = animBoundRect(pos, rectf(vec2f(), vec2f(80, 97)), _tex, 3, 2, 0, 2);
			a_up = animBoundRect(pos, rectf(vec2f(), vec2f(80, 97)), _tex, 3, 3, 0, 3);

			//std::cout << "x = " << bul.pos.x << "y = " << bul.pos.y << "\n";
		}

		inline void set_pos(vec2f _pos) { pos->pos = _pos; }
		inline void set_offset_pos(float _x, float _y) 
		{ 
			pos->pos.x += _x; 
			pos->pos.y += _y; 	
		}
		inline void set_dir(int _dir) 
		{
			dir = _dir; 
			isPlay = true; 
		}

		animBoundRect& choiceAnim(int& _dir)
		{
			if (_dir == 0)
				return a_up;
			else if (_dir == 2)
				return a_down;
			if (_dir == 1)
				return a_right;
			else if (_dir == 3)
				return a_left;						
		}

		virtual void render(float time) override
		{
			choiceAnim(dir).render(time);

			takeDamage::healBar.draw(time);

			ShowStringFont(std::to_string((int)live) + "%", 5, pos->pos.x, pos->pos.y, colori());
		}

		virtual void update(float time) override
		{
			if (isPlay)
				choiceAnim(dir).update(time);
		}
		virtual ~unitGoblin() { if (pos) { delete[] pos; std::cout << "вызван деструктор goblin\n"; } }
	};

	class unitGuard : public gameObject, public takeDamage
	{
	protected:
		int state;
		animBoundRect a_run;
		animBoundRect a_attak;
		animBoundRect a_die;
	public:
		unitGuard() : gameObject(NULL) { }
		unitGuard(rectf _p, MyTexture* _tex, const int _state = 0) : gameObject(new rectf(_p)), 
			takeDamage(pos, 60), state(_state)
		{
			a_run = animBoundRect(pos, rectf(-10, 0, 100, 100), _tex, 5, 0, 0, 0);
			a_attak = animBoundRect(pos, rectf(-10, 0, 100, 100), _tex, 5, 1, 0, 1);
			a_die = animBoundRect(pos, rectf(-10, 0, 100, 100), _tex, 5, 2, 0, 2);
		}

		virtual ~unitGuard() { if (pos) { delete[] pos; std::cout << "вызван деструктор unitGuard\n"; } }

		animBoundRect& get_state(const int _state) 
		{
			switch (_state)
			{
			case 0: return a_run;
			case 1: return a_attak;
			case 2: return a_die;
			default: return a_run;
			}
		}

		bool get_isDie() { return a_die.isEnd; }
		virtual void take_damage(float val) override
		{
			takeDamage::take_damage(val);
			if (live <= 0) { state = 2; }
		}

		virtual void render(float time) override
		{ 
			get_state(state).render(time);
			!a_die.isEnd ? takeDamage::healBar.draw(time) : void();
			!a_die.isEnd ? ShowStringFont(std::to_string((int)live) + "%", 5, pos->pos.x, pos->pos.y, colori()) : void();
		}

		virtual void update(float time) override
		{
			!a_die.isEnd ? get_state(state).update(time) : void();
		}
	};

	class unitCat : public gameObject
	{
	protected:
		animBoundRect idle;
		animBoundRect idle2;
		animBoundRect aggression;
		int var;
	public:
		unitCat() : gameObject(NULL) { }
		unitCat(rectf _pos, MyTexture* _tex) : gameObject(new rectf(_pos))
		{
			srand(time(NULL) + clock());
			var = rand() % 3;

			std::cout << var << std::endl;

			idle = animBoundRect(pos, rectf(0, 1, 96, 96), _tex, 2, 0, 0, 0);
			idle.set_speed_animation(1);
			idle2 = animBoundRect(pos, rectf(0, 1, 96, 96), _tex, 3, 0, 0, 0);
			idle2.set_speed_animation(0.8f);
			aggression = animBoundRect(pos, rectf(0, 1, 96, 96), _tex, 2, 0, 0, 1);
			aggression.set_speed_animation(0.7f);
		}
		animBoundRect& choiceAnim(int var)
		{
			switch (var)
			{
			case 0: return idle;
			case 1: return idle2;
			case 2: return aggression;
			default:
				break;
			}
		}
		void set_random_anim(animBoundRect& _anim)
		{
			if (_anim.isEnd)
				var = rand() % 3;
		}
		void render(float time) override
		{
			choiceAnim(var).render(time);
		}
		void update(float time) override
		{
			choiceAnim(var).update(time);
			set_random_anim(choiceAnim(var));
		}
		virtual ~unitCat() { if (pos) { delete[] pos; std::cout << "вызван деструктор cat\n";
		} }
	};

	class unitHuman : public gameObject, public giveDamage
	{
	protected:
		float t;
		animBoundRect test;
	public:
		unitHuman() : gameObject(NULL) { }
		unitHuman(rectf _pos, MyTexture* _tex) : gameObject(new rectf(_pos))
		{		
			test = animBoundRect(pos, rectf(0, 0, 85.3f, 102.4f), _tex, 5, 4, 0, 0);
			test.set_speed_animation(0.03f);

			bullet = sourceBullet(vec2f(pos->pos.x + (pos->size.x / 2), 
										pos->pos.y + (pos->size.y / 2)), 65, colori(255, 123, 0));
			bullet.makeBullet();
			t = 0;
		}
		void render(float time) override
		{
			test.render(time);
			bullet.draw(time);
		}
		void update(float time) override
		{
			test.update(time);
			bullet.update(time);
			t += time;
			if (t > 3)
			{
				bullet.makeBullet();
				t = 0;
			}
		}
		virtual ~unitHuman() { if (pos) { delete[] pos; std::cout << "вызван деструктор human\n"; } }
	};
#pragma endregion

#pragma region Init OpenGl
	static int win_id[3] = { 0, 0 };

	template<typename T1 = int, class T2 = int>
	int precent(const T1 _valPrecent, const T2 _maxVal)
	{
		return (int)_maxVal * _valPrecent / 100;
	}

	void reshape(int wid, int hei)
	{
		WID = wid;
		HEI = hei;

		width_sub = precent(width_sub_precent, WID);

		glutSetWindow(win_id[0]);
		glMatrixMode(GL_PROJECTION);
		glViewport(width_sub, 0, WID, HEI);
		glLoadIdentity();
		gluOrtho2D(0, WID, HEI, 0);
		
		for (auto obj : m_message)
			obj->call();

		glutSetWindow(win_id[1]);
		glutPositionWindow(0, 0);
		glutReshapeWindow(width_sub, HEI);
		glLoadIdentity();
		gluOrtho2D(0, width_sub, HEI, 0);
	}
	
	void initialize_main(int argc, char** argv)
	{
		glutInit(&argc, argv);
		glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA);
		glutInitWindowPosition(300, 50);
		glutInitWindowSize(WID, HEI);
		win_id[0] = glutCreateWindow("myTestGame_2019");

		glMatrixMode(GL_PROJECTION);
		glLoadIdentity();
		gluOrtho2D(0, WID, HEI, 0);
		glClearColor(0.2, 0.2, 0.2, 1);

		glutReshapeFunc(reshape);
	}
	void initialize_sub()
	{
		width_sub = precent(width_sub_precent, WID);
		win_id[1] = glutCreateSubWindow(win_id[0], width_sub, 0, 100, HEI);

		glLoadIdentity();
		gluOrtho2D(0, width_sub, HEI, 0);
	}

	class menu_gl
	{
	private:
		int id;
	public:
		menu_gl() { }
		menu_gl(void (*func)(int)) 
		{
			createMenu(func);
		}
		~menu_gl() { glutDetachMenu(GLUT_RIGHT_BUTTON); glutDestroyMenu(id); std::cout << "вызван деструктор menu_gl\n"; }
	private:
		void createMenu(void (*func)(int))
		{
			id = glutCreateMenu(func);
			glutAddMenuEntry("glut menu", 0);
			glutAddMenuEntry("layer_1", 1);
			glutAddMenuEntry("layer_2", 2);
			glutAddMenuEntry("save", 3);
			glutAddMenuEntry("load", 4);
			glutAttachMenu(GLUT_RIGHT_BUTTON);
		}
	};
#pragma endregion
}