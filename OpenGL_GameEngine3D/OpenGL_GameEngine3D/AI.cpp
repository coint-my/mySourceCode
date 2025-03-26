#include "AI.h"


AI::AI(void)
{
}


AI::~AI(void)
{
	rang1.clear();
	rang2.clear();
}
//возможно-ли захватить обьект
bool AI::isCap(Maps &_map, const int _ind, int &_count)
{
	vovan::Recti tR;
	vovan::Recti _r1 = RectR1(_ind, _map);

	for (int i = 0; i < _map.GetMap().size(); i++)
	{
		tR.Initialize(vovan::Recti(vovan::Vector2i((int)_map.GetMap().at(i).GetPosition().x, (int)_map.GetMap().at(i).GetPosition().y), 2, 2));

		if((tR.Intersect(_r1)) && (_map.IsColor(_map.GetMap().at(i).GetColor(), _map.GetMap().at(i).GetColorPlayer())))
			_count++;
	}
	if(_count > 0)
		return true;
	else
		return false;
}
bool AI::isClear(Maps &_map, const int _ind, int &_index)
{
	vovan::Recti tR;
	vovan::Recti _r1 = RectR1(_ind, _map);

	for (int i = 0; i < _map.GetMap().size(); i++)
	{
		tR.Initialize(vovan::Recti(vovan::Vector2i((int)_map.GetMap().at(i).GetPosition().x, (int)_map.GetMap().at(i).GetPosition().y), 2, 2));

		if((tR.Intersect(_r1)) && !_map.GetImp().at(i))
		{
			_index = i;
			return true;
		}
	}
	return false;
}
//задать ранги 1-2
void AI::SetRang(Maps &_map, const int _ind)
{
	vovan::Recti tR;
	vovan::Recti _r1 = RectR1(_ind, _map);
	vovan::Recti _r2 = RectR2(_ind, _map);

	for (int i = 0; i < _map.GetMap().size(); i++)
	{
		tR.Initialize(vovan::Recti(vovan::Vector2i((int)_map.GetMap().at(i).GetPosition().x, (int)_map.GetMap().at(i).GetPosition().y), 2, 2));

		if((tR.Intersect(_r1)) && (!_map.GetImp().at(i)))
			rang1.push_back(i);
		else if((tR.Intersect(_r2)) && (!_map.GetImp().at(i)))
			rang2.push_back(i);
	}
}
//вернуть прямоугольник 1-го ранга
vovan::Recti AI::RectR1(const int _ind, Maps &_map)
{
	int _rad = _map.GetMap().at(_ind).GetRadius();
	int _w = (_rad * 2) * 3;
	int _h = (_rad * 2) * 3;
	vovan::Vector2f pos = _map.GetMap().at(_ind).GetPosition();
	return vovan::Recti(vovan::Vector2i((int)pos.x - _w / 2, (int)pos.y - _h / 2), _w, _h);
}
//вернуть прямоугольник 2-го ранга
vovan::Recti AI::RectR2(const int _ind, Maps &_map)
{
	int _rad = _map.GetMap().at(_ind).GetRadius();
	int _w = (_rad * 2) * 5;
	int _h = (_rad * 2) * 5;
	vovan::Vector2f pos = _map.GetMap().at(_ind).GetPosition();
	return vovan::Recti(vovan::Vector2i((int)pos.x - _w / 2, (int)pos.y - _h / 2), _w, _h);
}
//логика игрового захвата
void AI::InitRang(Maps &_map)
{
	std::vector<int> _var;
	for (int i = 0; i < _map.GetMap().size(); i++)
	{
		if(_map.IsColor(_map.GetMap().at(i).GetColor(), _map.GetMap().at(i).GetColorEnemy()))
			_var.push_back(i);
	}
	std::vector<int> _var_capture;
	std::vector<int> rangs;
	std::vector<int> temp_count;

	std::vector<int> _var_capture2;
	std::vector<int> rangs2;
	std::vector<int> temp_count2;

	int count = 0;
	
	for (int i = 0; i < _var.size(); i++)
	{
		rang1.clear();
		rang2.clear();
		SetRang(_map, _var.at(i));

		for (int j = 0; j < rang1.size(); j++)
		{
			count = 0;

			if(isCap(_map, rang1.at(j), count))
			{
				temp_count.push_back(count);
				rangs.push_back(rang1.at(j));
				_var_capture.push_back(_var.at(i));
			}
		}
		for (int j = 0; j < rang2.size(); j++)
		{
			count = 0;

			if(isCap(_map, rang2.at(j), count))
			{
				temp_count2.push_back(count);
				rangs2.push_back(rang2.at(j));
				_var_capture2.push_back(_var.at(i));
			}
		}
	}
	if(_var_capture.size() == 0)
		buf.isCapture = false;
	else
	{
		int co = 0;
		int beg = 0;
		int end = 0;
		for (int i = 0; i < temp_count.size(); i++)
		{
			if(co < temp_count.at(i))
			{
				co = temp_count.at(i);
				beg = _var_capture.at(i);
				end = rangs.at(i);
				buf.range_one = true;
			}
		}
		for (int i = 0; i < temp_count2.size(); i++)
		{
			if(co < temp_count2.at(i))
			{
				co = temp_count2.at(i);
				beg = _var_capture2.at(i);
				end = rangs2.at(i);
				buf.range_one = false;
			}
		}
		buf.cur_ind = beg;
		buf.end_ind = end;
		buf.endP = _map.GetMap().at(buf.end_ind).GetPosition();
	}

	_var_capture2.clear();
	temp_count2.clear();
	rangs2.clear();

	_var_capture.clear();
	temp_count.clear();
	rangs.clear();

	_var.clear();
}
//логика пустого хода
void AI::InitRangTurn(Maps &_map)
{
	std::vector<int> _var;
	for (int i = 0; i < _map.GetMap().size(); i++)
	{
		if(_map.IsColor(_map.GetMap().at(i).GetColor(), _map.GetMap().at(i).GetColorEnemy()))
			_var.push_back(i);
	}

	srand(time(0));
	
	std::vector<int> _start_num;
	std::vector<int> _end_num;

	for (int i = 0; i < _var.size(); i++)
	{
		int _index = 0;

		if(isClear(_map, _var.at(i), _index))
		{
			_start_num.push_back(_var.at(i));
			_end_num.push_back(_index);
		}
	}

	int _ran = rand() % _start_num.size();

	buf.cur_ind = _start_num.at(_ran);
	buf.end_ind = _end_num.at(_ran);
	buf.endP = _map.GetMap().at(buf.end_ind).GetPosition();

	_start_num.clear();
	_end_num.clear();
	_var.clear();
}

void AI::Update(Maps &_map, Shapes &_fig)
{
	if(!buf.isActive)
	{
		buf.isActive = true;
		InitRang(_map);
		if(!buf.isCapture)
		{
			InitRangTurn(_map);
		}
		_fig.SetPoints(_map.GetMap().at(buf.cur_ind).GetPoints(), _map.GetMap().at(buf.cur_ind).GetPosition());
		_map.SetR1R2(RectR1(buf.end_ind, _map), _fig.GetColorPlayer());
	}
	else
	{
		vovan::Vector2f _p = vovan::Vector2f(_fig.GetPosition().x, _fig.GetPosition().y);
		float d = sqrt((_p.x - buf.endP.x) * (_p.x - buf.endP.x) + (_p.y - buf.endP.y) * (_p.y - buf.endP.y));

		_fig.Offset(vovan::Vector2f(5 * (buf.endP.x - _p.x) / d, 5 * (buf.endP.y - _p.y) / d));

		if(d < 6)
		{
			_map.GetMap().at(buf.end_ind).SetColor(_fig.GetColorEnemy());
			_map.SetImp(buf.end_ind, true);

			if(!buf.range_one)
				_map.SetImp(buf.cur_ind, false);

			for (int i = 0; i < _map.GetR1().size(); i++)
			{
				_map.GetMap().at(_map.GetR1().at(i)).SetColor(_fig.GetColorEnemy());
			}

			_fig = Shapes();
			buf.Reset();
		}
	}
}
