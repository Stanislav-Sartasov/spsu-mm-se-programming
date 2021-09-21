#include <stdio.h>
#include <ctype.h>

int NOD(int a, int b)
{
    if (a == 1 || b == 1) {
        return 1;
    }
    while (a % 2 == 0)
    {
        a >>= 1;
    }
    while (b != 0)
    {
        while (b % 2 == 0)
        {
            b >>= 1;
        }
        if (a > b)
        {
            int c = a;
            a = b;
            b = c;
        }
        b -= a;
    }
    return a;
}

int all_are_mutually_prime(int* arr)
{
    for (int i = 0; i < 2; i++)
    {
        for (int j = i + 1; j < 3; j++)
        {
            if (NOD(arr[i], arr[j]) != 1)
            {
                return 0;
            }
        }
    }
    return 1;
}

// input
int get_number()
{
    int buffer = 3;
    int rezult = 0;
    int k = -1;
    char ch;
    char* str;
    str = (char*)malloc(buffer * sizeof(char));

    printf("> ");

    do
    {
        k++;
        if (k > buffer - 1)
        {
            realloc(str, k + 1);
        }
        ch = getchar();
        str[k] = ch;
    } while (ch != '\n');

    if (k == 0)
    {
        printf("The input must not be empty. Try again\n\n");
        free(str);
        return get_number();
    }

    for (int i = 0; i < k; i++)
    {
        if (48 <= (int)str[i] && (int)str[i] <= 57)
        {
            rezult = rezult * 10 + ((int)str[i] - 48);
        }
        else
        {
            if ((int)str[i] == 32)
            {
                printf("The input must not contain spaces. ");
            }
            else if (65 <= (int)str[i] && (int)str[i] <= 90 || 97 <= (int)str[i] && (int)str[i] <= 122)
            {
                printf("The input must not contain letters. ");
            }
            else
            {
                printf("The input must not contain special characters. ");
            }
            printf("Only numbers. Try again\n\n");
            free(str);
            return get_number();
        }
    }



    free(str);
    printf("\n");
    return rezult;
}

main()
{
    printf("Defines Pythagorean triple\n");

    int arr[3];

    for (int i = 0; i < 3; i++)
    {
        printf("Input the number number %d without extraneous characters and press Enter\n", i + 1);
        arr[i] = get_number();
    }

    // sort, last index is max
    for (int i = 0; i < 2; i++)
    {
        for (int j = 1; j < 3; j++)
        {
            if (arr[i] > arr[j])
            {
                int c = arr[i];
                arr[i] = arr[j];
                arr[j] = c;
            }
        }
    }

    if (arr[0] * arr[0] + arr[1] * arr[1] == arr[2] * arr[2])
    {
        if (all_are_mutually_prime(arr))
        {
            printf("This is a mutually prime Pythagorean triple\n");
        }
        else
        {
            printf("This isn't a mutually prime Pythagorean triple\n");
        }
    }
    else
    {
        printf("This is not a Pythagorean triple\n");
    }

    return 0;
}