#include <stdio.h>
#include <math.h>

int main()
{
	printf("This program takes number from user, write period of this continued fraction and sequence elements\n");
	long long num;
	int check_num;
	do
	{
		printf("Enter natural number that is not a square number: \n");
		check_num = scanf("%lld", &num);
		while (getchar() != '\n');
	}
	while (check_num != 1 || num <= 0 || (pow(floorl(sqrtl(num)), 2) == num));
	unsigned long sqrt_num = floorl(sqrt(num)), start = sqrt_num, div = start, quit = (num - pow(div, 2));
	start = (sqrt_num + div) / quit;
	printf("Sequence elements: %lu", div);
	int period = 1;
	do
	{
		div = start * quit - div;
		quit = (num - pow(div, 2)) / quit;
		start = (sqrt_num + div) / quit;
		printf(", %lu", start);
		period++;
	}
	while (quit != 1);
	printf("\nPeriod of this continued fraction: %d\n", period);
	return 0;
}