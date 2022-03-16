#include <stdlib.h>
#include <string.h>

#include "mmanager.h"

void init()
{
	mmanager.first = NULL;
	mmanager.data = (byte*)malloc(M_SIZE * sizeof(byte));
}

void end()
{
	free(mmanager.data);
}

void* my_malloc(size_t size)
{
	if (sizeof(mblock_t) + size > M_SIZE)
		return NULL;
	
	// if the data is empty or if there is enough space before the first block
	int empty = mmanager.first == NULL;
	int before_first = (byte*)mmanager.first - (byte*)mmanager.data >= sizeof(mblock_t) + size;

	if (empty || before_first)
	{
		mblock_t* block = mmanager.data;
		block->next = mmanager.first;
		block->size = size;

		mmanager.first = block;

		return (void*)(mmanager.data + sizeof(mblock_t));
	}
	//
	
	// if there is enough space between some blocks
	mblock_t* block = mmanager.first;
	while (block->next != NULL)
	{
		if ((byte*)block->next - ((byte*)block + sizeof(mblock_t) + block->size) >= sizeof(mblock_t) + size)
		{
			mblock_t* new_block = (byte*)block + sizeof(mblock_t) + block->size;
			new_block->next = block->next;
			new_block->size = size;
			block->next = new_block;

			return (void*)((byte*)new_block + sizeof(mblock_t));
		}
		block = block->next;
	}
	//

	// if there is not enough space after blocks
	if ((byte*)block + sizeof(mblock_t) * 2 + block->size + size - mmanager.data > M_SIZE)
		return NULL;
	//

	// add the new block at the end
	mblock_t* new_block = (byte*)block + sizeof(mblock_t) + block->size;
	new_block->next = NULL;
	new_block->size = size;
	block->next = new_block;

	return (void*)((byte*)new_block + sizeof(mblock_t));
	//
}

void my_free(void* ptr)
{
	mblock_t* block = mmanager.first;
	if (block == NULL)
		return;

	// if it's the first block
	if (ptr == (byte*)block + sizeof(mblock_t))
	{
		mmanager.first = block->next;
		return;
	}
	//

	// else
	while (block->next != NULL)
	{
		if (block->next == (byte*)ptr - sizeof(mblock_t))
		{
			block->next = block->next->next;
			return;
		}

		block = block->next;
	}
	//
}

void* my_realloc(void* ptr, size_t size)
{
	mblock_t* block = mmanager.first;

	// searching for the block
	while (block != NULL)
	{
		if (block == (byte*)ptr - sizeof(mblock_t))
			break;
		block = block->next;
	}
	//

	// not found
	if (block == NULL)
		return NULL;
	//

	// if there is enough space between current and next block
	// or it is the last block and there is enough space after it
	int enough_between = block->next != NULL && (byte*)block->next - ((byte*)block + sizeof(mblock_t)) >= size;
	int enough_after = block->next == NULL && (byte*)block + sizeof(mblock_t) + size - (byte*)mmanager.data <= M_SIZE;

	if (enough_between || enough_after)
	{
		block->size = size;
		return ptr;
	}
	//

	// else
	void* new_memory = my_malloc(size);
	if (new_memory == NULL)
		return NULL;

	memcpy(new_memory, (byte*)block + sizeof(mblock_t), block->size < size ? block->size : size);

	my_free((byte*)block + sizeof(mblock_t));

	return new_memory;
	//
}

// I made this at 4:05 am...
void visualize()
{
	printf("[");

	if (mmanager.first == NULL)
	{
		for (int i = 0; i < M_SIZE; i++)
			printf(".");
		printf("]\n");
		return;
	}

	for (int i = 0; i < (byte*)mmanager.first - (byte*)mmanager.data; i++)
		printf(".");

	mblock_t* block = mmanager.first;
	while (1)
	{
		for (int i = 0; i < sizeof(mblock_t); i++)
			printf("O");

		for (int i = 0; i < block->size; i++)
			printf("#");

		if (block->next == NULL)
			break;

		for (int i = 0; i < (byte*)block->next - ((byte*)block + sizeof(mblock_t) + block->size); i++)
			printf(".");

		block = block->next;
	}

	for (int i = 0; i < M_SIZE - ((byte*)block + sizeof(mblock_t) + block->size - mmanager.data); i++)
		printf(".");

	printf("]\n\n");
}