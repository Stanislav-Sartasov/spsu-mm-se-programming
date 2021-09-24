#include <stdio.h>

// The function checks if two integers are coprime
int coprime(int first, int second)
{
    while(first != 0 && second != 0)
    {
        if (second > first)
            second %= first;
        else
            first %= second;
    }
    if (first + second == 1)
        return 1;
    else
        return 0;
}

// Fills given array with counted angles
void get_angles(double* out_angles, float a, float b, float c)
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

int my_scanf_decimal(const char* message)
{
    // Output message
    printf(message);
    int result;
    int scanf_result;
    // Endless loop awaiting user input
    while (1) {
        // Check if scanf was a success
        if (!scanf_s("%d", &result))
        {
            getchar();
        }
        // Check if number is greater than zero
        if (result < 0) {
            printf("Number should be greater than zero and be a number\nInput again:");
            continue;
        }
        // End the loop
        break;
    }
    return result;
}

int main()
{
    int a, b, c;
    // Information output
    printf("This programs checks if 3 inputted numbers are coprime Pythagorean numbers\n");
    printf("Please, input 3 numbers\n");
    
    // Input
    a = my_scanf_decimal("Input first number:");
    b = my_scanf_decimal("Input second number:");
    c = my_scanf_decimal("Input third number:");
    
    // Pythagorean triple check
    if (a * a + b * b == c * c || a * a + c * c == b * b || c * c + b * b == a * a)
        // Coprime check
        if(coprime(a, b) && coprime(b, c) && coprime(c, a))
            printf("These numbers are coprime Pythagorean triple");
        else
            printf("These numbers are not coprime, but are Pythagorean triple");
    else
        printf("These numbers are not a Pythagorean triple");
    return 0;
}
