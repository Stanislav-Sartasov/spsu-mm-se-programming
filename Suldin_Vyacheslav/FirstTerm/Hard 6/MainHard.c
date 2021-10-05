#include <stdio.h>
#define graph 1 // N simle grafs (defines for testing more than 1 graphs, but where is no need)
#define ver 7
#define nver (2+(ver-2)*graph)
#define max 6
//	https://onedrive.live.com/edit.aspx?resid=5A226B814C24ADF3!1151&ithint=file%2cxlsx&wdOrigin=OFFICECOM-WEB.START.MRU
//	SA(k,colors)=(n^2 - n) * ( (n - 2)^5 + (n - 2)^4 + 2 * (n - 2)^3 - (n - 2)^2 + (n - 2) ) ^ 'graphs' ( only A's)
//										/\ formula for A graph ( without (n^2-n), its disposable)
//	SB(k,colors)=(n^2 - n) * ( (n - 2)^5+ 2 * (n - 2)^4 + 3 * (n - 2)^3 )^ 'graphs' ( only B's)
//										/\ formula for B graph
//	K(ak,bk,colors) = (n^2-n) * (formula for A)^ak * (formula for B)^bk * C(from ak+bk by bk)
int get_prime_div(unsigned long long number);

int brute_force(int** sample, int len, int colors, int type) // creating simple ( only one type (k = 1) or complicated (k > 1) graph and calculating his N's for formula) 
{
	int a_sample[ver][max] = { {1,4,5,1,1  ,0}, {0,2,6,0,0,  0}, {1,3,6,1,1,  0}, {2,4,2,2,2,  0},
		{0,3,5,0,0,  0}, {0,4,6,0,0  ,0}, {1,2,5,1,1,  0} };   // first 5 - ribs, 6 - future color 
	if (type)
	{
		a_sample[0][2] = 1;
		for (int i = 0;i < 5;i++)
		{
			if (i == 1) a_sample[5][i] = 6;
			else a_sample[5][i] = 4;
		}
	}																		//	1-----6----11
	int count = 0, counter[nver + 1] = { 0 };								//	|\   /|\   /|
	while (len > 0)															//	|  2  |  7  |
	{																		//	|  |  |  |  |
		int i;																//	|  3  |  8  |
		for (i = 0; i < ver; i++)											//	|  |  |  |  |
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
	/*int i;											//	visualization graphs 
	for (i = 0; i < nver; i++)
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
		int l = 0;
		for (int l = 0; l < nver; l++)
		{
			sample[l][max - 1] = counter[l];
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
		for (int p = 3; p < 8; p++)
		{
			int** sample = (int**)malloc((nver) * sizeof(int*));
			for (int i = 0; i < nver; i++)
			{
				sample[i] = (int*)malloc(max * sizeof(int));
			}
			if (t == 0)
			{
				a_subsequence[p - 3] = brute_force(sample, graph, p, t) / (p * p - p); //  N's / (const for graph combining)
			}	
			else
			{
				b_subsequence[p-3] = brute_force(sample, graph, p, t) / (p * p - p);
			}
			for (int i = 0; i < nver; i++)
			{
				free(sample[i]);
			}
			free(sample);
		}
	/*for (int i = 0; i < 5;i++)							//for creating formulas ( 10 sec by hands )
	{
		printf("%5d   %5d\n", a_subsequence[i], b_subsequence[i]);
	}
	*/
	int a_formula[5] = { 1,1,2,-1,1 },						//suggestion for a ( 1*n^5+1*n^4+2*n^3+(-1)*n^2+1*n^1 )
		b_formula[5] = { 1,2,3,0,0 };						//suggestion for b ( 1*n^5+2*n^4+3*n^3 )		
	/*for (int i = 1; i <= 5; i++)							//testing
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
		if (a_test == a_subsequence[i - 1] && b_test == b_subsequence[i - 1])
		{
			printf("test %d +\n", i);
		}
		else
		{
			printf("test %d -\n", i);
		}
	}
	*/
	/*long long n[6][6] = {{0}};				//	testing^2
	for (int i = 1; i < 6;i++)
	{
		for (int j = 1; j < 6;j++)
		{
			n[i][j] = ((j - 1) * (j - 1) + j - 1);
			for (int k = 0; k < i; k++)
			{
				n[i][j] *= j * j * j * j * j - 8 * j * j * j * j + 27 * j * j * j - 50 * j * j + 52 * j - 24;
			}
		}
	}
	*/
	int n = 1984; // n - colors
	unsigned long long a = 0, b = 0; 		// a = (n-2)^5+(n-2)^4+2*(n-2)^3-(n-2)^2+(n-2) const for 1984 colors and graph A
	for (int l = 0; l <= 4;l++)				// b = (n-2)^5+2*(n-2)^4+3*(n-2)^3 const for 1984 colors and graph B
	{

		unsigned long long a_appendum = 1, b_appendum = 1;
		for (int j = 5 - l; j > 0;j--)
		{
			a_appendum *= n - 2;
			b_appendum *= n - 2;
		}
		a += a_formula[l] * a_appendum;
		b += b_formula[l] * b_appendum;
	}							
	unsigned long long h = n * n - n;  // n^2-n - const for this type of making graphs
	int combinations[] = { 2,2,2,2,3,3,3,7,7,11,13,19,29,31,41,43,47,79,83,89,97 }; // С ( 100!/(75!*25!) )
	unsigned long long answer_mod8 = 1;
	int a_divisors[20] = { 0 }, b_divisors[20] = { 0 }, i = 0, h_divisors[20] = { 0 };
	while (h != 1)								// canonical decomposition
	{
		h_divisors[i] = get_prime_div(h);
		h /= h_divisors[i];
		i++;
	}
	i = 0;
	while (a != 1)
	{
		a_divisors[i] = get_prime_div(a);
		a /= a_divisors[i];
		i++;
	}
	a_divisors[i] = 25;
	i = 0;
	while (b != 1)
	{
		b_divisors[i] = get_prime_div(b);
		b /= b_divisors[i];
		i++;
	}
	b_divisors[i] = 75;
	i = 0;
	for (i = 0; i < 21;i++)								//calculatings
	{
		answer_mod8 *= combinations[i];
		if (answer_mod8 > 100000000)
			answer_mod8 = answer_mod8 % 100000000;
	}
	for (i = 0; i < 9;i++)
	{
		answer_mod8 *= h_divisors[i];
		if (answer_mod8 > 100000000)
			answer_mod8 = answer_mod8 % 100000000;
	}
	while (a_divisors[6] > 0)
	{
		for (i = 0;i < 6;i++)
		{
			answer_mod8 *= a_divisors[i];
			if (answer_mod8 > 100000000)
				answer_mod8 = answer_mod8 % 100000000;
		}
		a_divisors[6]--;
	}
	while (b_divisors[10] > 0)
	{
		for (i = 0;i < 10;i++)
		{
			answer_mod8 *= b_divisors[i];
			if (answer_mod8 > 100000000)
				answer_mod8 = answer_mod8 % 100000000;
		}
		b_divisors[10]--;
	}
	printf("%lld", answer_mod8);
}

int get_prime_div(unsigned long long number)
{
	for (int i = 2; i < number / 2; i++)
	{
		if (number % i == 0)
		{
			return i;
		}
	}
	return number;
}