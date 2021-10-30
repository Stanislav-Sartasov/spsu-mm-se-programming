#include <stdio.h>
#include <stdlib.h>
#include <math.h>

int digital_root(long long a)
{
	return (a - 1) % 9 + 1;
}

int main()
{
	long long i, maxmdrs, sum, k;
	sum = 0;
	printf("This programm prints sum of mdrs(n) for n in range [2; 999999]\n");
	long long* mdrs = (long long*)malloc(999999 * sizeof(long long));
	for (i = 2; i < 10; ++i)
	{
		mdrs[i - 1] = i;
		sum = sum + mdrs[i - 1];
	}
	for (i = 10; i <= 999999; ++i)
	{
		maxmdrs = digital_root(i);
		for (k = 2; k < (long long)(sqrt(i)) + 1; ++k)
		{
			if (i % k == 0 && mdrs[k - 1] + mdrs[(i / k) - 1] > maxmdrs)
			{
				maxmdrs = mdrs[k - 1] + mdrs[(i / k) - 1];
			}
		}
		mdrs[i - 1] = maxmdrs;
		sum = sum + mdrs[i - 1];
	}
	printf("%lld", sum);
	free(mdrs);
	return 0;
}