#include <stdio.h>

int lcd(unsigned int a, unsigned int b)
{
    while ((a != 0) && (b != 0))
    {
        if (a > b)
            a %= b;
        else
            b %= a;
    }
    return a + b;
}

int is_pythagorean_triple(unsigned int a, unsigned int b, unsigned int c)
{
    return (a * a + b * b == c * c) || (a * a + c * c == b * b) || (b * b + c * c == a * a);
}

int main()
{
    unsigned int a, b, c;
    printf("This program checks that inputted triples are Pythagorean and prime\n");
    printf("Please enter 3 natural numbers - the a, b and c in the Pythagorean formula (a^2 + b^2 = c^2):\n");
    scanf("%d%d%d", &a, &b, &c);
    if (is_pythagorean_triple(a, b, c))
    {
        if (lcd(a, b) == 1 && lcd(b, c) == 1 && lcd(a, c) == 1)
        {
            printf("Inputted numbers are prime Pythagorean triples");
        }
        else
        {
            printf("Inputted numbers are Pythagorean triples");
        }
    }
    else
    {
        printf("Inputted numbers are not Pythagorean triples");
    }
    return 0;
}
