#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <locale.h>

const int mem_size = 1024 * 1024 * 1024;

struct mem_part
{
	unsigned int sz;
	char array[];
};

char* mem;


int is_enough(size_t size, char* memstart)
{
	if (size == 0) return 1;

	int k = (int)size;

	while ((*memstart == 0) && (k > 0))
	{
		k--;
		memstart++;
	}

	if (k == 0) return 0;

	return k;

}

unsigned int* search_for_mem(size_t size, char* start)
{
	if (start >= mem + mem_size) return NULL;
	char* mem_look = start;

	while (*mem_look != 0)
	{
		mem_look += *((int*)mem_look) + sizeof(int);
		if (mem_look != mem) mem_look++;
	}


	int available = is_enough(size, mem_look);

	if (!available)
	{
		return (unsigned int*)mem_look;
	}
	else
	{
		search_for_mem(size, mem_look + size - available);
	}
}

void initialize()
{
	mem = calloc(mem_size, 1);
}

void* my_malloc(size_t size)
{
	struct mem_part* memory = (struct mem_part*)search_for_mem(size + sizeof(char) + sizeof(int),
		mem);

	if (memory == NULL) return NULL;

	memory->sz = (unsigned int)size;

	char* end = memory->array + size;
	*end = 0;

	return memory->array;
}

void my_free(void* ptr)
{
	unsigned int* frptr = (char*)ptr - sizeof(int);

	memset(frptr, 0, *frptr + sizeof(int));
}

void* my_realloc(void* ptr, size_t new_size)
{
	char* new_ptr = (char*)ptr - sizeof(int);
	unsigned int old_size = *((unsigned int*)new_ptr);

	if (old_size > new_size) return ptr;

	unsigned int delta = (unsigned int)new_size - old_size;

	new_ptr += old_size + sizeof(int);

	if (!is_enough(delta + sizeof(char), new_ptr))
	{
		*(new_ptr + delta) = 0;
		*((unsigned int*)((char*)ptr - sizeof(int))) = (unsigned int)new_size;

		return ptr;
	}
	else
	{
		new_ptr = my_malloc(new_size);
		memcpy(new_ptr, ptr, old_size);
		my_free(ptr);

		return new_ptr;
	}
}

int main()
{
	setlocale(LC_ALL, "Rus");

	initialize();

	int* variable = my_malloc(sizeof(int));

	*variable = 42;

	printf("Выделенная память для одной целочисленной переменной. Его адрес %p и значение %d \n", variable, *variable);

	int* array_of_int = my_malloc(sizeof(int) * 10);

	printf("\nВыделенная память для целочисленной массива размером в 10 элементов (тип Interger). Адрес массива %p и значения:\n", array_of_int);


	for (int i = 0; i < 10; i++)
	{
		array_of_int[i] = i + 1;
		printf("%d ", array_of_int[i]);
	}

	double* array_of_double = my_malloc(sizeof(double) * 5);

	printf("\n\nВыделенная память для массива с плавающей запяятой (тип Double), Размер равен - 5, его адрес % p, элементы:\n ", array_of_double);

	for (int j = 0; j < 5; j++)
	{
		array_of_double[j] = j + 1;
		printf("%lf ", array_of_double[j]);
	}

	array_of_double = my_realloc(array_of_double, sizeof(double) * 7);

	array_of_double[5] = 6;
	array_of_double[6] = 7;

	printf("\n\nПереназначенная память для второго массива, его новый размер равен 7, а его адрес %p (он не изменился, поскольку достаточно места для его расширения), его элементы:\n ", array_of_double);

	for (int i = 0; i < 7; i++)
	{
		printf("%lf ", array_of_double[i]);
	}

	array_of_int = my_realloc(array_of_int, sizeof(int) * 15);

	for (int j = 10; j < 15; j++)
	{
		array_of_int[j] = j + 1;
	}

	printf("\n\nПерераспределенная память для первого массива,  его новый размер равен 15, а его адрес %p (он был изменен из-за нехватки места для перераспределения). Его элементы:\n", array_of_int);

	for (int j = 0; j < 15; j++)
	{
		printf("%d ", array_of_int[j]);
	}

	my_free(array_of_double);
	my_free(array_of_int);
	my_free(variable);

	printf("\n\nПамять сейчас свободна");

	free(mem);

	return 0;
}


