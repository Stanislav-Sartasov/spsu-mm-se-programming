#include <stdio.h>

int str_len(char* str)
{
	int i = 0;
	while (*(str + i) != '\0')
		i++;
	return i;
}

void binary(long long n, int size)
{
	for (int i = 0; i < size; i++)
		printf("%lld", (n >> (size-i-1)) & 1);
	printf("\n");
}

int main()
{
	char name[] = "Anton", surname[] = "Kraev", patronymic[] = "Ivanovich";
	int product_d = str_len(name) * str_len(surname) * str_len(patronymic);
	float product_f = (float)product_d;
	double product_lf = (double)-product_d;
	
	printf("This program counts the product of lengths of my first name, last name and patronymic.\n"
		"Then prints the binary representation of found product in following formats:\n\n");
	printf("Negative 32-bit integer:\n");
	binary(-product_d, 32);
	printf("Positive unit precision floating point number according to the IEEE 754 standard:\n");
	binary(*(int*)&product_f, 32);
	printf("Negative double precision floating point number according to the IEEE 754 standard:\n");
	binary(*(long long*)&product_lf, 64);
	
	return 0;
}