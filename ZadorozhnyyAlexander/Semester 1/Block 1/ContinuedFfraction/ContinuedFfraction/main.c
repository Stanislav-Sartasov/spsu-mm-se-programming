#include <stdio.h>
#include <math.h>


long long input()
{
    long long num;
    int is_right_num;
    do
    {
        printf("Enter natural number that is not an integer square of another number: ");
        is_right_num = scanf("%lld", &num);
        while (getchar() != '\n');
    } 
    while (!(is_right_num == 1 && num > 0 && (pow(floorl(sqrtl(num)), 2) != num)));
    printf("\n");
    return num;
}

int main()
{
    printf("This programm takes digit from user, takes sqrt and write period\n"
            "of this fraction, and also write all it's elements\n\n");
    long long num = input();
    unsigned long sqrt_num = floorl(sqrt(num));
    unsigned long start = floorl(sqrt(num)), div = 0, quit = 1;
    int period = 0;
    do 
    {
        div = start * quit - div;
        quit = (num - pow(div, 2)) / quit;
        start = (sqrt_num + div) / quit;
        if (period == 0)
            printf("Sequence of elements is: [%lu", div);
        printf(" %lu", start);
        period++;
    } 
    while (quit != 1);
    printf("]\nThe length of the period of number %lld: %d\n", num, period);
    return 0;
}