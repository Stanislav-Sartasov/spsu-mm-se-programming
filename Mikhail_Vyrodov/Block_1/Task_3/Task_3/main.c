#include <stdio.h>
#include <math.h>

void clear_input()
{
	char step;
	step = 0;
	while (step != '\n' && step != EOF)
	{
		step = getchar();
	}
}

void get_sides(float* side1, float* side2, float* side3)
{
	printf("Enter 3 positive numbers - sides of triangle.\n");
	while (1)
	{
		scanf_s("%f %f %f", side1, side2, side3);
		if (*side1 > 0 && *side2 > 0 && *side3 > 0 && getchar() == '\n')
		{
			break;
		}
		printf("Input was incorrect, please try again:\n");
		clear_input();
	}
}

int main()
{
	float a, b, c;
	float angle_a, angle_b, angle_c;
	float angles_array[3];
	int degrees, minutes, seconds;
	get_sides(&a, &b, &c);
	if (c < a + b && b < a + c && a < b + c)
	{
		angle_a = (float) acos((a * a + c * c - b * b) / (2.0 * a * c));
		angle_b = (float) acos((a * a + b * b - c * c) / (2.0 * a * b));
		angle_c = (float) acos((b * b + c * c - a * a) / (2.0 * b * c));
		angles_array[0] = angle_a;
		angles_array[1] = angle_b;
		angles_array[2] = angle_c;
		printf("The angles of the entered triangle are:\n");
		for (int i = 0; i < 3; ++i)
		{
			angles_array[i] = angles_array[i] * 180.0 / 3.14159265352;
			degrees = (int) angles_array[i];
			minutes = (int) (angles_array[i] * 60) % 60;
			seconds = (int) (angles_array[i] * 3600) % 60;
			printf("%d %d' %d''\n", degrees, minutes, seconds);
		}
	}
	else
	{
		printf("These sides form a degenerate triangle");
	}
	return 0;
}