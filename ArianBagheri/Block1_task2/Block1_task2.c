//Homework 1.2
//Pythagorean triplets

#include <stdio.h>
#include <stdbool.h>

void get_numbers(int* number1Ptr, int* number2Ptr, int* number3Ptr, char* after_number_ptr);
void print_pythagorean_triple(int* number1Ptr, int* number2Pptr, int* number3Ptr, int* check_number_ptr);
int computing_pythagorean_triple(int* number1Ptr, int* number2Ptr, int* number3Ptr);
void print_prime_pythagorean(int number1, int number2, int number3, int check_number);
int isPrimePythagorean1(int number1, int number2, int number3);
int isPrimePythagorean2(int x, int y);

int main()
{
    int number1, number2, number3, check_number;
    char after_number;

    printf("\nPlease enter three natural numbers, then this program will ");
    printf("print whether they are Pythagorean triple or not, and\n");
    printf("if so, whether they are also prime Pythagorean triple or not.");
    printf("The order in which numbers are entered is arbitrary.\n\n");

    get_numbers(&number1, &number2, &number3, &after_number);
    print_pythagorean_triple(&number1, &number2, &number3, &check_number);
    print_prime_pythagorean(number1, number2, number3, check_number);
    isPrimePythagorean1(number1, number2, number3);

    return 0;
}

void get_numbers(int* number1Ptr, int* number2Ptr, int* number3Ptr, char* after_number_ptr)
{
    int correct_input;

    printf("Please enter three natural numbers:\n");

    while (true)
    {
        correct_input = scanf_s("%d%d%d%c", number1Ptr, number2Ptr, number3Ptr, after_number_ptr);

        if (correct_input == 4 && *number1Ptr > 0 && *number3Ptr > 0
            && *number3Ptr > 0 && *after_number_ptr == '\n')
        {
            break;
        }
        else
        {
            while (*after_number_ptr != '\n')
            {
                scanf_s("%c", after_number_ptr);
            }

            *after_number_ptr = '\0';

            printf("Error! The number(s) entered are incorrect. Please enter ");
            printf("three natural numbers.\n");
        }
    }
}

void print_pythagorean_triple(int* number1Ptr, int* number2Ptr, int* number3Ptr, int* check_number_ptr)
{
    if (computing_pythagorean_triple(number1Ptr, number2Ptr, number3Ptr))
    {
        printf("This is a pythagorean triple.\n");
        *check_number_ptr = true;
    }
    else
    {
        printf("This is Not a pythagorean triple.\n");
        *check_number_ptr = false;
    }
}

int computing_pythagorean_triple(int* number1Ptr, int* number2Ptr, int* number3Ptr)
{
    if (*number1Ptr >= *number2Ptr && *number1Ptr >= *number3Ptr && (*number2Ptr *
        *number2Ptr + *number3Ptr * *number3Ptr == *number1Ptr * *number1Ptr))
    {
        return true;
    }
    else if (*number2Ptr >= *number1Ptr && *number2Ptr >= *number3Ptr && (*number1Ptr *
        *number1Ptr + *number3Ptr * *number3Ptr == *number2Ptr * *number2Ptr))
    {
        return true;
    }
    else if (*number3Ptr >= *number2Ptr && *number3Ptr >= *number1Ptr && (*number2Ptr *
        *number2Ptr + *number1Ptr * *number1Ptr == *number3Ptr * *number3Ptr))
    {
        return true;
    }
    else
    {
        return false;
    }
}

void print_prime_pythagorean(int number1, int number2, int number3, int check_number)
{
    if (isPrimePythagorean1(number1, number2, number3) && check_number == true)
    {
        printf("This is a prime (primitive) pythagorean triple.\n");
    }
    else
    {
        printf("This is Not a prime (primitive) pythagorean triple.\n");
    }
}

int isPrimePythagorean1(int number1, int number2, int number3)
{
    if (isPrimePythagorean2(number1, number2) == 1 &&
        isPrimePythagorean2(number1, number3) == 1 &&
        isPrimePythagorean2(number2, number3) == 1)
    {
        return true;
    }
    else
    {
        return false;
    }
}

int isPrimePythagorean2(int x, int y)
{
    int z = 0, t = 0;

    if (x > y || y > x)
    {
        if (y > x)
        {
            t = y;
            y = x;
            x = t;
        }
        while (y != 0)
        {
            z = x % y;
            x = y;
            y = z;
        }
    }
    return x;
}