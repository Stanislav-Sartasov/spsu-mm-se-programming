#include <stdio.h>

int drob(int high, int free, int i, int number, int count);

int program();

void flush_stdin(void)
{
    char ch;
    while (scanf_s("%c", &ch) == 1 && ch != '\n')
    {
    }
}

int get_number(int* number, const int top)
{
    return !(scanf_s("%d", number) == 1 && abs(*number) < top);
}

int main()
{
    printf("<<<description: The program represents the square root of a number in the form of a continued fraction and gives the length of the period.>>>\n\n");
    program();
    return 0;
}


int program(void)
{
    int i, ans = 0, n;

    while (printf("Enter number>>") && get_number(&n, 100000000) || n <= 0)
    {
        fprintf(stderr, "Wrong input!  ( use nubmers, > 0  < 10^8 ) \n");
        flush_stdin();
    }
    for (i = 1; ((i + 1) * (1 + i)) < n; ++i)
    {
    }
    printf("[__%d__", i);
    drob(1, 0, i, n, 0);
    while (printf("Another number? ( 0 - no, 1 - yes )>>>") && get_number(&ans, 10) || ans < 0 || ans > 1)
    {
        fprintf(stderr, "Wrong input!  ( use '0' or '1' ) \n");
        flush_stdin();
    }
    if (ans)
    {
        program();
    }
    else
    {
        printf("The program has completed<<<");
        return 0;
    }
}

int drob(int high, int free, int i, int number, int count)
{
    count++;
    int tmp, tmp2;
    tmp = (number - (i - free) * (i - free)) / high;
    tmp2 = 2 * i - free;
    for (tmp2; tmp2 - tmp >= 0;tmp2 = tmp2 - tmp)
    {
    }
    printf(", %d", (2 * i - free) / tmp);
    if (tmp == 1) // important '==1'
    {
        printf("]\n i = %d\n\n",count);
    }
    else
    {
        drob(tmp, tmp2, i, number, count);
    }
    return 0;
}