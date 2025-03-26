#include "ManagerEditor.h"

ManagerEditor::ManagerEditor(void)
{
	
}


ManagerEditor::~ManagerEditor(void)
{
	
}

void ManagerEditor::Render()
{
	glColor3ub(25, 25, 25);
	glRecti(tRect2.Left(), tRect2.Top(), tRect2.Right(), tRect2.Bottom());

	glColor3ub(30, 29, 1);
	glRecti(tRect.Left(), tRect.Top(), tRect.Right(), tRect.Bottom());

	for (int j = 0; j < map.GetMap().size(); j++)
	{
		glBegin(GL_POLYGON);
		for (int i = 0; i < map.GetPoints(j).size(); i++)
		{
			glColor3ub(map.GetMap().at(j).GetColor().r, map.GetMap().at(j).GetColor().g, map.GetMap().at(j).GetColor().b);
			glVertex2f(map.GetPoints(j).at(i).x, map.GetPoints(j).at(i).y);
		}
		glEnd();
	}
	glBegin(GL_POLYGON);
	glColor3ub(255, 128, 255);
	for (int i = 0; i < figure.GetPoints().size(); i++)
	{
		glVertex2f(figure.GetPoints().at(i).x, figure.GetPoints().at(i).y);
	}
	glEnd();
}

void ManagerEditor::Mouse(int _b, int _s, int _x, int _y)
{
	if(_b == GLUT_LEFT_BUTTON && _s == GLUT_UP)
	{
		if(buf.isActive)
		{
			buf.turnPlayer = false;
			VarRect();

			if(buf.varible)
			{
				map.SetCursor(-1);
				for (int i = 0; i < map.GetMap().size(); i++)
				{
					Shapes sh = map.GetMap().at(i);
					float d = sqrt((sh.GetPosition().x - _x) * (sh.GetPosition().x - _x) + (sh.GetPosition().y - _y) * (sh.GetPosition().y - _y));

					if(d < sh.GetRadius() && (!map.GetImp().at(i)))
					{
						vovan::Recti trm(vovan::Vector2i(_x, _y), 2, 2);

						buf.isActive = false;
						figure.SetPoints(map.GetMap().at(buf.cur_ind).GetPoints(), map.GetMap().at(buf.cur_ind).GetPosition());

						if(trm.Intersect(tRect))
						{
							buf.endP = map.GetMap().at(i).GetPosition();
							buf.end_ind = i;
							buf.updateOk = true;
							map.SetR1R2(RectR1(i), figure.GetColorEnemy());
						}
						else if(trm.Intersect(tRect2))
						{
							buf.endP = map.GetMap().at(i).GetPosition();
							buf.end_ind = i;
							buf.updateOk = true;
							map.SetImp(buf.cur_ind, false);
							map.SetR1R2(RectR1(i), figure.GetColorEnemy());
						}
						else
						{
							buf.BuffReset();
							figure = Shapes();
							tRect.Initialize(vovan::Recti());
							tRect2.Initialize(vovan::Recti());
						}
					}
				}
			}

			buf.varible = true;
		}
	}
	else if(_b == GLUT_RIGHT_BUTTON && _s == GLUT_UP)
	{
		buf.BuffReset();
		figure = Shapes();
		tRect.Initialize(vovan::Recti());
		tRect2.Initialize(vovan::Recti());
	}
}

void ManagerEditor::PassMouse(int _x, int _y)
{
	if(buf.turnPlayer)
	{
		for (int i = 0; i < map.GetMap().size(); i++)
		{
			Shapes sh = map.GetMap().at(i);
			float d = sqrt((sh.GetPosition().x - _x) * (sh.GetPosition().x - _x) + (sh.GetPosition().y - _y) * (sh.GetPosition().y - _y));

			if(d < sh.GetRadius())
			{
				if(map.IsColor(map.GetMap().at(i).GetColor(), map.GetMap().at(i).GetColorPlayer()))
				{
					buf.cur_ind = i;
					buf.isActive = true;
				}
				map.SetCursor(i);
				break;
			}
			else 
			{
				map.SetCursor(-1);
				buf.BuffReset();
			}
		}
	}
}

vovan::Recti ManagerEditor::RectR1(const int _ind)
{
	int _rad = map.GetMap().at(_ind).GetRadius();
	int _w = (_rad * 2) * 3;
	int _h = (_rad * 2) * 3;
	vovan::Vector2f pos = map.GetMap().at(_ind).GetPosition();
	return vovan::Recti(vovan::Vector2i((int)pos.x - _w / 2, (int)pos.y - _h / 2), _w, _h);
}

void ManagerEditor::VarRect()
{
	int _rad = map.GetMap().at(buf.cur_ind).GetRadius();
	Vector2f _p = map.GetMap().at(buf.cur_ind).GetPosition();
	vovan::Recti _r1(vovan::Vector2i(), (_rad * 2) * 3, (_rad * 2) * 3);
	vovan::Recti _r2(vovan::Vector2i(), (_rad * 2) * 5, (_rad * 2) * 5);
	tRect.Initialize(vovan::Recti(vovan::Vector2i((int)_p.x - (_r1.GetWid() / 2), (int)_p.y - (_r1.GetHei() / 2)), _r1.GetWid(), _r1.GetHei()));
	tRect2.Initialize(vovan::Recti(vovan::Vector2i((int)_p.x - (_r2.GetWid() / 2), (int)_p.y - (_r2.GetHei() / 2)), _r2.GetWid(), _r2.GetHei()));
}

void ManagerEditor::Init()
{
	map.InitMatrix(8, 6);

	map.SetImp(40, true);
	map.GetMap()[40].SetColor(map.GetMap()[0].GetColorPlayer());
	//map.SetImp(25, true);
	//map.GetMap()[25].SetColor(map.GetMap()[0].GetColorPlayer());

	map.SetImp(7, true);
	map.GetMap()[7].SetColor(map.GetMap()[0].GetColorEnemy());
	//map.SetImp(14, true);
	//map.GetMap()[14].SetColor(map.GetMap()[0].GetColorEnemy());
	//map.SetImp(27, true);
	//map.GetMap()[27].SetColor(map.GetMap()[0].GetColorEnemy());
}

void ManagerEditor::Update()
{
	if(ai.GetBufferAI().turn_AI)
	{
		ai.Update(map, figure);
	}
	else
	{
		map.Update();

		if(buf.updateOk)
		{
			vovan::Vector2f _p = vovan::Vector2f(figure.GetPosition().x, figure.GetPosition().y);
			float d = sqrt((_p.x - buf.endP.x) * (_p.x - buf.endP.x) + (_p.y - buf.endP.y) * (_p.y - buf.endP.y));

			figure.Offset(vovan::Vector2f(5 * (buf.endP.x - _p.x) / d, 5 * (buf.endP.y - _p.y) / d));

			if(d < 6)
			{
				map.GetMap().at(buf.end_ind).SetColor(figure.GetColorPlayer());
				map.SetImp(buf.end_ind, true);

				for (int i = 0; i < map.GetR1().size(); i++)
				{
					map.GetMap().at(map.GetR1().at(i)).SetColor(figure.GetColorPlayer());
				}

				figure = Shapes();
				tRect.Initialize(vovan::Recti());
				tRect2.Initialize(vovan::Recti());
				buf.BuffReset();
				ai.GetBufferAI().turn_AI = true;
			}
		}
	}
}
