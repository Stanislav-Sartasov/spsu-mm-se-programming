#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <math.h>
#include <stdbool.h>

bool isPrime(int n)
{
    for (int i = 2; i <= (n / 2); ++i)
    {
        if (!(n % i))
        {
            return false;
        }
    }
    return true;
}

int main(int argc, char** argv)
{
    int N = 31;
    for (int i = 1; i <= N; ++i)
    {
        int p = (int)pow(2, i);
        if (isPrime(i))
        {
            if (isPrime(p - 1))
            {
                printf("pow(2,%d) - 1 = %d\n", i, p - 1);
            }
        }
    }
    return 0;
}