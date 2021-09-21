#include <stdio.h>

#define NOT_PYTHAGOREAN 0
#define PYTHAGOREAN 1
#define PRIME_PYTHAGOREAN 2

void swap(int* a, int* b)
{
    int t = *a;
    *a = *b;
    *b = t;
}

int gcd(int a, int b)
{
    while (a != 0)
    {
        if (b >= a)
            b %= a;
        swap(&a, &b);
    }
    return b;
}

void inputTriple(int *a, int *b, int *c)
{
    printf("Enter three positive integers separated by space:\n");
    while (1)
    {
        int correctlyRead = scanf("%d %d %d", a, b, c);
        if (correctlyRead == 3 && *a > 0 && *b > 0 && *c > 0)
            break;
        else
        {
            fflush(stdin);
            printf("At least one of your inputs is not a positive integer. Please, try again:\n");
        }
    }
}

int getTripleType(int a, int b, int c)
{
    if (c < a)
        swap(&a, &c);
    if (c < b)
        swap(&b, &c);

    if (1ull * c * c == 1ull * a * a + 1ull * b * b)
        return (gcd(a, gcd(b, c)) == 1) ? PRIME_PYTHAGOREAN : PYTHAGOREAN;
    return NOT_PYTHAGOREAN;
}

void printAnswer(int tripleType)
{
    switch (tripleType)
    {
        case PYTHAGOREAN:
            printf("The given triple is Pythagorean\n");
            break;
        case PRIME_PYTHAGOREAN:
            printf("The given triple is prime Pythagorean\n");
            break;
        default:
            printf("The given triple is not Pythagorean\n");
    }
}

int main()
{
    printf("This program checks if the given triple is a Pythagorean triple or a prime Pythagorean triple or none of those.\n\n");

    int a, b, c;
    inputTriple(&a, &b, &c);
    printAnswer(getTripleType(a, b, c));

    return 0;
}
