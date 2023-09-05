//
// Created by Вячеслав Бучин on 16.12.2021.
//

#ifndef MEMORY_MANAGER_MEMORY_BLOCK_H
#define MEMORY_MANAGER_MEMORY_BLOCK_H

#include <stdlib.h>
#include <inttypes.h>

typedef struct block
{
	struct block* next;
	size_t size;
	void* data;
} memory_t;

void init_block(memory_t* block, memory_t* next, size_t size);

typedef uint8_t byte_t;


#endif //MEMORY_MANAGER_MEMORY_BLOCK_H
