#include<stdio.h>
#include<math.h>

int is_prime(int x)
{
	if (x == 0 || x == 1)
	{
		return 0;
	}
	int square_root = sqrt(x);
	for (int d = 2; d <= square_root; d++)
	{
		if (x % d == 0)
		{
			return 0;
		}
	}
	return 1;

}

int main()
{
	printf("The program outputs Mersenne numbers.\n");
	long long mersenne_number;
	for (int i = 1; i <= 31; i++)
	{
		mersenne_number = (long long)pow(2, i) - 1;
		if (is_prime(mersenne_number))
		{
			printf("%lld\n", mersenne_number);
		}
	}
	return 0;
}