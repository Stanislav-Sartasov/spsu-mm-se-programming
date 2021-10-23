#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>

#define MAX(a, b) ((a) > (b) ? (a) : (b))

int digital_root(int n)
{
	return (n % 9 ? n % 9 : 9);
}

int main()
{
	printf("Digital root.\nThis programm calculates sum of all MDRS for number between 2 and 999999.\n");
	const int max_length = 1000000;
	int *mdrs = (int *) malloc(max_length * sizeof(int));
	for (int i = 2; i < max_length; i++)
	{
		mdrs[i] = digital_root(i);
	}
	int ans = 0;
	for (int i = 2; i < max_length; i++)
	{
		for (int j = 2; j <= i && i * j < max_length; j++)
		{
			mdrs[i * j] = MAX(mdrs[i * j], mdrs[i] + mdrs[j]);
		}
		ans += mdrs[i];
	}
	printf("Sum of all MDRS(n) for n = [2, 999999]: %d", ans);
	return 0;
}