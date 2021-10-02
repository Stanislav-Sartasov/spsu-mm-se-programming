#include<stdio.h>
#include<math.h>


int gcd(int a, int b) 
{
	int c;
	while (b) 
	{
		c = a % b;
		a = b;
		b = c;
	}
	return abs(a);
}



int main() 
{
	printf("The program checks whether the entered numbers are a Pythagorean triple.\n");
	
	long long x, y, z;
	printf("Please enter the numbers:\n");
	scanf("%lld%lld%lld", &x, &y, &z);

	while ((x <= 0) || (y <= 0) || (z <= 0))
	{
		printf("Invalid input. Enter another number:");
		char cleanInput = 0;
		while (cleanInput != '\n' && cleanInput != EOF)
			cleanInput = getchar();
		scanf("%lld%lld%lld", &x, &y, &z);
	}
		
	
	if (x * x + y * y == z * z || x * x + z * z == y * y || y * y + z * z == x * x)
	{
		if (gcd(gcd(x, y), z) == 1)
			printf("The numbers are a primitive Pythagorean triple.");
		else
			printf("The numbers are a non-primitive Pythagorean triple.");

	}
	else
	{
		printf("The numbers are not a Pythagorean triple.");
	}
	return 0;
}