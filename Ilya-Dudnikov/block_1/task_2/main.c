#include <stdio.h>

int gcd(int a, int b) 
{
    if (b == 0) 
        return a;
    return gcd(b, a % b);
}

int get_input()
{
    while (1) 
    {
        printf("Enter the next number: ");
        int x;
        char tmp = '\0';
        int wft;
        if (scanf("%d%c", &x, &tmp) != 2 || tmp != '\n' || x <= 0) 
        {
            printf("Invalid input: you must enter a natural number\n");
            while (tmp != '\n')
                scanf("%c", &tmp);
        }
        else
        {
            return x;
        }
    }
}

int main() 
{
    printf("This programm checks if 3 given natural numbers are Pythagorean triple and if they form a prime Pythagorean triple\n");

    int x, y, z;
    x = get_input(), 
    y = get_input(), 
    z = get_input();

    if (x * x + y * y == z * z || x * x + z * z == y * y || y * y + z * z == x * x) 
    {
        if (gcd(x, y) == 1 && gcd(x, z) == 1 && gcd(y, z) == 1)
        {
            printf("This triple is a prime Pythagorean triple\n");
        } 
        else
        {
            printf("This triple is Pythagorean, but not prime\n");
        }
    }
    else
    {
        printf("This triple is not Pythagorean\n");
    }
    return 0;
}