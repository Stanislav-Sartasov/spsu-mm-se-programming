#define _CRT_SECURE_NO_WARNINGS
#define FALSE 0
#define TRUE 1

#include <stdio.h>
#include <math.h>
#include <string.h>

_Bool check_is_sqrt(long long int a)
{
	return (long long int)sqrt(a) == sqrt(a) ? FALSE : TRUE;
}

_Bool check_char(char* a_input)
{
	for (int i = 0; i < strlen(a_input); i++)
	{
		if (!(a_input[i] >= '0' && a_input[i] <= '9'))
			return FALSE;
	}
	return TRUE;
}

void check_input(char* a_input, long long int* a)
{
	while (TRUE)
	{
		if (!check_char(a_input) || atoll(a_input) <= 0)
		{
			printf("You must enter a natural integer, please repeat the input:\n");
			gets(a_input);
			continue;
		}
		else if (!check_is_sqrt(atoll(a_input)))
		{
			printf("The entered number is the square of another, please repeat the input:\n");
			gets(a_input);
			continue;
		}
		break;
	}
	*a = atoll(a_input);
}

int main()
{
	char a_input[256];
	long long int a, int_sqrt_a, func_output, period = 0, divided, denominator;
	printf("The program outputs the chain fraction of the root of the entered number.\n\n");
	printf("Enter a number:\n");
	gets(a_input);
	check_input(&a_input, &a);

	int_sqrt_a = sqrt(a);
	func_output = int_sqrt_a;
	divided = 0;
	denominator = 1;
	printf("\nContinued fraction: [%lld; ", int_sqrt_a);

	do
	{
		divided = func_output * denominator - divided;
		denominator = (a - divided * divided) / denominator;
		func_output = (int_sqrt_a + divided) / denominator;
		printf("%lld; ", func_output);
		period++;
	} while (denominator != 1);

	printf("\b\b]\n");
	printf("The period of this continued fraction is %lld\n", period);

	return 0;
}