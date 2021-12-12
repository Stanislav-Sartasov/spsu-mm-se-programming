#include "myPow.h"

void printNumber(hex result)
{
	for (int i = result.startNumber; i < result.sizeNumber; i++)
	{
		printf("%c", (int)(result.number[i] >= 10) * 7 + result.number[i] + 48);
	}
}

int myLog16(int n)
{
	int res = 0;
	for (int counter = 1; n / counter > 0; counter *= 16)
	{
		res++;
	}
	return res;
}

hex myHexPow(int numb, int exp)
{
	hex result;
	result.sizeNumber = exp * myLog16(numb);
	result.startNumber = result.sizeNumber - 1;
	result.number = malloc(sizeof(char) * result.sizeNumber);
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
			int tmp = (numb * result.number[j] + cur) / 16;
			result.number[j] = (numb * result.number[j] + cur) % 16;
			cur = tmp;
			if (result.number[j] != 0) result.startNumber = min(result.startNumber, j);
		}
	}
	return result;
}