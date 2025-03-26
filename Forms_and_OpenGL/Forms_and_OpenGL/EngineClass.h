#pragma once
#include "EngineForms.h"
ref class EngineClass
{
private: static Forms_and_OpenGL::EngineForms ^ form;

public: static Forms_and_OpenGL::EngineForms^ getForm();

public:	EngineClass(void);
};

