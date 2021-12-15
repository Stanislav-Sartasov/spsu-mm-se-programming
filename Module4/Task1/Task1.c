#include <stdio.h>
#include <stdlib.h>

#define LongNumber struct LongNumber

const unsigned char NUM_BASE = 16;

LongNumber
{
    unsigned char num;
    LongNumber* next;
};

void freeLongNumber(LongNumber* num)
{
    LongNumber* ptr_prv, * ptr;

    ptr = num;

    while (ptr->next != NULL)
    {
        ptr_prv = ptr;
        ptr = ptr->next;

        free(ptr_prv);
    }

    free(ptr);
}

LongNumber* createLongNumber(unsigned char num)
{
    LongNumber* result = malloc(sizeof(LongNumber));
    result->num = num;
    result->next = NULL;

    return result;
}

int getSize(LongNumber* num)
{
    int k = 1;

    while (num->next != NULL)
    {
        k++;
        num = num->next;
    }

    return k;
}

LongNumber* bigMult(LongNumber* num1, LongNumber* num2)
{
    LongNumber* result = malloc(sizeof(LongNumber));
    result->num = 0;
    result->next = NULL;

    int size1 = getSize(num1);
    int size2 = getSize(num2);

    LongNumber* small, * big;

    if (size1 > size2)
    {
        small = num2;
        big = num1;
    }
    else
    {
        small = num1;
        big = num2;
    }

    LongNumber* res_chk = result;

    while (small != NULL)
    {
        unsigned char delta_prv = 0;
        unsigned char delta_next = 0;

        LongNumber* big_temp = big;
        LongNumber* res_temp = res_chk;


        while (big_temp->next != NULL)
        {
            delta_next = small->num * big_temp->num;

            res_temp->num += delta_next % NUM_BASE + delta_prv;

            delta_prv = delta_next / NUM_BASE + res_temp->num / NUM_BASE;

            res_temp->num %= NUM_BASE;

            if (res_temp->next == NULL)
            {
                res_temp->next = malloc(sizeof(LongNumber));
                res_temp->next->num = 0;
                res_temp->next->next = NULL;
            }

            big_temp = big_temp->next;
            res_temp = res_temp->next;
        }

        delta_next = small->num * big_temp->num;

        if (res_temp == NULL)
        {
            res_temp = malloc(sizeof(LongNumber));
            res_temp->num = 0;
            res_temp->next = NULL;
        }

        res_temp->num += delta_next % NUM_BASE + delta_prv;

        delta_prv = delta_next / NUM_BASE + res_temp->num / NUM_BASE;

        res_temp->num %= NUM_BASE;

        if (delta_prv != 0)
        {
            if (res_temp->next == NULL)
            {
                res_temp->next = malloc(sizeof(LongNumber));
                res_temp->next->num = 0;
                res_temp->next->next = NULL;
            }

            res_temp->next->num += delta_prv;
        }

        small = small->next;
        res_chk = res_chk->next;
    }

    return result;

}

LongNumber* bigSum(LongNumber* num1, LongNumber* num2)
{
    LongNumber* result = calloc(1, sizeof(LongNumber));
    LongNumber* return_value = result;
    result->next = NULL;
    result->num = 0;

    while ((num1->next != NULL) && (num2->next != NULL))
    {
        result->num += num1->num + num2->num;
        result->next = calloc(1, sizeof(LongNumber));

        result->next->num = 0;
        result->next->next = NULL;

        if (result->num >= NUM_BASE)
        {
            result->next->num = result->num / NUM_BASE;
            result->num = result->num % NUM_BASE;
        }

        num1 = num1->next;
        num2 = num2->next;
        result = result->next;
    }

    result->num += num1->num + num2->num;

    if (result->num >= NUM_BASE)
    {
        result->next = calloc(1, sizeof(LongNumber));
        result->next->next = NULL;
        result->next->num = result->num / NUM_BASE;
        result->num = result->num % NUM_BASE;
    }

    LongNumber* cnt = NULL;

    if (num1->next != NULL)
    {
        cnt = num1->next;
    }
    else if (num2->next != NULL)
    {
        cnt = num2->next;
    }

    if (cnt != NULL)
    {
        while (cnt != NULL)
        {
            if (result->next == NULL)
            {

                result->next = calloc(1, sizeof(LongNumber));
                result->next->num = 0;
                result->next->next = NULL;

            }

            result = result->next;
            result->num += cnt->num;

            if (result->num >= NUM_BASE)
            {
                if (result->next == NULL)
                {

                    result->next = calloc(1, sizeof(LongNumber));
                    result->next->num = 0;
                    result->next->next = NULL;

                }

                result->next->num = result->num / NUM_BASE;
                result->num = result->num % NUM_BASE;
            }

            result = result->next;
            cnt = cnt->next;
        }
    }


    return return_value;
}

LongNumber* pow(LongNumber* num, int power)
{
    LongNumber* temp = malloc(sizeof(LongNumber));
    *temp = *num;

    for (int i = 1; i < power; i++)
    {
        *temp = *bigMult(temp, num);
    }

    return temp;
}

void toHex(unsigned char num)
{
    if (num < 10)
        printf("%d", num);
    else
        switch (num)
        {
        case 10:
        {
            printf("a");
            break;
        }
        case 11:
        {
            printf("b");
            break;
        }
        case 12:
        {
            printf("c");
            break;
        }
        case 13:
        {
            printf("d");
            break;
        }
        case 14:
        {
            printf("e");
            break;
        }
        case 15:
        {
            printf("f");
            break;
        }
        default:
        {
            break;
        }
        }

}

void printLongNumber(LongNumber* num)
{
    if (num->next == NULL)
    {
        if (num->num != 0)
            toHex(num->num);

        return;
    }

    printLongNumber(num->next);
    toHex(num->num);
}

int main()
{
    system("chcp 1251");
    system("cls");
    unsigned char num = 3;
    LongNumber* answer = createLongNumber(num);
    *answer = *pow(answer, 5000);
    printf("Ответ:");
    printLongNumber(answer);
    freeLongNumber(answer);

    return 0;
}