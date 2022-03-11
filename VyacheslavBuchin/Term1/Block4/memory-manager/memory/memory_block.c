//
// Created by Вячеслав Бучин on 16.12.2021.
//

#include "memory_block.h"

void init_block(memory_t* block, memory_t* next, size_t size)
{
	block->next = next;
	block->size = size;
	block->data = (byte_t*)block + sizeof(memory_t);
}