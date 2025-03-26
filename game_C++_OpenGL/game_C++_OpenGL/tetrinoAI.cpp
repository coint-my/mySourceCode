#include "tetrinoAI.h"

tetrinoAI::tetrinoAI(void)
{
}

tetrinoAI::~tetrinoAI(void)
{
	x = 0;
	rotation = 0;
	isGoNext = true;
}

bool tetrinoAI::Init(unity &u)
{
	tetrino tt = u.getTet();

	int xtt = 0;
	int ytt = 0;

	for(int i = 0; i < 4; i++)
	{
		int ycout = 0;

		for(int j = 0; j < 4; j++)
		{
			if(tt.map[i][j])
				ycout++;
		}

		if(ycout > ytt)
			ytt = ycout;
	}
	for(int j = 0; j < 4; j++)
	{
		int ycout = 0;

		for(int i = 0; i < 4; i++)
		{
			if(tt.map[i][j])
				ycout++;
		}

		if(ycout > xtt)
			xtt = ycout;
	}

	for(int j = u.hCount; j > 1; j--)
	{
		int xcout = 0;
		for(int i = 0; i < u.wCount; i++)
		{
			if(!u.Map(i, j))
				xcout++;
		}
		if(xcout >= xtt || xcout >= ytt)
			return true;
	}
	return false;
}


bool tetrinoAI::easy(unity &u)
{
	int top = 0;
	int right = 0;
	int left = 0;
	int bottom = 0;
	int leftOffset = 0;
	int downOffset = 0;
	x = 0;

	tetrino tt = u.getTet();
	for(int i = 0; i < 4; i++)
	{
		int cout = 0;
		for(int j = 0; j < 4; j++)
		{
			if(tt.map[i][j])
			{
				cout++;
				//leftOffset = i;
			}
		}
		if(cout > 0)
		{
			left = cout;
			break;
		}
	}
	bool off = false;
	for(int i = 3; i > 0; i--)
	{
		for(int j = 0; j < 4; j++)
		{
			if(tt.map[j][i])
			{
				leftOffset = j;
				off = true;
				break;
			}
		}
		if(off)
			break;
	}
	for(int i = 0; i < 4; i++)
	{
		int cout = 0;
		for(int j = 0; j < 4; j++)
		{
			if(tt.map[j][i])
				cout++;
		}
		if(cout > 0)
		{
			top = cout;
			break;
		}
	}
	for(int i = 3; i > 0; i--)
	{
		int cout = 0;
		for(int j = 0; j < 4; j++)
		{
			if(tt.map[i][j])
				cout++;
		}
		if(cout > 0)
		{
			right = cout;
			break;
		}
	}
	for(int i = 3; i > 0; i--)
	{
		int cout = 0;
		for(int j = 0; j < 4; j++)
		{
			if(tt.map[j][i])
				cout++;
		}
		if(cout > 0)
		{
			bottom = cout;
			break;
		}
	}
	int *var = new int[u.wCount];
	int *position = new int[u.wCount];
	bool br = false;
	for(int j = u.hCount - 1; j > 1; j--)
	{
		bool flagvar = u.Map(0 , u.hCount - 1);
		int xcout = 0;
		int c = 0;
		for(int i = 0; i < u.wCount; i++)
		{
			if(!u.Map(i, j))
			{
				c++;
				if(!flagvar)
				{
					position[xcout] = i - leftOffset;

					xcout++;
				}
				flagvar = true;
				var[xcout - 1] = c;
			}
			else
			{
				flagvar = false;
				c = 0;
			}
		}
		for(int i = 0; i < xcout; i++)
		{
			if(var[i] >= bottom)
			{
				bool istrue = false;
				for(int y = j; y > 1; y--)
				{
					for(int x = position[i] + 1; x <=
						(position[i] + 1) + bottom - 1; x++)
					{
						if(u.Map(x, y))
						{
							istrue = true;
							break;
						}
					}
					if(istrue)
						break;
				}
				if(!istrue)
				{
					x = position[i];
					br = true;
					break;
				}
			}
		}
		if(rotation > 3)
			rotation = 0;
		if(br)
			break;
	}

	delete []var;
	delete []position;
	return false;
}
void tetrinoAI::update(unity &u)
{
	//else if(x == u.getTet().X())
	//	u.moveDown();
	if(x < u.getTet().X())
		u.move(tetrino::LEFT);
	else if(x > u.getTet().X())
		u.move(tetrino::RIGHT);
}