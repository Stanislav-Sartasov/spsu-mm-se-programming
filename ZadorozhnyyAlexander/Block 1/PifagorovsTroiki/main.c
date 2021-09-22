#include <stdio.h>
#include <math.h>
#include <stdbool.h>


int NOD(int a, int b)
{
	int c;
	while (b)
	{
		c = a % b;
		a = b;
		b = c;
	}
	return a;
}


bool is_pifagorova_triple(int x, int y, int z)
{
	if (pow(x, 2) + pow(y, 2) == pow(z, 2) || pow(z, 2) + pow(y, 2) == pow(x, 2) || pow(x, 2) + pow(z, 2) == pow(y, 2))
		return true;
	return false;
}

int main() {
	int x, y, z;
	int count_right_num;
	printf("This programm check three number: is it Pythagorean triple(x^2 + y^2 = z^2) or not.\n");
	printf("If it's true, programm check is this Pythagorean triple - simple.\n");
	do
	{
		printf("Enter three natural number: ");
		count_right_num = scanf("%d%d%d", &x, &y, &z);
		while (getchar() != '\n');
	} while (!(count_right_num == 3 && x > 0 && y > 0 && z > 0)); 
	if (is_pifagorova_triple(x, y, z))
	{
		printf("This three number constitute a Pythagorean triple\n");
		if (NOD(x, y) == 1 && NOD(x, z) && NOD(y, z) == 1)
			printf("Moreover, it is simple Pythagorean triple.\n");
	}
	else
		printf("It's not Pythagorean triple.\n");
}