#pragma once

template<typename T>

class particle
{
private:
	T *t;
	int max;
public:
	particle(void);
	particle(const int size);
	particle(T& tupe, const particle& part);
	~particle();

	void Add(const T& tupe);
	int Max() const;

	T operator[](const int ind);
};
