#pragma once
namespace vovan
{
	struct Vector2f;
}
class Shape
{
public:
	Shape(void) {}
	virtual ~Shape(void) {}

	virtual void Initialize(const vovan::Vector2f *_point, const int _count, const int _rad) = 0;
	virtual void Update() = 0;
	virtual void Draw() = 0;
};

