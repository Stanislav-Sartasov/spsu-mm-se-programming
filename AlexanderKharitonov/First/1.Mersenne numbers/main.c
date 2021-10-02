#include <stdio.h>
#include <math.h>
#include <stdbool.h>

bool is_prime_number(int number) //Default prime check
{ //Default prime check
    if (number == 1)
        return false;
    for (int i = 2; i <= sqrt(number); i++)
    {
        if (number % i == 0)
            return false;
    }
    return true;
}

bool lucas_lehmer_primality_test(int p) //Lucas–Lehmer primality test https://en.wikipedia.org/wiki/Lucas%E2%80%93Lehmer_primality_test
{
    if (p == 2)
        return true;
    if (!is_prime_number(p))
        return false;
    const long long unsigned m_p = (long long unsigned) pow(2, p) - 1;
    long long unsigned s = 4;
    int i;
    for (i = 3; i <= p; i++)
    {
        s = (s * s - 2) % m_p;
    }
    return s == 0;
}

int main()
{
    printf("This program will write all Mersenne prime numbers that are less than 2^31\n");
    for (int i = 2; i <= 31; i++)
    {
        if (lucas_lehmer_primality_test(i))
            printf("%lld ", (long long) pow(2, i) - 1);
    }
}
