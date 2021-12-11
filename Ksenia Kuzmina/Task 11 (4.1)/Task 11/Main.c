#include <stdio.h>
#include <stdlib.h>
#define SIZE (1 + (log(3) / log(16)) * 5000) // number of digits in the number

void multiply(int* big_int, int number, int length)
{
	int* buffer = (int*)malloc((length + 1) * sizeof(int));
	memset(buffer, 0, (length + 1) * sizeof(int));
	int remainder = 0;

	memset(buffer, 0, (length + 1) * sizeof(int));
	remainder = 0;
	for (int j = 0; j < length; j++)
	{
		buffer[j] += (big_int[j] * number + remainder) % 16;
		remainder = (big_int[j] * number + remainder) / 16;
	}

	memcpy(big_int, buffer, (length + 1) * sizeof(int));
	free(buffer);
}

int main()
{
	printf("This program counts the number 3**5000 using long arithmetic algorithms and outputs it in hexadecimal notation.\n");
	int* number = (int*)malloc(SIZE * sizeof(int));
	memset(number, 0, SIZE * sizeof(int));
	number[0] = 1;

	for (int i = 0; i < 5000; i++)
	{
		multiply(number, 3, SIZE);
	}

	printf("The number is\n");

	int flag = 0;
	for (int i = 0; i < SIZE; i++)
	{
		if (number[SIZE - i - 1] && !flag)
			flag = 1;
		if (flag)
			printf("%X", number[SIZE - i - 1]);
	}

	return 0;
}