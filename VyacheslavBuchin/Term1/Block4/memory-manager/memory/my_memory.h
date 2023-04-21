//
// Created by Вячеслав Бучин on 16.12.2021.
//

#ifndef MEMORY_MANAGER_MY_MEMORY_H
#define MEMORY_MANAGER_MY_MEMORY_H

#include <stdlib.h>

void init();
void give_back();

void* my_malloc(size_t size);

void my_free(void* ptr);

void* my_realloc(void* ptr, size_t size);

#endif //MEMORY_MANAGER_MY_MEMORY_H
