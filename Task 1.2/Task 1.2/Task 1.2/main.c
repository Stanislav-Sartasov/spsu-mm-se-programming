#include <stdio.h>

// The function checks if two integers are coprime
int coprime(int first, int second)
{
    // If two numbers are coprime then their GCD is 1
    while (first != 0 && second != 0)
    {
        if (second > first)
            second %= first;
        else
            first %= second;
    }
    if (first + second == 1)
        return 1;
    else
        return 0;
}

int main()
{
    int a, b, c;
    printf("This programs checks if 3 inputted numbers are coprime Pythagorean numbers\n");
    printf("Please input 3 numbers\n");
    scanf_s("%d%d%d", &a, &b, &c);
    // Pythagorean triple check
    if (a * a + b * b == c * c || a * a + c * c == b * b || c * c + b * b == a * a)
        // Coprime check
        if (coprime(a, b) && coprime(b, c) && coprime(c, a))
            printf("These numbers are coprime Pythagorean triple");
        else
            printf("These numbers are not coprime, but are Pythagorean triple");
    else
        printf("These numbers are not a Pythagorean triple");
    return 0;
}
