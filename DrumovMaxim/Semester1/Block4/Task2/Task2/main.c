#include "manager.h"

int main()
{
	printf("the program tests the heap management capabilities (functions such as malloc, realloc, free)\n");
	init();
	int32_t* squares = (int32_t*)myMalloc(sizeof(int32_t) * 10);
	int32_t* uselessmas1 = (int32_t*)myMalloc(sizeof(int32_t) * 50);
	int32_t* uselessmas2 = (int32_t*)myMalloc(sizeof(int32_t) * 70);
	int32_t* uselessmas3 = (int32_t*)myMalloc(sizeof(int32_t) * 100);
	printf("memory allocated for arrays: \n");
	printf("address >> %x, memory >> %d bytes\n", squares, sizeof(int32_t) * 10);
	printf("address >> %x, memory >> %d bytes\n", uselessmas1, sizeof(int32_t) * 50);
	printf("address >> %x, memory >> %d bytes\n", uselessmas2, sizeof(int32_t) * 70);
	printf("address >> %x, memory >> %d bytes\n", uselessmas3, sizeof(int32_t) * 100);
	myFree(uselessmas1);
	myFree(uselessmas2);
	printf("the array with address >> %x has been deleted\n", uselessmas1);
	printf("the array with address >> %x has been deleted\n", uselessmas2);
	for (size_t i = 1; i < 11; i++)
	{
		squares[i - 1] = i * i;
	}
	printf("added content of array with address >> %x : \n", squares);
	for (size_t i = 1; i < 11; i++)
	{
		printf("%d\n", squares[i - 1]);
	}
	squares = (int32_t*)myRealloc(squares, sizeof(int32_t) * 15);
	printf("allocating more memory for the array with the address >> %x\n", squares);
	for (size_t i = 11; i < 16; i++)
	{
		squares[i - 1] = i * i;
	}
	printf("added content of array with address >> %x : \n", squares);
	for (size_t i = 1; i < 16; i++)
	{
		printf("%d\n", squares[i - 1]);
	}
	printf("allocating less memory for the array with the address >> %x\n", squares);
	squares = (int32_t*)myRealloc(squares, sizeof(int32_t) * 5);
	printf("array contents: \n");
	for (size_t i = 1; i < 6; i++)
	{
		printf("%d\n", squares[i - 1]);
	}
	myFree(squares);
	myFree(uselessmas3);
	printf("the array with address >> %x has been deleted\n", squares);
	printf("the array with address >> %x has been deleted\n", uselessmas3);
	freeMemory();
	printf("memory freed");
	return 0;
}