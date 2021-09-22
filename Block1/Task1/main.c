#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>

#define int64 __int64_t
#define SWAP(a, b) do { typeof(a) tmp = a; a = b; b = tmp; } while (0)


int bin_pow(int64 a, int64 n, int64 m)
{
    int64 res = 1;
    while (n)
    {
        if (n & 1)
            res *= a;
        a *= a;
        a %= m;
        res %= m;
        n >>= 1;
    }
    return res;
}

int64 gcd(int64 a, int64 b)
{
    while (b)
    {
        a %= b;
        SWAP(a, b);
    }
    return a;
}

bool is_prime(int64 n)
{
    // Discard simple cases
    if (n == 2 || n == 3)
        return true;
    if (n < 2 || n % 2 == 0)
        return false;

    // Fermat primality test

    for (int i = 0; i < 15; i++)
    {
        int64 a = rand() % (n - 1) + 1;
        if (bin_pow(a, n - 1, n) != 1)
            return false;
    }

    // Miller-Rabin primality test

    int64 s = 0;
    while ((n - 1) % (1 << (s + 1)) == 0)
        s++;
    int64 t = (n - 1) / (1 << s);
    for (int i = 0; i < 15; i++)
    {
        int64 a = (rand() % (n - 3)) + 2;
        int64 x = bin_pow(a, t, n);
        if (x == 1 || x == n - 1)
            continue;
        bool flag = true;
        for (int j = 0; j < s - 1; j++)
        {
            x = (x * x) % n;
            if (x == 1)
                return false;
            if (x == n - 1)
            {
                flag = false;
                break;
            }
        }
        if (flag)
            return false;
    }
    return true;
}

int main()
{
    printf("Mersenne prime\n");
    for (int i = 1; i <= 31; i++)
    {
        if (is_prime(((int64)1 << i) - 1))
            printf("%d\n", ((int64)1 << i) - 1);
    }
    return 0;
}
