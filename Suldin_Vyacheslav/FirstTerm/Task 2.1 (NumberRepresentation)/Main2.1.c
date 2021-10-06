#include <stdio.h>

void my_print(int mas[], int len, int type);

int len(char str[]);

int inverted(int number);

int IEEE_754_32b(int number);

int IEEE_754_64b(int number);

int len_bin(int number);

int main()
{
	printf("Description: the program represents the number of a negative 32-bit integer, a positive single-precision floating point and a negative double-precision floating point according to the IEEE 754 standard.\n");
	int num = len("Suldin ") * len("Vyacheslav ") * len("Romanovich ");
	inverted(num);
	IEEE_754_32b(num);
	IEEE_754_64b(num);
	return 0;
}

int inverted(int num)
{
	int a, mas[32] = { 0 };
	int flag = 1;

	for (a = 31; a >= 0 || num > 0; a--)
	{
		mas[a] = num % 2;
		num /= 2;
		if (mas[a])
		{
			mas[a] = 0;
			if (flag)
			{
				mas[a] = 1;
				flag = 0;
			}
		}
		else
		{
			mas[a] = 1;
			if (flag)
			{
				mas[a] = 0;
			}
		}
	}
	my_print(mas, 32, 0);
}

int IEEE_754_32b(int num)
{
	int a, mas[32] = { 0 }, exp = len_bin(num) + 127;
	if (num > 0)
	{
		mas[0] = 0;
	}
	else
	{
		mas[0] = 1;
		num *= -1;
	}
	for (a = 8 + exp - 127; a > 8; a--)
	{
		mas[a] = num % 2;
		num /= 2;
	}
	for (a = 8; a > 0 || exp > 0; a--)
	{
		mas[a] = exp % 2;
		exp /= 2;
	}
	my_print(mas, 32, 1);
}

int IEEE_754_64b(int num)
{
	int a, mas[64] = { 0 }, exp = len_bin(num) + 1023;
	num *= -1; // Task
	if (num > 0)
	{
		mas[0] = 0;
	}
	else
	{
		mas[0] = 1;
		num *= -1;
	}
	for (a = 11 + exp - 1023; a > 11; a--)
	{
		mas[a] = num % 2;
		num /= 2;
	}
	for (a = 11; a > 0 || exp > 0; a--)
	{
		mas[a] = exp % 2;
		exp /= 2;
	}
	my_print(mas, 64, 2);
}

void my_print(int mas[], int len, int type)
{
	printf("%d - ", type);
	for (int i = 0; i < len; i++)
	{
		printf("%d", mas[i]);
		mas[i] = 0;
		if (type == 0 && (i + 1) % 4 == 0)
		{
			printf(" ");
		}
		else if (type == 1 && (i == 0 || i == 8))
		{
			printf(" ");
		}
		else if (type == 2 && (i == 0 || i == 11))
		{
			printf(" ");
		}
	}
	printf("\n");
}

int len_bin(int number)
{
	int count = 0;
	for (int i = 2; number >= i; i *= 2)
	{
		count++;
	}
	return count;
}

int len(char str[])
{
	int i;
	for (i = 0; (int)str[i + 1] != 32; i++)
	{
	}
	return i+1;
}