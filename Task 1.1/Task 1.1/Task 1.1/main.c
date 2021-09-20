#include <stdio.h>

// Checks if the given number is prime
int is_prime(long number)
{
    for (unsigned int i = 2; i * i < number; i++)
    {
        if (number % i == 0)
        {
            return 0;
        }
    }
    if (number == 1)
    {
        return 0;
    }
    return 1;
}

int main()
{
    printf("This program outputs Mersenne primes in range [1, 2^31-1]\n");
    // This variable represents two to the power of n and every iteration is multiplied by 2
    unsigned int n = 1;
    for (int i = 0; i < 31; i++)
    {
        n *= 2;
        if (is_prime(n - 1))
        {
            printf("%ld\n", n - 1);
        }
    }
    return 0;
}
