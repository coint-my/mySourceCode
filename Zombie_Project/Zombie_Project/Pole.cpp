#include "Pole.h"

Pole::Pole(void)
{
}

Pole::~Pole(void)
{
}

void Pole::IntersectVisible(const int _x, const int _y)
{
	for(int j = 0; j < COUNT_H; j++)
		for(int i = 0; i < COUNT_W; i++)
		{
			if((tiles[i][j].X() < _x) && (tiles[i][j].Y() < _y) && 
				(tiles[i][j].X() + tiles[i][j].Width() > _x) && (tiles[i][j].Y() + tiles[i][j].Height() > _y))
			{
				tiles[i][j].is_intersect = true;
				break;
			}
			else
				tiles[i][j].is_intersect = false;
		}
}

void Pole::SetTile(bool _impos[][COUNT_H])
{
	for(int j = 0; j < COUNT_H; j++)
		for(int i = 0; i < COUNT_W; i++)
		{
			if(_impos[i][j])
			{
				tiles[i][j].is_intersect = false;
			}
			else
				tiles[i][j].is_intersect = true;
		}
}

void Pole::Draw_T(Painter &_paint, const short _colum) const
{
	for(int j = 0; j < COUNT_H; j++)
	{
		for(int i = 0; i < COUNT_W; i++)
		{
			if(tiles[i][j].is_intersect)
			{
				_paint.SetColor(Color(0.7, 0.7, 0.7));
				_paint.DrawRectTexture(tiles[i][j].X(), tiles[i][j].Y(), tiles[i][j].Width(), tiles[i][j].Height(), texture);
			}
			else
			{
				_paint.SetColor(Color(1.0, 1.0, 1.0));
				_paint.DrawRectTexture(tiles[i][j].X(), tiles[i][j].Y(), tiles[i][j].Width(), tiles[i][j].Height(), texture);
			}
		}
	}
	if(_colum > 10 && _colum < HEIGHT)
	{
		short _col = (_colum - 10) / tiles[0][0].Height();
		for(int i = 0; i < COUNT_W; i++)
		{
			_paint.SetColor(Color(1.0, 0.7, 0.7));
			_paint.DrawRectTexture(tiles[i][_col].X(), tiles[i][_col].Y(), tiles[i][_col].Width(), tiles[i][_col].Height(), texture);
		}
	}
}

void Pole::Initialize(const unsigned int _t)
{
	texture = _t;
	int start_x = 10;
	int start_y = 10;
	int wid = WIDTH / COUNT_W;
	int hei = HEIGHT / COUNT_H;

	for(int j = 0; j < COUNT_H; j++)
	{
		for(int i = 0; i < COUNT_W; i++)
		{
			tiles[i][j] = Tile(start_x + (i * wid), start_y + (j * hei), wid, hei);
		}
	}
}
