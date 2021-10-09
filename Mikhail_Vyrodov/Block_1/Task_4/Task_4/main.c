#include <stdio.h>
#include <math.h>

void clear_input()
{
	char step;
	step = '0';
	while (step != '\n' && step != EOF)
	{
		step = getchar();
	}
}

void get_number(long long* x)
{
	printf("Enter 1 natural number. It must not be a perfect square\n");
	while (1)
	{
		if (scanf_s("%lld", x))
		{
			if (*x <= 0)
			{
				printf("This number is less than one. ");
			}
			else if (sqrt(*x) - (long long)(sqrt(*x)) < 0.000001)
			{
				printf("This number is a perfect square. ");
			}
			else if (getchar() == '\n')
			{
				break;
			}
		}
		printf("Input was incorrect, please try again:\n");
		clear_input();
	}
}

int main()
{
	long long x;
	get_number(&x);
	long long divider = 1, first_element, substract, element = 1;
	first_element = (long long) sqrt(x);
	substract = first_element;
	printf("[%lld", first_element);
	int period = 0;
	do
	{
		divider = (x - substract * substract) / divider;
		element = (first_element + substract) / divider;
		substract = element * divider - substract;
		period += 1;
		printf(", %lld", element);
	} 
	while (divider != 1);
	printf("] Period is: %d", period);
	return 0;
}