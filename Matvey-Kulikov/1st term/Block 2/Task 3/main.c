#include <stdio.h>
#include <stdlib.h>

int get_digital_root(int number)
{
	if (number == 0)
	{
		return 0;
	}
	int root = (number % 9) ? (number % 9) : 9;
	return root;

}

int main()
{
	printf("This program calculates sum of all MDRS(n) for n from 2 to 999999\n");
	int* digital_roots = (int*) malloc(sizeof(int)*1000000);
	int sum = 0;
	for (int x = 2; x < 1000000; x++)
	{
		digital_roots[x] = get_digital_root(x);
		for (int y = 2; y * y <= x; y++)
		{
			if ((x % y == 0) && (digital_roots[x] < digital_roots[y] + digital_roots[x / y]))
			{
				digital_roots[x] = digital_roots[y] + digital_roots[x / y];
			}
		}
		sum += digital_roots[x];
	}
	free(digital_roots);
	printf("Sum is %d", sum);
	return 0;
}
