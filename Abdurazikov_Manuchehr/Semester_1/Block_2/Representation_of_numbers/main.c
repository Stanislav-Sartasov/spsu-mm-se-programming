#include <stdio.h>
#include <math.h>

#define NAME "Manuchehr"
#define SURNAME "Abdurazikov"
#define MIDDLE_NAME "Zarifovich"

void bin(long long dig, int bits, char* Num)
{
	if (dig > 0)
	{
		for (int i = bits - 1; i >= 0; i--)
		{
			Num[i] = dig % 2 ? '1' : '0';
			dig /= 2;
		}
	}
	else
	{
		for (int i = bits - 1; i >= 0; i--)
		{
			Num[i] = dig % 2 ? '1' : '0';
			dig /= 2;
		}
		for (int i = 0; i < bits; i++)
		{
			if (Num[i] == '1')
				Num[i] = '0';
			else
				Num[i] = '1';
		}
		for (int i = bits - 1; i >= 0; i--)
		{
			if (Num[i] == '1')
			{
				while (Num[i] == '1')
				{
					Num[i] = '0';
					i--;
				}
				Num[i] = '1';
				break;
			}
		}
	}

	for (int i = 0; i < bits; i++)
	{
		printf("%c", Num[i]);
	}
	printf("\n");
}

int main()
{

	printf("This program displays different representations of number 990,\n");
	printf("which is a product of lengths of words 'Abdurazikov Manuchehr Zarifovich'\n\n");

	const int x = 990;
	int a;
	float b;
	double c;
	char mas1[32] = { 0 };
	char mas2[32] = { 0 };
	char mas3[64] = { 0 };

	a = -x;
	b = x;
	c = -x;

	printf(" Negative 32-bit integer representation is ");
	bin(a, 32, &mas1);

	printf(" Positive float IEEE 754 representation is ");
	bin(*(int*)&b, 32, &mas2);

	printf("Negative double IEEE 754 representation is ");
	bin(*(long long*)&b, 64, &mas3);

	return 0;
}