#include <stdio.h>
#include <math.h>

double abs(double x) 
{
    return (x >= 0 ? x : -x);
}

int main() 
{
    printf("This programm prints the length of the continued fraction of sqrt(n) and a sequence [a0; a1, ..., an] used to form that fraction\n");   

    int number;
    char after = '\0';

    printf("Enter a natural number which is not a square of any other natural number: ");
    while (scanf("%d%c", &number, &after) != 2 || number <= 0 || abs((int)sqrt(number) - sqrt(number)) <= 1e-6 || after != '\n') 
    {
        printf("Invalid input error: you must enter a natural number which is not a square of any other natural number\n");
        while (after != '\n') 
            scanf("%c", &after);
        after = '\0';
        printf("Enter a number: ");
    } 

    /*
    Thm. The continued fraction expansion of √D is periodic, with a period of at most pq, if D = p/q
    If D is our input number, then D = D / 1, since it is natural, so the period of the continued
    fraction expansion of √D is no more than D.
    */
    int answer[number + 1];
    memset(answer, -1, sizeof(answer));
    answer[0] = (int)sqrt(number);


    /*
    Thm. In the continued fraction expansion of √D, the remainders always take the form 
    x_n = (√D+b_n)/c_n, where the numbers b_n, c_n, as well as the continued fraction digits
    a_n can be obtained by means of the following algorithm: 
    set a_0 = floor(D), b_1 = a_0, c_1 = D − a * a, and then compute:
    a_{n - 1} = floor((a_0 + b_{n - 1}) / c_{n - 1}), b_n = a_{n - 1}c_{n - 1} - b_{n - 1},
    c_n = (D - b_n * b_n) / c_{n - 1}

    Since a_i depends on b_i and c_i, if at some j the pair (b_j, c_j) == (b_1, c_1), 
    this means we found the period.

    The variables are named according to the theorem to avoid any confusion.
    */
    int a = answer[0], b = a, c = number - a * a;
    int b_start = b, c_start = c;
    
    int i = 1;
    do
    {
        a = (answer[0] + b) / c;
        b = a * c - b;
        c = (number - b * b) / c;
        answer[i++] = a;
    } while (c != c_start || b != b_start);

    printf("The length of the period is %d\n", i - 1);
    printf("[%d; ", answer[0]);
    for (int i = 1; i <= number; i++)
    {
        if (answer[i] == -1)
        {   
            printf("]");
            break;
        }
        if (i != 1) printf(", ");
        printf("%d", answer[i]);
    }
    printf("\n");
    return 0;
}