#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>

#define INT64 __int64_t
#define MIN(a, b) ((a) < (b) ? (a) : (b))
#define MAX(a, b) ((a) > (b) ? (a) : (b))
#define SQ(x) (x) * (x)

void swap(INT64* a, INT64* b)
{
	INT64 t = *a;
	*a = *b;
	*b = t;
}

INT64 gcd(INT64 a, INT64 b)
{
	while (b)
	{
		a %= b;
		swap(a, b);
	}
	return a;
}

int main()
{
	printf("Pythagorean triple\nEnter three positive integer:\n");
	INT64 x = 0, y = 0, z = 0;
	int correctly_scan = 0;
	while (3 != correctly_scan || 0 >= x || 0 >= y || 0 >= z)
	{
		correctly_scan = scanf("%ld%ld%ld", &x, &y, &z);
		if (3 != correctly_scan || 0 >= x || 0 >= y || 0 >= z)
		{
			while (fgetc(stdin) != '\n')
				;
			printf("\nSomething from the input is not positive integer\nTry again:\n");
		}
	}
	INT64 min = MIN(x, MIN(y, z)), max = MAX(x, MAX(y, z)), mid = x + y + z - min - max;
	bool is_triple = (SQ(min) + SQ(mid) == SQ(max) ? true : false);
	if (is_triple && 1 == gcd(x, y) && 1 == gcd(y, z) && 1 == gcd(x, z))
		printf("This numbers are prime Pythagorean triple");
	else if (is_triple)
		printf("This numbers are Pythagorean triple");
	else
		printf("This numbers aren't Pythagorean triple");
	return 0;
}
