#include <stdio.h>
#include <math.h>

#define NAME "Manuchehr"
#define SURNAME "Abdurazikov"
#define MIDDLE_NAME "Zarifovich"

void bin(long long num, int bits, char* binNum);

int main()
{

	printf("This program displays different representations of number 504,\n");
	printf("which is a product of lengths of words Denisov Alexey Gennadievich\n\n");

	const int x = 504;

	int a = -x; char binA[32] = { 0 };
	float b = x; char binB[32] = { 0 };
	double c = -x; char binC[64] = { 0 };

	printf("Negative 32-bit integer representation is ");
	bin(a, 32, &binA);

	printf("Positive float IEEE 754 representation is ");
	bin(*(int*)&b, 32, &binB);

	printf("Negative double IEEE 754 representation is ");
	bin(*(long long*)&b, 64, &binC);

	return 0;

}

void bin(long long num, int bits, char* binNum)
{
	if (num > 0)
	{
		for (int i = bits - 1; i >= 0; i--)
		{
			binNum[i] = num % 2 ? '1' : '0';
			num /= 2;
		}
	}
	else
	{
		for (int i = bits - 1; i >= 0; i--)
		{
			binNum[i] = num % 2 ? '1' : '0';
			num /= 2;
		}
		for (int i = 0; i < bits; i++)
		{
			if (binNum[i] == '1')
				binNum[i] = '0';
			else
				binNum[i] = '1';
		}
		for (int i = bits - 1; i >= 0; i--)
		{
			if (binNum[i] == '1')
			{
				while (binNum[i] == '1')
				{
					binNum[i] = '0';
					i--;
				}
				binNum[i] = '1';
				break;
			}
		}
	}

	for (int i = 0; i < bits; i++)
	{
		printf("%c", binNum[i]);
	}
	printf("\n");
}