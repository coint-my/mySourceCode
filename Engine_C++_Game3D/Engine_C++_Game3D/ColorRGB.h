#pragma once

class ColorRGB
{
private:
	float r, g, b;
public:
	ColorRGB(void);
	ColorRGB(float _r, float _g, float _b) : r(_r), g(_g), b(_b) {}
	float* GetColor() const;
	void SetColor(float _r, float _g, float _b);
	~ColorRGB(void);
};
