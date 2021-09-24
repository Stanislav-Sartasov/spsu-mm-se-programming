
#include <stdio.h>

int is_prime(int number)
{
    if (number == 1) return 0;
    for (unsigned int i = 2; i * i <= number; i++)
    {
        if (!(number % i)) return 0;
    }
    return 1;
}

int main()
{
    printf("This program prints all Mersenne prime numbers on the [1; 2^31 - 1] interval.\n");
    unsigned int current = 1;
    for (int n = 0; n < 32; n++)
    {
        current *= 2;
        if (is_prime(current - 1))
        {
            printf("%d\n", current - 1);
        }
    }
    return 0;
}
