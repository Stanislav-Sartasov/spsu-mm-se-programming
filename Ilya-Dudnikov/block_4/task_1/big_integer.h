#ifndef TASK_1_BIG_INTEGER_H
#define TASK_1_BIG_INTEGER_H

#define BASE 256

typedef struct big_integer
{
	int *digits;
	int size;
	int digits_cnt;
} big_int;

/**
 * @brief Frees the memory allocated to (number)
 * @param number - big int
 */
void delete_big_int(big_int *number);

/**
 * @param val - int
 * @param size - int
 * @return pointer to a big_int with the given value and (size) digits
 */
big_int *set_value(int val, int size);

/**
 * Converts big integer to hexadecimal
 * @param num - big integer
 * @return num's hexadecimal value as a string
 */
char *big_int_to_hexadecimal(big_int *num);

/**
 * @param left - big integer
 * @param right - big integer
 * @return pointer to a big_int = sum of two big integers
 */
big_int *big_int_add(big_int *left, big_int *right);

/**
 * @param left - big integer
 * @param right - big integer
 * @return pointer to a big_int = product of two big integers
 */
big_int *big_int_multiply(big_int *left, big_int *right);

/**
 * @param num - big integer
 * @param power - int
 * @return pointer to a big_int = num raised to the power of parameter "power"
 */
big_int *big_int_power(big_int *num, int power);

#endif //TASK_1_BIG_INTEGER_H
