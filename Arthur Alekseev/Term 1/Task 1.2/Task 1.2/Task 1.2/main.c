#include <stdio.h>

// The function finds greatest common divider
int gcd(int first, int second)
{
	while (first != 0 && second != 0)
	{
		if (second > first)
			second %= first;
		else
			first %= second;
	}
	return first + second;
}

// Function checks if three given numbers are coprime
int coprime(int a, int b, int c) {
	return gcd(gcd(a, b), c) == 1;
}

int my_scanf_decimal(const char* message)
{
	// Output message
	printf(message);
	int result;
	int scanf_result;
	// Endless loop awaiting user input
	while (1) {
		// Check if scanf was a success
		if (!scanf_s("%d", &result))
		{
			// Skip entire string until new line
			while (getchar() != '\n') {}
		}
		// Check if number is greater than zero
		if (result < 0) {
			printf("Number should be greater than zero and be a number\nInput again:");
			continue;
		}
		// End the loop
		break;
	}
	return result;
}

int main()
{
	int a, b, c;
	// Information output
	printf("This programs checks if 3 inputted numbers are coprime Pythagorean numbers\n");
	printf("Please, input 3 numbers\n");

	// Input
	a = my_scanf_decimal("Input first number:");
	b = my_scanf_decimal("Input second number:");
	c = my_scanf_decimal("Input third number:");

	// Pythagorean triple check
	if (a * a + b * b == c * c || a * a + c * c == b * b || c * c + b * b == a * a)
		// Coprime check
		if (coprime(a, b, c))
			printf("These numbers are coprime Pythagorean triple");
		else
			printf("These numbers are not coprime, but are Pythagorean triple");
	else
		printf("These numbers are not a Pythagorean triple");
	return 0;
}
