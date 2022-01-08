#include <stdio.h>
#include <math.h>

long long infunc();

int main()
{
    printf("This programm takes digit from user, takes sqrt and write period\n");
    
    long long digit;
    unsigned long sqrt_number;
    long double floorl(long double __x);
    double sqrt(double __x);
    unsigned long start;
    unsigned long div;
    unsigned long finish;
    int i;

    digit = infunc();
    sqrt_number = floorl(sqrt(digit));
    start = floorl(sqrt(digit)), div = 0, finish = 1;
    i = 0;
    do 
    {
        start = (sqrt_number + div) / finish;
        div = (start * finish - div);
        finish = (digit - pow(div, 2)) / finish;
        if (i == 0)
            printf("Sequence of elements is: [%lu", div);
        printf(" %lu", start);
        i++;
    } 
    while (finish != 1);
    printf("]\nThe length of the period of number %lld: %d\n", digit, i);
    return 0;
}

long long infunc()
{
    long long number;
    int is_right_num;
    do
    {
        printf("Please enter natural number that is not an integer square of another number:\n"
        ">>>>  ");
        is_right_num = scanf("%lld", &number);
        while (getchar() != '\n');
    } 
    while (!(is_right_num == 1 && number > 0 && (pow(floorl(sqrtl(number)), 2) != number)));
    printf("\n");
    return number;
}