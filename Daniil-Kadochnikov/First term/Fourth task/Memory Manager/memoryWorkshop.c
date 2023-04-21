#define INIT_MEMORY 256
#include <stdlib.h>
#include <stdio.h>
#include "memoryWorkshop.h"



void init()
{
	memory = (char*)malloc(INIT_MEMORY * sizeof(char));
	struct block* firstBlock;
	firstBlock = (struct block*)memory;
	firstBlock->size = INIT_MEMORY - sizeof(*firstBlock);
	firstBlock->next = NULL;
	firstBlock->back = NULL;
	firstBlock->free = 0;
	firstBlock->data = (char*)firstBlock + sizeof(*firstBlock);
}

void createBlock(struct block* current, unsigned int size)
{
	struct block* block;
	block = (struct block*)(current->data + size);
	block->size = current->size - size - sizeof(*block);
	block->next = current->next;
	block->back = current;
	block->free = 0;
	block->data = (char*)block + sizeof(*block);
	current->next = block;
}

void* myMalloc(unsigned int size)
{
	struct block* current;
	struct block* previous = NULL;
	current = (struct block*)memory;
	while (current && !(!(current->free) && size <= current->size))
	{
		previous = current;
		current = current->next;
	}

	if (current != NULL)
	{
		current->free = 1;
		if (current->size - size >= sizeof(*current))
		{
			createBlock(current, size);
		}
		current->size = size;
		return current->data;
	}
	return NULL;
}

void* myRealloc(void* pointer, unsigned int size)
{
	unsigned int add;
	struct block* current;
	struct block* block;
	block = (struct block*)((char*)pointer - sizeof(struct block));
	current = block;
	while (current->next)
	{
		if (!current->next->free)
		{
			current = current->next;
		}
	}

	if (current->next == NULL)
	{
		add = memory + INIT_MEMORY - block->data;
	}
	else
	{
		add = ((char*)current->next - block->data);
	}

	if (add >= size)
	{
		block->size = add;
		block->next = current->next;
		if (add - size >= sizeof(*current))
		{
			createBlock(block, size);
		}
		block->size = size;
		return block->data;
	}
	else
	{
		char* result = (char*)myMalloc(size);
		for (unsigned int coef1 = 0; coef1 < block->size; coef1++)
		{
			result[coef1] = ((char*)pointer)[coef1];
		}
		block->free = 0;
		return result;
	}

	return NULL;
}

void myFree(void* pointer)
{
	unsigned int size;
	struct block* block;
	block = (struct block*)((char*)pointer - sizeof(struct block));
	block->free = 0;
	struct block* previous = block;
	struct block* following = block;
	while (previous->back)
	{
		if (!previous->back->free)
		{
			previous = previous->back;
		}
		else
		{
			break;
		}
	}

	while (following->next)
	{
		if (!following->next->free)
		{
			following = following->next;
		}
		else
		{
			break;
		}
	}

	if (following->next == NULL)
	{
		size = memory + INIT_MEMORY - previous->data;
	}
	else
	{
		size = (char*)following->next - previous->data;
		following->next->back = previous;
	}

	previous->size = size;
	previous->next = following->next;
}

void print()
{
	printf("\n");
	printf("Demonstration of the memory line, where 1 - reserved memory, 0 - free.\n");
	struct block* current;
	current = (struct block*)memory;
	for (int coef1 = 0; coef1 < INIT_MEMORY; coef1++)
	{
		if ((struct block*)&memory[coef1] == current)
		{
			if (current->free)
			{
				printf("{");
				unsigned int n = 0;
				while (n < sizeof(*current))
				{
					printf("#");
					n++;
					coef1++;
				}
				n = 0;
				while (n < current->size)
				{
					printf("%d", current->free);
					n++;
					coef1++;
				}
				coef1--;
				current = current->next;
				printf("}");
			}
			else
			{
				printf("0");
				current = current->next;
			}
		}
		else
		{
			printf("0");
		}
	}
	printf("\n\n");
}