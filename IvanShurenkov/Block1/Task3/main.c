#include <stdio.h>
#include <math.h>


#define MIN(a, b) ((a) < (b) ? (a) : (b))
#define MAX(a, b) ((a) > (b) ? (a) : (b))
#define SQ(x) (x) * (x)


double find_degrees(double a, double b, double c)
{
    return acos((SQ(b) + SQ(c) - SQ(a)) / (2 * b * c)) * 180 / M_PI;
}

void print_deg_min_sec(double a)
{
    printf("%d %d'%d\"; ", (int)a, (int)(a * 60) % 60, (int)(a * 3600) % 60);
}

int main()
{
    printf("Angles of the triangle. Enter three positive numbers:");
    double a = 0, b = 0, c = 0;
    while (0 >= a || 0 >= b || 0 >= c)
    {
        scanf("%lf %lf %lf", &a, &b, &c);
        if (0 >= a || 0 >= b || 0 >= c)
            printf("It isn't positive number. Try again:");
    }
    double max = MAX(MAX(a, b), c), min = MIN(MIN(a, b), c), mid = a - max + b - min + c;
    if (max >= min + mid)
    {
        printf("Triangle don't exist.");
        return 0;
    }
    double angle_a = find_degrees(a, b, c), angle_b = find_degrees(b, a, c), angle_c = find_degrees(c, a, b);
    printf("Angles of the triangle are:\t");
    print_deg_min_sec(angle_a);
    print_deg_min_sec(angle_b);
    print_deg_min_sec(angle_c);
    return 0;
}
