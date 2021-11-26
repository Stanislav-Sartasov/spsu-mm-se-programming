#ifndef TASK_1_BIG_INTEGER_H
#define TASK_1_BIG_INTEGER_H

#define BASE 256
#define INT_SIZE 1000

typedef struct big_integer
{
	int digits[INT_SIZE];
	int size;
} big_int;

/**
 * @brief Sets value of given big integer to 0
 * @param value - big integer
 */
void set_to_zero(big_int *value);

/**
 * @brief Sets big integer's value to val
 * @param number - big integer
 * @param val - int
 */
void set_value(big_int *number, int val);


/**
 * @brief Calculates sum of two big integers and saves it in left
 * @param left - big integer
 * @param right - big integer
 */
void big_int_add(big_int *left, big_int *right);

/**
 * @param left - big integer
 * @param right - big integer
 * @return product of two big integers
 */
big_int big_int_multiply(big_int *left, big_int *right);

/**
 * @param num - big integer
 * @param power - int
 * @return num raised to the power of parameter "power"
 */
big_int big_int_power(big_int *num, int power);

#endif //TASK_1_BIG_INTEGER_H
