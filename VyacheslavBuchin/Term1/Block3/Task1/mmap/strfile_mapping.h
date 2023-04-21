//
// Created by Вячеслав Бучин on 30.10.2021.
//

#ifndef TASK1_STRFILE_MAPPING_H
#define TASK1_STRFILE_MAPPING_H

#include <sys/mman.h>

size_t mapFile(void** destination, int fileDescriptor, int prot, int flag);

int unmapFile(void* ptr, size_t size);

void parseFile(char* begin, const char* end, char** destination, char symbol);

#endif //TASK1_STRFILE_MAPPING_H
