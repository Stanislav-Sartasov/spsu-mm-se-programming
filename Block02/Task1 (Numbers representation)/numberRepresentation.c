#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>


int lenString(char* message)
{
	int number = 0;
	while (message[number])
	{
		++number;
	}
	return number;
}

union
{
	double db;
	float fl;
	int in[2];
} uniVar;


int dbToInt(int number)
{
	uniVar.db = number;
	int in = uniVar.in[0];
	return in;
}

int flToInt(int number)
{
	uniVar.fl = number;
	int in = uniVar.in[0];
	return in;
}

void decToBin(int number, char type)
{
	if (type == sizeof(double))
	{
		int digits[64];
		int mask = 1;

		for (int i = 1; i >= 0; --i)
		{
			for (int j = 0; j < 8 * sizeof(int); ++j)
			{
				if (uniVar.in[i] & mask)
				{
					if (i == 1)
					{
						digits[j + 32] = 1;
					}
					else
					{
						digits[j] = 1;
					}
				}
				else
				{
					if (i == 1)
					{
						digits[j + 32] = 0;
					}
					else
					{
						digits[j] = 0;
					}
				}
				mask <<= 1;
			}
		}
		for (int i = 63; i >= 0; --i)
		{
			printf("%d", digits[i]);
		}
	}
	else
	{
		int digits[32];
		int mask = 1;
		for (int k = 0; k < 8 * type; ++k)
		{
			if (number & mask)
			{
				digits[k] = 1;
			}
			else
			{
				digits[k] = 0;
			}
			mask <<= 1;
		}
		for (int i = 31; i >= 0; --i)
		{
			printf("%d", digits[i]);
		}
	}
}

int main()
{
	printf("The program shows a binary representation of decimal number in different formats.\n");

	const char* fstName = "Anatoliy";
	const char* famName = "Kim";
	const char* patName = "Aleksandrovich";
	int prodOfNum = lenString(fstName) * lenString(famName) * lenString(patName); // = 336
	printf("The given decimal number is: %d\n", prodOfNum);

	printf("Negative integer represent:	");
	decToBin(prodOfNum * -1, sizeof(int));				// Negative
	printf("\n");
	printf("Positive float represent:	");
	decToBin(flToInt(prodOfNum), sizeof(float));		// Float IEEE-754
	printf("\n");
	printf("Negative double represent:	");
	decToBin(dbToInt(prodOfNum * -1), sizeof(double));	// Negative Double IEEE-754
	
	return 0;
}