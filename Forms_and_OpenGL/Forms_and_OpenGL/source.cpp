#pragma once
#include <windows.h>              // ������������ ����� ��� Windows
#include <gl\glut.h>                // ������������ ����� ��� ���������� OpenGL32
#include <gl\glu.h>              // ������������ ����� ��� ���������� GLu32
#include <gl\glaux.h>              // ������������ ����� ��� ���������� GLaux
#include "EngineForms.h"
#include "Property.h"
#include "EngineClass.h"
#include "Render.h"
#include <tchar.h>

#pragma comment(lib, "opengl32.lib")
#pragma comment(lib, "glu32.lib")
#pragma comment(lib, "glut32.lib")

HGLRC  hRC=NULL;              // ���������� �������� ����������
HDC  hDC=NULL;              // ��������� �������� ���������� GDI
HWND  hWnd=NULL;              // ����� ����� �������� ���������� ����
HINSTANCE  hInstance;              // ����� ����� �������� ���������� ����������
RECT rect_window_gl;			//������������� ���� opengl
Render render;

//bool  keys[256];                // ������, ������������ ��� �������� � �����������
bool  active=true;                // ���� ���������� ����, ������������� � true �� ���������
bool  fullscreen=false;              // ���� ������ ����, ������������� � ������������� �� ���������

LRESULT  CALLBACK WndProc( HWND, UINT, WPARAM, LPARAM );        // �������� ������� WndProc

using namespace System;
[System::Runtime::InteropServices::DllImport("user32.dll", SetLastError = true)]
System::IntPtr SetParent(System::IntPtr hWndChild, System::IntPtr hWndNewParent);
[STAThread]

GLvoid ReSizeGLScene( GLsizei width, GLsizei height )        // �������� ������ � ���������������� ���� GL
{
	if( height == 0 )              // �������������� ������� �� ����
	{
		height = 1;
	}
    
	glViewport( 0, 0, width, height );          // ����� ������� ������� ������
	glMatrixMode( GL_PROJECTION );            // ����� ������� ��������
	glLoadIdentity();              // ����� ������� ��������
 
	// ���������� ����������� �������������� �������� ��� ����
	gluPerspective( 45.0f, (GLfloat)width/(GLfloat)height, 0.1f, 100.0f );
 
	glMatrixMode( GL_MODELVIEW );            // ����� ������� ���� ������
	glLoadIdentity();              // ����� ������� ���� ������
}

int InitGL( GLvoid )                // ��� ��������� ������� OpenGL ���������� �����
{
	glShadeModel( GL_SMOOTH );            // ��������� ������� �������� �����������
	glClearColor(0.1f, 0.1f, 0.1f, 0.0f);          // ������� ������ � ������ ����
	glClearDepth( 1.0f );              // ��������� ������� ������ �������
    glEnable( GL_DEPTH_TEST );            // ��������� ���� �������
    glDepthFunc( GL_LEQUAL );            // ��� ����� �������
	glHint( GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST );      // ��������� � ���������� �����������

	return true;                // ������������� ������ �������
}

int DrawGLScene( GLvoid )                // ����� ����� ����������� ��� ����������
{
    glClear( GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT );      // �������� ����� � ����� �������
    //glLoadIdentity();              // �������� ������� �������

	render.draw();

	glTranslatef(0, 0, -100);
	glColor3ub(255, 0, 155);
	glutWireCone(20, 50, 20, 50);
	glTranslatef(0, 0, 100);

    return true;                // ���������� ������ �������
}

GLvoid KillGLWindow( GLvoid )              // ���������� ���������� ����
{
	if( fullscreen )              // �� � ������������� ������?
    {
		ChangeDisplaySettings( NULL, 0 );        // ���� ��, �� ������������� ������� � ������� �����
        ShowCursor( true );            // �������� ������ �����
    }
	if( hRC )                // ���������� �� �������� ����������?
    {
		if( !wglMakeCurrent( NULL, NULL ) )        // �������� �� ���������� RC � DC?
        {
			MessageBox( NULL, (LPCWSTR)"Release Of DC And RC Failed.", (LPCWSTR)"SHUTDOWN ERROR", MB_OK | MB_ICONINFORMATION );
        }
		if( !wglDeleteContext( hRC ) )        // �������� �� ������� RC?
        {
			MessageBox( NULL, (LPCWSTR)"Release Rendering Context Failed.", (LPCWSTR)"SHUTDOWN ERROR", MB_OK | MB_ICONINFORMATION );
        }
		hRC = NULL;              // ���������� RC � NULL
    }
	if( hDC && !ReleaseDC( hWnd, hDC ) )          // �������� �� ���������� DC?
    {
        MessageBox( NULL, (LPCWSTR)"Release Device Context Failed.", (LPCWSTR)"SHUTDOWN ERROR", MB_OK | MB_ICONINFORMATION );
        hDC=NULL;                // ���������� DC � NULL
    }
	if(hWnd && !DestroyWindow(hWnd))            // �������� �� ���������� ����?
	{
		MessageBox( NULL, (LPCWSTR)"Could Not Release hWnd.", (LPCWSTR)"SHUTDOWN ERROR", MB_OK | MB_ICONINFORMATION );
		hWnd = NULL;                // ���������� hWnd � NULL
	}
	if( !UnregisterClass( (LPCWSTR)"OpenGL", hInstance ) )        // �������� �� ����������������� �����
	{
		MessageBox( NULL, (LPCWSTR)"Could Not Unregister Class.", (LPCWSTR)"SHUTDOWN ERROR", MB_OK | MB_ICONINFORMATION);
		hInstance = NULL;                // ���������� hInstance � NULL
	}
}

BOOL CreateGLWindow( LPCWSTR title, int width, int height, int bits, bool fullscreenflag )
{
	//------------------------------------------�������� ������ OpenGL
	EngineClass en;
	//------------------------------------------

	rect_window_gl.left = 0;
	rect_window_gl.right = EngineClass::getForm()->getPanel()->Width;
	rect_window_gl.top = 0;
	rect_window_gl.bottom = EngineClass::getForm()->getPanel()->Height;

	GLuint    PixelFormat;              // ������ ��������� ����� ������
	WNDCLASS  wc;                // ��������� ������ ����
	DWORD    dwExStyle;              // ����������� ����� ����
	DWORD    dwStyle;              // ������� ����� ����
	RECT WindowRect;                // Grabs Rectangle Upper Left / Lower Right Values
	WindowRect.left=(long)0;              // ���������� ����� ������������ � 0
	WindowRect.right=(long)width;              // ���������� ������ ������������ � Width
	WindowRect.top=(long)0;                // ���������� ������� ������������ � 0
	WindowRect.bottom=(long)height;              // ���������� ������ ������������ � Height
	fullscreen=fullscreenflag;              // ������������� �������� ���������� ���������� fullscreen
	hInstance    = GetModuleHandle(NULL);        // ������� ���������� ������ ����������
	wc.style    = CS_HREDRAW | CS_VREDRAW | CS_OWNDC;      // ���������� ��� ����������� � ������ ������� DC
	wc.lpfnWndProc    = (WNDPROC) WndProc;          // ��������� ��������� ���������
	wc.cbClsExtra    = 0;              // ��� �������������� ���������� ��� ����
	wc.cbWndExtra    = 0;              // ��� �������������� ���������� ��� ����
	wc.hInstance    = hInstance;            // ������������� ����������
	wc.hIcon    = LoadIcon(NULL, IDI_WINLOGO);        // ��������� ������ �� ���������
	wc.hCursor    = LoadCursor(NULL, IDC_ARROW);        // ��������� ��������� �����
	wc.hbrBackground  = NULL;              // ��� �� ��������� ��� GL
	wc.lpszMenuName    = NULL;              // ���� � ���� �� �����
	wc.lpszClassName  = _T("OpenGL");            // ������������� ��� ������

	if( !RegisterClass( &wc ) )              // �������� ���������������� ����� ����
	{
		MessageBox( NULL, _T("Failed To Register The Window Class."), _T("ERROR"), MB_OK | MB_ICONEXCLAMATION );
		return false;                // ����� � ����������� �������� �������� false
	}

	if( fullscreen )                // ������������� �����?
	{
		DEVMODE dmScreenSettings;            // ����� ����������
		memset( &dmScreenSettings, 0, sizeof( dmScreenSettings ) );    // ������� ��� �������� ���������
		dmScreenSettings.dmSize=sizeof( dmScreenSettings );      // ������ ��������� Devmode
		dmScreenSettings.dmPelsWidth  =   width;        // ������ ������
		dmScreenSettings.dmPelsHeight  =   height;        // ������ ������
		dmScreenSettings.dmBitsPerPel  =   bits;        // ������� �����
		dmScreenSettings.dmFields= DM_BITSPERPEL | DM_PELSWIDTH | DM_PELSHEIGHT;// ����� �������
		// �������� ���������� ��������� ����� � �������� ���������.  ����������: CDS_FULLSCREEN ������� ������ ����������.
		if( ChangeDisplaySettings( &dmScreenSettings, CDS_FULLSCREEN ) != DISP_CHANGE_SUCCESSFUL )
		{
			// ���� ������������ � ������������� ����� ����������, ����� ���������� ��� ��������: ������� ����� ��� �����.
			if( MessageBox( NULL, _T("The Requested Fullscreen Mode Is Not Supported By\nYour Video Card. Use Windowed Mode Instead?"),
			_T("NeHe GL"), MB_YESNO | MB_ICONEXCLAMATION) == IDYES )
			{
				 fullscreen = false;          // ����� �������� ������ (fullscreen = false)
			}
			else
			{
				// ������������� ����, ���������� ������������ � �������� ����.
				MessageBox( NULL, _T("Program Will Now Close."), _T("ERROR"), MB_OK | MB_ICONSTOP );
				return false;            // ����� � ����������� �������� false
			}
		}
	}

	if(fullscreen)                  // �� �������� � ������������� ������?
	{
		dwExStyle  =   WS_EX_APPWINDOW;          // ����������� ����� ����
		dwStyle    =   WS_POPUP;            // ������� ����� ����
		ShowCursor( false );              // ������ ��������� �����
	}
	else
	{
		dwExStyle  =   WS_EX_APPWINDOW | WS_EX_WINDOWEDGE;      // ����������� ����� ����
		dwStyle    =   WS_VISIBLE | WS_POPUP;        // ������� ����� ����
	}

	AdjustWindowRectEx( &rect_window_gl, dwStyle, false, dwExStyle );      // ��������� ���� ���������� �������

	if( !( hWnd = CreateWindowEx(  dwExStyle,          // ����������� ����� ��� ����
          _T("OpenGL"),          // ��� ������
          title,            // ��������� ����
		  WS_CLIPSIBLINGS |        // ��������� ����� ��� ����
          WS_CLIPCHILDREN |        // ��������� ����� ��� ����
          dwStyle,          // ���������� ����� ��� ����
          0, 0,            // ������� ����
          rect_window_gl.right - rect_window_gl.left,    // ���������� ���������� ������
          rect_window_gl.bottom - rect_window_gl.top,    // ���������� ���������� ������
          NULL,            // ��� �������������
          NULL,            // ��� ����
          hInstance,          // ���������� ����������
          NULL ) ) )          // �� ������� ������ �� WM_CREATE (???)
	{
		KillGLWindow();                // ������������ �����
		MessageBox( NULL, _T("Window Creation Error."), _T("ERROR"), MB_OK | MB_ICONEXCLAMATION );
		return false;                // ������� false
	}

	static  PIXELFORMATDESCRIPTOR pfd=            // pfd �������� Windows ����� ����� ����� �� ����� ������� �������
	{
		sizeof(PIXELFORMATDESCRIPTOR),            // ������ ����������� ������� ������� ��������
		1,                  // ����� ������
		PFD_DRAW_TO_WINDOW |              // ������ ��� ����
		PFD_SUPPORT_OPENGL |              // ������ ��� OpenGL
		PFD_DOUBLEBUFFER,              // ������ ��� �������� ������
		PFD_TYPE_RGBA,                // ��������� RGBA ������
		bits,                  // ���������� ��� ������� �����
		0, 0, 0, 0, 0, 0,              // ������������� �������� �����
		0,                  // ��� ������ ������������
		0,                  // ��������� ��� ������������
		0,                  // ��� ������ ����������
		0, 0, 0, 0,                // ���� ���������� ������������
		32,                  // 32 ������ Z-����� (����� �������)
		0,                  // ��� ������ ���������
		0,                  // ��� ��������������� �������
		PFD_MAIN_PLANE,                // ������� ���� ���������
		0,                  // ���������������
		0, 0, 0                  // ����� ���� ������������
	};

	if( !( hDC = GetDC( hWnd ) ) )              // ����� �� �� �������� �������� ����������?
	{
		KillGLWindow();                // ������������ �����
		MessageBox( NULL, _T("Can't Create A GL Device Context."), _T("ERROR"), MB_OK | MB_ICONEXCLAMATION );
		return false;                // ������� false
	}

	if( !( PixelFormat = ChoosePixelFormat( hDC, &pfd ) ) )        // ������ �� ���������� ������ �������?
	{
		KillGLWindow();                // ������������ �����
		MessageBox( NULL, _T("Can't Find A Suitable PixelFormat."), _T("ERROR"), MB_OK | MB_ICONEXCLAMATION );
		return false;                // ������� false
	}

	if( !SetPixelFormat( hDC, PixelFormat, &pfd ) )          // �������� �� ���������� ������ �������?
	{
		KillGLWindow();                // ������������ �����
		MessageBox( NULL, _T("Can't Set The PixelFormat."), _T("ERROR"), MB_OK | MB_ICONEXCLAMATION );
		return false;                // ������� false
	}

	if( !( hRC = wglCreateContext( hDC ) ) )          // �������� �� ���������� �������� ����������?
	{
		KillGLWindow();                // ������������ �����
		MessageBox( NULL, _T("Can't Create A GL Rendering Context."), _T("ERROR"), MB_OK | MB_ICONEXCLAMATION);
		return false;                // ������� false
	}

	if( !wglMakeCurrent( hDC, hRC ) )            // ����������� ������������ �������� ����������
	{
		KillGLWindow();                // ������������ �����
		MessageBox( NULL, _T("Can't Activate The GL Rendering Context."), _T("ERROR"), MB_OK | MB_ICONEXCLAMATION );
		return false;                // ������� false
	}

	//ShowWindow( hWnd, SW_SHOW );              // �������� ����
	SetForegroundWindow( hWnd );              // ������ ������� ���������
	SetFocus( hWnd );                // ���������� ����� ���������� �� ���� ����
	//ReSizeGLScene( width, height );              // �������� ����������� ��� ������ OpenGL ������.
	ReSizeGLScene( (GLsizei)rect_window_gl.right, (GLsizei)rect_window_gl.bottom );

	SetParent((IntPtr)hWnd, EngineClass::getForm()->getPanelHandle());
	EngineClass::getForm()->Show();

	if( !InitGL() )                  // ������������� ������ ��� ���������� ����
	{
		KillGLWindow();                // ������������ �����
		MessageBox( NULL, _T("Initialization Failed."), _T("ERROR"), MB_OK | MB_ICONEXCLAMATION );
		return false;                // ������� false
	}

	return true;                  // �� � �������!
}

LRESULT CALLBACK WndProc(  HWND  hWnd,            // ���������� ������� ����
        UINT  uMsg,            // ��������� ��� ����� ����
        WPARAM  wParam,            // �������������� ����������
        LPARAM  lParam)            // �������������� ����������
{
	switch (uMsg)                // �������� ��������� ��� ����
	{
		case WM_ACTIVATE:            // �������� ��������� ���������� ����
		{
		  if( !HIWORD( wParam ) )          // ��������� ��������� �����������
		  {
			active = true;          // ��������� �������
		  }
		  else
		  {
			active = false;          // ��������� ������ �� �������
		  }
 
		  return 0;            // ������������ � ���� ��������� ���������
		}
		case WM_SYSCOMMAND:            // ������������� ��������� �������
		{
		  switch ( wParam )            // ������������� ��������� �����
		  {
			case SC_SCREENSAVE:        // �������� �� ���������� �����������?
			case SC_MONITORPOWER:        // �������� �� ������� ������� � ����� ���������� �������?
			return 0;          // ������������� ���
		  }
		  break;              // �����
		}
		case WM_CLOSE:              // �� �������� ��������� � ��������?
		{
		  PostQuitMessage( 0 );          // ��������� ��������� � ������
		  return 0;            // ��������� �����
		}
		case WM_KEYDOWN:            // ���� �� ������ ������?
		{
		  keys[wParam] = true;          // ���� ���, �� ����������� ���� ������ true
		  return 0;            // ������������
		}
		case WM_KEYUP:              // ���� �� �������� �������?
		{
		  keys[wParam] = false;          //  ���� ���, �� ����������� ���� ������ false
		  return 0;            // ������������
		}
		case WM_SIZE:              // �������� ������� OpenGL ����
		{
		  ReSizeGLScene( LOWORD(lParam), HIWORD(lParam) );  // ������� �����=Width, ������� �����=Height
		  return 0;            // ������������
		}
	}
	// ���������� ��� �������������� ��������� DefWindowProc
	return DefWindowProc( hWnd, uMsg, wParam, lParam );
}


int WINAPI WinMain(  HINSTANCE  hInstance,        // ���������� ����������
      HINSTANCE  hPrevInstance,        // ���������� ������������� ����������
      LPSTR    lpCmdLine,        // ��������� ��������� ������
      int    nCmdShow )        // ��������� ����������� ����
{
	MSG  msg;              // ��������� ��� �������� ��������� Windows
	BOOL  done = false;            // ���������� ���������� ��� ������ �� �����
	// ���������� ������������, ����� ����� ������ �� ������������
	//if( MessageBox( NULL, _T("������ �� �� ��������� ���������� � ������������� ������?"),  _T("��������� � ������������� ������?"), MB_YESNO | MB_ICONQUESTION) == IDNO )
	//{
	//	fullscreen = false;          // ������� �����
	//}

	// ������� ���� OpenGL ����
	if( !CreateGLWindow( _T("NeHe OpenGL ����"), W_WIDTH, W_HEIGHT, 32, /*fullscreen*/false ) )
	{
		return 0;              // �����, ���� ���� �� ����� ���� �������
	}
	while( !done )                // ���� ������������, ���� done �� ����� true
	{
		if( PeekMessage( &msg, NULL, 0, 0, PM_REMOVE ) )    // ���� �� � ������� �����-������ ���������?
		{
			if( msg.message == WM_QUIT )        // �� ������� ��������� � ������?
			{
				MessageBox(NULL, _T("Program Will Now Close."), _T("ERROR"), MB_OK | MB_ICONSTOP);
				done = true;          // ���� ���, done=true
			}
			else              // ���� ���, ������������ ���������
			{
				TranslateMessage( &msg );        // ��������� ���������
				DispatchMessage( &msg );        // �������� ���������
			}
		}
		else                // ���� ��� ���������
		{
			// ������������� �����.
			if( active )          // ������� �� ���������?
			{
				if(keys[VK_ESCAPE])        // ���� �� ������ ������� ESC?
				{
					done = true;      // ESC ������� �� �������� ���������� ���������
				}
				else            // �� ����� ��� ������, ������� �����.
				{
					DrawGLScene();        // ������ �����
					SwapBuffers( hDC );    // ������ ����� (������� �����������)
				}
			}
			if( keys[VK_F1] )          // ���� �� ������ F1?
			{
				keys[VK_F1] = false;        // ���� ���, ������ �������� ������ ������� �� false
				KillGLWindow();          // ��������� ������� ����
				fullscreen = !fullscreen;      // ����������� �����
				// ���������� ���� OpenGL ����
				if( !CreateGLWindow( _T("NeHe OpenGL ���������"), W_WIDTH, W_HEIGHT, 32, fullscreen ) )
				{
					return 0;        // �������, ���� ��� ����������
				}
			}
		}
	}
	// Shutdown
	KillGLWindow();                // ��������� ����
	return ( msg.wParam );              // ������� �� ���������
}