#include "big_int.h"

big_int power(int number, int degree)
{
    big_int result = create_big_int(1, 1);
    big_int exp = create_big_int(3, 1);
    while (degree)
    {
        if (degree % 2)
        {
            result = multiply(result, exp);
        }
        exp = multiply(exp, exp);
        degree /= 2;
    }
    
    return result;
}

big_int multiply(big_int a, big_int b)
{
    big_int result = create_big_int(0, a.len + b.len);
    unsigned long long digit;
    for (unsigned i = 0; i < a.len; i++)
    {
        unsigned long long carry = 0;
        for (unsigned j = 0; j < b.len || carry != 0; j++)
        {
            if (j < b.len)
            {
                digit = result.digits[i + j] + (unsigned long long)a.digits[i] * b.digits[j] + carry;
            }
            else
            {
                digit = carry + result.digits[i + j];
            }
            result.digits[i + j] = digit % BASE;
            carry = digit / BASE;
        }
    }
    free(a.digits);
    return result;
}
big_int create_big_int(int value, int size)
{
    big_int number;
    unsigned value_copy = value;
    if (value == 0)
    {
        number.len = size;
        number.digits = (unsigned*)calloc(number.len, sizeof(unsigned));
        if (number.digits == NULL)
        {
            printf("Failed to allocate memory");
            return;
        }
    }
    while (value_copy)
    {
        number.len = size;
        number.digits = (unsigned*)malloc(number.len * sizeof(unsigned));
        if (number.digits == NULL)
        {
            printf("Failed to allocate memory");
            return;
        }
        for (unsigned i = 0; i < number.len; i++)
        {
            number.digits[i] = value_copy % BASE;
            value_copy /= BASE;
        }
        if (value_copy)
        {
            size += 5;
            free(number.digits);
            value_copy = value;
        }
    }
    return number; 
}

int index_without_zeros(big_int number)
{
    int32_t i = number.len - 1;
    while (number.digits[i] == 0)
        i--;
    return i;
}

void print_big_int_hex(big_int number)
{
    int i;
    unsigned digit;
    i = index_without_zeros(number);
    while (i >= 0)
    {
        digit = number.digits[i];
        printf("%X", number.digits[i]);
        i--;
    }
}