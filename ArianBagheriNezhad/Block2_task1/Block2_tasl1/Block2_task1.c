//Homework 2.1
//Representation of numbers

#include <stdio.h>
#include <string.h>

#define NAME "Arian"
#define SURNAME "Bagheri"
#define PATRONYMIC "Hezhad"
#define LONG 32
#define LLONG 64

void print_int(int mult);
void print_float(float mult);
void print_double(double mult);

int main()
{
	int Multiplication, mult_a;
	float mult_b;
	double mult_c;

	printf("\nCalculate the product of the lengths of your first name, last name and patronymic\n");
	printf("Display the binary representation of the following quantities in the specified data formats:\n\n");

	Multiplication = strlen(NAME) * strlen(SURNAME) * strlen(PATRONYMIC);


	//A.

	mult_a = Multiplication;
	printf("\nA. negative 32-bit integer, the modulus of which is equal to the found product.\n");
	printf("%d: ", -mult_a);
	print_int(-mult_a);

	//B.
	mult_b = Multiplication;
	printf("\nB. a positive floating point number of single precision according to the IEEE 754\n");
	printf("standard, the modulus of which is equal to the found product.\n");
	printf("%f: ", mult_b);
	print_float(mult_b);

	//C.
	mult_c = Multiplication;
	printf("\nC. a negative double-precision floating-point number according to the IEEE 754\n");
	printf("standard, the modulus of which is equal to the found product.\n");
	printf("%lf: ", -mult_c);
	print_double(-mult_c);

	return 0;
}

void print_int(int mult)
{
	int i, k;

	for (i = LONG - 1; i >= 0; i--)
	{
		k = mult >> i;

		if (k & 1)
		{
			printf("1");
		}
		else
		{
			printf("0");
		}
	}

	printf("\n");
}

void print_float(float mult)
{
	int i, k;
	int mult_n = *(int*)&mult;

	for (i = LONG - 1; i >= 0; i--)
	{
		k = mult_n >> i;

		if (k & 1)
		{
			printf("1");
		}
		else
		{
			printf("0");
		}
	}

	printf("\n");
}

void print_double(double mult)
{
	int i, k;
	long long mult_nn = *(long long*)&mult;

	for (i = LLONG - 1; i >= 0; i--)
	{
		k = mult_nn >> i;

		if (k & 1)
		{
			printf("1");
		}
		else
		{
			printf("0");
		}
	}

	printf("\n");
}