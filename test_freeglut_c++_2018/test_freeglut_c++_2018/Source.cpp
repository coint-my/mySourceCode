#include <iostream>
#include <fstream>
#include <cmath>
#include <time.h>
#include <string>
#include <vector>
#include "gl/glut.h"
#include "gl/src/SOIL.h"

#define WID 800
#define HEI 600

static GLuint _texture = 0;

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
		//freeImage();
		//glDeleteTextures(1, &id);
		//SOIL_free_image_data(data);
		//delete[] data;
	}
};
class MyTexture
{
public: texture tex;

public: MyTexture() { }
public: MyTexture(const char* fileName) { LoadTexture(fileName); }
	
public: inline void LoadTexture(const char* _fn)
{
	if (!tex.LoadTexture(_fn))
		printf("texture failure! file path(%s)", _fn);

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
   ~MyTexture()
	{
		/*tex.freeImage();
		glDeleteTextures(1, &tex.id);
		printf("---destructor---");
		system("PAUSE");*/
	}
};
#pragma endregion

MyTexture text_my_blook;
MyTexture text_my_blook2;
MyTexture text;
MyTexture text2;
MyTexture tree1;

#pragma region GAME UTILITES
namespace physix
{
	
}
struct Layer
{
	int wid, hei;
	std::string data;
	Layer() { }
	Layer(const int _wid, const int _hei, const std::string _data) : wid(_wid), hei(_hei), data(_data) { }
	~Layer() { data.clear(); }
};
struct Time
{
	double sec; 
	double endsec;

	Time() { sec = 0.0; endsec = 0.0; }
	bool Updata(const double _sec)
	{
		clock_t t1 = clock();

		sec = (double(t1) / CLOCKS_PER_SEC);
		if (sec - endsec >= _sec)
		{
			endsec = sec;
			return true;
		}
		else return false;
	}
}sec_time;
struct Rect
{
	float x, y, wid, hei;
	Rect() : x(0), y(0), wid(1), hei(1) { }
	Rect(float _x, float _y, float _wid, float _hei) : x(_x), y(_y), wid(_wid), hei(_hei) { }
	inline void setRect(Rect _rect) 
	{
		x = _rect.x; y = _rect.y; wid = _rect.wid; hei = _rect.hei;
	}
};
class object : public Rect
{
protected: Rect rtex;
public: object() : Rect(), rtex() { }
public: virtual void render(const MyTexture& _texture) { }
};
class loadText
{
protected: char* data;
protected: std::vector<Layer> layers;
public: int _w, _h;
public: loadText() { data = 0; _w = 0; _h = 0; }
public: loadText(const char* _fileName, const char* _fileNameLayer)
{
	load(_fileName);
	loadLayers(_fileNameLayer);
}
public: ~loadText()
{
	delete[] data;
	layers.clear();
}
protected: inline void load(const char* _fileName)
	{
		int length;
		char temp_w[10];
		char temp_h[10];
		int wi = 0;
		int he = 0;

		std::ifstream is;
		is.open(_fileName, std::ifstream::in);

		// get length of file:
		is.getline(temp_w, 10);
		is.getline(temp_h, 10);
		wi = atoi(temp_w);
		he = atoi(temp_h);
		
		length = wi * he;
		// allocate memory:
		data = new char[length];
		int i = 0;
		// read data as a block:
		while (is.good())
		{
			data[i] = is.get();
			i++;
		}
		//is.read(data, length);
		is.close();

		_w = wi;
		_h = he;
	}
protected: inline void loadLayers(const char* _fileName)
{
	char temp_w[10];
	char temp_h[10];
	int wi = 0;
	int he = 0;

	std::ifstream is;
	is.open(_fileName, std::ifstream::in);

	// get length of file:
	is.getline(temp_w, 10);
	is.getline(temp_h, 10);
	wi = atoi(temp_w);
	he = atoi(temp_h);

	const int length = wi * he;
	int layer = 0;
	Layer l;
	int i = 0;
	std::string temp_layer_hanlde;
	std::string temp_layer_data;
	// read data as a block:
	int curr_layer = 0;
	while (std::getline(is, temp_layer_hanlde))
	{
		//is.getline(temp_layer_hanlde, wi);

		if (strncmp(temp_layer_hanlde.c_str(), "layer ", 6) == 0)
			curr_layer++;
		else
		{
			temp_layer_data.append(temp_layer_hanlde);
			i++;
			if (i == he)
			{
				layers.push_back(Layer(wi, he, temp_layer_data));
				i = 0;
				temp_layer_data.clear();
			}
		}
	}
	is.close();
}
};

#pragma endregion
enum TypeAnimation { DOWN, UP, LEFT, RIGHT };
template <typename T1, typename T2>
struct Par
{
	T1 t1;
	T2 t2;
	Par() { }
	Par(const T1 _t1, const T2 _t2) { t1 = _t1; t2 = _t2; }
};
class Tile : public object
{
protected: MyTexture _tex;
protected: int count = 1;
public: Tile() { }
public: void initialize(Rect _pos)
{
	setRect(_pos);
	setRTex(_pos);
}
public: void initialize(Rect _pos, const MyTexture& _texID)
{
	_tex = _texID;
	setRect(_pos);
	setRTex(_pos);
}
public: void inline setRTex(const Rect _srtex)
{
	rtex.setRect(_srtex);
}
public: void inline setRTex()
{
	Rect _r = Rect(0, 0, _tex.tex.width, _tex.tex.height);
	rtex.setRect(_r);
}
public: void render(const MyTexture& _texture, const Rect _recTex)
{
	glBindTexture(GL_TEXTURE_2D, _texture.tex.id);
	int wi = _texture.tex.width;
	int he = _texture.tex.height;

	rtex.setRect(Rect(_recTex.wid * count, _recTex.y, _recTex.wid + (count * _recTex.wid),
		_recTex.hei));

		if (sec_time.Updata(0.2))
		{
			//printf("time = %lf\n", sec_time.sec);
			if (count < 3)
			{
				count++;
				//printf("x = %f y = %f wid = %f hei = %f\n", rtex.x, rtex.y, rtex.wid, rtex.hei);
			}
			else
			{
				count = 0;
				//printf("------------------------------------------------\n");
			}
		}

	glBegin(GL_QUADS);
	glTexCoord2f(rtex.x / wi, rtex.y / he); glVertex2f(x, y);
	glTexCoord2f(rtex.x / wi, rtex.hei / he); glVertex2f(x, hei + y);
	glTexCoord2f(rtex.wid / wi, rtex.hei / he); glVertex2f(wid + x, hei + y);
	glTexCoord2f(rtex.wid / wi, rtex.y / he); glVertex2f(wid + x, y);
	glEnd();
}
public: void render(const MyTexture& _texture)
{
	glBindTexture(GL_TEXTURE_2D, _texture.tex.id);

	int wi = _texture.tex.width;
	int he = _texture.tex.height;

	glBegin(GL_QUADS);
	glTexCoord2f(rtex.x / wi, rtex.y / he); glVertex2f(x, y);
	glTexCoord2f(rtex.x / wi, rtex.hei / he);  glVertex2f(x, hei + y);
	glTexCoord2f(rtex.wid / wi, rtex.hei / he); glVertex2f(wid + x, hei + y);
	glTexCoord2f(rtex.wid / wi, rtex.y / he);  glVertex2f(wid + x, y);
	glEnd();
}
public: void render(void)
{
	glBindTexture(GL_TEXTURE_2D, _tex.tex.id);

	int wi = _tex.tex.width;
	int he = _tex.tex.height;

	glBegin(GL_QUADS);
	glTexCoord2f(rtex.x / wi, rtex.y / he); glVertex2f(x, y);
	glTexCoord2f(rtex.x / wi, rtex.hei / he);  glVertex2f(x, hei + y);
	glTexCoord2f(rtex.wid / wi, rtex.hei / he); glVertex2f(wid + x, hei + y);
	glTexCoord2f(rtex.wid / wi, rtex.y / he);  glVertex2f(wid + x, y);
	glEnd();
}
public: ~Tile() { }
};
struct TileLayer
{
	std::vector<Tile> layer;
	TileLayer() { }

	~TileLayer() { }
};
class Animation : public object
{
private: int frameCount;
private: int count;
private: Rect rectBound;
public: Animation() { }
public: Animation(const Rect _rText, const int _cFrame) : frameCount(_cFrame), count(0) 
{
	rectBound.setRect(_rText);
}
public: inline void SetPos(const Rect _pos) { setRect(_pos); }
public: void render(const MyTexture &_texture, Time& _t, const double _s)
{
	glBindTexture(GL_TEXTURE_2D, _texture.tex.id);
	int wi = _texture.tex.width;
	int he = _texture.tex.height;

	rtex.setRect(Rect(rectBound.wid * count, rectBound.y, rectBound.wid + (count * rectBound.wid),
		rectBound.hei));

	if (_t.Updata(_s))
	{
		if (count < frameCount)
			count++;
		else
			count = 0;
	}

	glBegin(GL_QUADS);
	glTexCoord2f(rtex.x / wi, rtex.y / he); glVertex2f(x, y);
	glTexCoord2f(rtex.x / wi, rtex.hei / he); glVertex2f(x, hei + y);
	glTexCoord2f(rtex.wid / wi, rtex.hei / he); glVertex2f(wid + x, hei + y);
	glTexCoord2f(rtex.wid / wi, rtex.y / he); glVertex2f(wid + x, y);
	glEnd();
}
};
class Unit
{
protected: std::vector<Par<TypeAnimation, Animation>> u_anim;
protected: MyTexture rTexture;
protected: double sec_time;
protected: Time myTime;
public: Unit(double _t = 0.2) : sec_time(_t) { }
public: void set_pos(const Rect _pos)
{
	for (size_t i = 0; i < u_anim.size(); i++)
		u_anim[i].t2.SetPos(_pos);
}
public: virtual void Initialize(const MyTexture& _text, const Rect _pos)
{
}
public: virtual void render(const TypeAnimation _t)
{
	//rDown.render(rTexture, myTime, sec_time);
}
};
class UnitCustom : public Unit
{
public: UnitCustom() { }
public: void Initialize(const MyTexture& _text, const Rect _pos, const std::vector<Par<TypeAnimation, Animation>> _copyAnim)
{
	rTexture = _text;
	u_anim = _copyAnim;
	set_pos(_pos);
}
public: void render(const TypeAnimation _t)
{
	for (size_t i = 0; i < u_anim.size(); i++)
		if (u_anim[i].t1 == _t)
			u_anim[i].t2.render(rTexture, myTime, sec_time);
}
public: virtual ~UnitCustom()
{
	u_anim.clear();
}
};
class loadLevel : public loadText
{
public: Tile **_pole;
public: std::vector<TileLayer> pLayers;
public: Rect r_tile;
public: loadLevel() : r_tile(0, 0, 60, 60) { }
public: loadLevel(const char* _fileName, const char* _layer, const Rect _rTile = Rect(0, 0, 100, 100)) 
	: loadText(_fileName, _layer)
{
	_pole = new Tile *[_w];
	for (int i = 0; i < _w; i++)
	_pole[i] = new Tile[_h];

	r_tile = _rTile;
}
public: void lLoadGround()
{
	int ind = 0;
	for (size_t h = 0; h < _h; h++)
	{
		for (size_t w = 0; w < _w; w++)
		{
			if (data[ind] == '1')
			{
				_pole[w][h].initialize(Rect(w * r_tile.wid, h * r_tile.hei, r_tile.wid, r_tile.hei),
					text_my_blook2);
				_pole[w][h].setRTex(Rect(0, 0, 256, 254));
			}
			if (data[ind] == '2')
			{
				_pole[w][h].initialize(Rect(w * r_tile.wid, h * r_tile.hei, r_tile.wid, r_tile.hei),
					text_my_blook2);
				_pole[w][h].setRTex(Rect(256, 0, 512, 254));
			}
			if (data[ind] == '3')
			{
				_pole[w][h].initialize(Rect(w * r_tile.wid, h * r_tile.hei, r_tile.wid, r_tile.hei),
					text_my_blook2);
				_pole[w][h].setRTex(Rect(512, 0, 768, 254));
			}
			if (data[ind] == '.')
			{
				_pole[w][h].initialize(Rect(w * r_tile.wid, h * r_tile.hei, r_tile.wid, r_tile.hei),
					text2);
				_pole[w][h].setRTex();
			}
			//printf("h=%i w=%i char=%c\n", h, w, data[ind]);
			ind++;
		}
		ind++;
	}
}
public: void lLoadLayers()
{
	for (size_t lay = 0; lay < layers.size(); lay++)
	{
		int ind = 0;
		TileLayer tl;
		for (size_t h = 0; h < _h; h++)
		{
			Tile tile;
			for (size_t w = 0; w < _w; w++)
			{
				if (layers[lay].data[ind] == '1')
				{
					tile.initialize(Rect(w * r_tile.wid, h * r_tile.hei, r_tile.wid, r_tile.hei),
						tree1);
					tile.setRTex(Rect(0, 0, 774, 1126));
					tl.layer.push_back(tile);
				}
				else if (layers[lay].data[ind] == '2')
				{
					tile.initialize(Rect(w * r_tile.wid, h * r_tile.hei, r_tile.wid, r_tile.hei),
						tree1);
					tile.setRTex(Rect(774, 0, 1540, 1216));
					tl.layer.push_back(tile);
				}
				else if (layers[lay].data[ind] == '3')
				{
					tile.initialize(Rect(w * r_tile.wid, h * r_tile.hei, r_tile.wid, r_tile.hei),
						tree1);
					tile.setRTex(Rect(1540, 0, 2320, 1126));
					tl.layer.push_back(tile);
				}
				else if (layers[lay].data[ind] == '4')
				{
					tile.initialize(Rect(w * r_tile.wid, h * r_tile.hei, r_tile.wid, r_tile.hei),
						tree1);
					tile.setRTex(Rect(0, 1130, 1160, 2260));
					tl.layer.push_back(tile);
				}
				else if (layers[lay].data[ind] == '5')
				{
					tile.initialize(Rect(w * r_tile.wid, h * r_tile.hei, r_tile.wid, r_tile.hei),
						tree1);
					tile.setRTex(Rect(1160, 1130, 2320, 2260));
					tl.layer.push_back(tile);
				}
				ind++;
			}
		}
		pLayers.push_back(tl);
	}
}
public: ~loadLevel()
{
	for (int i = 0; i < _h; i++)
	delete[] _pole[i];
	delete[] _pole;
}
};

UnitCustom player;
UnitCustom enemy;
loadLevel l = loadLevel("levels/level_ground_1.txt", "levels/level_layer_1.txt");
int _fps = 0;

void render()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	glEnable(GL_TEXTURE_2D);
	glEnable(GL_BLEND);

	for (size_t x = 0; x < l._w; x++)
	{
		for (size_t y = 0; y < l._h; y++)
		{
			l._pole[x][y].render();
		}
	}

	for (size_t lay = 0; lay < l.pLayers.size(); lay++)
	{
		for (size_t i = 0; i < l.pLayers[lay].layer.size(); i++)
		{
			l.pLayers[lay].layer[i].render();
		}
	}

	player.render(TypeAnimation::DOWN);
	enemy.render(TypeAnimation::DOWN);

	glutSwapBuffers();
	glDisable(GL_TEXTURE_2D);
	glDisable(GL_BLEND);

	_fps++;
	if (sec_time.Updata(1))
	{
		printf("fps = %i\n", _fps);
		_fps = 0;
	}
}

void update(int = 0)
{
	glutPostRedisplay();
	glutTimerFunc(1, update, 0);
}

int main(int argc, char** argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA);
	glutInitWindowPosition(300, 100);
	glutInitWindowSize(WID, HEI);
	glutCreateWindow("GameTestGlut");

	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluOrtho2D(0, WID, HEI, 0);
	glClearColor(0.2, 0.2, 0.2, 1);

	text.LoadTexture("texture/Goblin_04.png");
	text2.LoadTexture("texture/sea_1.bmp");
	text_my_blook.LoadTexture("texture/maus_01.png");
	text_my_blook2.LoadTexture("texture/ground_01.jpg");
	tree1.LoadTexture("texture/tree_01.png");

	std::vector<Par<TypeAnimation, Animation>> un_an;
	un_an.push_back(Par<TypeAnimation, Animation>(TypeAnimation::DOWN, Animation(Rect(0, 0, 80, 96), 3)));
	un_an.push_back(Par<TypeAnimation, Animation>(TypeAnimation::LEFT, Animation(Rect(0, 96, 80, 192), 3)));
	un_an.push_back(Par<TypeAnimation, Animation>(TypeAnimation::RIGHT, Animation(Rect(0, 192, 80, 288), 3)));
	un_an.push_back(Par<TypeAnimation, Animation>(TypeAnimation::UP, Animation(Rect(0, 288, 80, 384), 3)));
	player.Initialize(text, Rect(400, 200, 100, 100), un_an);
	un_an.clear();
	un_an.push_back(Par<TypeAnimation, Animation>(TypeAnimation::DOWN, Animation(Rect(0, 0, 48, 65), 3)));
	un_an.push_back(Par<TypeAnimation, Animation>(TypeAnimation::LEFT, Animation(Rect(0, 65, 48, 130), 3)));
	un_an.push_back(Par<TypeAnimation, Animation>(TypeAnimation::RIGHT, Animation(Rect(0, 130, 48, 195), 3)));
	un_an.push_back(Par<TypeAnimation, Animation>(TypeAnimation::UP, Animation(Rect(0, 195, 48, 260), 3)));
	enemy.Initialize(text_my_blook, Rect(100, 100, 100, 100), un_an);
	un_an.clear();

	l.lLoadGround();
	l.lLoadLayers();

	glutDisplayFunc(render);
	update();

	glutMainLoop();

	return 0;
}
