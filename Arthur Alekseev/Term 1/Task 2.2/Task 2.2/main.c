#include <stdio.h>
#include <stdlib.h>

int** create_matrix_8_rows(int size) 
{
	// allocate memory
	unsigned long long** mat = (unsigned long long**)malloc(8 * sizeof(unsigned long long*));

	for (unsigned long long i = 0; i < 8; i++)
		mat[i] = (unsigned long long*)malloc(size * sizeof(unsigned long long));

	// fill with zeros
	for (int i = 0; i < 8; i++)
		for (int j = 0; j < size; j++)
			mat[i][j] = 0;

	return mat;
}

void fill_initial(unsigned long long** arr, int money) 
{
	for (int i = 0; i < 8; i++)
	{
		for (int j = 0; j < money; j++)
		{
			arr[i][0] = 1;
			arr[0][j] = 1;
		}
	}
}

void fill_counted(unsigned long long** arr, int* coin_vals, int money)
{
	for (int i = 1; i < 8; i++)
	{
		for (int j = 1; j < money; j++)
		{
			if (j < coin_vals[i])
			{
				arr[i][j] = arr[i - 1][j];
			}
			else
			{
				arr[i][j] = arr[i - 1][j] + arr[i][j - coin_vals[i]];
			}
		}
	}
}

void free_array(unsigned long long** arr) 
{
	// Free allocated memory
	for (int i = 0; i < 8; i++) 
		free(arr[i]);
	free(arr);
}

int my_scanf_decimal(const char* message)
{
	// Output message
	printf(message);
	int result;
	int scanf_result;
	// Endless loop awaiting user input
	while (1) 
	{
		// Check if scanf was a success
		if (!scanf_s("%d", &result))
		{
			// Skip entire string until new line
			while (getchar() != '\n') {}
		}
		// Check if number is greater than zero
		if (result < 0) 
		{
			printf("Number should be greater than zero and be a number.\nInput again:");
			continue;
		}

		if (result > 40000) 
		{
			printf("Number should be less than 40000 because numbers so big are not for the C language's unsigned long long limitations.\nInput again:");
			result = -1;
			continue;
		}
		// End the loop
		break;
	}
	return result;
}


int main()
{
	printf("This program outputs the number of the ways a given quantity of money can be presented using coins with these values: 1c, 2c, 5c, 10c, 20c, 50c, 1p, 2p.\n\n");

	int money = my_scanf_decimal("Please, input a number between zero and 40000:");
	// In th algorithm 0 is also used
	money++;
	// All values here in cents
	int coin_vals[] = { 1, 2, 5, 10, 20, 50, 100, 200 };
	// Create matrix for counting
	unsigned long long** arr = create_matrix_8_rows(money);
	// Prepare array with an initial state
	fill_initial(arr, money);
	// Count ways to present the given sum
	fill_counted(arr, coin_vals, money);
	// Output sum
	printf("\nThis number can be presented in %llu ways.\n", arr[7][money - 1]);
	//Free allocated memory
	free_array(arr);

	return 0;
}