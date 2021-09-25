#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <ctype.h>
#include <math.h>

int isTrash(char *str)
{
	int integer_len = 0, after_integer_len = 0;
	short point_counter = 0;
	for (int i = 0; i < strlen(str) - 1; i++)
	{
		if (point_counter == 0 && isdigit(str[i]))
			integer_len += 1;
		if (point_counter == 1 && isdigit(str[i]))
			after_integer_len += 1;
		if ((str[i] == '.' || str[i] == ',') && point_counter == 0 && i != strlen(str) - 2 && i != 0)
		{
			str[i] = '.';
			point_counter += 1;
		}
		else
		{
			if (!isdigit(str[i])  || integer_len > 8 || after_integer_len > 6)
				return 1;
		}
	}

	return 0;

}

int isGoodTriangle(double side_1, double side_2, double side_3)
{
	if (side_1 < side_2 + side_3 && side_2 < side_3 + side_1 && side_3 < side_1 + side_2)
		return 1;
	else
		return 0;
}

double fromSidesToAngles(double side_1, double side_2, double side_3)
{
	return acos((side_1 * side_1 + side_2 * side_2 - side_3 * side_3) / (2 * side_1 * side_2));
}

double inDegMinSec(double angle)
{
	int degrees = (int) angle;
	int minutes = (int) ((angle - (int) angle) * 60);
	int seconds = ((int) ((angle - (int) angle) * 60 * 60)) % 60;
	printf("deg:%d,min:%d,sec:%d\n", degrees, minutes, seconds);
	return 0;
}

int main()
{
	const short MaxLenOfStr = 50;
	double side_1 = 0, side_2 = 0, side_3 = 0;
	char *str = (char *) malloc(sizeof(char) * MaxLenOfStr);
	short counter = 1;
	printf("This program checks whether it is possible to construct a\n");
	printf("non-degenerate triangle with three sides entered by the user.\n");
	printf("If possible, it also outputs the angles of this triangle in degrees, minutes and seconds.\n");
	printf("Please, enter numbers (> 0 and possible with a fractional part) which will not cause an overflow.\n");
	do
	{
		printf("Enter a %d number:", counter);
		fgets(str, MaxLenOfStr, stdin);
		if (isTrash(str) || strtod(str, 0) == 0)
		{
			printf("Error! Enter a natural number which won't cause an overflow (less than a billion and\n");
			printf("no more than 6 digits after the decimal point.\n");
		}
		else
		{
			if (counter == 1)
				side_1 = strtod(str, 0);
			if (counter == 2)
				side_2 = strtod(str, 0);
			if (counter == 3)
				side_3 = strtod(str, 0);
			counter += 1;
		}
	}
	while (counter != 4);
	if (isGoodTriangle(side_1, side_2, side_3))
	{
		printf("It is possible to construct a non-degenerate triangle. Here are angles:\n");
		inDegMinSec(fromSidesToAngles(side_1, side_2, side_3) * 180 / M_PI);
		inDegMinSec(fromSidesToAngles(side_2, side_3, side_1) * 180 / M_PI);
		inDegMinSec(fromSidesToAngles(side_3, side_1, side_2) * 180 / M_PI);
	}
	else
		printf("It is impossible to construct a non-degenerate triangle.");
	return 0;
}