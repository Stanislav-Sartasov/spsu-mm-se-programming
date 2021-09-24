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
    printf("This programm checks if 3 given natural numbers are Pythagorean triple and if they form a primitive Pythagorean triple\n");

    int x, y, z;
    char after;
    printf("Please enter three natural numbers: ");
    while (scanf("%d %d %d%c", &x, &y, &z, &after) != 4 || x <= 0 || y <= 0 || z <= 0 || after != '\n') 
    {
        printf("Invalid input error: you must enter three natural numbers\n");
        while (after != '\n') scanf("%c", &after);
        after = '\0';
        printf("Please enter three numbers: ");
    }

    if (x * x + y * y == z * z || x * x + z * z == y * y || y * y + z * z == x * x) 
    {
        if (gcd(gcd(x, y), z) == 1)
        {
            printf("This triple is a primitive Pythagorean triple\n");
        } 
        else
        {
            printf("This triple is Pythagorean, but not primitive\n");
        }
    }
    else
    {
        printf("This triple is not Pythagorean\n");
    }
    return 0;
}
