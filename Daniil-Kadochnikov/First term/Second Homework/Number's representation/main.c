#include <stdio.h>
#include <string.h>

#define SURNAME "Kadochnikov"
#define NAME "Daniil"
#define PATRONYMIC "Aleksandrovich"
#define PRODUCT strlen(SURNAME) * strlen(NAME) * strlen(PATRONYMIC)



void binSystem(char* binRep, int size, long long product)
{
	while (size >= 0)
	{
		if (product & 1) binRep[size] = '1';
		else binRep[size] = '0';
		product = product >> 1;
		size--;
	}
}

void negative32Bytes()
{
	char binRep[33] = "\0";
	int product = PRODUCT;
	binSystem(&binRep, 31, -product);
	printf("The negative binomial representation of the product in 32 bytes is %s.\n\n", binRep);
}

void positiveSingleIEEE754()
{
	char binRep[33] = "\0";
	float product = PRODUCT;
	product = *(long int*)&product;
	binSystem(&binRep, 31, (long int)product);
	printf("The positive binomial representation of the product in Single Precision IEEE 754 Floating-Point Standart is\n%s.\n\n", binRep);
}

void negativeDoubleIEEE754()
{
	char binRep[65] = "\0";
	double product = PRODUCT;
	product = -product;
	long long number = *(long long*)&product;
	binSystem(&binRep, 63, number);
	printf("The negative binomial representation of the product in Double Precision IEEE 754 Floating-Point Standart is\n%s.\n", binRep);
}

int main()
{
	printf("The programm counts the product of the legthes of the surname, name and patronymic\nand gives its representation in negative binomial 32 bytes system,\npositive Single Precision IEEE 754 Floating-Point Standart\nand negative Double Precision IEEE 754 Floating-Point Standart.\n\n");
	printf("The product of the lengthes of my surname, name and patronymic is %d.\n\n", PRODUCT);
	negative32Bytes();
	positiveSingleIEEE754();
	negativeDoubleIEEE754();
	return 0;
}