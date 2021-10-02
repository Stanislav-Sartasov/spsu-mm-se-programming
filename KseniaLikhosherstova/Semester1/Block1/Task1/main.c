#include<stdio.h>
#include<math.h>

int main()
{
	printf("The program outputs Mersenne numbers.\n");
	long long mersenne_number;
	for (int i = 2; i <= 31; ++i)
	{
		mersenne_number = (long long)pow(2, i) - 1;
		//  Проверка на простоту чисел
		int is_prime = 1;
		for (int j = 2; j * j <= i; ++j)
		{
			if (i % j == 0)
			{
				is_prime = 0;
				break;
			}
		}
		if (is_prime)
			printf("%lld\n", mersenne_number);
    }
	return 0;
}