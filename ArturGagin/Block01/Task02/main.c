#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <ctype.h>

int isTrash(const char *str)
{
	for (int i = 0; i < strlen(str) - 1 ; i++)
	{
		if (!isdigit(str[i]) || str[0] == '0' || i > 8)
			return 1;
	}
	return 0;
}

int isPythagoreanTriple(long long a, long long b, long long c)
{
	if (a * a + b * b == c * c || b * b + c * c == a * a || a * a + c * c == b * b)
		return 1;
	else
		return 0;
}

int isCoprime(long long number_1, long long number_2)
{
	if (number_1 == number_2)
		return number_1 == 1;
	else
	{
		if (number_1 > number_2)
			return isCoprime(number_1 - number_2, number_2);
		else
			return isCoprime(number_2 - number_1, number_1);
	}
}

int main()
{
	const short MaxLenOfStr = 50;
	long long number_1 = 0, number_2 = 0, number_3 = 0;
	char *str = (char *) malloc(sizeof(char) * MaxLenOfStr);
	short counter = 1;
	printf("This program checks whether three numbers are a Pythagorean triple.\n");
	printf("And if they're, the program can also check whether the numbers are mutually prime.\n");
	printf("Please, enter only natural numbers which will not cause an overflow.\n");
	do
	{
		printf("Enter a %d number:", counter);
		fgets(str, MaxLenOfStr, stdin);
		if (isTrash(str) || strtod(str, 0) == 0)
			printf("Error! Enter a natural number which won't cause an overflow (less than a billion).\n");
		else
		{
			if (counter == 1)
				number_1 = strtoll(str, 0, 10);
			if (counter == 2)
				number_2 = strtoll(str, 0, 10);
			if (counter == 3)
				number_3 = strtoll(str, 0, 10);
			counter += 1;
		}

	}
	while (counter != 4);

	if (isPythagoreanTriple(number_1, number_2, number_3))
	{
		if (isCoprime(number_1, number_2) && isCoprime(number_2, number_3) && isCoprime(number_1, number_3))
			printf("This is a mutually simple Pythagorean triple.");
		else
			printf("This is a Pythagorean triple, but not mutually simple.");
	}
	else
		printf("This isn't a Pythagorean triple.");
	return 0;
}