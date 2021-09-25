#include <stdio.h>
#include <math.h>

short is_prime(unsigned long long number)
{
    if (number == 1 || number == 0)
    {
        return 0;
    }
    for (unsigned long long i = 2; i < (unsigned long long)sqrt(number) + 1; ++i)
    {
        if (number % i == 0)
        {
            return 0;
        }
    }
    return 1;
}

int main() 
{
    printf("Description: This program will write all Mersene prime numbers until 2^31-1\n");

    const int until_power = 31;
    unsigned long long current_number = 4;
    for (int i = 2; i <= until_power; ++i) 
    {
        if (is_prime(current_number - 1)) 
        {
            printf("%llu ", current_number - 1);
        }
        current_number *= 2;
    }

    return 0;
}