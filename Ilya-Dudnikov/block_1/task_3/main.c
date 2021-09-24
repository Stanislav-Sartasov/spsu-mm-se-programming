#include <stdio.h>
#include <math.h>

double calculateAngle(double a, double b, double c) 
{
    return acos((a * a + b * b - c * c) / (2 * a * b));
}

double printAngle(double angle) 
{
    double degrees = (180 * angle) / M_PI;
    printf("%d degrees ", (int)degrees);
    
    double minutes = (degrees - (int)degrees) * 60;
    printf("%d minutes ", (int)minutes);

    double seconds = (degrees - (int)degrees) * 3600;
    printf("%d seconds\n", (int)seconds % 60);
}

int main() 
{
    printf("This programm checks if it is possible to create a non-degenerate triangle with sides of given lengths, \n");
    printf("and if so, prints the angles of this triangle \n");

    double a, b, c;
    char after = '\0';

    printf("Please enter three numbers: ");
    while (scanf("%lf %lf %lf%c", &a, &b, &c, &after) != 4 || a <= 0 || b <= 0 || c <= 0 || after != '\n') 
    {
        printf("Invalid input error: you must enter three positive numbers\n");
        while (after != '\n') scanf("%c", &after);
        after = '\0';
        printf("Please enter three numbers: ");
    }

    if (a + b > c && a + c > b && b + c > a) 
    {
        printf("It is possible to create a non-degenerate triangle using these sides. Its angles are:\n");
        printAngle(calculateAngle(a, b, c));
        printAngle(calculateAngle(a, c, b));
        printAngle(calculateAngle(b, c, a));
    }
    else
    {
        printf("It is impossible to make a non-degenerate triangle with these sides\n");
    }
    return 0;
}
