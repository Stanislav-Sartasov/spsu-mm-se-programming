
/*Name and first name : Ragalison Hilaire ;
  adress email: st085683@student.spbu.ru ;
  Цели задачи: Для введённых трёх пользователем чисел нужно определить, если они являются
               пифагоровой тройкой, причём, надо определить,  если они являются  также простой
               пифагоровой тройкой. Порядок, в котором вводятся числа, произвольный.*/

#include <stdio.h>

unsigned int isPythagorean1(unsigned int, unsigned int, unsigned int);
unsigned int isPythagorean2(unsigned int, unsigned int, unsigned int);
unsigned int isPythagorean3(unsigned int, unsigned int, unsigned int);

void showTripletPyt(unsigned int, unsigned int, unsigned int);

unsigned int checkPairNumber(unsigned int, unsigned int, unsigned int);



int main()
{
    unsigned int a1, a2, a3;
    printf("\nEnter any natural number: ");
    scanf_s("%u", &a1);

    printf("Enter any natural number: ");
    scanf_s("%u", &a2);

    printf("Enter any natural number: ");
    scanf_s("%u", &a3);

    if (isPythagorean1(a1, a2, a3))
    {
        showTripletPyt(a1, a2, a3);
        if ((a1 == a2) || (a1 == a3))
        {
            printf("Even if, the triplet (%u, %u, %u) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if ((a2 == a1) || (a2 == a3))
        {
            printf("Even if, the triplet (%u, %u, %u) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if ((a3 == a1) || (a3 == a2))
        {
            printf("Even if, the triplet (%u, %u, %u) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if (checkPairNumber(a1, a2, a3) == 0)
        {
            printf("Even if, the triplet (%u, %u, %u) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }

        else
        {
            printf("\n***These numbers (%u, %u, %u) ARE a PRIME Pythagorean triple.***\n\n--->The Pythagorean triple is PRIMITIVE.\n", a1, a2, a3);
        }
    }

    else if (isPythagorean2(a1, a2, a3))
    {
        showTripletPyt(a1, a2, a3);
        if ((a1 == a2) || (a1 == a3))
        {
            printf("Even if, the triplet (%u, %u, %u) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if ((a2 == a1) || (a2 == a3))
        {
            printf("Even if, the triplet (%u, %u, %u) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if ((a3 == a1) || (a3 == a2))
        {
            printf("Even if, the triplet (%u, %u, %u) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if (checkPairNumber(a1, a2, a3) == 0)
        {
            printf("Even if, the triplet (%u, %u, %u) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }

        else
        {
            printf("\n***These numbers (%u, %u, %u) ARE a PRIME Pythagorean triple.***\n\n--->The Pythagorean triple is PRIMITIVE.\n", a1, a2, a3);
        }
    }

    else if (isPythagorean3(a1, a2, a3))
    {
        showTripletPyt(a1, a2, a3);
        if ((a1 == a2) || (a1 == a3))
        {
            printf("Even if, the triplet (%u, %u, %u) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if ((a2 == a1) || (a2 == a3))
        {
            printf("Even if, the triplet (%u, %u, %u) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if ((a3 == a1) || (a3 == a2))
        {
            printf("Even if, the triplet (%u, %u, %u) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }
        else if (checkPairNumber(a1, a2, a3) == 0)
        {
            printf("Even if, the triplet (%u, %u, %u) of numbers, recently entered, IS Pythagorean triple.\n***They AREN'T a prime Pythagorean triple.***\n\n--->So, this Pythagorean triple IS NOT PRIMITIVE.\n", a1, a2, a3);
        }

        else
        {
            printf("\n***These numbers (%u, %u, %u) ARE a PRIME Pythagorean triple.***\n\n--->The Pythagorean triple is PRIMITIVE.\n", a1, a2, a3);
        }

    }

    else
    {
        printf("\nTriplet (%u, %u, %u) of numbers, recently entered, IS NOT Pythagorean triple.\n", a1, a2, a3);

    }

    return 0;
}




unsigned int isPythagorean1(unsigned int a, unsigned int b, unsigned int c)
{
    return a * a + b * b == c * c;
}

unsigned int isPythagorean2(unsigned int c, unsigned int a, unsigned int b)
{
    return c * c == a * a + b * b;
}

unsigned int isPythagorean3(unsigned int a, unsigned int c, unsigned int b)
{
    return a * a + b * b == c * c;
}

void showTripletPyt(unsigned int x, unsigned int y, unsigned int z)
{
    printf("\n--->Triplet (%u, %u, %u) of numbers, recently entered, IS Pythagorean triple.\n", x, y, z);
}

unsigned int checkPairNumber(unsigned int number1, unsigned int number2, unsigned int number3)
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




