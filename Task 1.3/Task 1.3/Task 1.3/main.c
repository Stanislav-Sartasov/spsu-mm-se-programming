#include <stdio.h>
#include <math.h>

int check_possibility(float a, float b, float c)
{
    // Check if the triangle is possible
    if (a + b > c && a + c > b && b + c > a)
    {
        return 1;
    }
    return 0;
}

// Formatted output for angles
void print_pretty_angle(double angle)
{
    // Converting degrees to angle degrees, minutes and seconds
    int angle_degrees = (int)floor(angle);
    int angle_minutes = (int)floor((angle - floor(angle)) * 60);
    int angle_seconds = (int)floor((angle - floor(angle)) * 3600) % 60;
    // Formatted output
    printf("%d %d'%d\"\n", angle_degrees, angle_minutes, angle_seconds);
}

// Fills given array with counted angles
void get_angles(double *out_angles, float a, float b, float c)
{
    // Cosine theorem is used here to determine angles
    out_angles[0] = (float)acos((a * a + b * b - c * c) / (2.0 * a * b));
    out_angles[1] = (float)acos((b * b + c * c - a * a) / (2.0 * c * b));
    out_angles[2] = (float)acos((a * a + c * c - b * b) / (2.0 * a * c));
    // Normalize angles from rad to degrees
    for (int i = 0; i < 3; i++)
    {
        out_angles[i] = out_angles[i] * 180.0 / 3.14159265352f;
    }
}

int main()
{
    printf("This program checks the possibility of a triangle and outputs the angle it has by it's sizes.\n\n");
    printf("Input 3 numbers that represent 3 sides of a triangle\n");
    // Variables for storing triangle sides
    float a, b, c;
    // Array for angles
    double angles_arr[3] = {0};
    scanf_s("%f%f%f", &a, &b, &c);
    if (check_possibility(a, b, c))
    {
        printf("Angles of the given triangle are:\n");
        // Get angles as an array
        get_angles(angles_arr, a, b, c);
        // Output angles
        for (int j = 0; j < 3; j++)
        {
            print_pretty_angle(angles_arr[j]);
        }
    }
    else
    {
        printf("Triangle with these sides does not exist.");
    }
    return 0;
}
