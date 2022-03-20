#pragma once
#include <stdio.h>
#include "manager.h"

block* first_block = NULL;

void init()
{
	space = (char*)malloc(SIZE_OF_SPACE * sizeof(char));
}

void free_init()
{
	free(space);
}

void garbage_collector(block* curr)
{
	if (!(curr) || !(curr->next))
	{
		return;
	}

	while (curr->next && curr->flag)
	{
		curr = curr->next;
	}

	if (!(curr->next))
	{
		return;
	}

	block* str = curr;
	block* prv = curr;
	curr = curr->next;

	while (curr->next && !(curr->flag))
	{
		prv = curr;
		curr = curr->next;
	}

	if (str != prv)
	{
		if (!(curr->next) && !(curr->flag))
		{
			str->size = (int)curr - (int)str + curr->size;
			str->next = NULL;
		}
		else
		{
			str->size = (int)curr - (int)str - SIZE_OF_BLOCK;
			str->next = curr;
		}
	}

	garbage_collector(curr);
}

void* my_malloc(int size)
{
	garbage_collector(first_block);

	size = (size + 3) & (-4);

	if (!(first_block))
	{
		first_block = (block*)space;
		first_block->next = NULL;
		first_block->size = size;
		first_block->flag = 1;
		return first_block->data;
	}

	block* curr = first_block;
	while ((curr->flag) || (curr > curr->size))
	{
		if ((char*)curr + curr->size + size + SIZE_OF_BLOCK > space + SIZE_OF_SPACE)
		{
			return NULL;
		}

		if (!(curr->next))
		{
			curr->next = (char*)curr + curr->size + SIZE_OF_BLOCK;
			curr = curr->next;
			curr->next = NULL;
			break;
		}

		curr = curr->next;
	}

	if (curr->size >= size + (SIZE_OF_BLOCK + 4))
	{
		block* b_curr = curr->data + size;
		b_curr->next = curr->next;
		curr->next = b_curr;
		b_curr->flag = 0;
		b_curr->size = curr->size - size - (SIZE_OF_BLOCK + 4);
	}

	curr->size = size;
	curr->flag = 1;
	return curr->data;
}

void* my_calloc(int size)
{
	char* rezult = (char*)my_malloc(size * sizeof(char));
	for (int i = 0; i < size; i++)
	{
		rezult[i] = 0;
	}

	return rezult;
}

void* my_realloc(void* pointer, int size)
{
	block* curr = (block*)pointer;
	if (curr->size <= size)
	{
		curr->size = size;

		return curr->data;
	}
	else
	{
		curr->flag = 0;
		char* rezult = (char*)my_malloc(size * sizeof(char));
		for (int i = 0; i < size; i++)
		{
			rezult[i] = curr->data[i];
		}

		return rezult;
	}
}

void my_free(void* pointer)
{
	*((char*)pointer - 8) = 0;
}