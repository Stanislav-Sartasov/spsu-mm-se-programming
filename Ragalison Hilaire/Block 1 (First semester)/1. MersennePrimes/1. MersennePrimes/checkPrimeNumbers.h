

/*Name and first name : Ragalison Hilaire ;
  adress email: st085683@student.spbu.ru ;
  Цель задачи: вывести на экран все простые числа Мерсенна на отрезке [1; 2^31 -1] */

#include <stdbool.h>

bool isPrime(int x)
{
	if (x > 1)
	{
		for (int i = 2; i < x; i++)
		{
			if (x % i == 0)
			{
				return false;
			}
		}
		return true;
	}
	else
		return false;
}


