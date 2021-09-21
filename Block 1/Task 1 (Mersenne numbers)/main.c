#include <stdio.h>

int is_prime(unsigned int n)
{
    for (unsigned int d = 2; d*d < n; ++d)
    {
        if (n % d == 0)
        {
            return 0;
        }
    }
    return (n != 1);
}

int main()
{
    printf("This program displays all Mersenne primes on the segment [1; 2^31 - 1]\n");
    unsigned int m = 1;
    for (int i = 0; i < 31; ++i)
    {
        m *= 2;
        if (is_prime(m-1))
        {
            printf("%d\n", m-1);
        }
    }
}