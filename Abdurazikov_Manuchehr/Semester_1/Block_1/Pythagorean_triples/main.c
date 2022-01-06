#include <stdio.h>
#include <stdlib.h>

int gcd (int a, int b) 
{
	for (int c; b; ) 
    {
		c = a % b;
		a = b;
		b = c;
	}
	return abs(a);
}

int main() 
{
    printf("This program determines whether the 3 integers entered are simple Pythagorean triples or not\n");
    printf("Please enter three integers: ");
    
    int x;
    int y;
    int z;
    char last;
    
    while (scanf("%d\n %d %d%c", &x, &y, &z, &last) != 4 || x <= 0 || y <= 0 || z <= 0 || last != '\n') 
    {
        printf("Input was incorrect, please try again:\n");
        while (last != '\n') scanf("%c", &last);
        last = '\0';
        printf("Please enter three integers: ");
    }

    if (x >= y)
	{
		if (x > z)
		{
			x = x + z;
			z = x - z;
			x = x - z;
		}
	}
	else
	{
		if (y > z)
		{
			y = y + z;
			z = y - z;
			y = y - z;
		}
	}

	if (x * x + y * y == z * z)
    {
        if (gcd(gcd(x, y), z) == 1)
        {
            printf("This triple is a primitive Pythagorean triple\n");
        } 
        else
        {
            printf("This triple is Pythagorean, but not primitive\n");
        }
    }
    else
    {
        printf("This triple is not Pythagorean\n");
    }
    return 0;
}