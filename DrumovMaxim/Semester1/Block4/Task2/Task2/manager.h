#pragma once

#include <stdlib.h>
#include <stdio.h>
#include <stdint.h>
#include <stdbool.h>

#define HEAPSIZE 65536
#define BLOCKSIZE sizeof(block)

typedef struct block
{
	int8_t* address;
	bool availability;
	uint64_t size;
	struct block* prev;
	struct block* next;
} block;


uint8_t* memory;

block* blocks;

void init();
void* myMalloc(size_t size);
void myFree(void* ptr);
void freeMemory();
void* myRealloc(void* ptr, size_t size);