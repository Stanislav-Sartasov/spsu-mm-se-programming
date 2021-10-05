#include <stdio.h>

/// Returns the integer part of sqrt(a) where sqrt is square root
long long intPartOfSqrt(long long a)
{
    long long left = 1, right = (long long)3e9;
    while (right - left > 1)
    {
        long long middle = (right + left) / 2;
        if (middle * middle > a)
            right = middle;
        else
            left = middle;
    }
    return left;
}

/// Returns the integer part of (sqrt(radical) + *c) / *denominator where sqrt is square root
long long intPartOfFraction(long long radical, long long *c, long long *denominator)
{
    long long radicalIntPart = intPartOfSqrt(radical);
    long long result = (radicalIntPart + *c) / *denominator;

    *c = -((radicalIntPart + *c) % *denominator - radicalIntPart);
    *denominator = (radical - (*c * *c)) / *denominator;

    return result;
}

int isSquare(long long a)
{
    long long intSqrt = intPartOfSqrt(a);
    return a == intSqrt * intSqrt;
}

void input(long long *radical)
{
    printf("Enter a positive integer that is not the square of any integer:\n");
    while (1)
    {
        int correctlyScanned = scanf("%lld", radical);
        if (correctlyScanned == 1 && *radical > 0)
        {
            if (!isSquare(*radical))
                break;
            else
                printf("Your input is a square of some integer. Please, try again:\n");
        }
        else
        {
            while (fgetc(stdin) != '\n')
                ;
            printf("Your input is not a positive integer. Please, try again:\n");
        }
    }
}

void printContinuedFraction(long long radical)
{
    printf("The square root of the given number can be represent as the following continued fraction:\n");

    long long constant = 0, denominator = 1;
    printf("[%lld", intPartOfFraction(radical, &constant, &denominator));

    long long start_const = constant, start_den = denominator;
    int cycleSize = 0;
    do
    {
        printf(", %lld", intPartOfFraction(radical, &constant, &denominator));
        cycleSize++;
    }
    while (constant != start_const || denominator != start_den);

    printf("].\n");
    printf("The cycle length of this continued fraction is %d.\n", cycleSize);
}

int main()
{
    printf("This program calculates the continued fraction of the given number square root and the length of its cycle.\n\n");

    long long radical;
    input(&radical);
    printContinuedFraction(radical);

    return 0;
}
