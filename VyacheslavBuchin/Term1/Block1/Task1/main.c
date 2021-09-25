#include <stdio.h>

int isPrime(int n)
{
    if (n == 1)
        return 0;
    for (int i = 2; 1ll * i * i <= n; i++)
    {
        if (n % i == 0)
            return 0;
    }
    return 1;
}

int main()
{
    printf("This program prints prime Mersenne numbers in range [1; 2^31 - 1].\n\n");

    for (int i = 1; i <= 31; i++)
    {
        int number = (1ll << i) - 1;
        if (isPrime(number))
            printf("%d\n", number);
    }

    return 0;
}
