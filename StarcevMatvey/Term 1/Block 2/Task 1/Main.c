#include <stdio.h>
#include <math.h>
#define NAME "Matvey"
#define SURNAME "Starcev"
#define MIDDLE_NAME "Sergeevich"

int main()
{
	printf("Outputs the product of the lengths of the name, surname and middle name \nin binary notation of different types\n\n");

	long int number = strlen(NAME) * strlen(SURNAME) * strlen(MIDDLE_NAME); // =420

	char* bin = (char*)malloc(32 * sizeof(char));
	char* negative_bin = (char*)malloc(32 * sizeof(char));

	// bin and negative_bin code
	long int k = number;
	for (int i = 31; i > -1; i--)
	{
		if (k - pow(2, i) >= 0)
		{
			bin[i] = '1';
			negative_bin[i] = '0';
			k -= pow(2, i);
		}
		else
		{
			bin[i] = '0';
			negative_bin[i] = '1';
		}
	}

	// plus one for negative
	for (int i = 0; i < 32; i++)
	{
		if (negative_bin[i] == '0')
		{
			negative_bin[i] = '1';
			break;
		}
		else
		{
			negative_bin[i] = '0';
		}
	}

	// print negative number's code + 1
	printf("Negative int: ");
	for (int i = 31; i > -1; i--)
	{
		printf("%c", negative_bin[i]);
	}


	// order
	int n = 31;
	while (bin[n] != '1')
	{
		n--;
	}
	
	// print number in IEEE 754
	printf("\n\nFloat: ");
	int bin_n = n + 127;
	printf("0-");
	for (int i = 7; i > -1; i--)
	{
		if (bin_n - pow(2, i) >= 0)
		{
			printf("1");
			bin_n -= pow(2, i);
		}
		else
		{
			printf("0");
		}
	}
	printf("-");
	for (int i = n - 1; i > -1; i--)
	{
		printf("%c", bin[i]);
	}
	for (int i = 0; i < 23 - n; i++)
	{
		printf("0");
	}

	// print negative number in double
	printf("\n\nNegative double: ");
	bin_n = n + 1023;
	printf("1-");
	for (int i = 10; i > -1; i--)
	{
		if (bin_n - pow(2, i) >= 0)
		{
			printf("1");
			bin_n -= pow(2, i);
		}
		else
		{
			printf("0");
		}
	}
	printf("-");
	for (int i = n - 1; i > -1; i--)
	{
		printf("%c", bin[i]);
	}
	for (int i = 0; i < 52 - n; i++)
	{
		printf("0");
	}


	free(bin);
	free(negative_bin);

	return 0;
}