#pragma once
#include "EngineBase.h"

class GameObject
{
protected:
	std::vector<Translation> points;
	std::vector<Translation> textPoints;
	Translation translation;
	Rotation rotation;
	Color color;
	bool isTexture;
public:
	GameObject(void);
	virtual ~GameObject(void);

	virtual void Initialize(const Translation *_tr, const int _count){}
	virtual void Update(){}
	virtual void Render(){}

	void LoadPoints(const Translation *const _list, const int _count);

	void SetTranslation(const Translation &_tr) { translation = _tr; }
	void SetColor(const Color& _col) { color = _col; }

	std::vector<Translation> &GetPoints() { return points; }
	std::vector<Translation> &GetTextureCoord() { return textPoints; }
	const inline Translation &GetTranslation() const { return translation; }
	inline bool IsTexture() { return isTexture; } 
	const Rotation &GetRotation() const { return rotation; }
	const Color &GetColor() const { return color; }
};

