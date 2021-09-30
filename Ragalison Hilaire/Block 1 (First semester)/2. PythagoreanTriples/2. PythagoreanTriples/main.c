
/*Name and first name : Ragalison Hilaire ;
  adress email: st085683@student.spbu.ru ;
  Цели задачи: Для введённых трёх пользователем чисел нужно определить, если они являются
               пифагоровой тройкой, причём, надо определить,  если они являются  также простой
               пифагоровой тройкой. Порядок, в котором вводятся числа, произвольный.*/

#include <stdio.h>
#include <stdlib.h>

unsigned int isPythagorean1( int,  int,  int);
unsigned int isPythagorean2( int,  int,  int);
unsigned int isPythagorean3( int,  int,  int);

void showTripletPyt( int,  int,  int);

unsigned int checkPrimeNumbers( int,  int,  int);



int main()
{
    int a1, *p1, a2, *p2, a3, *p3;
    p1 = &a1;
    p2 = &a2;
    p3 = &a3;
    
    printf("\n1. Enter any natural number: ");
    scanf_s("%d", p1);
    do
    {
        switch (*p1 < 0)
        {
            case 1:
            {
                printf("\nFirst number entered is negative. Try again!\n-->");
                scanf_s("%d", p1);
                break;
            }
            /*default:
            {
                printf("\n\4The main purpose of this exercise is to determine a pythagorean triple \nand a primitive pythagorean triple.\n");
                break;
            }*/
        }
    } while (a1 < 0);


    printf("\n2. Enter any natural number: ");
    scanf_s("%d", p2);
    do
    {
        switch (*p2 < 0)
        {
            case 1:
            {
                printf("\nSecond number entered is negative. Try again!\n-->");
                scanf_s("%d", p2);
                break;
            }
           /*default:
            {   
                 printf("\n\4The main purpose of this exercise is to determine a pythagorean triple \nand a primitive pythagorean triple.\n");
            }*/
        }
    } while (a2 < 0);

    
    printf("\n3. Enter any natural number: ");
    scanf_s("%d", p3);
    do
    {
        switch (*p3 < 0)
        {
            case 1:
            {
                printf("\nThird number entered is negative. Try again!\n-->");
                scanf_s("%d", p3);
                break;
            }
            /*default:
            {
                printf("\n\4The main purpose of this exercise is to determine a pythagorean triple \nand a primitive pythagorean triple.\n");
            }*/ 
        }
    } while (a3 < 0);
    


    if (isPythagorean1(a1, a2, a3))
    {
        showTripletPyt(a1, a2, a3);
        if ((a1 == a2) || (a1 == a3))
        {
            printf("Even if, the triplet (%d, %d, %d) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if ((a2 == a1) || (a2 == a3))
        {
            printf("Even if, the triplet (%d, %d, %d) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if ((a3 == a1) || (a3 == a2))
        {
            printf("Even if, the triplet (%d, %d, %d) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if (checkPrimeNumbers(a1, a2, a3) == 0)
        {
            printf("Even if, the triplet (%d, %d, %d) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }

        else
        {
            printf("\n***These numbers (%d, %d, %d) ARE a PRIME Pythagorean triple.***\n\n--->The Pythagorean triple is PRIMITIVE.\n", a1, a2, a3);
        }
    }

    else if (isPythagorean2(a1, a2, a3))
    {
        showTripletPyt(a1, a2, a3);
        if ((a1 == a2) || (a1 == a3))
        {
            printf("Even if, the triplet (%d, %d, %d) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if ((a2 == a1) || (a2 == a3))
        {
            printf("Even if, the triplet (%d, %d, %d) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if ((a3 == a1) || (a3 == a2))
        {
            printf("Even if, the triplet (%d, %d, %d) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if (checkPrimeNumbers(a1, a2, a3) == 0)
        {
            printf("Even if, the triplet (%d, %d, %d) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }

        else
        {
            printf("\n***These numbers (%d, %d, %d) ARE a PRIME Pythagorean triple.***\n\n--->The Pythagorean triple is PRIMITIVE.\n", a1, a2, a3);
        }
    }

    else if (isPythagorean3(a1, a2, a3))
    {
        showTripletPyt(a1, a2, a3);
        if ((a1 == a2) || (a1 == a3))
        {
            printf("Even if, the triplet (%d, %d, %d) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if ((a2 == a1) || (a2 == a3))
        {
            printf("Even if, the triplet (%d, %d, %d) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if ((a3 == a1) || (a3 == a2))
        {
            printf("Even if, the triplet (%d, %d, %d) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if (checkPrimeNumbers(a1, a2, a3) == 0)
        {
            printf("Even if, the triplet (%d, %d, %d) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }

        else
        {
            printf("\n***These numbers (%d, %d, %d) ARE a PRIME Pythagorean triple.***\n\n--->The Pythagorean triple is PRIMITIVE.\n", a1, a2, a3);
        }

    }

    else
    {
        printf("\nTriplet (%d, %d, %d) of numbers, recently entered, IS NOT Pythagorean triple.\n", a1, a2, a3);

    }

    return 0;
}




unsigned int isPythagorean1( int a,  int b,  int c)
{
    return a * a + b * b == c * c;
}

unsigned int isPythagorean2( int c,  int a,  int b)
{
    return c * c == a * a + b * b;
}

unsigned int isPythagorean3( int a,  int c,  int b)
{
    return a * a + b * b == c * c;
}

void showTripletPyt( int x,  int y, int z)
{
    printf("\n--->Triplet (%d, %d, %d) of numbers, recently entered, IS Pythagorean triple.\n", x, y, z);
}

unsigned int checkPrimeNumbers( int number1, int number2, int number3)
{
    int test1 = (number1 % 2), test2 = (number2 % 2), test3 = (number3 % 2);

    if (test1 == 0 && test2 == 0)
    {
        return 0;
    }

    else if (test1 == 0 && test3 == 0)
    {
        return 0;
    }

    else if (test2 == 0 && test3 == 0)
    {
        return 0;
    }
    else if (test2 == 0 && test1 == 0)
    {
        return 0;
    }
    else if (test3 == 0 && test1 == 0)
    {
        return 0;
    }
    else if (test3 == 0 && test2 == 0)
    {
        return 0;
    }

    else
    {
        return 1;
    }
}




