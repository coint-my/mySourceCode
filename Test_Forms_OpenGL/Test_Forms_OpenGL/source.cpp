#include "forms_1.h"

using namespace System;
using namespace System::Windows::Forms;

[STAThread]

void main(array<String^>^ args)
{
	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false);
	Test_Forms_OpenGL::forms_1 form;
	Application::Run(%form);
}