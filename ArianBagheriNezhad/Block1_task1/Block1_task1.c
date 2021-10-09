//Homework 1.1
//Mersenne Prime Numbers
//The first method:

#include <stdio.h>
#include <math.h>
#include <stdbool.h>

#define RANGE 31

int prime_number(int number)
{
    int i;

    if (number == 1) {
        return false;
    }
    else {
        for (i = 2; i <= (int)sqrt(number); i++) //i <= number or i < sqrt(number)
        {  
            if (number % i == 0) {
                return false;
                break;
            }
        }
    }
    return true;
}

int main()
{
    int i, mersenne_numbers;

    printf("\nThis program prints Mersenne prime numbers in the range [1; 2^31 - 1]:\n\n");

    for (i = 1; i <= RANGE; i++) {
        mersenne_numbers = pow(2, (i)) - 1;

        if (prime_number(mersenne_numbers)) {
            printf("%d\t", mersenne_numbers);
        }
    }

    return 0;
}


//The second method:

/*
#include <stdio.h>
#include <math.h>

#define NATURAL_NUMBER 31
#define TRUE 1
#define FALSE 0

void initialize_The_Array_To_Zero1(int * array_Of_M_Numbers_Ptr);
void initialize_The_Array_To_Zero2(int *array_Of_P_Numbers_ptr);
void calculation_Of_Mersenne_Prime_Numbers(int * array_Of_M_Numbers_Ptr);
int prime_number1 (int * array_Of_M_Numbers_Ptr, int * array_Of_P_Numbers_Ptr);
int prime_number2 (int n);
void print_Mersenne_Prime_Numbers(int * array_Of_M_Numbers_Ptr, int k);

int main()
{
    int array_Of_M_Numbers[NATURAL_NUMBER], array_Of_P_Numbers[NATURAL_NUMBER];  //M ---> Mersenne   P ---> Prime
    int * array_Of_M_Numbers_Ptr = array_Of_M_Numbers, *array_Of_P_Numbers_Ptr = array_Of_P_Numbers;
    int k;

    initialize_The_Array_To_Zero1(array_Of_M_Numbers);
    initialize_The_Array_To_Zero2(array_Of_P_Numbers);
    calculation_Of_Mersenne_Prime_Numbers(array_Of_M_Numbers);
    k = prime_number1 (array_Of_M_Numbers_Ptr, array_Of_P_Numbers_Ptr);
    print_Mersenne_Prime_Numbers(array_Of_P_Numbers_Ptr, k);

    return 0;
}

void initialize_The_Array_To_Zero1(int * array_Of_M_Numbers_Ptr)
{
    int i;

    for (i = 0; i < NATURAL_NUMBER; i++) {
        *(array_Of_M_Numbers_Ptr + i) = 0;
    }
}

void initialize_The_Array_To_Zero2(int * array_Of_P_Numbers_Ptr)
{
    int i;

    for (i = 0; i < NATURAL_NUMBER; i++) {
        *(array_Of_P_Numbers_Ptr + i) = 0;
    }
}

void calculation_Of_Mersenne_Prime_Numbers(int * array_Of_M_Numbers_Ptr)
{
    int i;

    for (i = 1; i <= NATURAL_NUMBER; i++) {
        *(array_Of_M_Numbers_Ptr + i) = (pow(2, i)) - 1;
    }
}

int prime_number1 (int * array_Of_M_Numbers_Ptr, int * array_Of_P_Numbers_Ptr)
{
    int i, k = 0;

    for (i = 1; i <= NATURAL_NUMBER; i++) {
        int number = *(array_Of_M_Numbers_Ptr + i);
        if (prime_number2(number)) {
            *(array_Of_P_Numbers_Ptr + (k++)) = number;
        }
    }

    return k;
}

int prime_number2 (int number) {
    int i;

    if (number == 1) {
        return FALSE;
    }
    else{
     for (i = 2; i <= (int) sqrt (number); i++) {
        if (number % i == 0) {
            return FALSE;
            break;
        }
     }
    }

    return TRUE;
}

void print_Mersenne_Prime_Numbers(int * array_Of_P_Numbers_Ptr, int k)
{
    int i;

    printf("\nThis program prints Mersenne prime numbers in the range [1; 2^31 - 1]:\n\n");

    for (i = 0; i < k; i++) {
        printf("%d    ", *(array_Of_P_Numbers_Ptr + i));
    }
}
*/