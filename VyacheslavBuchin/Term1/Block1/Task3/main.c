#include <stdio.h>
#include <math.h>

#define _USE_MATH_DEFINES

void swap(double* a, double* b)
{
    double t = *a;
    *a = *b;
    *b = t;
}

/// If triangle is degenerate returns true else false.
int isDegenerate(double a, double b, double c)
{
    if (a > c)
        swap(&a, &c);
    if (b > c)
        swap(&b, &c);
    return a + b <= c;
}

void input(double* a, double* b, double* c)
{
    printf("Note: the integer and the fractional part must be separated by dot symbol (.)\n");
    printf("Enter three positive numbers (they represent lengths of triangle sides) separated by space:\n");
    while (1)
    {
        int correctlyScanned = scanf("%lf %lf %lf", a, b, c);
        if (correctlyScanned == 3 && *a > 0 && *b > 0 && *c > 0)
            break;
        else
        {
            while (fgetc(stdin) != '\n')
                ;
            printf("At least one of your inputs is not a positive number. Please, try again:\n");
        }
    }
}

/// Returns an angle between a and b sides in triangle if third side is c.
double getAngle(double a, double b, double c)
{
    return acos((a * a + b * b - c * c) / (2 * a * b)); // using cosine theorem
}

void printAngleInDegrees(double a)
{
    a = a / M_PI * 180; // translates radians to degrees

    int degrees, minutes, seconds;
    degrees = floor(a);
    a -= degrees;

    minutes = a * 60;
    a -= minutes / 60.0;

    seconds = floor(a * 3600);

    printf("%d°%d'%d''\n", degrees, minutes, seconds);
}

int main()
{
    printf("This program checks whether exists a triangle with three given sides and prints its angles if does.\n\n");

    double a, b, c; // sides of a triangle
    input(&a, &b, &c);

    if (!isDegenerate(a, b, c)) // triangle is non-degenerate
    {
        printf("Angles of given triangle are\n");
        printAngleInDegrees(getAngle(a, b, c));
        printAngleInDegrees(getAngle(b, c, a));
        printAngleInDegrees(getAngle(c, a, b));
    }
    else
        printf("Triangle with given sides does not exist.\n");

    return 0;
}
