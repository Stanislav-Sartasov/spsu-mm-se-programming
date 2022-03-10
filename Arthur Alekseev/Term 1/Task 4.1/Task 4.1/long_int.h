#ifndef MY_LONG_INT
#define MY_LONG_INT
#include <stdlib.h>

struct my_long_int 
{
	int *digits;
	int max_length;
	int length;
};

struct my_long_int* init_long_number(int dec_number, int max_length);
struct my_long_int* add_int(struct my_long_int* left, struct my_long_int* right, int add_int);
struct my_long_int* mult_int(struct my_long_int* left, struct my_long_int* right);
struct my_long_int* power_int(int number, int power);

void print_long_num(struct my_long_int* output);
void free_int(struct my_long_int* target);

#endif