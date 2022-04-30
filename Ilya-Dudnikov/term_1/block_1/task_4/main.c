#include <stdio.h>

long long binary_search(long long x) {
	long long left = 1, right = 1;
	while (right * right < x)
		right *= 2;
	if (right * right == x)
		return right;
	while (right - left > 1) {
		long long middle = (left + right) / 2;

		if (middle * middle <= x)
			left = middle;
		else
			right = middle;
	}
	return left;
}

int main() {
	printf("This programm prints the length of the continued fraction of sqrt(n) and a sequence [a0; a1, ..., an] used to form that fraction\n");

	long long number;
	char after = '\0';

	long long a_start;
	printf("Enter a natural number which is not a square of any other natural number: ");
	while (scanf("%lld%c", &number, &after) != 2 || number <= 0 ||
	       (a_start = binary_search(number)) * a_start == number || after != '\n') {
		printf("Invalid input error: you must enter a natural number which is not a square of any other natural number\n");
		while (after != '\n')
			scanf("%c", &after);
		after = '\0';
		printf("Enter a number: ");
	}

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

	printf("[%lld; ", a_start);
	long long a = a_start, b = a, c = number - a * a;
	long long b_start = b, c_start = c;

	long long i = 1;
	do {
		a = (a_start + b) / c;
		b = a * c - b;
		c = (number - b * b) / c;
		if (i++ != 1) printf(", ");
		printf("%lld", a);
	} while (c != c_start || b != b_start);
	printf("]\n");
	printf("The length of the period is %lld\n", i - 1);
	return 0;
}