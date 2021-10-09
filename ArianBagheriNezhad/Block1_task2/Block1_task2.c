//Homework 1.2
//Pythagorean triplets

#include <stdio.h>
#include <stdbool.h>

void get_numbers(int* number1_ptr, int* number2_ptr, int* number3_ptr, char* after_number_ptr);
void print_pythagorean_triple(int* number1_ptr, int* number2_ptr, int* number3_ptr, int* check_number_ptr);
int Computing_pythagorean_triple(int* number1_ptr, int* number2_ptr, int* number3_ptr);
void print_prime_pythagorean(int number1, int number2, int number3, int check_number);
int is_prime_pythagorean1(int number1, int number2, int number3);
int is_prime_pythagorean2(int x, int y);

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
    is_prime_pythagorean1(number1, number2, number3);

    return 0;
}

void get_numbers(int* number1_ptr, int* number2_ptr, int* number3_ptr, char* after_number_ptr)
{
    int correct_input;

    printf("Please enter three natural numbers:\n");

    while (true) {
        correct_input = scanf_s("%d%d%d%c", number1_ptr, number2_ptr, number3_ptr, after_number_ptr);

        if (correct_input == 4 && *number1_ptr > 0 && *number2_ptr > 0
            && *number3_ptr > 0 && *after_number_ptr == '\n') {
            break;
        }
        else {
            while (*after_number_ptr != '\n') {
                scanf_s("%c", after_number_ptr);
            }

            *after_number_ptr = '\0';

            printf("Error! The number(s) entered are incorrect. Please enter ");
            printf("three natural numbers.\n");
        }
    }
}

void print_pythagorean_triple(int* number1_ptr, int* number2_ptr, int* number3_ptr, int* check_number_ptr)
{
    if (Computing_pythagorean_triple(number1_ptr, number2_ptr, number3_ptr)) {
        printf("This is a pythagorean triple.\n");
        *check_number_ptr = true;
    }
    else {
        printf("This is Not a pythagorean triple.\n");
        *check_number_ptr = false;
    }
}

int Computing_pythagorean_triple(int* number1_ptr, int* number2_ptr, int* number3_ptr)
{
    if (*number1_ptr >= *number2_ptr && *number1_ptr >= *number3_ptr && (*number2_ptr *
        *number2_ptr + *number3_ptr * *number3_ptr == *number1_ptr * *number2_ptr))
    {
        return true;
    }
    else if (*number2_ptr >= *number1_ptr && *number2_ptr >= *number3_ptr && (*number1_ptr *
        *number1_ptr + *number3_ptr * *number3_ptr == *number2_ptr * *number2_ptr))
    {
        return true;
    }
    else if (*number3_ptr >= *number2_ptr && *number3_ptr >= *number1_ptr && (*number2_ptr *
        *number2_ptr + *number1_ptr * *number1_ptr == *number3_ptr * *number3_ptr))
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
    if (is_prime_pythagorean1(number1, number2, number3) && check_number == true) {
        printf("This is a prime (primitive) pythagorean triple.\n");
    }
    else {
        printf("This is Not a prime (primitive) pythagorean triple.\n");
    }
}

int is_prime_pythagorean1(int number1, int number2, int number3)
{
    if (is_prime_pythagorean2(number1, number2) == 1 &&
        is_prime_pythagorean2(number1, number3) == 1 &&
        is_prime_pythagorean2(number2, number3) == 1) {
        return true;
    }
    else {
        return false;
    }
}

int is_prime_pythagorean2(int x, int y)
{
    int z = 0, t = 0;

    if (x > y || y > x) {
        if (y > x) {
            t = y;
            y = x;
            x = t;
        }
        while (y != 0) {
            z = x % y;
            if (z == 0) {
                return y;
                break;
            }
            x = y;
            y = z;
        }
    }
}