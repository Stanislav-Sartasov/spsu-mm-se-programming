#pragma once

struct block
{
	size_t size;
	struct block* next;
	struct block* back;
	char free;
	char* data;
};

char* memory;

void myFree(void* pointer);
void* myRealloc(void* pointer, unsigned int size);
void* myMalloc(unsigned int size);
void print();
void init();