#include <stdio.h>
#include <stdlib.h>
#include <math.h>

void multiply(unsigned char* big_int, int number, int length)
{
	unsigned char* buffer = (unsigned char*)malloc((length + 1) * sizeof(unsigned char));
	memset(buffer, 0, (length + 1) * sizeof(unsigned char));
	int remainder = 0;
	for (int j = 0; j < length; j++)
	{
		buffer[j] += (big_int[j] * number + remainder) % 256;
		remainder = (big_int[j] * number + remainder) / 256;
	}

	memcpy(big_int, buffer, (length + 1) * sizeof(unsigned char));
	free(buffer);
}

int main()
{
	printf("This program counts the number 3**5000 using long arithmetic algorithms and outputs it in hexadecimal notation.\n");
	int size = (1 + (log(3) / log(256) * 5000));
	unsigned char* number = (unsigned char*)malloc(size * sizeof(unsigned char));
	memset(number, 0, size * sizeof(unsigned char));
	number[0] = 1;

	for (int i = 0; i < 5000; i++)
	{
		multiply(number, 3, size);
	}

	printf("The number is\n");

	int flag = 0;
	for (int i = 0; i < size; i++)
	{
		if (number[size - i - 1] && !flag)
			flag = 1;
		if (flag)
			printf("%02X", number[size - i - 1]);
	}
	return 0;
}