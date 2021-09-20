#include <stdio.h>
#include <math.h>

int angles(float a, float b, float c);

int description = 1;

void flush_stdin(void)
{
    char ch;
    while (scanf_s("%c", &ch) == 1 && ch != '\n')
    {
    }
}

int get_number(float* number, const int top)
{
    return !(scanf_s("%f", number) == 1 && abs(*number) < top);
}

int main()
{
    float k[3], tmp, ans=0,n;
    int i;
    if (description)
    {
        printf("<<<Description: enter 3 int or float numbers. Program will output angles of triangle with that numbers as its sides>>>\n\n");
        description = 0;
    }


    for (i = 0; i < 3; i++)
    {
        while (printf("Enter number>>") && get_number(&n, 100000000))
        {
            fprintf(stderr, "Wrong input!  ( use nubmers float or int, <10^8 ) \n");
            flush_stdin();
        }
        k[i] = n;
    }

    if (k[0] + k[1] > k[2] && k[0] + k[2] > k[1] && k[1] + k[2] > k[0])
    {

        for (i = 0; i < 3; ++i)
        {
            angles(k[0], k[1], k[2]);
            tmp = k[1];
            k[1] = k[2];
            k[2] = k[0];
            k[0] = tmp;
        }
    }
    else
    {
        printf("Can't create a triangle.\n");
    }
    while (printf("Continue? (0-no, 1-9 yes)>>>") && get_number(&ans, 10))
    {
        fprintf(stderr, "Wrong input!  ( use nubmers <10^8 ) \n");
        flush_stdin();
    }
    if (ans)
    {
        main();
    }
    printf("The program has completed");
    return 0;
 }
    

int angles(float a, float b, float c)
{
    double PI = 3.141592653589793238463;
    float sec, angle;
    int grad, min;
    angle = acos((b * b + c * c - a * a) / (2 * b * c)) * 180 / PI;
    for (grad = 0; grad <= angle - 1; ++grad)
    {
    }
    for (min = 0; min <= ((angle - grad) * 60) - 1; ++min)
    {
    }
    sec = ((angle - grad) * 60 - min) * 60;
    printf("%.2f - %d* %d' %.0f +-1''\n", a, grad, min, sec);
    return 0;
}