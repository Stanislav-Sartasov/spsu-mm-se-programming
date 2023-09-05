#ifndef MYMEM_H
#define MYMEM_H

void init();

void destroy();

void* my_malloc(size_t size);

void my_free(void* ptr);

void* my_realloc(void* ptr, size_t size);

#endif