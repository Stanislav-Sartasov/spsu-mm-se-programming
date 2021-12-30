#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include "mman.h"
#include <stdlib.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <errno.h>
#include <string.h>
#include <stdarg.h>

struct string
{
    char* array;
    int len;
};

int comparing(struct string* a, struct string* b)
{
    return strcmp(a->array, b->array);
}

int main(int argc, char* argv[])
{
    int i, strings_number, str_start, str_end, strings_count;
    int fdin, fdout;
    char* src, * dst;
    struct stat statbuf;
    strings_number = 1;
    char* arr;
    arr = (char*)malloc(5 * sizeof(char));
    printf("This programm sorts strings from input file and writes sorted strings to the output file\n");
    if (argc != 3)
    {
        printf("Not enough arguments or too many arguments\n");
    }
    fdin = open(argv[1], O_RDONLY);
    fdout = open(argv[2], O_RDWR | O_TRUNC | O_CREAT, S_IWRITE);
    if (fdin < 0)
    {
        printf("Error reading <%s> file\n", argv[1]);
    }
    if (fdout < 0)
    {
        printf("Error reading <%s> file\n", argv[2]);
    }
    if (fdin == NULL)
    {
        printf("Cannot open %s for the reading\n", argv[1]);
    }
    if (fdout == NULL)
    {
        printf("Cannot create %s for the writing\n", argv[2]);
    }
    if (fstat(fdin, &statbuf) < 0)
    {
        printf("fstat error\n");
    }
    if ((src = mmap(0, statbuf.st_size, PROT_READ, MAP_SHARED, fdin, 0)) == MAP_FAILED)
    {
        printf("Error in calling mmap function for the input file\n");
    }
    if ((dst = mmap(0, statbuf.st_size, PROT_READ | PROT_WRITE, MAP_SHARED, fdout, 0)) == MAP_FAILED)
    {
        printf("Error in calling mmap function for the output file\n");
    }
    /* Counting strings */
    for (i = 0; i < statbuf.st_size; ++i)
    {
        if (src[i] == '\n')
        {
            ++strings_number;
        }
    }
    struct string* strings = malloc(strings_number * sizeof(struct string));
    str_start = 0;
    strings_count = 0;
    /* Reading strings */
    for (i = 0; i < statbuf.st_size; i++)
    {
        if (src[i] == '\n')
        {
            strings[strings_count].len = i - str_start;
            strings[strings_count].array = &src[str_start];
            str_start = i + 1;
            strings_count++;
        }
    }
    /* Reading last string */
    strings[strings_count].array = malloc(sizeof(char) * i - str_start + 2);
    int k = 0;
    for (k = 0; k < i + 2 - str_start; k++)
    {
        strings[strings_count].array[k] = src[str_start + k];
    }
    strings[strings_count].len = i - str_start + 2;
    strings[strings_count].array[i - str_start + 1] = '\n';
    /* Sorting and writing strings */
    qsort(strings, strings_number, sizeof(struct string), comparing);
    for (i = 0; i < strings_number; i++)
    {
        write(fdout, strings[i].array, strings[i].len * sizeof(char));
    }
    printf("Strings are sorted and writed to the output file\n");
    free(strings);
    munmap(src, statbuf.st_size);
    munmap(dst, statbuf.st_size);
    close(fdin);
    close(fdout);
    return 0;
}