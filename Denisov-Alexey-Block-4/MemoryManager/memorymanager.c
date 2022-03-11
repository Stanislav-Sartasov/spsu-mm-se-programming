#include "memorymanager.h"

void init()
{
	memory = (BYTE*)calloc(MEMORY_SIZE, sizeof(BYTE));
}

void deinit()
{
	free(memory);
}

void* myMalloc(size_t size)
{	
	block* current = (block*)memory;

	while (current->size != 0 && current <= &memory[MEMORY_SIZE])
	{
		if (!current->isOccupied && current->size >= size + 2 * sizeof(block))
		{
			block* new = (block*)((int)current + current->size);
			new->isOccupied = 0;
			new->size = current->size - size - sizeof(block);

			current->isOccupied = 1;
			current->size = size + sizeof(block);

			return (void*)((int)current + sizeof(block));
		}
		else if (!current->isOccupied && current->size >= size + sizeof(block))
		{
			current->isOccupied = 1;
			
			return (void*)((int)current + sizeof(block));
		}

		current = (block*)((int)current + current->size);
	}

	if (MEMORY_SIZE - ((int)memory - (int)current) >= size + sizeof(block))
	{
		current->isOccupied = 1;
		current->size = size + sizeof(block);

		return (void*)((int)current + sizeof(block));
	}

	return NULL;
}

void myFree(void* pointer)
{
	if (pointer == NULL)
		return;
	
	block* current = (block*)((int)pointer - sizeof(block));
	current->isOccupied = 0;
	return;
}

void* myRealloc(void* pointer, size_t size)
{
	if (pointer == NULL)
	{
		pointer = myMalloc(size);
		return pointer;
	}
	
	block* current = (block*)((int)pointer - sizeof(block));

	if (current->size - sizeof(block) >= size)
	{
		return pointer;
	}
	if (!((block*)((int)current + current->size))->isOccupied)
	{
		block* head = (block*)((int)current + current->size);
		while (!head->isOccupied)
		{
			current->size += head->size;
			head = (block*)((int)head + head->size);
		}

		if (current->size - sizeof(block) >= size)
		{
			return pointer;
		}
	}
	
	current->isOccupied = 0;
	void* newPointer = myMalloc(size);

	for (int i = 0; i < current->size; i++)
	{
		((BYTE*)newPointer)[i] = ((BYTE*)pointer)[i];
	}
	
	return newPointer;
}