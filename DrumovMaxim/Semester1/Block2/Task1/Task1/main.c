#include<stdio.h>
#include<math.h>
#include<stdlib.h>

int getLength()
{
	char name[] = {'M', 'a', 'k', 's', 'i', 'm'};
	char surname[] = {'D', 'r', 'u', 'm', 'o', 'v'};
	char patronymic[] = {'A', 'n', 'd', 'r', 'e', 'e', 'v', 'i', 'c', 'h'};
	return (sizeof(name) * sizeof(surname) * sizeof(patronymic));
}

int binNegInt32(int* binNum, int length, int* counter)
{
	for (int i = 31; i >= 0; i--)
	{
		binNum[i] = length % 2;
		length = length / 2;
		if (length != 0)
		{
			(*counter)++;
		} 
	}
	for (int i = 0; i <= 31; i++)
	{
		if (binNum[i] == 0)
		{
			binNum[i] = 1;
		}
		else 
		{
			binNum[i] = 0;
		}
	}
	if (binNum[31] == 0)
	{
		binNum[31] = 1;
		return;
	}
	for (int i = 31; i >= 0; i--) 
	{
		if (binNum[i] == 1)
		{
			binNum[i] = 0;
		}
		else
		{
			binNum[i] = 1;
			return;
		}
	}
}

int getExhibitorPosFloat(int* arr, int exp)
{
	int exhibitor = exp + 127;
	for (int i = 7; i >= 0; i--)
	{
		arr[i] = exhibitor % 2;
		exhibitor = exhibitor / 2;
	}
}

int getFractPartPosFloat(int* resultFractPart, int length, int exp)
{
	int* bin = (int*)malloc(exp * sizeof(int));
	for (int i = exp - 1; i >= 0; i--)
	{
		bin[i] = length % 2;
		length = length / 2;
	}
	for (int i = 0; i <= 22; i++)
	{
		if (i < exp - 1)
		{
			resultFractPart[i] = bin[i];
		}
		else
		{
			resultFractPart[i] = 0;
		}
	}
	free(bin);
}

int binPosFloat(int* binNum, int length, int exp)
{
	binNum[0] = 0;
	int exhibitor[8], fractionalPart[23];
	getExhibitorPosFloat(&exhibitor, exp);
	getFractPartPosFloat(&fractionalPart, length, exp);
	for (int i = 1; i <= 8; i++)
	{
		binNum[i] = exhibitor[i - 1];
	}
	for (int i = 9; i <= 31; i++)
	{
		binNum[i] = fractionalPart[i - 9];
	}
}

int getExhibitorNegDouble(int* arr, int exp)
{
	int exhibitor = exp + 1023;
	for (int i = 10; i >= 0; i--)
	{
		arr[i] = exhibitor % 2;
		exhibitor = exhibitor / 2;
	}
}

int getFractPartNegDouble(int* resultFractPart, int length, int exp)
{
	int* bin = (int*)malloc(exp * sizeof(int));
	for (int i = exp - 1; i >= 0; i--)
	{
		bin[i] = length % 2;
		length = length / 2;
	}
	for (int i = 0; i <= 51; i++)
	{
		if (i < exp - 1)
		{
			resultFractPart[i] = bin[i];
		}
		else
		{
			resultFractPart[i] = 0;
		}
	}
	free(bin);
}

int binNegDouble(int* binNum, int length, int exp)
{
	binNum[0] = 1;
	int exhibitor[11], fractionalPart[52];
	getExhibitorNegDouble(&exhibitor, exp);
	getFractPartNegDouble(&fractionalPart, length, exp);
	for (int i = 1; i <= 11; i++)
	{
		binNum[i] = exhibitor[i - 1];
	}
	for (int i = 12; i <= 63; i++)
	{
		binNum[i] = fractionalPart[i - 12];
	}
}


int main()
{
	int arrNegBinInt32[32], arrPosBinFloat[32], arrNegBinDouble[64];
	int length = getLength();
	int exp = 0;
	printf("This program outputs the binary representation of the product of my last name, first name and patronymic\n");
	printf("in the specified data formats:\n\n");
	binNegInt32(&arrNegBinInt32, length, &exp);
	printf("Negative 32-bit integer whose modulus is equal to the found product: \n\n");
	for (int i = 0; i <=31; i++)
	{
		printf("%d", arrNegBinInt32[i]);
	}
	binPosFloat(&arrPosBinFloat, length, exp);
	printf("\n\nA positive floating point number of single precision according to the IEEE 754 standard,\n");
	printf("the modulus of which is equal to the found product: \n\n");
	for (int i = 0; i <= 31; i++)
	{
		printf("%d", arrPosBinFloat[i]);
	}
	binNegDouble(&arrNegBinDouble, length, exp);
	printf("\n\nA negative double-precision floating-point number according to the IEEE 754 standard,\n");
	printf("the modulus of which is equal to the found product: \n\n");
	for (int i = 0; i <= 63; i++)
	{
		printf("%d", arrNegBinDouble[i]);
	}
	printf("\n");
	return 0;
}