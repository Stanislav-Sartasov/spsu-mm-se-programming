#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>


void swap(int *pa,int *pb)
{
	int temp = *pa;
	*pa = *pb;
	*pb = temp;
}

void is_pythagorean(int x, int y, int z)
{
	if (x * x + y * y == z * z)
	{
		for (int d = 2; d <= x; d++)
		{
			if ((x % d == 0) && (y % d == 0) && (z % d == 0))
			{
				printf("\nThe triple is Pythagorean!\n\n");
				break;
			}
			if (d == x)
			{
				printf("\nThe triple is primitive Pythagorean!\n\n");
			}
		}
	}
	else
	{
		printf("\nThe triple is not Pythagorean!\n\n");
	}
}

int main()
{
	printf("This program determines whether a triple of numbers ");
	printf("is Pythagorean,and also whether it is primitive.\n\n");

	printf("Enter three natural numbers: \n");
	int x, y, z;
	char end;
	while (scanf("%d %d %d%c", &x, &y, &z, &end) != 4 || x <= 0 || y <= 0 || z <= 0 || end != '\n')
	{
		while (end != '\n')
		{
			scanf("%c", &end);
		}
		end = '\0';
		printf("Input error, please try again: \n");
	}

	if (x > y)
		swap(&x, &y);
	if (y > z)
		swap(&y, &z);
	if (x > y)
		swap(&x, &y);

	is_pythagorean(x, y, z);
	return 0;
}