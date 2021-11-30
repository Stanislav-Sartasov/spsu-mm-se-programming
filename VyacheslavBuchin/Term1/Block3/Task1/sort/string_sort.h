//
// Created by Вячеслав Бучин on 26.10.2021.
//

#ifndef TASK1_STRING_SORT_H
#define TASK1_STRING_SORT_H

#include <stdlib.h>

int stringComparator(const void* left, const void* right);

void stringSort(char** begin, char** end, int (*compare) (const void*, const void*));

#endif //TASK1_STRING_SORT_H
