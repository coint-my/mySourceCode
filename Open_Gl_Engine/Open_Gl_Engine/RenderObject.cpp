#include "RenderObject.h"

using namespace std;

RenderObject::RenderObject(void)
{
	static int countRenderObject = 0;
	countRenderObject++;
	cout << "Load : RenderObject (" << countRenderObject << ")" << endl;
}


RenderObject::~RenderObject(void)
{
	list_obj.clear();
}

void RenderObject::AddObject(const GameObject& _obj)
{
	list_obj.push_back(_obj);
}

void RenderObject::Render()
{

	for(int ob = 0; ob < list_obj.size(); ob++)
	{
		GameObject *obj = &list_obj[ob];
		
		glPushMatrix();
			glRotatef(obj->GetRotation().rot, obj->GetRotation().dir.x, obj->GetRotation().dir.y, obj->GetRotation().dir.z);
			glTranslatef(obj->GetTranslation().x, obj->GetTranslation().y, obj->GetTranslation().z);
			glColor3ub(obj->GetColor().red, obj->GetColor().green, obj->GetColor().blue);

			glNormal3f(0.0, 0.0, -1.0);

			glBegin(GL_QUADS);
			for (int i = 0; i < obj->GetPoints().size(); i++)
			{
				if(obj->IsTexture())
					glTexCoord2f(obj->GetTextureCoord()[i].x, obj->GetTextureCoord()[i].y);

				glVertex3f(obj->GetPoints()[i].x, obj->GetPoints()[i].y, obj->GetPoints()[i].z);
			}
			glEnd();
			obj = 0;
		glPopMatrix();
	}
}

void RenderObject::Update()
{
	for(int ob = 0; ob < list_obj.size(); ob++)
	{
		list_obj[ob].Update();
	}
}

//void RenderObject::Render()
//{
//
//	for(int ob = 0; ob < list_obj.size(); ob++)
//	{
//		glRotatef(list_obj[ob]->GetRotation().rot, list_obj[ob]->GetRotation().dir.x, list_obj[ob]->GetRotation().dir.y, list_obj[ob]->GetRotation().dir.z);
//		glTranslatef(list_obj[ob]->GetTranslation().x, list_obj[ob]->GetTranslation().y, list_obj[ob]->GetTranslation().z);
//		glColor3ub(list_obj[ob]->GetColor().red, list_obj[ob]->GetColor().green, list_obj[ob]->GetColor().blue);
//
//		glBegin(GL_POLYGON);
//		for (int i = 0; i < list_obj[ob]->GetPoints().Size(); i++)
//		{
//			glVertex3f(list_obj[ob]->GetPoints()[i].x, list_obj[ob]->GetPoints()[i].y, list_obj[ob]->GetPoints()[i].z);
//		}
//		glEnd();
//	}
//}
//
//void RenderObject::Update()
//{
//	for(int ob = 0; ob < list_obj.size(); ob++)
//	{
//		list_obj[ob]->Update();
//	}
//}