#include "myMain.h"
#include <SFML/Graphics.hpp>
#include <math.h>
#include <stdlib.h>
//#include <stack>

#define PI 3.1415
#define PI2 PI / 2
#define PI3 3 * PI / 2
#define DR 0.0174533

struct MyVector2f
{
	float x, y;

	MyVector2f() { x = 0; y = 0; }
	MyVector2f(const float _x, const float _y) { x = _x; y = _y; }
	MyVector2f(const sf::Vector2f& _pos) { x = _pos.x; y = _pos.y; }

	MyVector2f myNormalize() 
	{ 
		float len = sqrt(x * x + y * y); 
		x = x / len;
		y = y / len; 
		return MyVector2f(x, y); 
	}

	static float myDot(const MyVector2f _first, const MyVector2f _second)
	{
		return _first.x * _second.x + _first.y * _second.y;
	}

	MyVector2f operator*(const MyVector2f& _vec) { return MyVector2f(x * _vec.x, y * _vec.y); }
	MyVector2f operator*(const float _mul) { return MyVector2f(x * _mul, y * _mul); }
	MyVector2f operator/(const MyVector2f& _vec) { return MyVector2f(x / _vec.x, y / _vec.y); }
	MyVector2f operator+(const MyVector2f& _vec) { return MyVector2f(x + _vec.x, y + _vec.y); }
	MyVector2f& operator+=(const MyVector2f& _vec) { x += _vec.x; y += _vec.y; return *this; }
	MyVector2f operator-(const MyVector2f& _vec) { return MyVector2f(x - _vec.x, y - _vec.y); }
	MyVector2f& operator-=(const MyVector2f& _vec) { x -= _vec.x; y -= _vec.y; return *this; }
	MyVector2f operator-() { return MyVector2f(-x, -y); }
};

struct Player : public sf::Drawable
{
	sf::VertexArray arr; 
	sf::RectangleShape rect;

	MyVector2f pos;
	MyVector2f dir;
	float ang;

	Player(const MyVector2f _pos) : pos(_pos), ang(0)
	{
		arr = sf::VertexArray(sf::PrimitiveType::Lines, 2); 
		rect = sf::RectangleShape(sf::Vector2f(10, 10));

		mySetPosition(_pos);
		mySetAngle(0);
	}

	//void myOffsetPos(const sf::Vector2f _pos) { mySetPosition(pos + _pos); }
	void myOffsetPos(const MyVector2f _pos) { mySetPosition(pos + _pos); }
	void myOffsetPos(const float _val, const bool _isX)
	{ 
		if (_isX)  { pos.x += _val; } else { pos.y += _val; }
		mySetPosition(pos);
	}

	void mySetPosition(const MyVector2f _pos) 
	{ 
		pos = _pos; 
		rect.setPosition(sf::Vector2f(pos.x, pos.y) - sf::Vector2f(5, 5));
		mySetDir(); 
	}

	void mySetAngle(float _rad) 
	{ 
		ang += _rad; 
		if (ang < 0) { ang += 2 * PI; }
		else if (ang > 2 * PI) { ang -= 2 * PI; }
		mySetDir(); 
	}

private: void mySetDir() 
{ 
	dir = MyVector2f(cosf(ang) * 20, sinf(ang) * 20); 
	arr[0].position = sf::Vector2f(pos.x, pos.y); 
	MyVector2f myVec(dir + pos);
	arr[1].position = sf::Vector2f(myVec.x, myVec.y);
}

private: void draw(sf::RenderTarget& _target, sf::RenderStates _states) const override
	   {
		   _target.draw(arr, _states); _target.draw(rect, _states);
	   }

}p(MyVector2f(340, 290));

struct MyMap : public sf::Drawable
{
	static const int w = 8, h = 8, len = w * h, size = 64;

	const int map[len] =
	{
		1,1,1,1,1,1,1,1,
		1,0,1,0,0,0,0,1,
		1,0,1,0,0,2,0,1,
		1,0,1,0,0,0,0,1,
		1,0,0,0,0,0,2,1,
		1,0,0,0,0,0,0,1,
		1,0,0,0,0,0,0,1,
		1,1,1,1,1,1,1,1
	};

	sf::RectangleShape rect[len];

	MyMap()
	{
		for (int y = 0; y < h; y++)
		{
			for (int x = 0; x < w; x++)
			{
				rect[y * w + x] = sf::RectangleShape(sf::Vector2f(size - 1, size - 1));
				rect[y * w + x].setPosition(sf::Vector2f(x * size, y * size));
				rect[y * w + x].setFillColor(sf::Color::Cyan);
			}
		}
	}

private: void draw(sf::RenderTarget& _target, sf::RenderStates _states) const override
{
	for (int y = 0; y < h; y++)
		for (int x = 0; x < w; x++)
			if (map[y * w + x] > 0)
				_target.draw(rect[y * w + x]);
}

}m;

float myDist(const MyVector2f _a, const MyVector2f _b, float ang)
{
	return sqrtf((_b.x - _a.x) * (_b.x - _a.x) + (_b.y - _a.y) * (_b.y - _a.y));
}

sf::Image image;

void myDrawRays3D(sf::RenderWindow& _window)
{
	int r, mx, my, mp, dof; float rx, ry, ra, xo, yo, disT;
	ra = p.ang - DR * 30; if (ra < 0) { ra += 2 * PI; } if (ra > 2 * PI) { ra -= 2 * PI; }

	for (r = 0; r < 60; r++)
	{
		int vmt = 0, hmt = 0;
		dof = 0;
		float disH = 1000000, hx = p.pos.x, hy = p.pos.y;
		float aTan = -1 / tan(ra);
		if (ra > PI) 
		{ 
			ry = (((int)p.pos.y>>6)<<6) - 0.0001; rx = (p.pos.y - ry) * aTan + p.pos.x;
			yo = -m.size; xo = -yo * aTan;
		}
		if (ra < PI)
		{
			ry = (((int)p.pos.y>>6)<<6) + m.size; rx = (p.pos.y - ry) * aTan + p.pos.x;
			yo = m.size; xo = -yo * aTan;
		}
		if (ra == 0 || ra == PI) { rx = p.pos.x; ry = p.pos.y; dof = 8; }

		while (dof < 8)
		{
			mx = (int)(rx)>>6; my = (int)(ry)>>6; mp = my * m.w + mx;
			if (mp > 0 && mp < m.len && m.map[mp] > 0) 
			{
				vmt = m.map[mp] - 1; hx = rx; hy = ry; disH = myDist(p.pos, MyVector2f(hx, hy), ra); break;
			}
			else { rx += xo; ry += yo; dof += 1; }
		}

		dof = 0;
		float disV = 1000000, vx = p.pos.x, vy = p.pos.y;
		float nTan = -tan(ra);
		if (ra > PI2 && ra < PI3)
		{
			rx = (((int)p.pos.x >> 6) << 6) - 0.0001; ry = (p.pos.x - rx) * nTan + p.pos.y;
			xo = -m.size; yo = -xo * nTan;
		}
		if (ra < PI2 || ra > PI3)
		{
			rx = (((int)p.pos.x >> 6) << 6) + m.size; ry = (p.pos.x - rx) * nTan + p.pos.y;
			xo = m.size; yo = -xo * nTan;
		}
		if (ra == 0 || ra == PI) { rx = p.pos.x; ry = p.pos.y; dof = 8; }

		while (dof < 8)
		{
			mx = (int)(rx) >> 6; my = (int)(ry) >> 6; mp = my * m.w + mx;
			if (mp > 0 && mp < m.len && m.map[mp] > 0) 
			{ 
				hmt = m.map[mp] - 1; vx = rx; vy = ry; disV = myDist(p.pos, MyVector2f(vx, vy), ra); break;
			}
			else { rx += xo; ry += yo; dof += 1; }
		}

		float shade = 1;
		if (disV < disH) { /*hmt = vmt;*/ shade = 0.5; rx = vx; ry = vy; disT = disV; }
		if (disH < disV) { rx = hx; ry = hy; disT = disH; }

		sf::Vertex line[] =
		{
			sf::Vertex(sf::Vector2f(p.pos.x, p.pos.y)),
			sf::Vertex(sf::Vector2f(rx, ry))
		};
		_window.draw(line, 2, sf::Lines);

		float ca = p.ang - ra; if (ca < 0) { ca += 2 * PI; } if (ca > 2 * PI) { ca -= 2 * PI; } disT = disT * cos(ca);
		
		float lineH = (m.size * 320) / disT; 
		float ty_step = 32.0 / (float)lineH;
		float ty_off = 0;
		if (lineH > 320) { ty_off = (lineH - 320) / 2.0; lineH = 320; }
		float lineOff = 160 - ((int)lineH >> 1);

		float ty = ty_off * ty_step + hmt * 32;
		float tx;
		if (shade == 1) { tx = (int)(rx / 2.0) % 32; if (ra > 180) { tx = 31 - tx; } }
		else { tx = (int)(ry / 2.0) % 32; if (ra > 90 && ra < 270) { tx = 31 - tx; } }

		std::vector<sf::Vertex> quads;
		std::vector<sf::Vertex> mass;
		for (int y = 0; y < lineH; y++)
		{
			sf::Vertex vertex1 = sf::Vertex(sf::Vector2f(r * 8 + 530 - 4, y + lineOff - 4));
			vertex1.color = image.getPixel((int)tx, (int)ty % image.getSize().y);
			mass.push_back(vertex1);
			sf::Vertex vertex2 = sf::Vertex(sf::Vector2f(r * 8 + 530 + 4, y + lineOff - 4));
			vertex2.color = image.getPixel((int)tx, (int)ty % image.getSize().y);
			mass.push_back(vertex2);
			sf::Vertex vertex3 = sf::Vertex(sf::Vector2f(r * 8 + 530 + 4, y + lineOff + 4));
			vertex3.color = image.getPixel((int)tx, (int)ty % image.getSize().y);
			mass.push_back(vertex3);
			sf::Vertex vertex4 = sf::Vertex(sf::Vector2f(r * 8 + 530 - 4, y + lineOff + 4));
			vertex4.color = image.getPixel((int)tx, (int)ty % image.getSize().y);
			mass.push_back(vertex4);
			ty += ty_step;
		}
		_window.draw(mass.data(), lineH * 4, sf::Quads);
		mass.clear();
		ra += DR; if (ra < 0) { ra += 2 * PI; } if (ra > 2 * PI) { ra -= 2 * PI; }
	}
}

void myHandle(const bool* _isKeyDown)
{
	enum MyKeyDir { A, D, W, S };

	int xo = 0; if (p.dir.x < 0) { xo -= 20; } else { xo = 20; }
	int yo = 0; if (p.dir.y < 0) { yo -= 20; } else { yo = 20; }
	int ipx = p.pos.x / m.size, ipx_add_xo = (p.pos.x + xo) / m.size, ipx_sub_xo = (p.pos.x - xo) / m.size;
	int ipy = p.pos.y / m.size, ipy_add_yo = (p.pos.y + yo) / m.size, ipy_sub_yo = (p.pos.y - yo) / m.size;

	if (_isKeyDown[MyKeyDir::A]) { p.mySetAngle(-0.05); }
	if (_isKeyDown[MyKeyDir::D]) { p.mySetAngle(0.05); }
	if (_isKeyDown[MyKeyDir::W]) 
	{ 
		if (m.map[ipy * m.w + ipx_add_xo] == 0) { p.myOffsetPos((p.dir.myNormalize() * 2).x, true); }
		if (m.map[ipy_add_yo * m.w + ipx] == 0) { p.myOffsetPos((p.dir.myNormalize() * 2).y, false); }
	}
	if (_isKeyDown[MyKeyDir::S]) 
	{
		if (m.map[ipy * m.w + ipx_sub_xo] == 0) { p.myOffsetPos(-(p.dir.myNormalize() * 2).x, true); }
		if (m.map[ipy_sub_yo * m.w + ipx] == 0) { p.myOffsetPos(-(p.dir.myNormalize() * 2).y, false); }
	}
}

int main()
{
    unsigned int w = 1280;
    unsigned int h = 720;

    sf::RenderWindow window(sf::VideoMode(w, h), "Ray tracing", sf::Style::Titlebar | sf::Style::Close);
    window.setFramerateLimit(60);
	window.setKeyRepeatEnabled(false);

	if (!image.loadFromFile("texture//my_map_for_raycast.png")) { printf("error texture = chest_grid_empty.png"); }	

	sf::Clock clock;
	sf::Time elapsed;

	bool isKeyDown[] = { false, false, false, false };

	while (window.isOpen())
	{
		sf::Event event;
		elapsed = clock.restart();

		while (window.pollEvent(event))
		{
			if (event.type == sf::Event::Closed || sf::Keyboard::isKeyPressed(sf::Keyboard::Key::Escape))
			{
				window.close();
			}
			else if (event.type == sf::Event::KeyPressed)
			{
				if (event.key.code == sf::Keyboard::A) { isKeyDown[0] = true; }
				else if (event.key.code == sf::Keyboard::D) { isKeyDown[1] = true; }
				else if (event.key.code == sf::Keyboard::W) { isKeyDown[2] = true; }
				else if (event.key.code == sf::Keyboard::S) { isKeyDown[3] = true; }
			} 
			else if (event.type == sf::Event::KeyReleased)
			{
				if (event.key.code == sf::Keyboard::A) { isKeyDown[0] = false; }
				else if (event.key.code == sf::Keyboard::D) { isKeyDown[1] = false; }
				else if (event.key.code == sf::Keyboard::W) { isKeyDown[2] = false; }
				else if (event.key.code == sf::Keyboard::S) { isKeyDown[3] = false; }
			}
		}

		myHandle(isKeyDown);

		window.clear(sf::Color::Black);

		window.draw(m);
		myDrawRays3D(window);
		window.draw(p);

		window.display();
	}

    return 0;
}