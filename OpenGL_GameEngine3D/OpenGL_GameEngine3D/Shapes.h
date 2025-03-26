#pragma once
#include "Shape.h"
#include <vector>
#include "VovanLib.h"

class Shapes : public Shape
{
private:
	std::vector<vovan::Vector2f> points;
	vovan::Color color;
	vovan::Color colActive;
	vovan::Color colPass;
	vovan::Color colPlayer;
	vovan::Color colEnemy;
	int rad;
protected:
	vovan::Vector2f position;
public:
	Shapes(void);
	~Shapes(void);

	void Offset(const vovan::Vector2f &_pos);
	void Initialize(const vovan::Vector2f *_point, const int _count, const int _rad);
	void Update();
	void Draw();
	void SetColor(const vovan::Color &_col) { color = _col; }
	void SetPoints(const std::vector<vovan::Vector2f> &_poi, const vovan::Vector2f &_pos);
	void SetRadius(const int _rad) { rad = _rad; }

	std::vector<vovan::Vector2f> &GetPoints() { return points; }
	vovan::Color &GetColor() { return color; }
	vovan::Color &GetColorActive() { return colActive; }
	vovan::Color &GetColorPass() { return colPass; }
	vovan::Color &GetColorPlayer() { return colPlayer; }
	vovan::Color &GetColorEnemy() { return colEnemy; }
	vovan::Vector2f &GetPosition() { return position; }

	int GetRadius() { return rad; }
};

