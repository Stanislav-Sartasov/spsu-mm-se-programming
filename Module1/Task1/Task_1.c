#define _CRT_SECURE_NO_WARNINGS
#include <stdlib.h> 
#include <stdio.h> 
#include <math.h>
#include <stdbool.h>

bool check_number(int n)
{
    for (int i = 2; i < sqrt(n) + 1; i++) {
        if (n % i == 0)
        {
            return false;
        }
    }
    return true;
}

int main()
{
    system("chcp 1251");
    system("cls");

    printf("Вывод всех простых чисел Мерсенна на отрезке [1; 2^31 - 1]: \n");

    for (int i = 2; i <= 31; i++)
    {
        if (check_number((int)pow(2, i) - 1))
        {
            printf("%d \n", (int)pow(2, i) - 1);
        }
    }

    return 0;
}

