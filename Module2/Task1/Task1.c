#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>

void translete(unsigned long long int num, int length)
{
	unsigned long long int* array = (unsigned long long int*)malloc(length * sizeof(unsigned long long int));

	if (NULL == array)
	{
		printf("Неудачная попытка выделение паяти для массива");
		exit(1);
	}
	int i = 0;

	for (i = 0; i < length - 1; i++)
	{
		array[i] = num % 2;
		num = num / 2;
	}
	array[i] = (num % 2);

	for (i; i >= 0; i--)
	{
		printf("%llu", array[i]);
	}

	free(array);


}

int main()
{

	system("chcp 1251");
	system("cls");

	int long_name_int = -336;
	float long_name_float = 336;
	double long_name_double = -336;

	unsigned int* a = (unsigned int*)&(long_name_int);
	unsigned int* b = (unsigned int*)&long_name_float;
	unsigned long long int* c = (unsigned long long int*) & long_name_double;

	printf("Произведение длин ФИО = 336");

	printf("\n Решение А: ");
	translete(*a, 32);

	printf("\n Решение B: ");
	translete(*b, 32);

	printf("\n Решение С: ");
	translete(*c, 64);

	return 0;
}
