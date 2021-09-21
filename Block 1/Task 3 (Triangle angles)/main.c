#include <stdio.h>
#include <math.h>

int is_a_triangle(double a, double b, double c)
{
    return ((a + b > c) && (b + c > a) && (a + c > b));
}

double from_radians_to_degrees(double angle)
{
    return angle * 180 / M_PI;
}

void pretty_printed_angle(double angle)
{
    int degrees = (int)angle;
    int minutes = (int)(angle * 60) % 60;
    int seconds = (int)(angle * 3600) % 60;
    printf("%d %d' %d''\n", degrees, minutes, seconds);
}

int main()
{
    double a, b, c;
    printf("This program prints angles of non degenerate triangle\n");
    printf("Please enter 3 positive numbers - the sides of the triangle:\n");
    scanf("%lf %lf %lf", &a, &b, &c);
    if (!is_a_triangle(a, b, c))
    {
        printf("This is a degenerate triangle\n");
        return 0;
    }
    double angles[] = {0, 0, 0};
    angles[0] = acos((a * a + b * b - c * c)/(2.0 * a * b));
    angles[1] = acos((a * a + c * c - b * b)/(2.0 * a * c));
    angles[2] = acos((c * c + b * b - a * a)/(2.0 * c * b));

    for (int i = 0; i < 3; ++i)
    {
        pretty_printed_angle(from_radians_to_degrees(angles[i]));
    }
    return 0;
}
