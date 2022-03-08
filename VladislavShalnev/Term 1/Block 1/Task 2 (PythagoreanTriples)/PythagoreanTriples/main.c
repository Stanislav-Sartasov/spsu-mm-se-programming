
#include <stdio.h>

int is_pythagorean_triple(int x, int y, int z)
{
	return (x * x + y * y == z * z) || (x * x + z * z == y * y) || (y * y + z * z == x * x);
}

int is_coprime(int x, int y, int z)
{
	return gcd(x, gcd(y, z)) == 1;
}

int gcd(int firstNumber, int secondNumber)
{
	while (firstNumber != 0 && secondNumber != 0)
	{
		if (firstNumber > secondNumber) firstNumber %= secondNumber;
		else secondNumber %= firstNumber;
	}
	return firstNumber + secondNumber;
}

void input(int *adress, char *message)
{
	while (1)
	{
		char input[256];

		printf(message);
		fgets(input, sizeof(input), stdin);

		if (!sscanf_s(input, "%d", adress))
		{
			printf("Inputed value is not a natural number.\n");
			continue;
		}
		if (*adress <= 0)
		{
			printf("Inputed number can't be less than zero or equal to zero.\n");
			continue;
		}
		return;
	}
	
}

int main()
{
	int x, y, z;
	printf("This program checks if entered numbers triple is a pythagorean triple and prime pythagorean triple.\n\n");
	printf("Enter natural numbers triple x y z:\n\n");
	input(&x, "Enter first number: ");
	input(&y, "Enter second number: ");
	input(&z, "Enter third number: ");
	printf("\n"); // separation between input and answer

	if (is_pythagorean_triple(x, y, z))
	{
		printf("Triple %d %d %d is a pythagorean triple", x, y, z);
		if (is_coprime(x, y, z))
		{
			printf(" and a prime pythagorean triple.\n");
		}
		else
		{
			printf(" and not a prime pythagorean triple.\n");
		}
	}
	else
	{
		printf("Triple %d %d %d is not a pythagorean triple.\n", x, y, z);
	}

	return 0;
}