#include <stdio.h>
#include <stdlib.h>
#define	BASE 4294967296

struct number
{
	int degree;
	unsigned int* poly;
};
typedef struct number number;

int conventor(int number)
{
	if (number == 10) return printf("A");
	if (number == 11) return printf("B");
	if (number == 12) return printf("C");
	if (number == 13) return printf("D");
	if (number == 14) return printf("E");
	if (number == 15) return printf("F");
	else return printf("%d", number);
}

number long_sum(number* a, number* b)
{
	number c;
	int deg;
	if (a->degree > b->degree)
		deg = a->degree;
	else
		deg = b->degree;
	c.degree = deg;
	c.poly = (unsigned int*)calloc(c.degree + 1, sizeof(unsigned int));

	for (int j = 0; j < deg; j++)
	{
		unsigned long long int tmp = (unsigned long long int)a->poly[j] + (unsigned long long int)b->poly[j];
		if (tmp + c.poly[j] < BASE)
		{
			c.poly[j] += tmp;
			continue;
		}
		else
			c.poly[j] = tmp % BASE;

		int i = j;
		while (c.poly[i + 1] == BASE - 1)
		{
			c.poly[i + 1] = 0;
			i++;
		}
		c.poly[i + 1]++;
	}

	if (c.poly[c.degree] != 0)
		c.degree++;

	free(a->poly);
	free(b->poly);
	return c;
}

number long_mul(number* a, number* b)
{
	number c;
	c.degree = a->degree + b->degree;
	c.poly = (unsigned int*)malloc(sizeof(unsigned int) * c.degree);
	for (int i = 0; i < c.degree; i++)
		c.poly[i] = 0;

	for (int i = 0; i < a->degree; i++)
		for (int j = 0; j < b->degree; j++)
		{
			number simple;
			unsigned long long int simple_simple[2] = { 0 };
			simple.degree = c.degree;
			simple.poly = (unsigned int*)calloc( simple.degree, sizeof(unsigned int));

			unsigned long long int simple_mult = (unsigned long long int)a->poly[i] * (unsigned long long int)b->poly[j];
			simple_simple[0] = simple_mult % BASE;
			simple_simple[1] = (simple_mult - simple_simple[0])/BASE;
			for (int y = 0; y < 2; y++)
			{
				simple.poly[i + j + y] = simple_simple[y];
			}
			c = long_sum(&c,&simple);
		}

	free(a->poly);
	free(b->poly);

	if (c.poly[c.degree - 1] == 0)
		c.degree--;

	return c;
}

void translation( number a, int from, int to)
{
	unsigned int* kern = (unsigned int*)malloc(sizeof(unsigned int) * from);
	unsigned int first = 1;
	int flag = 0;
	for (int j = 0; j < from; j++)
	{
		kern[j] = first;
		first *= 2;
	}
	for (int i = a.degree - 1; i >= 0; i--)
	{
		int* tmp = (int*)malloc(from * sizeof(int));
		for (int j = 0; j < from ;j++)
		{
			if (a.poly[i] >= kern[from - 1 - j])
			{
				tmp[j] = 1;
				a.poly[i] -= kern[from - 1 - j];
			}
			else
				tmp[j] = 0;
		}
		for (int y = 0; y < (int)(from / to); y++)
		{
			int sum = 0;
			for (int u = 0; u < to; u++)
				sum += tmp[y * to + u] * (int)kern[to - 1 - u];

			if (sum != 0 || flag == 1)
			{
				flag = 1;
				conventor(sum);
			}
		}
		free(tmp);
	}
	free(kern);
}

number long_deg(unsigned int c, int degree)
{
	number initial;

	initial.degree = 1;
	initial.poly = (unsigned int*)malloc(sizeof(unsigned int) * initial.degree);
	initial.poly[0] = c;

	number result = initial;
	if (degree == 1) return result;

	for (int i = 1; i < degree; i++)
	{
		number tmp, power;
		tmp.degree = i != 1 ? result.degree : 1;
		tmp.poly = (unsigned int*)malloc(sizeof(unsigned int) * tmp.degree);
		for (int k = 0; k < tmp.degree; k++)
			tmp.poly[k] = i != 1 ? result.poly[k] : c;
		power.degree = 1;
		power.poly = (unsigned int*)malloc(sizeof(unsigned int) * power.degree);
		power.poly[0] = c;
		result = long_mul(&tmp, &power);
	}
	free(initial.poly);
	return result;
}

void long_show(number number)
{
	translation(number, 32, 4);
}

int main()
{
	number c;

	c = long_deg(3, 5000);

	long_show(c);

	free(c.poly);
}


