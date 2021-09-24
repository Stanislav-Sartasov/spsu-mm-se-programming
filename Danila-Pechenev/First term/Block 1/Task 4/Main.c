#include <stdio.h>
#include <math.h>

// A state form: a + 1 / (b / (c + {sqrt(number of user)} ))
void count_new_state(int* a, int* b, int* c, int* number, int* sq_root)
{
	int delta = *sq_root - *c;
	int denominator = *number - (int)pow(delta, 2);
	*b = denominator / *b;
	int tmp = *sq_root + delta;
	*a = tmp / *b;
	*c = tmp % *b;
}

void clean_input()  // Cleaning the input area before using scanf_s function
{
	char s = ' ';
	while (s != '\n' && s != EOF)
	{
		s = getchar();
	}
}

int is_correct_input(int* number)  // Returns 1 only if the input is correct
{
	return (scanf_s("%d", number) && 2 <= *number && *number <= 1000000000 &&
		(*number - (int)pow((int)sqrt(*number), 2) >= 1));
}

double get_input_number()
{
	int number;
	while (!(is_correct_input(&number) && getchar() == '\n'))
	{
		clean_input();
		printf("Your input is incorrect! Please, try again:\n");
	}
	return number;
}

int main()
{
	printf("This program for the input natural number that is not the square of another natural number\n");
	printf("output the period and the sequence[a0; a1...ai] of continued fraction.\n");
	printf("Restrictions on the entered numbers: 2 <= n <= 1000000000.\n");
	printf("\n");

	printf("Please, input a natural number:\n");
	int number = get_input_number();
	int sq_root = sqrt(number);
	int a = sq_root;
	int b = 1;
	int c = 0;

	count_new_state(&a, &b, &c, &number, &sq_root);
	int start_a = a;
	int start_b = b;
	int start_c = c;
	int a_i[32001] = { 0 };
	a_i[0] = start_a;
	int period = 1;

	while (period <= 32000)
	{
		count_new_state(&a, &b, &c, &number, &sq_root);
		if (start_a == a && start_b == b && start_c == c)
		{
			printf("The period is %d. The sequence is:\n", period);
			break;
		}
		a_i[period] = a;
		period++;
	}
	printf("[%d;\n%d", sq_root, a_i[0]);
	for (int i = 1; a_i[i] != 0; i++)
	{
		if (i % 30 == 0)
		{
			printf(", \n%d", a_i[i]);
		}
		else
		{
			printf(", %d", a_i[i]);
		}
	}
	printf("]\n");
}