#include <stdio.h>

int drob(int top, int free, int i);

int digit, count, description = 1;

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
    int i, ans = 0, n;
    if (description)
    {
        printf("<<<Description:The program represents the square root of a number in the form of a continued fraction and gives the length of the period.>>>\n\n");
    }
    description = 0;
    while (printf("Enter number>>") && get_number(&n, 100000000))
    {
        fprintf(stderr, "Wrong input!  ( use nubmers, <10^8 ) \n");
        flush_stdin();
    }
    digit = n;
    for (i = 1; ((i + 1) * (1 + i)) < digit; ++i)
    {
    }
    printf("[__%d__", i);
    drob(1, 0, i);
    printf("\ni = %d\n", count);
    while (printf("Another number? ( 0 - no, 1-9 - yes )>>>") && get_number(&ans, 10))
    {
        fprintf(stderr, "Wrong input!  ( use nubmers, <10^8 ) \n");
        flush_stdin();
    }
    if (ans)
    {
        count = 0;
        main();
    }
    else
    {
        printf("The program has completed<<<");
        return 0;
    }
}

int drob(int top, int free, int i)
{
    count++;
    int tmp, tmp2;
    tmp = (digit - (i - free) * (i - free)) / top;
    tmp2 = 2 * i - free;
    for (tmp2; tmp2 - tmp >= 0;tmp2 = tmp2 - tmp)
    {
    }
    printf(", %d", (2 * i - free) / tmp);
    if (tmp == 1) // important '==1'
    {
        printf("]");
    }
    else
    {
        drob(tmp, tmp2, i);
    }
    return 0;
}