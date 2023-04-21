#include <stdlib.h>
#include "manager.h"

block *first_block = NULL;
char *my_memory;
const int SIZE_OF_MEMORY = 256;

void init_memory()
{
	my_memory = malloc(SIZE_OF_MEMORY * sizeof(char));
}

void delete_null_block()
{
	while (first_block != NULL && first_block->is_use == 0)
	{
		first_block = first_block->next;
	}
	block *temp = first_block;
	while (temp != NULL && temp->next != NULL)
	{
		block *next = temp->next;
		while (next != NULL && !(next->is_use))
		{
			temp->next = next->next;
			next = temp->next;
		}
		temp = temp->next;
	}
}

void *my_malloc(int size)
{
	delete_null_block();
	size = size + sizeof(block);
	if (first_block == NULL)
	{
		first_block = (block *) my_memory;
		first_block->size = size;
		first_block->is_use = 1;
		first_block->next = NULL;
		return (char *) first_block + sizeof(block);
	}
	block *temp = NULL;
	if ((char *) my_memory + size <= (char *) first_block)
	{
		temp = (block *) my_memory;
		temp->size = size;
		temp->is_use = 1;
		temp->next = first_block;
		first_block = temp;
		return (char *) first_block + sizeof(block);
	}
	temp = first_block;
	while (temp != NULL)
	{
		if (temp->next == NULL && (void *) temp + temp->size + size <= (void *) my_memory + SIZE_OF_MEMORY ||
			temp->next != NULL && (void *) temp + temp->size + size <= ((void *) temp->next))
		{
			block *new = (char *) temp + temp->size;
			new->size = size;
			new->is_use = 1;
			new->next = NULL;
			temp->next = new;
			return (char *) new + sizeof(block);
		}
		temp = temp->next;
	}
	return NULL;
}

void *my_calloc(int size)
{
	char *temp = my_malloc(size);
	for (int i = 0; i < size; i++)
	{
		temp[i] = 0;
	}
	return (void *) temp;
}

void *my_realloc(void *ptr, int size)
{
	block *temp = (block *) ((char *) ptr - sizeof(block));
	if (temp->size - sizeof(block) >= size)
	{
		temp->size = size + sizeof(block);
		return ptr;
	}
	else
	{
		my_free(ptr);
		char *new_ptr = my_malloc(size);
		for (int i = 0; i < size; i++)
		{
			new_ptr[i] = ((char *) ptr)[i];
		}
		return new_ptr;
	}
}

void my_free(void *ptr)
{
	if (ptr != NULL)
	{
		block *temp = (block *) ((char *) ptr - sizeof(block));
		(*temp).is_use = 0;
	}
}

void free_memory()
{
	free(my_memory);
}
