#include <stdio.h>
#include <math.h>


long long gcd(long long a, long long b) // Euclidean algorithm
{
	for (int c;b;)
	{
		c = a % b;
		a = b;
		b = c;
	}
	if (a)
	{
		return a;
	}
	else
	{
		return b;
	}
}

int pythagoreanTriple(long long a, long long b, long long c) // hypotenuse test
{
	if (a >= b && a >= c && (b * b + c * c == a * a))
	{
		return 1;
	}
	if (b >= a && b >= c && (a * a + c * c == b * b))
	{
		return 1;
	}
	if (c >= b && c >= a && (b * b + a * a == c * c))
	{
		return 1;
	}
	return 0;
}

int primeTriple(long long a, long long b, long long c)
{
	if (gcd(a, b) == 1 && gcd(b, c) == 1 && gcd(a, c) == 1)
	{
		return 1;
	}
	else
	{
		return 0;
	}
}

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
	printf("In this program, you need to enter three natural numbers and check if they are a Pythagorean triple, if so.\n");
	printf("Check if they are a simple Pythagorean triple\n\n");

	printf("Input the first natural number:\n");
	long long firstNum = getInputNum();
	
	printf("Input the second natural number:\n");
	long long secondNum = getInputNum();
	
	printf("Input the third natural number:\n");
	long long thirdNum = getInputNum();

	if (pythagoreanTriple(firstNum, secondNum, thirdNum))
	{
		printf("This is the pythagorean triple!\n");
		if (primeTriple(firstNum, secondNum, thirdNum))
		{
			printf("Also it's prime pythagorean triple.\n");
		}
		else
		{
			printf("Also it's not prime pythagorean triple.\n");
		}
	}
	else
	{
		printf("This is not the pythagorean triple.\n");
	}
}