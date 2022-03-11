#include <stdio.h>
#include "memoryWorkshop.h"



void error()
{
	printf("\nFailed to allocate or reallocate memory.\n");
}

int main()
{
	printf("The program implements the malloc, realloc, and free functions.\nStarting the demonstration.\n");
	init();
	print();
	int coef1;

	printf("Allocating memory for 5 integers.\n");
	int* integers;
	if ((integers = (int*)myMalloc(5 * sizeof(int))) == NULL)
	{
		error();
	}
	print();

	printf("Assigning values for the allocated memory form 1 to 5\n");
	for (coef1 = 0; coef1 < 5; coef1++)
	{
		integers[coef1] = coef1 + 1;
	}

	printf("\nPrinting those 5 elements.\n>>>");
	for (coef1 = 0; coef1 < 5; coef1++)
	{
		printf("%d ", integers[coef1]);
	}

	printf("\n\nReallocating memory from 5 to 10 integers.\n");
	if ((integers = (int*)myRealloc(integers, 10 * sizeof(int))) == NULL)
	{
		error();
	}
	print();

	printf("Assigning new values for last 5 and new 5 integers from 10 to 19\n");
	for (coef1 = 0; coef1 < 10; coef1++)
	{
		integers[coef1] = coef1 + 10;
	}

	printf("\nPrinting those 10 elements.\n>>>");
	for (coef1 = 0; coef1 < 10; coef1++)
	{
		printf("%d ", integers[coef1]);
	}

	printf("\n\nAllocating memory for 13 chars.\n");
	char* chars;
	char line[13] = "Hello World!\0";
	if ((chars = (char*)myMalloc(13 * sizeof(char))) == NULL)
	{
		error();
	}
	print();

	printf("Assigning string \"Hello World!\" for new allocated memory.\n");
	for (coef1 = 0; coef1 < 13; coef1++)
	{
		chars[coef1] = line[coef1];
	}

	printf("\nPrinting line.\n>>>");
	for (coef1 = 0; coef1 < 13; coef1++)
	{
		printf("%c", chars[coef1]);
	}

	printf("\n\nReleasing integer's memory.\n");
	myFree(integers);
	print();

	printf("Allocating memory for 10 doubles.\n");
	double* doubles;
	if ((doubles = (double*)myMalloc(10 * sizeof(double))) == NULL)
	{
		error();
	}
	print();

	printf("Assigning doubles for new allocated memory.\n");
	for (coef1 = 0; coef1 < 10; coef1++)
	{
		doubles[coef1] = (double)coef1 * 1.4;
	}

	printf("\nPrinting doubles.\n>>>");
	for (coef1 = 0; coef1 < 10; coef1++)
	{
		printf("%lf ", doubles[coef1]);
	}

	printf("\n\nAllocating memory for 10 more doubles to provoke NULL output.\n");
	double* doubles2;
	if ((doubles2 = (double*)myMalloc(10 * sizeof(double))) == NULL)
	{
		error();
	}
	print();

	printf("Releasing char's memory.\n");
	myFree(chars);
	print();

	printf("Releasing double's memory.\n");
	myFree(doubles);
	print();

	printf("Demostration is over.\n");
	free(memory);
	printf("Completing the programm.\n");
	return 0;
}