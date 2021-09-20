/* "MERSENNE NUMBERS"
These are numbers of the form 2^N - 1, where N is a natural number. We should display all Mersenne primes on the segment [1; 2^31 - 1]. */

#include <stdio.h>
#include <math.h>

int isPrime(unsigned int number)
{
    for (unsigned int i = 2; i <= sqrt(number); i++)
    {
        if (number % i == 0)
            return 0;
    }
    if (number == 1)
        return 0;
    return 1;
}

int main()
{
    printf("There are Mersenne primes on the segment [1; 2^31 - 1]:\n");
    // 2147483647 means 2^31-1
    const unsigned int upper_bound = 2147483647;
    for (unsigned int number = 1; number <= upper_bound; number = (number + 1) * 2 - 1)
    {
        if (isPrime(number))
            printf("%u ", number);
    }
    return 0;
}
