#include <stdio.h>
#include <stdlib.h>
#include <string.h>

const int mem_size = 1024 * 1024 * 1024;

struct MemPart
{
    unsigned int sz;
    char array[];
};

char* mem;


int isEnough(size_t size, char* memstart)
{
    if (size == 0) return 1;

    int k = (int)size;

    while ((*memstart == 0) && (k > 0))
    {
        k--;
        memstart++;
    }

    if (k == 0)
        return 0;
    else
        return k;

}

unsigned int* searchForMem(size_t size, char* start)
{
    if (start >= mem + mem_size) return NULL;
    char* memlook = start;

    while (*memlook != 0)
    {
        memlook += *((int*)memlook) + sizeof(int);
        if (memlook != mem) memlook++;
    }


    int available = isEnough(size, memlook);

    if (!available)
    {
        return (unsigned int*)memlook;
    }
    else
    {
        searchForMem(size, memlook + size - available);
    }
}

void initialize()
{
    mem = calloc(mem_size, 1);
}

void* myMalloc(size_t size)
{
    struct MemPart* memory = (struct MemPart*)searchForMem(size + sizeof(char) + sizeof(int),
        mem);

    if (memory == NULL) return NULL;

    memory->sz = (unsigned int)size;

    char* end = memory->array + size;
    *end = 0;

    return memory->array;
}

void myFree(void* ptr)
{
    unsigned int* frptr = (char*)ptr - sizeof(int);

    memset(frptr, 0, *frptr + sizeof(int));
}

void* myRealloc(void* ptr, size_t new_size)
{
    char* new_ptr = (char*)ptr - sizeof(int);
    unsigned int old_size = *((unsigned int*)new_ptr);

    if (old_size > new_size) return ptr;

    unsigned int delta = (unsigned int)new_size - old_size;

    new_ptr += old_size + sizeof(int);

    if (!isEnough(delta + sizeof(char), new_ptr))
    {
        *(new_ptr + delta) = 0;
        *((unsigned int*)((char*)ptr - sizeof(int))) = (unsigned int)new_size;

        return ptr;
    }
    else
    {
        new_ptr = myMalloc(new_size);
        memcpy(new_ptr, ptr, old_size);
        myFree(ptr);

        return new_ptr;
    }
}

int main()
{
    system("chcp 1251");
    system("cls");
    initialize();

    int* variable = myMalloc(sizeof(int));

    *variable = 42;

    printf("Выделенная память для одной целочисленной переменной. Его адрес %p и значение %d \n", variable, *variable);

    int* arrayofint = myMalloc(sizeof(int) * 10);

    printf("\nВыделенная память для целочисленной массива размером в 10 элементов (тип Interger). Адрес массива %p и значения:\n", arrayofint);


    for (int i = 0; i < 10; i++)
    {
        arrayofint[i] = i + 1;
        printf("%d ", arrayofint[i]);
    }

    double* arrayofdouble = myMalloc(sizeof(double) * 5);

    printf("\n\nВыделенная память для массива с плавающей запяятой (тип Double), Размер равен - 5, его адрес % p, элементы:\n ", arrayofdouble);

    for (int j = 0; j < 5; j++)
    {
        arrayofdouble[j] = j + 1;
        printf("%lf ", arrayofdouble[j]);
    }

    arrayofdouble = myRealloc(arrayofdouble, sizeof(double) * 7);

    arrayofdouble[5] = 6;
    arrayofdouble[6] = 7;

    printf("\n\nПереназначенная память для второго массива, его новый размер равен 7, а его адрес %p (он не изменился, поскольку достаточно места для его расширения), его элементы:\n ", arrayofdouble);

    for (int i = 0; i < 7; i++)
    {
        printf("%lf ", arrayofdouble[i]);
    }

    arrayofint = myRealloc(arrayofint, sizeof(int) * 15);

    for (int j = 10; j < 15; j++)
    {
        arrayofint[j] = j + 1;
    }

    printf("\n\nПерераспределенная память для первого массива,  его новый размер равен 15, а его адрес %p (он был изменен из-за нехватки места для перераспределения). Его элементы:\n", arrayofint);

    for (int j = 0; j < 15; j++)
    {
        printf("%d ", arrayofint[j]);
    }

    myFree(arrayofdouble);
    myFree(arrayofint);
    myFree(variable);

    printf("\n\nПамять сейчас свободна");

    free(mem);

    return 0;
}


