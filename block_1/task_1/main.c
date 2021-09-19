#include <stdio.h>

int is_prime(long long n) {
    if (n == 1)
        return 0;

    for (long long j = 2; j * j <= n; j++) 
    {
        if (n % j == 0) 
            return 0;
    }
    return 1;
}

int main() {
    printf("This programm prints out the Mersenne prime numbers\n");


    for (long long i = 2; i <= (1LL << 31); i *= 2)
    {
        if (is_prime(i - 1)) 
        {
            printf("%lld\n", i - 1);
        }
    }
    return 0;
}