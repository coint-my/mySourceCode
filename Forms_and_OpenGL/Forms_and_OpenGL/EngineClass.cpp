#include "EngineClass.h"


EngineClass::EngineClass(void)
{
	form = gcnew Forms_and_OpenGL::EngineForms;
}

Forms_and_OpenGL::EngineForms^ EngineClass::getForm()
{
	return form;
}
