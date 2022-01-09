#include <stdint.h>
#include <stddef.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include <math.h>

#define MIN_DIGITS_FOR_KATATSUBA 64



size_t max_num(size_t a, size_t b)
{
    return a > b ? a : b;
}


size_t count_digits(uint64_t value)
{
    size_t count = 0;
    do
    {
        count++;
        value /= 10;
    } while (value);
    return count;
}


size_t round_to_pow(size_t n)
{
    if (n <= 1) 
    {
        return 1; 
    }

    while (n & (n - 1))
    {
        n++;
    }
    return n;
}


typedef int digit;


struct big_int 
{
    size_t digits_count; 
    digit *digits;      
};


void free_big_int(struct big_int *v)
{
    v->digits_count = 0;
    free(v->digits);
    v->digits = NULL;
}


struct big_int new_big_int(size_t digits_count)
{
    return (struct big_int) 
    {
        .digits_count = digits_count,
        .digits = calloc(digits_count, sizeof(digit))
    };
}

void change_size_to(struct big_int *v, size_t n)
{
    v->digits = realloc(v->digits, n * sizeof(digit));

    for (size_t i = v->digits_count; i < n; ++i)
    {
        v->digits[i] = 0;
    }

    v->digits_count = n;
}


void remove_leading_zeroes(struct big_int *v)
{
    if (v->digits_count <= 1) 
    { 
        return; 
    }

    size_t leading_zeroes = 0;
    for (size_t i = v->digits_count; i > 0 && v->digits[i - 1] == 0; --i)
    {
        leading_zeroes++;
    }
    v->digits_count -= leading_zeroes;
    if (v->digits_count == 0)
    {
        v->digits_count = 1;
    } 
    change_size_to(v, v->digits_count);
}

struct big_int to_big_int(int64_t value)
{
    struct big_int res = new_big_int(count_digits(llabs(value)));
    
    size_t i = 0;
    do
    {
        res.digits[i] = value % 10;
        value /= 10;
        i++;
    } while (value);

    return res;
}


struct big_int copy_big_int(struct big_int value)
{
    struct big_int res = new_big_int(value.digits_count);

    memcpy(res.digits, value.digits, value.digits_count * sizeof(digit));
   
    return res; 
}


void normalize(struct big_int *v)
{
    if (v == NULL || v->digits_count == 0) 
    {
        return; 
    }

    for (size_t i = 0; i + 1 < v->digits_count; ++i)
    {
        v->digits[i + 1] += v->digits[i] / 10;
        v->digits[i] %= 10;
    }

    struct big_int additional_digits = to_big_int(v->digits[v->digits_count - 1]);
    if (additional_digits.digits_count > 1)
    {
        size_t last_digit = v->digits_count;
        v->digits_count += additional_digits.digits_count - 1;
        digit *digits = realloc(v->digits, v->digits_count * sizeof(digit));
        if (digits == NULL)
        {
            printf("Memory error in normalization!\n");
            exit(1);
        }
        for (size_t i = 0; i < additional_digits.digits_count; ++i)
        {
            digits[last_digit + i - 1] = additional_digits.digits[i];
        }
        v->digits = digits;
    }
    free_big_int(&additional_digits);

    remove_leading_zeroes(v);
}

struct big_int sum(struct big_int a, struct big_int b)
{
    struct big_int res = {0};

    size_t n = max_num(a.digits_count, b.digits_count);
    struct big_int *m_num = NULL;
    if (a.digits_count == n)
    {
        res = copy_big_int(a);
        m_num = &b;
    }
    else
    {
        res = copy_big_int(b);
        m_num = &a;
    }
    for (size_t i = 0; i < m_num->digits_count; ++i)
    {
        res.digits[i] += m_num->digits[i];
    }
    return res;
}


struct big_int usual_mul(struct big_int a, struct big_int b)
{
    struct big_int res = new_big_int(a.digits_count + b.digits_count);
    for (size_t i = 0; i < a.digits_count; ++i)
    {
        for (size_t j = 0; j < b.digits_count; ++j)
        {
            res.digits[i + j] += a.digits[i] * b.digits[j];
        }
    }
    return res;
}



struct big_int karatsuba_mul(struct big_int x, struct big_int y)
{
    size_t n = max_num(x.digits_count, y.digits_count);
    n = round_to_pow(n);

    struct big_int X = copy_big_int(x); change_size_to(&X, n);
    struct big_int Y = copy_big_int(y); change_size_to(&Y, n);
    if (
        X.digits_count < MIN_DIGITS_FOR_KATATSUBA && 
        Y.digits_count < MIN_DIGITS_FOR_KATATSUBA
    )
    {
        struct big_int res = usual_mul(x, y);
        free_big_int(&X);
        free_big_int(&Y);
        return res;
    }
    
    struct big_int res = new_big_int(2 * n);

    size_t k = n / 2;

    struct big_int Xr = {
        .digits_count = k,
        .digits = X.digits
    };

    struct big_int Xl = {
        .digits_count = k,
        .digits = X.digits + k
    };

    struct big_int Yr = {
        .digits_count = k,
        .digits = Y.digits
    };
   
    struct big_int Yl = {
        .digits_count = k,
        .digits = Y.digits + k
    };
 
    struct big_int XlYl = karatsuba_mul(Xl, Yl);
    struct big_int XrYr = karatsuba_mul(Xr, Yr);

    struct big_int Xl_plus_Xr = sum(Xl, Xr);
    struct big_int Yl_plus_Yr = sum(Yl, Yr);

    struct big_int XlYr_plus_XrYl = karatsuba_mul(Xl_plus_Xr, Yl_plus_Yr);
    for (size_t i = 0; i < n; ++i)
    {
        XlYr_plus_XrYl.digits[i] -= XlYl.digits[i] + XrYr.digits[i];
    }

    for (size_t i = 0; i < n; ++i)
    {
        res.digits[i] = XrYr.digits[i];
    }

    for (size_t i = n; i < 2 * n; ++i)
    {
        res.digits[i] = XlYl.digits[i - n];
    }

    for (size_t i = k; i < n + k; ++i)
    {
        res.digits[i] += XlYr_plus_XrYl.digits[i - k];
    }

    free_big_int(&X);
    free_big_int(&Y);
    free_big_int(&XlYl);
    free_big_int(&XrYr);
    free_big_int(&Xl_plus_Xr);
    free_big_int(&Yl_plus_Yr);
    free_big_int(&XlYr_plus_XrYl);

    return res; 
}

struct big_int big_int_pow(size_t x, size_t n)
{
    if (n == 0) 
    {
        return to_big_int(0); 
    }

    struct big_int res = to_big_int(1);
    struct big_int curr_pow = to_big_int(x);
    for (size_t i = 1; i <= n; i *= 2)
    {
        if (n & i) 
        {
            struct big_int tmp = res; 
            res = karatsuba_mul(res, curr_pow);
            normalize(&res);
            free_big_int(&tmp);
        } 
        struct big_int tmp = curr_pow; 
        curr_pow = karatsuba_mul(curr_pow, curr_pow);
        normalize(&curr_pow);
        free_big_int(&tmp);
    }
    return res;
}

void reverse_big_int(struct big_int *x)
{
    for (size_t i = 0; i < (x->digits_count + 1) / 2; ++i)
    {
        int tmp = x->digits[i];
        x->digits[i] = x->digits[x->digits_count - 1 - i];
        x->digits[x->digits_count - 1 - i] = tmp;
    }
}


struct big_int big_int_div(struct big_int x, size_t y, size_t *mod)
{
    size_t len = x.digits_count;
    struct big_int res = new_big_int(len);

    size_t zeroes_pos = -1;
    size_t pos = 0;
    size_t v = 0;
    for (size_t i = 0; i < len; ++i)
    {
        v *= 10;
        v += x.digits[len - 1 - i];

        if (v / y > 0)
        {
            if (zeroes_pos != -1)
            {
                for (size_t j = zeroes_pos + 1; j < i; ++j)
                {
                    res.digits[pos] = 0;
                    pos++;
                }
            }
            res.digits[pos] = v / y;
            pos++;
            v %= y;
            zeroes_pos = i;
        }
    }
    for (size_t i = zeroes_pos + 1; i < len; ++i)
    {
        res.digits[pos] = 0;
        pos++;
    }
    if (mod) 
    {
        *mod = v; 
    }

    change_size_to(&res, pos);
    reverse_big_int(&res);
    normalize(&res);

    return res;
}


struct big_int to_hex(struct big_int v)
{
    struct big_int res = new_big_int(v.digits_count);
    struct big_int curr = copy_big_int(v);
    size_t pos = 0;
    while (curr.digits_count > 2 || ( curr.digits_count == 2 && (curr.digits[0] + curr.digits[1] * 10 >= 16) ))
    {
        size_t mod = 0;
        struct big_int tmp = curr;
        curr = big_int_div(curr, 16, &mod);
        free_big_int(&tmp);

        res.digits[pos] = mod;
        pos++;
    }

    res.digits[pos] = curr.digits[0];
    if (curr.digits_count == 2) 
    {
        res.digits[pos] += curr.digits[1] * 10;
    }

    remove_leading_zeroes(&res);

    return res;
}


void print_bit_int_digits(struct big_int v, size_t from, size_t to)
{
    if (v.digits_count == 0) 
    {
        return;
    }

    from = v.digits_count < from ? v.digits_count : from;
    to = v.digits_count < to ? v.digits_count : to;

    for (size_t i = from; i < to; ++i)
    {
        printf("%i", v.digits[to - 1 - i]);
    }
}


void print_big_int(struct big_int v) 
{ 
    print_bit_int_digits(v, 0, v.digits_count); 
}


void print_big_int_in_hex(struct big_int v) 
{ 
    if (v.digits_count == 0) 
    { 
        return;
    }


    for (size_t i = 0; i < v.digits_count; ++i)
    {
        digit d = v.digits[v.digits_count - 1 - i];
        if (d < 10)
        {
            printf("%i", d);
        }
        else
        {
            printf("%c", 'a' + d - 10);
        }
    }
}

int main()
{

    printf("The program calculates 3^5000 and displays it in hexadecimal format.\n\n");
    printf("3^5000 in hex:\n");

    struct big_int v = big_int_pow(3, 5000);
    struct big_int hex = to_hex(v);

    print_big_int_in_hex(hex);
    printf("\n");

    free_big_int(&v);
    free_big_int(&hex);

    return 0;
}
