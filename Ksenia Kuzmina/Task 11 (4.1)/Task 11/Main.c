#include <stdio.h>
#include <stdlib.h>
#include <math.h>

void multiply(char* big_int, int number, int length)
{
	char* buffer = (char*)malloc((length + 1) * sizeof(char));
	memset(buffer, 0, (length + 1) * sizeof(char));
	int remainder = 0;
	for (int j = 0; j < length; j++)
	{
		buffer[j] += (big_int[j] * number + remainder) % 16;
		remainder = (big_int[j] * number + remainder) / 16;
	}

	memcpy(big_int, buffer, (length + 1) * sizeof(char));
	free(buffer);
}

int main()
{
	printf("This program counts the number 3**5000 using long arithmetic algorithms and outputs it in hexadecimal notation.\n");
	int size = (1 + (log(3) / log(16) * 5000));
	char* number = (char*)malloc(size * sizeof(char));
	memset(number, 0, size * sizeof(char));
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
			printf("%X", number[size - i - 1]);
	}

	return 0;
}
