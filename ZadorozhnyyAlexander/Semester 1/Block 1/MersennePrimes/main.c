#include <stdio.h>
#include <math.h>
#include <stdbool.h>


bool is_prime_number(int number)
{
	if (number == 1)
		return false;
	for (int i = 2; i <= (int)sqrt(number); i++)
	{
		if (number % i == 0)
			return false;
	}
	return true;
}


int main() 
{
	printf("This programm write Mersenne prime numbers(2^n - 1) in the range [1; 2^31 - 1]\n\n");
	printf("Prime Mersenne number: ");
	for (int degree = 1;degree <= 31; degree++)
	{
		int mersenne_number = pow(2, degree) - 1;
		if (is_prime_number(mersenne_number))
			printf("%d ", mersenne_number);
	}
	return 0;
}