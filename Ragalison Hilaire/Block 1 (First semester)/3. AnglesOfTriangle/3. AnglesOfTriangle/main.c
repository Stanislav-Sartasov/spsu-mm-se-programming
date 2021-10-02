
/*Name and first name : Ragalison Hilaire ;
  adress email: st085683@student.spbu.ru ;

  Цели задачи: ОПРЕДЕЛИТЬ, ЕСЛИ МОЖНО, исходя из ТРЁХ введённых ПОЛЬЗОВАТЕЛЕМ чисел,
  ПОСТРОИТЬ НЕВЫРОЖДЕННЫЙ ТРЕУГОЛЬНИК с соответствующими сторонами. Из утверждения этого условия, следовать
  ОПРЕДЕЛЕНИЕ ЕГО УГЛЫ в ГРАДУСАХ, МИНУТАХ и СЕКУНДАХ с точностью до секунды.
  ПРЕДУСМОТРЕТЬ ввод пользователем чисел с ДРОБНОЙ ЧАСТЬЮ. */


#include <stdio.h>
#include <stdlib.h>
#include <math.h>


int generateTriangle(long double, long double, long double);
void showFractionalPart3Num(long double, long double, long double);
void showTripletEntered(long double, long double, long double);
void determinationAngleSidesA2A3(long double, long double, long double);
void determinationAngleSidesA1A3(long double, long double, long double);
void determinationAngleSidesA1A2(long double, long double, long double);

int main()

{
    long double a1, a2, a3, *p1, *p2, *p3;
    p1 = &a1;
    p2 = &a2;
    p3 = &a3;

    printf("\n1.Enter any number different to 0 : ");
    scanf_s("%Lf", p1);
    do
    {
        switch (*p1 <= 0)
        {
            case 1:
            {
                printf("\n\tError! First number entered is 0 or negative.\n\tWe'll not be able to construct a triangle. Try again!\n-->");
                scanf_s("%Lf", p1);
                break;
            }
            default:
            {
                printf("\n\tNumber entered is different to 0.\n\tCould you enter a second number?\n");
                break;
            }
        }
    } while (a1 <= 0);


    printf("\n2. Enter any number different to 0 : ");
    scanf_s("%Lf", p2);
    do
    {
        switch (*p2 <= 0)
        {
            case 1:
            {
                printf("\n\tError! Second number entered is 0 or negative.\n\tWe'll not be able to construct a triangle. Try again!\n-->");
                scanf_s("%Lf", p2);
                break;
            }
            default:
            {
                printf("\n\tSecond number entered is different to 0.\n\tCould you enter a third number?\n");
                break;
            }
        }
    } while ( a2 <= 0);

    printf("\n3. Enter any number different to 0 : ");
    scanf_s("%Lf", p3);

    do
    {
        switch (*p3 <= 0)
        {
            case 1:
            {
                printf("\n\tError! Third number entered is equal to 0 or negative.\n\tWe'll not be able to construct a triangle. Try again!\n-->");
                scanf_s("%Lf", p3);
                break;
            }
            default:
            {
                printf("\n\tNumber entered is different to 0.\n\tWe'll check if we can constuct triangle with these three numbers.\n");
                break;
            }
        }
    } while (a3 <=0);



    showTripletEntered(a1, a2, a3);
    showFractionalPart3Num(a1, a2, a3);

    if (generateTriangle(a1, a2, a3) == 1)

    {
        printf("\n--->We can construct a non-degenerate triangle with the corresponding\n sides based on these three numbers (%Lf, %Lf, %Lf) entered by the user.\n", a1, a2, a3);
        determinationAngleSidesA2A3(a1, a2, a3);
        determinationAngleSidesA1A3(a1, a2, a3);
        determinationAngleSidesA1A2(a1, a2, a3);
        return EXIT_SUCCESS;
    }

    else
        printf("\n--->We can't construct a non-degenerate triangle with the corresponding\n sides based on these three numbers (%Lf, %Lf, %Lf)\n entered by the user.\n", a1, a2, a3);

    return 0;

}

void showTripletEntered(long double  a, long double  b, long double c)
{
    printf("\n--->Triplet of rational numbers, recently entered is (%Lf, %Lf, %Lf) .\n", a, b, c);
}

void showFractionalPart3Num(long double a1, long double a2, long double a3)
{
    long double pp1 = 0, pp2 = 0, pp3 = 0, x = 0, y = 0, z = 0;
    pp1 = floor(a1);
    pp2 = floor(a2);
    pp3 = floor(a3);
    x = a1 - pp1;
    y = a2 - pp2;
    z = a3 - pp3;
    printf("\t\tFractional part of %Lf is %f .\n", a1, x);
    printf("\t\tFractional part of %Lf is %f .\n", a2, y);
    printf("\t\tFractional part of %Lf is %f .\n", a3, z);
}

int generateTriangle(long double k, long double l, long double m)
{
    if ((((k + l) > m) && (k + m) > l) && ((l + m) > k))
    {
        return 1;
    }

    else
        return 0;
}

void determinationAngleSidesA2A3(long double r1, long double s1, long double t1)
{
    long double n1 = 0, d1 = 0, ac1 = 0, min1 = 0, sec1 = 0;
    n1 = pow(s1, 2) + pow(t1, 2) - pow(r1, 2);
    d1 = 2 * s1 * t1;
    ac1 = acosl(n1 / d1) * 57.3;
    min1 = ac1 * 60;
    sec1 = ac1 * 360;
    printf("\n--->ANGLE IN DEGREE between two sides %Lf and %Lf in triplet (%Lf, %Lf, %Lf) \n \t\tis EQUAL TO: %Lf ; %Lf in minute(') ; %Lf in second('').\n\n", s1, t1, r1, s1, t1, ac1, min1, sec1);

}
void determinationAngleSidesA1A3(long double r2, long double s2, long double t2)
{
    long double n2 = 0, d2 = 0, ac2 = 0, min2 = 0, sec2 = 0;
    n2 = pow(r2, 2) + pow(t2, 2) - pow(s2, 2);
    d2 = 2 * r2 * t2;
    ac2 = acosl(n2 / d2) * 57.3;
    min2 = ac2 * 60;
    sec2 = ac2 * 360;
    printf("\n--->ANGLE IN DEGREE between two sides %Lf and %Lf in triplet (%Lf, %Lf, %Lf) \n \t\tis EQUAL TO: %Lf ; %Lf in minute(') ; %Lf in second('').\n\n", r2, t2, r2, s2, t2, ac2, min2, sec2);

}

void determinationAngleSidesA1A2(long double r3, long double s3, long double t3)
{
    long double n3 = 0, d3 = 0, ac3 = 0, min3 = 0, sec3 = 0;
    n3 = pow(r3, 2) + pow(s3, 2) - pow(t3, 2);
    d3 = 2 * r3 * s3;
    ac3 = acosl(n3 / d3) * 57.3;
    min3 = ac3 * 60;
    sec3 = ac3 * 360;
    printf("\n--->ANGLE IN DEGREE between two sides %Lf and %Lf in triplet (%Lf, %Lf, %Lf) \n \t\tis EQUAL TO: %Lf ; %Lf in minute(') ; %Lf in second('').\n\n", r3, s3, r3, s3, t3, ac3, min3, sec3);

}
