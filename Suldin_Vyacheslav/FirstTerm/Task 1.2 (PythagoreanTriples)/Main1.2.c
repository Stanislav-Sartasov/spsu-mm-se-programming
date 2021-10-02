#include <stdio.h>
#include <math.h>

int pytho_triple();

void flush_stdin(void)
{
	char ch;
	while (scanf_s("%c", &ch) == 1 && ch != '\n')
	{
	}
}

int get_number(int* number, const int top)
{
	return !(scanf_s("%d", number) == 1 && abs(*number) < top);
}

int main()
{
	printf("<<<Description: enter 3 int numbers. Program will say if they Pythogorean triples (and primitive) or not.>>>\n\n");
	printf("You can manipulate program using 'space' ( 'number' 'number' 'number' 'ans' 'number' 'number' 'number' 'ans' ...)\n");
	printf("Enter numbers ( 1 by 1 or 3 together )\n\n");
	pytho_triple();
	return 0;
}

int pytho_triple(void)
{
	int n, i, k[3], j, sorting = 1, tmp, ans;

	for (i = 0; i < 3; i++)
	{
		while (get_number(&n, 100000000) || n <= 0 )
		{
			fprintf(stderr, "Wrong input!  ( use int nubmers, > 0 and < 10^8 )  numbers remaining: %d \n", 3 - i);
			flush_stdin();
		}
		k[i] = n;
	}
	while (sorting)
	{
		sorting = 0;
		for (i = 0; i < 2; ++i)
			if (k[i] > k[i + 1])
			{
				tmp = k[i];
				k[i] = k[i + 1];
				k[i + 1] = tmp;
				sorting = 1;
			}
	}
	if (k[0] * k[0] + k[1] * k[1] - k[2] * k[2])
	{
		printf("Not Pythagorean triple");
	}
	else if ((k[0] + k[1]) % 2)
	{
		for (j = 3; j < pow(k[0], 0.5) + 2; j = j + 2)
		{
			if (k[0] % j == 0 && k[1] % j == 0)
			{
				printf("Pythagorean triple, but not primitive: (%d)\n", j);
				j = 0;
				break;
			}
		}
		if (j != 0)
		{
			printf("Primitive Pythagorean triple");
		}
	}
	else
	{
		printf("Pythagorean triple, but not primitive: (2)");
	}
	while (printf("\nAnother triple? (0 - no, 1 - yes)") && get_number(&ans, 10) || ans < 0 || ans > 1)
	{
		fprintf(stderr, "Wrong input! ( use '0' or '1' )\n");
		flush_stdin();
	}
	if (ans)
	{
		printf("\nEnter numbers\n");
		pytho_triple();
	}
	else
	{
		printf("The program has completed<<<");
		return 0;
	}
}