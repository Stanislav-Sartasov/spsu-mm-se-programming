#include "allocator.h"

#include <stdio.h>
#include <stdlib.h>


int main()
{

    printf("The program testing function: myMalloc, myRealloc, myFree\n");

    init();

    printf("\nAllocating %d bytes of memory for the first array: ", 15 * sizeof(int));
    int* first_arr = my_malloc(15 * sizeof(int));
    for (int i = 0; i < 15; i++)
    {
        first_arr[i] = i;
        printf("%i ", first_arr[i]);
    }
    printf("\n");

    my_free(first_arr);


    printf("\nAllocating %d bytes of memory for the second array: ", 5 * sizeof(int));
    int* second_arr = my_malloc(5 * sizeof(int));
    for (size_t i = 0; i < 5; i++)
    {
        second_arr[i] = i;
        printf("%i ", second_arr[i]);
    }
    printf("\n");


    int* digits = my_realloc(second_arr, 10 * sizeof(int));
    if (digits == NULL)
    {
        printf(stderr, "Coudn't reallocate!\n");
        my_free(second_arr);
        exit(1);
    }
    printf("\nReallocating memory and adding new elements from 5 to 9: ");
    for (size_t i = 5; i < 10; i++)
    {
        digits[i] = i;
        printf("%i ", digits[i]);
    }
    printf("\n");

    printf("\nUpdate the second array of %d bytes: ", 10 * sizeof(int));
    for (size_t i = 0; i < 10; i++)
    {
        printf("%i ", digits[i]);
    }
    printf("\n");


    my_free(digits);
    printf("Freed successful\n");

    deinit();
    return 0;
}