#include <stdlib.h>
#include <stdio.h>
#include <math.h> 

void get_angle(double a, double b, double c, int i)
{
	// angle opposite A side
	// i % 3 + 1 - number of angle
	int angle_sec = round((180 / acos(-1.0)) * acos((pow(b, 2) + pow(c, 2) - pow(a, 2)) / (2 * b * c)) * 3600);
	int angle_min = angle_sec / 60;
	int angle = angle_min / 60;
	printf("Angle opposite side number %d: %d degrees, %d minutes, %d seconds\n", i % 3 + 1, angle, angle_min % 60, angle_sec % 60);
}

int max_of_array(double* arr, int size)
{
	double max = *arr;
	for (int i = 0; i < size; i++)
	{
		if (max < arr[i])
		{
			max = arr[i];
		}
	}

	return max;
}

int sum_of_array(double* arr, int size)
{
	double sum = 0;
	for (int i = 0; i < size; i++)
	{
		sum += arr[i];
	}
	return sum;
}

// input
double get_number_with_point()
{
	double rezult = 0;
	int buffer = 4;
	int k = -1;
	int c = 0;
	char ch;
	char* str;
	str = (char*)malloc(buffer * sizeof(char));

	printf("> ");

	do
	{
		k++;
		if (k > buffer - 1)
		{
			realloc(str, k + 4);
		}

		ch = getchar();
		if (ch == '.' || ch == ',')
		{
			c++;
		}
		str[k] = ch;
	} while (ch != '\n');

	if (k == 0)
	{
		printf("Please input something\n\n");
		free(str);
		return get_number_with_point();
	}

	for (int i = 0; i < k; i++)
	{
		if ((c > 1 || str[i] < '0' || '9' < str[i] || str[0] == '.' || str[0] == ',' || str[k - 1] == '.' || str[k - 1] == ',') && (str[i] != '.' && str[i] != ','))
		{
			printf("Incorrect input. Try again\n\n");
			free(str);
			return get_number_with_point();
		}
	}

	for (int i = 0; i < k; i++)
	{
		if (str[i] == '.' || str[i] == ',')
		{
			for (int j = i + 1; j < k; j++)
			{
				rezult += ((int)str[j] - 48) * pow(10, i - j);
			}
			break;
		}
		rezult = rezult * 10 + (int)str[i] - 48;
	}

	printf("\n");
	free(str);
	return rezult;
}

int main() 
{
	double arr[3];

	printf("Calculates angles of a non-degenerate triangle\n");

	for (int i = 0; i < 3; i++)
	{
		printf("Input angle number %d and press Enter\n", i + 1);
		arr[i] = get_number_with_point();
	}

	if (2 * max_of_array(arr, 3) >= sum_of_array(arr, 3))
	{
		printf("Triangle is a degenerate triangle\n");
	}
	else
	{
		for (int i = 0; i < 9; i = i + 4)
		{
			get_angle(arr[i % 3], arr[(i + 1) % 3], arr[(i + 2) % 3], i);
		}
	}

	return 0;
}