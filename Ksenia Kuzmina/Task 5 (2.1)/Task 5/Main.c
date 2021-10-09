#include <stdio.h>
#include <string.h>

void bin(long long len, char* binary, int size)
{
	for (int i = size - 1; i >= 0; i--)
	{
		binary[i] = (len & 1);
		len = len >> 1;
	}
	for (int i = 0; i < size; i++)
	{
		printf("%d", binary[i]);
	}
	printf("\n\n");
}

int main()
{
	printf("This program calculates the product of the lengths of my first name, last name and patronymic\n");
	printf("and presents it in the format of a binary number.\n\n");

	char* name = "Ksenia";
	char* surname = "Kuzmina";
	char* patronymic = "Antonovna";

	printf("My name is Ksenia. The length of the name is %d\n", strlen(name));
	printf("My surname is Kusmina. The length of the surname is %d\n", strlen(surname));
	printf("My patronymic is Antonovna. The length of the patronymic is %d\n\n", strlen(patronymic));

	int len = strlen(name) * strlen(surname) * strlen(patronymic);

	printf("The product of the lengts is %d\n\n", len);

	char a[32];
	char b[32];
	char c[64];

	int len1 = -len;
	float len2 = len;
	double len3 = -len;

	int len4 = *(int*)&len2;
	long long len5 = *(long long*)&len3;

	printf("The number %d as a negative 32-bit integer: ", len);
	bin(len1, &a, 32);
	printf("The number %d as single precision positive floating point number according to IEEE 754 standard: \n", len);
	bin(len4, &b, 32);
	printf("The number %d as a negative double precision floating point number according to the IEEE 754 standard \n", len);
	bin(len5, &c, 64);

	return 0;
}