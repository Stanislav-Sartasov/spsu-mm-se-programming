#include <stdio.h>
#include <math.h>


void input(char string[], long long *a);

int main()
{
    long long D;
    printf("This program prints the continued fraction of square root of given natural number and it's period\n");
    input("Please enter natural number:\n", &D);
    unsigned long R = floor(sqrt(D));
    int k = 1;
    printf("[ %lu ", R);
    unsigned long a = R, P = 0, Q = 1;
    do {
        P = a * Q - P;
        Q = (D - P * P) / Q;
        a = (R + P) / Q;
        printf("%lu ", a);
        k += 1;
    }
    while(Q != 1);
    printf("]\nPeriod is: %d", k);
}

void input(char string[], long long *a)
{
    int result;
    do
    {
        printf(string);
        result = scanf("%lld", a);
        fflush(stdin);
    }
    while (!(result == 1 && *a > 0));
}