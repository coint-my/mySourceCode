#pragma once
class StringRead
{
public:
	StringRead(void);
	~StringRead(void);

	static void ShowString(const char _string, const int _x, const int _y);
	static void ShowString(const char *_string, const int _x, const int _y);
	static void ShowString(const char *_string, const int _intejer, const char* _strend, const int _x, const int _y);
	static void ShowString(int _intejer, const int _x, const int _y);
};

