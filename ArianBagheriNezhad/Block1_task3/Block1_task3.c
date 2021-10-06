//Homework 1.3
//Angles of a triangle

#include <stdio.h>
#include <stdbool.h>
#include <math.h>

#define M_PI 3.14159

void get_the_sides(double* first_side_ptr, double* second_side_ptr, double* third_side_ptr, char* ch_ptr);
int is_Nondegenerate_triangle(double* first_side_ptr, double* second_side_ptr, double* third_side_ptr);
void angle_calculation(double* first_side_ptr, double* second_side_ptr, double* third_side_ptr);
void print_results(double angle);

int main()
{
    double first_side, second_side, third_side;
    char ch;

    printf("\nEnter the size of the three sides.\n");
    printf("This program will tell you whether a Nondegenerate triangle can be formed with these three sides or not.\n");
    printf("If yes, it will show its angles in degrees, minutes and seconds (accurate to the second).\n");
    printf("Provide for user input of numbers with a fractional part.\n\n");

    get_the_sides(&first_side, &second_side, &third_side, &ch);

    if (is_Nondegenerate_triangle(&first_side, &second_side, &third_side)) {
        printf("A Nondegenerate triangle can be formed with these three sides.\n");

        printf("The first angle: ");
        angle_calculation(&second_side, &third_side, &first_side);

        printf("The second angle: ");
        angle_calculation(&first_side, &third_side, &second_side);

        printf("The third angle: ");
        angle_calculation(&first_side, &second_side, &third_side);
    }
    else {
        printf("A Nondegenerate triangle can NOT be formed with these three sides.\n");
    }

    return 0;
}

void get_the_sides(double* first_side_ptr, double* second_side_ptr, double* third_side_ptr, char* ch_ptr)
{
    int correct_input;

    printf("Enter the size of the three sides:\n");

    while (true) {
        correct_input = scanf_s("%lf %lf %lf%c", first_side_ptr, second_side_ptr, third_side_ptr, ch_ptr);

        if (correct_input == 4 && *first_side_ptr > 0 && *second_side_ptr > 0
            && *third_side_ptr > 0 && *ch_ptr == '\n') {
            break;
        }
        else {
            while (*ch_ptr != '\n') {
                scanf_s("%c", ch_ptr);
            }

            *ch_ptr = '\0';

            printf("Error! The number(s) entered are incorrect. Please enter ");
            printf("three natural numbers.\n");
        }
    }
}

int is_Nondegenerate_triangle(double* first_side_ptr, double* second_side_ptr, double* third_side_ptr)
{
    if (*first_side_ptr + *second_side_ptr > *third_side_ptr && *second_side_ptr + *third_side_ptr > *first_side_ptr &&
        *first_side_ptr + *third_side_ptr > *second_side_ptr) {
        return true;
    }
    else {
        return false;
    }
}

void angle_calculation(double* first_side_ptr, double* second_side_ptr, double* third_side_ptr)
{
    double angle = acos((pow(*first_side_ptr, 2) + pow(*second_side_ptr, 2) - pow(*third_side_ptr, 2)) / (2.0 * *first_side_ptr * *second_side_ptr));
    print_results(angle);

}

void print_results(double angle)
{
    double degrees = (180 * angle) / M_PI;
    printf("%d Degrees  ", (int)degrees);

    double minutes = (degrees - (int)degrees) * 60;
    printf("%d Minutes  ", (int)minutes);

    double seconds = (degrees - (int)degrees) * 3600;
    printf("%d Seconds\n", (int)seconds % 60);
}