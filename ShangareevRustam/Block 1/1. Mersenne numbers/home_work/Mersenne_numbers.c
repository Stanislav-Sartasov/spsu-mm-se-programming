#include <stdio.h>
#include <math.h>
#include <stdbool.h>

bool is_prime(int n) // prime check algorithm
{
	bool result = true;
	for (int divisor = 2; divisor <= sqrt(n); divisor++)
	{
		if (n % divisor == 0)
		{
			result = false;
			break;
		}
	}
	return result;
}

int main()
{
	int Mersenne_number = 2 * 2 - 1; /* first Mersenne number
	is 2 - 1 = 1 not prime */
	for (int i = 1; i <= 30; i++)
	{
		if (is_prime(Mersenne_number))
		{
			printf("%d ", Mersenne_number);
		}
		Mersenne_number = 2 * Mersenne_number + 1; /* getting
		the next Mersenne number */
	}
	return 0;
}