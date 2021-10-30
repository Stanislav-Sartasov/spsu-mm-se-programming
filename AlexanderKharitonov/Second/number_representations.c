#include <stdio.h>
#include <string.h>

#define NAME strlen("Харитонов") * strlen("Александр") * strlen("Алексеевич")



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

int main()
{
	printf("The program counts the product of the lengths of the surname, name and patronymic\nand gives its representation in negative binomial 32 bytes system,\npositive Single Precision IEEE 754 Floating-Point Standard\nand negative Double Precision IEEE 754 Floating-Point Standard.\n\n");
	char negative_32_representation[33] = "\0";
	char positive_single_representation[33] = "\0";
	char negative_double_representation[65] = "\0";
	int int_product = NAME;
	float float_product =NAME;
	double double_product = NAME;
	binSystem(&negative_32_representation, 31, -float_product);
	float_product = *(long int*)&float_product;
	binSystem(&positive_single_representation, 31, (long int)float_product);
	double_product = -double_product;
	long long number = *(long long*)&double_product;
	binSystem(&negative_double_representation, 63, number);
	printf("The product of the lengths of my surname, name and patronymic is %d.\n\n", int_product);
	printf("The negative binomial representation of the product in 32 bytes is %s.\n\n", negative_32_representation);
	printf("The negative binomial representation of the product in Double Precision IEEE 754 Floating-Point Standard is\n%s.\n", positive_single_representation);
	printf("The positive binomial representation of the product in Single Precision IEEE 754 Floating-Point Standard is\n%s.\n\n", negative_double_representation);
	return 0;
}