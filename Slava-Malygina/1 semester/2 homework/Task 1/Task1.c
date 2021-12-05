#include <stdio.h>
#include <string.h>

int module()
{
	char* name = "Slavyana";
	char* surname = "Malygina";
	char* patronymic = "Konstantinovna";
	return (strlen(name) * strlen(surname) * strlen(patronymic));
}

void bin(int length, char* str)
{
	for (int i = 31; i >= 0; i--)
	{
		str[i] = '0' + (length & 1);
		length = length >> 1;
	}
}

void rev(int length, char* str)
{
	bin(length, &str[0]);
	for (int i = 0; i < 32; i++)
	{
		printf("%c", str[i]);
	}
}

void bin2(long long length, char* str)
{
	for (int i = 63; i >= 0; i--)
	{
		str[i] = '0' + (length & 1);
		length = length >> 1;
	}
}

void rev2(long long length, char* str)
{
	bin2(length, &str[0]);
	for (int i = 0; i < 64; i++)
	{
		printf("%c", str[i]);
	}
}

int main()
{
	printf("This program multiplies the lengths of my first name, last name and patronymic and displays a binary representation of the resulting number in the specified data formats:\n");
	float module2 = module();
	double module3 = -module();
	char a[33];
	char b[65];
	a[32] = 0;
	b[64] = 0;
	printf("In negative 32-bit integer:\n");
	rev(-module(), a);
	printf("\n\n");
	printf("In a positive floating point number of unit precision according to the IEEE 754 standard:\n");
	rev(*(int*)&module2, a);
	printf("\n\n");
	printf("In negative double precision floating point number according to IEEE 754 standard:\n");
	rev2(*(long long*)&module3, b);
	printf("\n\n");
	return 0;
}