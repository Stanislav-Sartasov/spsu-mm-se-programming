#include "myPow.h"

void printHexNumber(base256 result)
{
	if (result.number[result.startNumber] / 16 == 0)
	{
		printf("%c", (int)(result.number[result.startNumber] % 16 >= 10) * 7 + result.number[result.startNumber] % 16 + 48);
	}
	else
	{
		printf("%c", (int)(result.number[result.startNumber] / 16 >= 10) * 7 + result.number[result.startNumber] / 16 + 48);
		printf("%c", (int)(result.number[result.startNumber] % 16 >= 10) * 7 + result.number[result.startNumber] % 16 + 48);
	}
	for (int i = result.startNumber + 1; i < result.sizeNumber; i++)
	{
		printf("%c", (int)(result.number[i] / 16 >= 10) * 7 + result.number[i] / 16 + 48);
		printf("%c", (int)(result.number[i] % 16 >= 10) * 7 + result.number[i] % 16 + 48);
	}
	free(result.number);
}

base256 my256NumPow(int numb, int exp)
{
	base256 result;
	result.sizeNumber = (int)floor(exp * (log(numb) / log(16))) + 1;
	result.startNumber = result.sizeNumber - 1;
	result.number = malloc(sizeof(unsigned char) * result.sizeNumber);
	for (int i = 0; i < result.sizeNumber; i++)
	{
		result.number[i] = 0;
	}
	result.number[result.sizeNumber - 1] = 1;
	for (int i = 0; i < exp; i++)
	{
		int cur = 0;
		for (int j = result.sizeNumber - 1; j >= 0; j--)
		{
			int tmp = (numb * result.number[j] + cur) / 256;
			result.number[j] = (numb * result.number[j] + cur) % 256;
			cur = tmp;
			if (result.number[j] != 0) result.startNumber = min(result.startNumber, j);
		}
	}
	return result;
}