#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>
#include <math.h>

#define INT64 __int64_t
#define SQ(x) (x) * (x)


INT64 cnt_next_fraction(INT64 *integer_part, INT64 *denominator, INT64 square) {
    // denominator / (sqrt(square) + integer_part) == (sqrt(square) - integer_part) / ((square - integer_part ^ 2) / denominator)
    // (integer_part + sqrt(square)) / denominator
    *integer_part = (INT64) sqrt(square) - (*integer_part);
    *denominator = (square - SQ(*integer_part)) / (*denominator);
    INT64 ret = (INT64) (((*integer_part) + (INT64) sqrt(square)) / (*denominator));
    *integer_part = ((*integer_part) + (int) sqrt(square)) % (*denominator);
    return ret;
}


int main() {
    INT64 n = 0;
    printf("Enter positive integer, which is not square.\n");
    while (0 >= n || n == SQ((int) sqrt(n))) {
        scanf("%ld", &n);
        if (n == SQ((int) sqrt(n))) {
            printf("Try again. Your number is square!\n");
        } else if (n <= 0) {
            printf("Try again. Your number is not positive!\n");
        }
    }

    printf("%ld\n", (INT64) sqrt(n));
    INT64 integer_part = 0, denominator = 1;
    INT64 out = cnt_next_fraction(&integer_part, &denominator, n);
    INT64 first_int_part = integer_part, first_denominator = denominator;
    do {
        printf("%ld ", out);
        out = cnt_next_fraction(&integer_part, &denominator, n);
    } while(first_int_part != integer_part || first_denominator != denominator);
    return 0;
}
