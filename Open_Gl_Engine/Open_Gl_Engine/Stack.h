#ifndef STACK_H
#include <iostream>
#include <exception>

#define HB template<typename T>

HB class Stack
{
private:
	T *arr;
	T *temp;
	int size;
	int count;
	int cur;
public:
	Stack(void) : count(10), size(0), cur(-1) { arr = new T[count]; }
	Stack(const int _count) : count(_count), size(0), cur(-1) { arr = new T[count]; }
	~Stack() { delete [] arr; }

	int Size() const { return size; }

	bool isFull() { return cur == count - 1; }
	bool isEmpty() { return cur == -1; }

	void clear()
	{
		delete [] arr;
		arr = 0;
		size = 0;
		count = 10;
		cur = -1;

		arr = new T[count];
	}

	void Erase(const int _ind)
	{
		try
		{
			if(!isEmpty())
			{
				if(_ind < 0 || _ind > size - 1)
					throw "выход за пределы массива";
				else
				{
					temp = new T[count];
					copy(arr, temp, _ind);
					delete []arr;
					arr = 0;

					arr = new T[count];
					copy(temp, arr);
					delete []temp;
					temp = 0;
					size--;
				}
			}
		}
		catch(const char* _name)
		{
			std::cout << "ошибка !!! (" << _name << ")" << std::endl;
		}
	}

	void copy(const T *_tupe, T *_val)
	{
		int ii = 0;
		Stack st(count);
		while(ii < size)
		{
			T _t = _tupe[ii];
			st.push(_t);
			_val[ii] = st[ii];
			ii++;
		}
	}

	void copy(const T *_tupe, T *_val, const int _goo)
	{
		int ii = 0;
		Stack st(count);
		bool isFlag = false;
		while(ii < size)
		{
			if(_goo != ii)
			{
				T _t = _tupe[ii];
				st.push(_t);
				_val[isFlag ? ii - 1 : ii] = st[isFlag ? ii - 1 : ii];
			}
			else
				isFlag = true;

			ii++;
		}
	}

	void push(const T &_t)
	{
		if(!isFull())
		{
			size++;
			arr[++cur] = _t;
		}
		else 
		{
			std::cout << "stak is full !!!" << std::endl;

			temp = new T[count];
			copy(arr, temp);
		    delete []arr;
			arr = 0;

			count = count * 2;
			arr = new T[count];
			copy(temp, arr);
			delete []temp;
			temp = 0;

			size++;
			arr[++cur] = _t;
		}
	}

	const T &operator[](int _ind)
	{
		if(_ind > -1 && _ind < size)
			return this->arr[_ind];
		else 
			std::cout << "out of range exeption (Wrong index [" << _ind + 1 << "])" << std::endl;

		return T();
	}
};
#endif