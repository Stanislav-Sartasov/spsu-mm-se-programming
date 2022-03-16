#include <stdio.h>
#define graph 1 // number of simple graphs (defines for testing more than 1 graphs, but where is no need)
#define ver 7
#define nver (2+(ver-2)*graph) // do not change
#define max 6

//change -  N(number_of_a, number_of_b, n)
#define number_of_a 25
#define number_of_b 75
#define	n 1984

//	SA(k,colors)=(n^2 - n) * ( (n - 2)^5 + (n - 2)^4 + 2 * (n - 2)^3 - (n - 2)^2 + (n - 2) ) ^ 'graphs' ( only A's)
//										/\ formula for A graph ( without (n^2-n), its disposable)
//	SB(k,colors)=(n^2 - n) * ( (n - 2)^5+ 2 * (n - 2)^4 + 3 * (n - 2)^3 )^ 'graphs' ( only B's)
//										/\ formula for B graph
//	K(ak,bk,colors) = (n^2 - n) * ('formula for A')^ak * ('formula for B')^bk * C(from ak+bk by bk)

int get_prime_div(unsigned long long number);

int brute_force(int** sample, int len, int colors, int type) // creating simple ( only one type (k = 1) or complicated (k > 1) graph and calculating his N's for formula) 
{
	int a_sample[ver][max] = { {1, 4, 5, 1, 1, 0}, {0, 2, 6, 0, 0, 0}, {1, 3, 6, 1, 1, 0}, {2, 4, 2, 2, 2, 0},
		{0, 3, 5, 0, 0, 0}, {0, 4, 6, 0, 0, 0}, {1, 2, 5, 1, 1, 0} };   // first 5 - ribs, 6 - future color 

	if (type)
	{
		a_sample[0][2] = 1;
		for (int i = 0;i < 5;i++)
		{
			if (i == 1) a_sample[5][i] = 6;
			else a_sample[5][i] = 4;
		}
	}

	int count = 0, counter[nver + 1] = { 0 };								//	1-----6----11
																			//	|\   /|\   /|
	while (len > 0)															//	|  2  |  7  |
	{																		//	|  |  |  |  |															//	|  3  |  8  |
		for (int i = 0; i < ver; i++)										//	|  |  |  |  |
		{																	//	|  4  |  9  |
			if (sample[i + (ver - 2) * (graph - len)][0] >= 0)				//	|/   \|/   \|
			{																//	0-----5-----10....
				sample[i + (ver - 2) * (graph - len)][3] = a_sample[i][1] + (ver - 2) * (graph - len);
				sample[i + (ver - 2) * (graph - len)][4] = a_sample[i][2] + (ver - 2) * (graph - len);
			}
			else
			{
				for (int j = 0;j < max - 1;j++)
				{
					sample[i + (ver - 2) * (graph - len)][j] = a_sample[i][j] + (ver - 2) * (graph - len);
				}
			}
			sample[i + (ver - 2) * (graph - len)][max - 1] = 0;
		}
		len--;
	}
	/*int i;											//	visualization graphs to be sure
	for (i = 0; i < nver; i++)							// (checking their ribs)
	{
		printf("%d -- ", i);
		for (int j = 0;j < max ;j++)
		{
			printf("%d ", sample[i][j]);
		}
		printf("\n");
	}
	*/
	while (counter[nver] != 1)
	{
		for (int j = 0; j < nver; j++)
		{
			if (counter[j] == colors)
			{
				counter[j] = 0;
				counter[j + 1]++;
			}
		}
		for (int i = 0; i < nver; i++)
		{
			sample[i][max - 1] = counter[i];
		}

		int flag = 1;

		for (int i = 0; i < nver && flag == 1; i++)
		{
			for (int j = 0; j < max - 1; j++)
			{
				if (sample[i][max - 1] == sample[sample[i][j]][max - 1])
				{
					flag = 0;
					break;
				}
			}
		}
		if (flag)
		{
			count++;
		}
		counter[0]++;
	}
	return count;
}

int main()
{
	int a_subsequence[5] = { 0 }, b_subsequence[5] = { 0 };		//	getting values for formulas 

	for (int t = 0; t < 2; t++)
		for (int j = 3; j < 8; j++)
		{
			int** sample = (int**)malloc((nver) * sizeof(int*));
			for (int i = 0; i < nver; i++)
			{
				sample[i] = (int*)malloc(max * sizeof(int));
			}
			if (t == 0)
			{
				a_subsequence[j - 3] = brute_force(sample, graph, j, t) / (j * j - j); //  N's / (const for graph combining)
			}
			else
			{
				b_subsequence[j - 3] = brute_force(sample, graph, j, t) / (j * j - j);
			}
			for (int i = 0; i < nver; i++)
			{
				free(sample[i]);
			}
			free(sample);
		}

	int a_formula[5] = { 1, -5 , -5, -5, -5 },				//creating formulas automattly
		b_formula[5] = { 1, -5 , -5, -5, -5 };
	int a_cont = 1, b_cont = 1;

	while (a_cont || b_cont)
	{
		for (int j = 4; j > 0; j--)
		{
			if (a_formula[j] == 5 || b_formula[j] == 5)
			{
				if (a_formula[j] == 5)
				{
					a_formula[j] = -5;
					a_formula[j - 1]++;
				}
				if (b_formula[j] == 5)
				{
					b_formula[j] = -5;
					b_formula[j - 1]++;
				}
			}
		}

		for (int i = 1; i <= 5; i++)
		{
			int a_test = 0, b_test = 0;
			for (int l = 0; l <= 4;l++)
			{
				int a_r = 1, b_r = 1;
				for (int j = 5 - l; j > 0;j--)
				{
					a_r *= i;
					b_r *= i;
				}
				a_test += a_formula[l] * a_r;
				b_test += b_formula[l] * b_r;
			}
			if (a_test == a_subsequence[i - 1] && (a_cont == 0 || i == 1)) a_cont = 0;
			else a_cont = 1;
			if (b_test == b_subsequence[i - 1] && (b_cont == 0 || i == 1)) b_cont = 0;
			else b_cont = 1;
		}
		if (a_cont) a_formula[4]++;
		if (b_cont) b_formula[4]++;
	}										// getting coefficents

	unsigned long long a = 0, b = 0; 		// a = (n-2)^5+(n-2)^4+2*(n-2)^3-(n-2)^2+(n-2) const for n colors and graph A
											// b = (n-2)^5+2*(n-2)^4+3*(n-2)^3 const for n colors and graph B
	for (int i = 0; i < ver - 2;i++)
	{
		unsigned long long a_appendum = 1, b_appendum = 1;

		for (int j = ver - 2 - i; j > 0;j--)
		{
			a_appendum *= n - 2;
			b_appendum *= n - 2;
		}
		a += a_formula[i] * a_appendum;
		b += b_formula[i] * b_appendum;
	}

	unsigned long long h = n * n - n;  // n^2-n - const for this type of making graphs
	unsigned long long answer_mod8 = 1;
	int a_divisors[100] = { 0 }, b_divisors[100] = { 0 }, h_divisors[20] = { 0 };
	int i = 0;

	while (h != 1)								// canonical decomposition
	{
		h_divisors[i] = get_prime_div(h);
		h /= h_divisors[i];
		i++;
	}
	for (int k = 0; k < i ;k++)
	{
		answer_mod8 *= h_divisors[k];
		if (answer_mod8 > 100000000)
			answer_mod8 = answer_mod8 % 100000000;
	}
	i = 0;
	while (a != 1)
	{
		a_divisors[i] = get_prime_div(a);
		a /= a_divisors[i];
		i++;
	}
	a_divisors[i] = number_of_a;					// last = power
	while (a_divisors[i] > 0)
	{
		for (int k = 0;k < i;k++)
		{
			answer_mod8 *= a_divisors[k];

			if (answer_mod8 > 100000000)
				answer_mod8 = answer_mod8 % 100000000;
		}
		a_divisors[i]--;
	}
	i = 0;
	while (b != 1)
	{
		b_divisors[i] = get_prime_div(b);
		b /= b_divisors[i];
		i++;
	}
	b_divisors[i] = number_of_b;					// last = power
	while (b_divisors[i] > 0)
	{
		for (int k = 0;k < i;k++)
		{
			answer_mod8 *= b_divisors[k];
			if (answer_mod8 > 100000000)
				answer_mod8 = answer_mod8 % 100000000;
		}
		b_divisors[i]--;
	}

	int combinations[number_of_a + number_of_b] = { 0 };

	for (int i = number_of_b + 1; i <= number_of_a + number_of_b;i++) // calculating combinations
	{
		int l = i;
		while (l > 1)
		{
			int tmp = get_prime_div(l);
			combinations[tmp]++;
			l /= tmp;
		}
	}
	for (int k = 2; k <= number_of_a; k++)
	{
		int l = k;
		while (l > 1)
		{
			int tmp = get_prime_div(l);
			combinations[tmp]--;
			l /= tmp;
		}
	}
	for (i = 0; i < number_of_a + number_of_b;i++)			// multiply by combinations
	{
		while (combinations[i] > 0)
		{
			answer_mod8 *= i;
			if (answer_mod8 > 100000000)
				answer_mod8 = answer_mod8 % 100000000;
			combinations[i]--;
		}
	}
	printf("%lld ", answer_mod8);
}

int get_prime_div(unsigned long long number)
{
	for (int i = 2; i < number ; i++)
	{
		if (number % i == 0)
		{
			return i;
		}
	}
	return number;
}