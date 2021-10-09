#include <stdio.h>
#include <math.h>

int get_number()
{
	printf(">>> ");
	int num;
	char end;
	int read_result = scanf("%d%c", &num, &end);
	if ((read_result == 2) && (end == '\n'))
    {
		if (num > 0)
        {
			if (pow((int)sqrt(num), 2) != num)
			{
				return num;
			}
			printf("Number must not be square of other natural number\n");
		}
		else
		{
			printf("Number must be natural (positive integer)\n");
		}
	}
	else
	{
		printf("Please enter correct natural number\n");
	}

	while (end != '\n')
    {
		scanf("%c", &end);
	}

	return get_number();
}

int main()
{
	printf("This program displays the coefficients of the continued fraction and its period for square root of given number\n");
	printf("Enter natural number (not square of any other one)\n");
	int num = get_number();
	double num_root = sqrt(num);
	int int_part = (int)num_root;
	int remaining = 0;
	int div = 1;
	int period = 0;
	printf("a0: %d\na1-ai: [ ", int_part);
	do
    {
		period++;
		remaining = int_part * div - remaining;
		div = (num - pow(remaining, 2)) / div;
		int_part = (num_root + remaining) / div;
		printf("%d ", int_part);

	}
	while (div != 1);
	printf("]\nPeriod is %d", period);
	return 0;
}
