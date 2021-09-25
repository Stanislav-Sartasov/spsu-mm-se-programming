#define _CRT_SECURE_NO_WARNINGS
#define _USE_MATH_DEFINES
#include <stdlib.h> 
#include <stdio.h> 
#include <math.h>
#include <stdbool.h>
#include <limits.h>

#define CLI_BUFFER_SIZE  10


bool CheckTangle(double a,double b,double c )
{
    if (a > 0 && b > 0 &&  c>0)
    {
        if (a + b > c && a + c > b && c + b > a)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    return false;
}
double Result_angle(double a, double b, double c)
{
    return acos((pow(a, 2) + pow(b, 2) - pow(c, 2)) / (2 * a * b)) * 180 / M_PI;
}


bool check_error(double *value)
{
    size_t length = 0;
    char* end = NULL;
    char buf[CLI_BUFFER_SIZE] = "";



    fflush(stdout);


    if (!fgets(buf, sizeof(buf), stdin)) {
        return false;
    }


    length = strlen(buf);
    if (buf[length - 1] == '\n')
    {
        buf[--length] = '\0';


        errno = 0;
        *value = strtod(buf, &end);


        if (length == 0) {
            printf("Ошибка: введена пустая строка.\n");
            return false;
        }
        if (errno != 0 || *end != '\0') {
            printf("Ошибка: некорректный символ.\n");
            printf("\t%s\n", buf);
            printf("\t%*c\n", (int)(end - buf) + 1, '^');

            return false;
        }

    

    }
    else
    {
        scanf("%*[^\n]");
        scanf("%*c");
        printf("Ошибка: не вводите больше чем %d символа(ов).\n", CLI_BUFFER_SIZE - 2);

        return false;
    }

    return true;
}

void check_enter(double* cislo)
{
    bool status = false;

    do {
        status = check_error(cislo);
        if (!status) {
            printf("Пожалуйста, попробуйте еще раз.\n");
        }
    } while (!status);
}



int main()
{
 
    double a = 0, b = 0, c = 0;

    bool error = false;
    system("chcp 1251");
    system("cls");

    printf("Пожалуйста, задайте 3 стороны треугольника: \n");
    printf("Введите велечену стороны № 1 -"); check_enter(&a);
    printf("Введите велечену стороны № 2 -"); check_enter(&b);
    printf("Введите велечену стороны № 3 -"); check_enter(&c);
    

   if (CheckTangle(a,b,c))
    {
       
        printf("Угол № 1 - %.2lf \n", Result_angle(a, c, b));
        printf("Угол № 2 - %.2lf \n", Result_angle(a, b, c));
        printf("Угол № 3 - %.2lf \n", Result_angle(b, c, a));
    }
    else
    {
        printf("\n Ответ: К сожалению, построить невырожденный треугольник с указанными сторонами невозможно! \n");
    }
    return 0;
}
