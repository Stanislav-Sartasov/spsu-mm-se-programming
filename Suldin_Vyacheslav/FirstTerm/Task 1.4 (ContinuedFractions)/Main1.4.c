#include <stdio.h>

int fraction(int high, int free, int i, int number, int count);

int continued_fraction();

void flush_stdin(void)
{
	char ch;
	while (scanf_s("%c", &ch) == 1 && ch != '\n')
	{
	}
}

int get_number(int* number, const int top)
{
	return !(scanf_s("%d", number) == 1 && *number < top);
}

int main()
{
	printf("<<<Description: The program represents the square root of a number in the form of a continued fraction and gives the length of the period.>>>\n\n");
	printf("You can manipulate program using 'space' ( 'number' 'answer' 'number'...)");
	printf("Enter int number\n");
	continued_fraction();
	return 0;
}

int continued_fraction(void)
{
	int i, ans = 0, n;

	while (get_number(&n, 100000000000) || n <= 1)
	{
		fprintf(stderr, "Wrong input!  ( use nubmers, > 1  < 10^10 ) \n");
		flush_stdin();
	}
	for (i = 1; ((i + 1) * (1 + i)) < n; ++i)
	{
	}
	if ((i + 1) * (i + 1) == n)
	{
		printf("Sorry, but Your number is square of %d\n", i + 1);
	}
	else
	{
		printf("[__%d__", i);
		fraction(1, 0, i, n, 0);
	}
	while (printf("Another number? ( 0 - no, 1 - yes )\n") && get_number(&ans, 10) || ans < 0 || ans > 1)
	{
		fprintf(stderr, "Wrong input!  ( use '0' or '1' ) \n");
		flush_stdin();
	}
	if (ans)
	{
		printf("Enter number\n");
		continued_fraction();
	}
	else
	{
		printf("The program has completed<<<\n\n");
		return 0;
	}
}

int fraction(int high, int free, int i, int number, int count)
{
	count++;
	int tmp, tmp2;
	tmp = (number - (i - free) * (i - free)) / high;
	tmp2 = 2 * i - free;
	for (tmp2; tmp2 - tmp >= 0;tmp2 = tmp2 - tmp)
	{
	}
	printf(", %d", (2 * i - free) / tmp);
	if (tmp == 1) // important '==1'
	{
		printf("]\n i = %d\n\n", count);
	}
	else
	{
		fraction(tmp, tmp2, i, number, count);
	}
	return 0;
}