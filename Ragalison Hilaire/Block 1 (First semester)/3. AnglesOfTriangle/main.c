
#include <stdio.h>
#include <stdlib.h>
#include <math.h>

int isgenerateTriangle(double k, double l, double m)
{
	return (((k + l) > m) && ((k + m) > l) && ((l + m) > k));
}

void determinationAngleSidesA2A3(double r1, double s1, double t1)
{
	double n1, d1, ac1;
	int min1, sec1;
	n1 = pow(s1, 2) + pow(t1, 2) - pow(r1, 2);
	d1 = 2 * s1 * t1;
	ac1 = acos(n1 / d1) * 180 / M_PI;
	min1 = (int) (ac1 * 60) % 60;
	sec1 = (int) (ac1 * 3600) % 60;

	printf("%d %d %d\n", (int) ac1, min1, sec1);
}

void determinationAngleSidesA1A3(double r2, double s2, double t2)
{
	double n2, d2, ac2;
	int min2, sec2;
	n2 = pow(r2, 2) + pow(t2, 2) - pow(s2, 2);
	d2 = 2 * r2 * t2;
	ac2 = acos(n2 / d2) * 180 / M_PI;
	min2 = (int) (ac2 * 60) % 60;
	sec2 = (int) (ac2 * 3600) % 60;

	printf("%d %d %d\n", (int) ac2, min2, sec2);
}

void determinationAngleSidesA1A2(double r3, double s3, double t3)
{
	double n3, d3, ac3;
	int min3, sec3;
	n3 = pow(r3, 2) + pow(s3, 2) - pow(t3, 2);
	d3 = 2 * r3 * s3;
	ac3 = acos(n3 / d3) * 180 / M_PI;
	min3 = (int) (ac3 * 60) % 60;
	sec3 = (int) (ac3 * 3600) % 60;

	printf("%d %d %d\n", (int) ac3, min3, sec3);
}

int main()
{
	int result;
	double a1, a2, a3;
	do
	{
		printf("Please enter natural double numbers\n");
		result = scanf_s("%lf%lf%lf", &a1, &a2, &a3);
		fflush(stdin);
	}
	while (result != 3 || a1 <= 0 || a2 <= 0 || a3 <= 0);

	if (isgenerateTriangle(a1, a2, a3))
	{
		printf("\n--->We can construct a non-degenerate triangle with the corresponding\n sides based on these three numbers (%Lf, %Lf, %Lf) entered by the user.\n",
			   a1, a2, a3);
		determinationAngleSidesA2A3(a1, a2, a3);
		determinationAngleSidesA1A3(a1, a2, a3);
		determinationAngleSidesA1A2(a1, a2, a3);
		return 0;
	}

	else
		printf("\n--->We can't construct a non-degenerate triangle with the corresponding\n sides based on these three numbers (%Lf, %Lf, %Lf)\n entered by the user.\n",
			   a1, a2, a3);

	return 0;

}

