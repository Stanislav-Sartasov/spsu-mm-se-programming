#include <stdio.h>
#include <stdlib.h>
#include <string.h>

char* buffer;
int mem_size = 100;

#pragma pack(1)
struct header
{
	int size;
	char allocated;
};
#pragma pop

void* my_malloc(size_t size)
{
	struct header* current;
	current = (struct header*)buffer;
	while (current->size != 0 && current->size + (int)current + sizeof(struct header) - (int)buffer <= mem_size)
	{
		if ((current->size >= size + 2 * sizeof(struct header)) && !(current->allocated))
		{
			struct header* new_block;
			new_block = (struct header*)((int)current + size + sizeof(struct header));
			new_block->size = current->size - size - sizeof(struct header);
			new_block->allocated = 0;
			current->size = size + sizeof(struct header);
			current->allocated = 1;
			return current + 1;
		}

		if ((current->size >= size + sizeof(struct header)) && !(current->allocated))
		{
			current->allocated = 1;
			return current + 1;
		}

		current = (struct header*)((int)current + (int)current->size);
	}

	if (mem_size - ((int)current - (int)buffer) >= size + sizeof(struct header))
	{
		current->size = size + sizeof(struct header);
		current->allocated = 1;
		return current + 1;
	}
	return NULL;
}

void my_free(void* ptr)
{
	if (ptr != NULL)
	{
		((struct header*)ptr - 1)->allocated = 0;

		struct header* current;
		current = (struct header*)buffer;
		while (current->size != 0)
		{
			if (current->size + (int)current + sizeof(struct header) - (int)buffer >= mem_size)
				current->size = 0;

			if ((current->allocated == 0) && (((struct header*)((int)current + (int)current->size))->allocated == 0))
			{
				current->size += ((struct header*)((int)current + (int)current->size))->size;
				if (((struct header*)((int)current + (int)current->size))->size == 0)
					current->size = 0;
				continue;
			}

			current = (struct header*)((int)current + (int)current->size);
		}
	}
}

void print_buffer()
{
	for (int i = 0; i < mem_size / 4; i++)
		printf("%d ", ((int*)buffer)[i]);
	printf("\n\n");
}

void* my_realloc(void* ptr, size_t size)
{
	if (ptr != NULL)
	{
		int old_size = ((struct header*)ptr - 1)->size;
		my_free(ptr);
		void* new_pointer = my_malloc(size);
		if (new_pointer == NULL)
			return NULL;
		else
		{
			for (int i = 0; i < old_size; i++)
				((char*)new_pointer)[i] = ((char*)ptr)[i];
			return new_pointer;
		}
	}
}

void init()
{
	buffer = (char*)malloc(sizeof(char) * mem_size);

	for (int i = 0; i < mem_size; i++)
		buffer[i] = 0;
}

void init_stop()
{
	free(buffer);
}

int main()
{
	printf("This program implements a memory manager\n\n");

	int* arr;
	int* arr2;
	int* arr3;

	init();

	printf("Let's allocate some memory and fill it with integers:\n");

	arr = my_malloc(5 * sizeof(int));
	for (int i = 0; i < 5; i++)
	{
		arr[i] = 1;
		printf("%d ", arr[i]);
	}
	printf("\n");

	printf("Memory allocation is successful!\n");
	printf("Let's add some arrays and try to free it.\n");

	arr2 = my_malloc(4 * sizeof(int));
	for (int i = 0; i < 3; i++)
	{
		arr2[i] = 2;
		printf("%d ", arr2[i]);
	}
	printf("\n");
	arr3 = my_malloc(10 * sizeof(int));
	for (int i = 0; i < 10; i++)
	{
		arr3[i] = 3;
		printf("%d ", arr3[i]);
	}
	printf("\n");

	my_free(arr3);
	my_free(arr);
	my_free(arr2);

	printf("Free successful!\n");
	printf("The manager automatically merged blocks of memory. We can use it all. Let's check this and allocate a new array.\n");

	arr = my_malloc(23 * sizeof(int));
	for (int i = 0; i < 23; i++)
	{
		arr[i] = 4;
		printf("%d ", arr[i]);
	}
	printf("\n");

	printf("We placed 23 elements. This is the maximum number of elements we can place here counting the header.\n");
	printf("Merging is successful!\n");
	printf("Let's remove some elements from the array. We have to reallocate memory.\n");

	arr = my_realloc(arr, 22 * sizeof(int));
	for (int i = 0; i < 22; i++)
	{
		arr[i] = 5;
		printf("%d ", arr[i]);
	}
	printf("\n");

	printf("Let's add another element again.\n");

	arr = my_realloc(arr, 23 * sizeof(int));
	for (int i = 0; i < 23; i++)
	{
		arr[i] = 6;
		printf("%d ", arr[i]);
	}
	printf("\n");
	printf("Reallocation is successful!\n");
	printf("This is the power of the memory manager!\n");

	init_stop();
	return 0;
}