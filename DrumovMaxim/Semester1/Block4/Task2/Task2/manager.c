#include "manager.h"

void init()
{
	memory = (int8_t*)malloc(sizeof(int8_t) * HEAPSIZE);
	if (memory == NULL)
	{
		printf("impossible memory for a heap\n");
		exit(-1);
	}
	blocks = memory;
	blocks->address = memory + BLOCKSIZE;
	blocks->availability = true;
	blocks->size = HEAPSIZE - BLOCKSIZE;
	blocks->next = NULL;
	blocks->prev = NULL;
}

size_t alignment(size_t size)
{
	return !(size % 4) ? size : size + 4 - size % 4;
}

void* split(block** current, size_t size)
{
	block* newBlock = (*current)->address + size;
	newBlock->size = (*current)->size - BLOCKSIZE - size;
	newBlock->address = (*current)->address + size + BLOCKSIZE;
	newBlock->availability = true;
	newBlock->prev = (*current);
	newBlock->next = (*current)->next;
	(*current)->next = newBlock;
	(*current)->availability = false;
	(*current)->size = size;
	return (void*)(*current)->address;
}

void* myMalloc(size_t size)
{
	if (size == 0)
	{
		return NULL;
	}
	size = alignment(size);
	block* current = blocks;
	while (current->next != NULL && (current->size < size + BLOCKSIZE || !current->availability))
	{
		current = current->next;
	}

	if (current->size < size + BLOCKSIZE || !current->availability)
	{
		return NULL;
	}
	if (current->size < BLOCKSIZE + size + sizeof(int8_t))
	{
		current->availability = false;
		return current->address;
	}

	return split(&current, size);
}

block* find(void* ptr)
{
	block* current = blocks;
	while (current->next != NULL && current->address != ptr)
	{
		current = current->next;
	}
	return current;
}

void merge(block** delete)
{
	(*delete)->availability = true;
	if ((*delete)->next != NULL)
	{
		if ((*delete)->next->availability)
		{
			(*delete)->size += BLOCKSIZE + (*delete)->next->size;
			(*delete)->next = (*delete)->next->next;
		}
	}
	if ((*delete)->prev != NULL)
	{
		if ((*delete)->prev->availability)
		{
			(*delete)->prev->size += BLOCKSIZE + (*delete)->size;
			(*delete)->prev->next = (*delete)->next;
		}
	}
}

void myFree(void* ptr)
{
	block* current = find(ptr);
	if (current == NULL)
	{
		return;
	}
	merge(&current);
}

void* myRealloc(void* ptr, size_t size)
{
	block* current = find(ptr);
	size = alignment(size);
	if (current == NULL || size == 0)
	{
		myFree(ptr);
		return NULL;
	}
	if (size == current->size)
	{
		return (void*)current->address;
	}
	if (size < current->size)
	{
		return split(&current, size);
	}
	int8_t* data = (int8_t*)myMalloc(sizeof(int8_t) * current->size);
	if (data == NULL)
	{
		return NULL;
	}
	for (size_t i = 0; i < current->size; i++)
	{
		data[i] = *(current->address + i);
	}
	myFree(ptr);
	int8_t* ptrNew = (int8_t*)myMalloc(sizeof(int8_t) * size);
	if (ptrNew == NULL)
	{
		return NULL;
	}
	for (size_t i = 0; i < current->size; i++)
	{
		ptrNew[i] = *(data + i);
	}
	myFree(data);
	return (void*)ptrNew;
}

void freeMemory()
{
	free(memory);
}