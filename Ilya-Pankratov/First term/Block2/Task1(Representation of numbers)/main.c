#include <stdio.h>

int negative32(int number);

int float32(int number);

int double64(int number);

int countMantissa(int number, int size);

int len(char* string);

int myPrint(int* arr, int size);

int main()
{
	printf("This program calculates the product of the first name, last name and patronymic and represents it in the form:\n"
		"A) a negative 32-bit integer, the modulus of which is equal to the found product\n"
		"B) a positive floating point number of single precision according to the IEEE 754 standard\n"
		"C) a negative double-precision floating-point number according to the IEEE 754 standard\n\n");

	int product = len("Pankratov") * len("Ilya") * len("Alexeyevich");
	printf("The product of the first name, last name and patronymic: %d\n", product);
	negative32(product);
	float32(product);
	double64(-product);
	return 0;
}

int negative32(int number)
{
	int bytes[32] = { 0 };
	printf("A: ");

	for (int i = 31; i >= 0; i--)
	{
		if (i == 31)
			bytes[i] = !(number % 2) + 1;
		else
		{
			bytes[i] = !(number % 2) + (bytes[i + 1] == 2);
			bytes[i + 1] = bytes[i + 1] == 2 ? 0 : bytes[i + 1];
		}
		number /= 2;
	}

	for (int i = 0; i < 32; i++)
		printf("%d", bytes[i]);
	printf("\n");
	return 0;
}

int float32(int number)
{
	printf("B: ");
	int bytes[32] = { 0 };

	if (number < 0)
	{
		bytes[0] = 1;
		number = -number;
	}

	int mantissa = countMantissa(number, 23);
	int exp = 127 + mantissa;

	for (int i = 8 + mantissa; i > 8; i--)
	{
		bytes[i] = number % 2;
		number /= 2;
	}

	for (int i = 8; i > 0; i--)
	{
		bytes[i] = exp % 2;
		exp /= 2;
	}
	myPrint(&bytes, 32);
}

int double64(int number)
{
	printf("C: ");
	int bytes[64] = { 0 };

	if (number < 0)
	{
		bytes[0] = 1;
		number = -number;
	}

	int mantissa = countMantissa(number, 52);
	int exp = 1023 + mantissa;

	for (int i = 11 + mantissa; i > 11; i--)
	{
		bytes[i] = number % 2;
		number /= 2;
	}

	for (int i = 11; i > 0; i--)
	{
		bytes[i] = exp % 2;
		exp /= 2;
	}
	myPrint(&bytes, 64);
}

int len(char* string)
{
	int lenght = 0;
	while (string[lenght] != '\0')
		lenght += 1;
	return lenght;
}

int countMantissa(int number, int size)
{
	int result = -1;
	while (number > 0)
	{
		result += 1;
		number /= 2;
		if (result == size)
			break;
	}
	return result;
}

int myPrint(int* arr, int size)
{
	printf("%d\t", arr[0]);
	for (int i = 1; i < size; i++)
	{
		printf("%d", arr[i]);
		if (size == 64 && i == 11)
			printf("\t");
		if (size == 32 && i == 8)
			printf("\t");
	}
	printf("\n");
}