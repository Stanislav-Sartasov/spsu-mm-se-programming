#include <stdio.h>
#include <stdlib.h>
#include <string.h>

char* buffer;
int mem_size = 100;

struct header
{
    int size;
    int allocated;
};

void* my_malloc(size_t size)
{
    struct header* current;
    current = (struct header*)buffer;
    while (current->size != 0)
    {
        if ((current->size >= size + 16) && !(current->allocated))
        {
            struct header* new_block;
            new_block = (struct header*)((int)current + size + 8);
            new_block->size = current->size - size - 8;
            new_block->allocated = 0;
            current->size = size + 8;
            current->allocated = 1;
            return current + 1;
        }

        if ((current->size >= size + 8) && !(current->allocated))
        {
            current->allocated = 1;
            return current + 1;
        }

        current = (struct header*)((int)current + (int)current->size);
    }

    if (mem_size - ((int)current - (int)buffer) >= size + 8)
    {
        current->size = size + 8;
        current->allocated = 1;
        return current + 1;
    }
    return NULL;
}

void my_free(void* ptr)
{
    ((struct header*)ptr - 1)->allocated = 0;

    struct header* current;
    current = (struct header*)buffer;
    while (current->size != 0)
    {
        if ((current->allocated == 0) && (((struct header*)((int)current + (int)current->size))->allocated == 0))
        {
            current->size += ((struct header*)((int)current + (int)current->size))->size;
            return;
        }
        current = (struct header*)((int)current + (int)current->size);
    }
}

void* my_realloc(void* ptr, size_t size)
{
    int old_size = ((struct header*)ptr - 1)->size;
    my_free(ptr);
    void* new_pointer = my_malloc(size * sizeof(int));
    if (new_pointer == NULL)
        return NULL;
    else
    {
        for (int i = 0; i < old_size; i++)
            ((char*)new_pointer)[i] = ((char*)ptr)[i];
        return new_pointer;
    }
}

void init()
{
    buffer = (char*)malloc(sizeof(char) * mem_size);

    for (int i = 0; i < mem_size; i++)
        buffer[i] = 0;
}

void init_stop()
{
    free(buffer);
}

void print_buffer()
{
    for (int i = 0; i < mem_size / 4; i++)
        printf("%d ", ((int*)buffer)[i]);
    printf("\n\n");
}

int main()
{
    printf("This program implements a memory manager\n\n");

    int* arr;
    int* arr2;
    int* arr3;
    
    init();
    printf("It's a piece of our memory. It's free\n");
    print_buffer();
    printf("Let's allocate some memory and fill it with integers:\n");

    arr = my_malloc(5 * sizeof(int));
    for (int i = 0; i < 5; i++)
        arr[i] = 1;

    print_buffer();
    printf("What can we see here? The first two numbers are the header.\n");
    printf("The first number is the allocated memory, counting the header.\n");
    printf("The second number is a flag indicating that a busy memory block is coming after it.\n");
    printf("After it we can see our array. Memory allocation is successful!\n");
    printf("Let's add some arrays and try to free it.\n\n");

    arr2 = my_malloc(3 * sizeof(int));
    for (int i = 0; i < 3; i++)
        arr2[i] = 2;
    arr3 = my_malloc(10 * sizeof(int));
    for (int i = 0; i < 10; i++)
        arr3[i] = 3;
    
    printf("Before free:\n");
    print_buffer();

    my_free(arr);
    my_free(arr2);

    printf("After free:\n");
    print_buffer();

    printf("Allocation flags are 0. Now we can rewrite buffer after them. Free is successful!\n");
    printf("The manager automatically merged blocks of memory. We can use it all. Let's check this.\n");

    arr = my_malloc(6 * sizeof(int));
    for (int i = 0; i < 6; i++)
        arr[i] = 4;

    print_buffer();
    printf("We placed 6 elements, although we had 5 of them in the block before free. Merging is successful!\n");
    printf("Let's add some elements to the array. We have to reallocate memory.\n");
    arr = my_realloc(arr, 10);
    for (int i = 0; i < 10; i++)
        arr[i] = 5;
    print_buffer();

    printf("Reallocation is successful!\n");
    printf("This is the power of the memory manager!\n");
    init_stop();
    return 0;
}