#include "particle.h"

template<typename T> particle<T>::particle()
{
	max = 1;
	t = new T[max];
}

template<typename T> particle<T>::particle(const int size)
{
	max = size;
	t = new T[max];
}

template<typename T> particle<T>::particle(T& tupe, const particle& part)
{
	t = new T[part.Max() + 1];
	for(int i = 0; i < part.Max(); i++)
	{
		t[i] = part[i];
	}
	t[part.Max() + 1] = tupe;
	max++;
}

template<typename T> particle<T>::~particle()
{
	delete [] t;
}

template<typename T> int particle<T>::Max() const
{
	return max;
}

template<typename T> void particle<T>::Add(const T& tupe)
{
	particle<T> &p = particle(tupe, *this);
	*this = p;
}

template<typename T> T particle<T>::operator [](const int ind)
{
	return t[ind];
}
