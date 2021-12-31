#include <stdio.h>
#include <stdlib.h>

int max_size = 100;
char* heap_start;

void my_free(char* ptr);

struct header
{
	unsigned size;
	char status;
};

typedef struct header header;

void my_init()
{
	char* my_heap = (char*)calloc(max_size, sizeof(char));
	heap_start = my_heap;
	header* header = heap_start;
	header->size = max_size;
	header->status = 0;
}

void* my_malloc(size_t size)
{
	header* head;
	header* footer;
	header* next_header;
	unsigned dif;
	head = heap_start;
	unsigned size_sum = size + sizeof(header);
	char* heap_start_copy = heap_start;
	char* header_copy = head;
	while ((head->size < size || head->status == 1) & (size_sum <= max_size))
	{
		dif = head->size + (head->status ? (sizeof(header) * 2) : sizeof(header));
		size_sum += dif;
		header_copy += dif;
		head = header_copy;
	}
	if (size_sum > max_size)
	{
		return NULL;
	}
	unsigned free_size = head->size;
	head->size = size;
	head->status = 1;
	footer = (char*)head + sizeof(header) + size;
	footer->size = size;
	footer->status = 1;
	next_header = footer + 1;
	if (free_size - size - 2 * sizeof(header) > 0)
	{
		next_header->size = free_size - size - 2 * sizeof(header);
		next_header->status = 0;
	}
	return head + 1;

}

void* my_realloc(char* ptr, unsigned new_size)
{
	if (ptr == NULL)
	{
		printf("One of the pointers was NULL\n");
		return NULL;
	}
	header* old_header = ptr - sizeof(header);
	unsigned old_size = old_header->size;
	my_free(ptr);
	char* new_ptr = my_malloc(new_size);
	if (new_ptr == NULL)
	{
		printf("Memory allocation error");
		return NULL;
	}
	for (size_t i = 0; i < old_size; i++)
	{
		new_ptr[i] = ptr[i];
	}
	return new_ptr;
}

void my_free(char* ptr)
{
	header* head = ptr - sizeof(header);
	head->status = 0;
	header* footer;
	footer = (char*)(ptr + head->size);
	footer->size = 0;
	footer->status = 0;
	if (head == heap_start)
	{
		header* next_header = ptr + head->size - 1;
		if (next_header->status == 0)
		{
			head->size += next_header->size + sizeof(header);
		}
		return;
	}
	header* prev_footer = ptr - sizeof(header) * 2;
	header* next_header = (char*)(ptr + head->size + 8);
	if (prev_footer->status == 0)
	{
		header* big_header;
		big_header = (char*)prev_footer - prev_footer->size - sizeof(header);
		big_header->size = head->size + prev_footer->size + (next_header->status ? 0 : next_header->size);
	}
	if (next_header->status == 0)
	{
		head->size += next_header->size + sizeof(header);
	}
}

int main()
{
	my_init();
	int i;
	printf("This program creates a memory manager\n");
	char* arr_first = my_malloc(5);
	char* arr_second = my_malloc(7);
	char* arr_third = my_malloc(7);
	if (arr_first == NULL || arr_second == NULL || arr_third == NULL)
	{
		printf("Not enough memory error\n");
		return 1;
	}
	printf("Memory is successfuly allocated\n");
	for (i = 0; i < 5; i++)
	{
		arr_first[i] = 15;
	}
	for (i = 0; i < 7; i++)
	{
		arr_second[i] = 8;
	}
	my_free(arr_second);
	my_free(arr_first);
	printf("Memory is successfully freed\n");
	for (i = 0; i < 7; i++)
	{
		arr_third[i] = 7;
		printf("%d ", arr_third[i]);
	}
	printf("\nThis is the arr_third's elements before realloc\n");
	arr_third = my_realloc(arr_third, 6);
	if (arr_third == NULL)
	{
		printf("Memory allocation error\n");
		return 1;
	}
	for (i = 0; i < 6; i++)
	{
		printf("%d ", arr_third[i]);
	}
	printf("\nThis is the arr_third's elements after realloc\n");
	my_free(arr_third);
	printf("Memory was successfuly 'realloced' and freed\n");
	return 0;
}