#include <stdio.h>

unsigned int lcd(int a, int b);
int is_pythagorean_triple(int a, int b, int c);
void input(char string[], int *a, int *b, int *c);

int main()
{
    int a, b, c;
    printf("This program checks that inputted triples are Pythagorean and prime\n");
    input("Please enter 3 natural numbers satisfying the Pythagorean formula (a^2 + b^2 = c^2):\n", &a, &b, &c);
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

void input(char string[], int *a, int *b, int *c)
{
    int result;
    do
    {
        printf(string);
        result = scanf("%d%d%d", a, b, c);
        fflush(stdin);
    }
    while (!(result == 3 && *a > 0 && *b > 0 && *c > 0));
}

int is_pythagorean_triple(int a, int b, int c)
{
    return (a * a + b * b == c * c) || (a * a + c * c == b * b) || (b * b + c * c == a * a);
}

unsigned int lcd(int a, int b)
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
