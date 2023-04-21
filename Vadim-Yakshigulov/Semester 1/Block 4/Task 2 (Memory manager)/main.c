#include "memory_manager.h"
#include <stdio.h>

void print_block_info(allocated_block_t *block)
{
	printf("[prev:%x; id:%x; next:%x; size:%d; is_free:%d]\n", block->prev_block, block, block->next_block, block->size, block->is_free);
}

void print_blocks_structure(allocated_block_t *parent_block)
{
	printf("\nBlocks structure is:\n");
	while (parent_block != NULL)
	{
		print_block_info(parent_block);
		parent_block = parent_block->next_block;
	}
	printf("\n");
}

int main()
{
	init();
	printf("This program tests my_malloc, my_free and my_realloc realizations\n");
	print_blocks_structure(initial_block);

	printf("Allocating 5*5 matrix\n");
	int** p = my_malloc(5 * sizeof(int*));
	for (int i = 0; i < 5; i++)
		p[i] = my_malloc(5 * sizeof(int));

	for (int i = 0; i < 5; i++)
		for (int j = 0; j < 5; j++)
			p[i][j] = i + j;

	print_blocks_structure(initial_block);

	for (int i = 0; i < 5; i++)
	{
		for (int j = 0; j < 5; j++)
			printf("%d ", p[i][j]);
		printf("\n");
	}

	printf("\nReallocating from 5*5 to 5*2 matrix\n");
	for (int i = 0; i < 5; i++)
		p[i] = my_realloc(p[i], 2 * sizeof(int));

	print_blocks_structure(initial_block);

	for (int i = 0; i < 5; i++)
	{
		for (int j = 0; j < 2; j++)
			printf("%d ", p[i][j]);
		printf("\n");
	}

	for (int i = 0; i < 5; i++)
		my_free(p[i]);
	my_free(p);

	print_blocks_structure(initial_block);
	destroy();
}