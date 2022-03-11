#include <stdio.h>
#include <stdlib.h>
#include "big_int.h"

int main()
{
    printf("This program calculates (3^5000) via long arithmetics alghoritms and prints it in hex\n");
    big_int number;
    number = power(3, 5000);
    print_big_int_hex(number);
    free(number.digits);
    return 0;
}