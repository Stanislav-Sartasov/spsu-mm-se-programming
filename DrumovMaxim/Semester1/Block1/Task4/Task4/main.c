#include<stdio.h>
#include<math.h>

void cleanInput()
{
	char s;
	do
	{
		s = getchar();
	} while (s != '\n' && s != EOF);
}

int correctInput(long long* number)
{
	return (scanf("%10lld", number) && *number >= 1);
}

long long getInputNum()
{
	long long number = 0;
	while (!(correctInput(&number) && getchar() == '\n'))
	{
		cleanInput();
		printf("Your input is not correct! Try again:\n");
	}
	return number;
}

int main()
{
	printf("This program displays the period and sequence for a user-entered number that is not a square integer.\n");
	printf("Input natural number: \n");

	long long number = getInputNum();
	unsigned int sqrtNum = floor(sqrt(number));

	if (number == sqrtNum * sqrtNum)
	{
		printf("That's square! Input again: \n");
		number = getInputNum();
	}

	printf("[ %u;", sqrtNum);

	int value = sqrtNum;
	int remainder = 0;
	int denominator = 1;
	int counter = 0;

	do
	{
		remainder = value * denominator - remainder;
		denominator = (number - remainder * remainder) / denominator;
		value = (sqrtNum + remainder) / denominator;
		printf("  %u", value);
		++counter;
	} while (denominator != 1);

	printf(" ] \n Period is: %d", counter);
}