#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include "memory_managment.h"

void fill_array_of_integers(int* array, int start, int end)
{
	for (int i = start; i < end; i++)
		array[i] = (rand() % 200) - 100;
}

void show_array_of_integers(int* array, int size)
{
	printf("Array: ");
	for (int i = 0; i < size - 1; i++)
		printf("%d, ", array[i]);
	printf("%d.\n", array[size - 1]);
}

void fill_and_show(int* array, int start, int end, int flag)
{
	if (flag)
	{
		show_array_of_integers(array, end);
	}
	else 
	{
		fill_array_of_integers(array, start, end);
		show_array_of_integers(array, end);
	}
	show_memory_situation();
}
int main()
{
	printf("This program shows the memory manager. Firstly, you have to know the designations. \n"
		"The program use '|Used - 0/1|H: 12|D: X|F: Y|' - a block of memory, where 'Used 0' means "
		"that block isn't used and 'Used 1' has the opposite meaning; 'H: 12' - the header size; 'D: X' means "
		"that the user has X bytes available to store his values and 'F: Y' means that internal block fragmentation is Y bytes.\n\n");

	int size = 10;
	srand(time(NULL));
	unit();
	
	printf("Now we will test the function my_malloc.\nLet's create an array of integers for %d elements.\n"
		"And then we fill it with random numbers:\n", size);
	int* first_array = (int*)my_malloc(size * sizeof(int));
	fill_and_show(first_array, 0, size, 0);

	printf("Now we will look at the function my_calloc for new array\n");
	int* second_array = (int*)my_calloc(size * sizeof(int));
	fill_and_show(second_array, 0, size, 1);

	printf("Now we gonna to see how the function my_realloc is working.\nLet's extend the first array of numbers. "
		"Now array consist of %d numbers\n", 2 * size);
	first_array = (int*)my_realloc(first_array, 2 * size * sizeof(int));
	fill_and_show(first_array, size, 2 * size, 0);

	printf("Now we will use the function my_free to free all memory and then create a big array to use all valiable memory.\n", DEFAULT_MEMORY_SIZE);
	my_free(first_array);
	my_free(second_array);
	size = (DEFAULT_MEMORY_SIZE - HEADER_SIZE) / sizeof(int);
	first_array = (int*)my_malloc(size * sizeof(int));
	fill_and_show(first_array, 0, size, 0);

	printf("Now we gonna free the memory and allocate it for 7 integer\n");
	my_free(first_array);
	int* a[7]; 
	for (int i = 0; i < 7; i++)
	{
		a[i] = (int**)my_malloc(1 * sizeof(int*));
	}
	show_memory_situation();
	
	printf("Let's delete the 2, 3, 6, 7 integers\n");
	my_free(a[1]), my_free(a[2]), my_free(a[5]), my_free(a[6]);
	show_memory_situation();
	
	printf("Let's add two arrays of two integers\n");
	a[1] = (int*)my_malloc(2 * sizeof(int));
	a[2] = (int*)my_malloc(2 * sizeof(int));
	show_memory_situation();
	
	printf("Free the memory:\n");
	for (int i = 0; i < 5; i++)
	{
		my_free(a[i]);
	}
	show_memory_situation();

	free_unit();
	printf("Thank you for testing the program! That's over.\n\n");
	return 0;
}