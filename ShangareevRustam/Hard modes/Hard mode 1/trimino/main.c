#include <stdio.h>
#include <stdlib.h>

void next_profile(int profile_array[12][3], int pos, int profile, int row_num, long long int** dp)
{
	// processing received profile
	if (pos == 10)
	{
		int next_profile = 0, digit = 6561;
		if (profile_array[0][0] == 0 &&
			profile_array[0][1] == 0 &&
			profile_array[0][2] == 0 &&
			profile_array[10][0] == 0 &&
			profile_array[10][1] == 0 &&
			profile_array[10][2] == 0 &&
			profile_array[11][0] == 0 &&
			profile_array[11][1] == 0 &&
			profile_array[11][2] == 0)
		{
			for (int k = 1; k <= 9; k++)
			{
				next_profile += (profile_array[k][1] + profile_array[k][2]) * digit;
				digit /= 3;
			}
			dp[row_num + 1][next_profile] = dp[row_num + 1][next_profile] + dp[row_num][profile];
		}
	}
	// filling current profile
	else if (profile_array[pos][0] == 0)
	{
		if (profile_array[pos][0] + profile_array[pos][1] + profile_array[pos][2] == 0)
		{
			profile_array[pos][0] += 1;
			profile_array[pos][1] += 1;
			profile_array[pos][2] += 1;
			next_profile(profile_array, pos + 1, profile, row_num, dp);
			profile_array[pos][0] -= 1;
			profile_array[pos][1] -= 1;
			profile_array[pos][2] -= 1;
		}
		if (profile_array[pos][0] + profile_array[pos + 1][0] + profile_array[pos + 2][0] == 0)
		{
			profile_array[pos][0] += 1;
			profile_array[pos + 1][0] += 1;
			profile_array[pos + 2][0] += 1;
			next_profile(profile_array, pos + 1, profile, row_num, dp);
			profile_array[pos][0] -= 1;
			profile_array[pos + 1][0] -= 1;
			profile_array[pos + 2][0] -= 1;
		}
		if (profile_array[pos][0] + profile_array[pos][1] + profile_array[pos - 1][1] == 0)
		{
			profile_array[pos][0] += 1;
			profile_array[pos][1] += 1;
			profile_array[pos - 1][1] += 1;
			next_profile(profile_array, pos + 1, profile, row_num, dp);
			profile_array[pos][0] -= 1;
			profile_array[pos][1] -= 1;
			profile_array[pos - 1][1] -= 1;
		}
		if (profile_array[pos + 1][0] + profile_array[pos][0] + profile_array[pos + 1][1] == 0)
		{
			profile_array[pos + 1][0] += 1;
			profile_array[pos][0] += 1;
			profile_array[pos + 1][1] += 1;
			next_profile(profile_array, pos + 1, profile, row_num, dp);
			profile_array[pos + 1][0] -= 1;
			profile_array[pos][0] -= 1;
			profile_array[pos + 1][1] -= 1;
		}
		if (profile_array[pos][0] + profile_array[pos][1] + profile_array[pos + 1][1] == 0)
		{
			profile_array[pos][0] += 1;
			profile_array[pos][1] += 1;
			profile_array[pos + 1][1] += 1;
			next_profile(profile_array, pos + 1, profile, row_num, dp);
			profile_array[pos][0] -= 1;
			profile_array[pos][1] -= 1;
			profile_array[pos + 1][1] -= 1;
		}
		if (profile_array[pos][0] + profile_array[pos][1] + profile_array[pos + 1][0] == 0)
		{
			profile_array[pos][0] += 1;
			profile_array[pos][1] += 1;
			profile_array[pos + 1][0] += 1;
			next_profile(profile_array, pos + 1, profile, row_num, dp);
			profile_array[pos][0] -= 1;
			profile_array[pos][1] -= 1;
			profile_array[pos + 1][0] -= 1;
		}
	}
	// moving to the next position of the profile array
	else
	{
		next_profile(profile_array, pos + 1, profile, row_num, dp);
	}
}

int main()
{
	printf("This program finds the number of ways that you can fill a 9x12 area with a trimino:\n");
	// allocation of dynamic memory for an array
	long long int** dp;
	dp = (long long int**)malloc(13 * sizeof(long long int*));
	for (int i = 0; i < 13; i++)
	{
		dp[i] = (long long int*) malloc(19683 * sizeof(long long int));
	}
	for (int i = 0; i < 13; i++)
	{
		for (int j = 0; j < 19683; j++)
		{
			dp[i][j] = 0;
		}
	}
	dp[0][0] = 1;
	// iterating through all profiles for each column
	for (int row_num = 0; row_num < 12; row_num++)
	{
		for (int profile = 0; profile < 19683; profile++)
		{
			// converting a numeric profile to an array
			int profile_array[12][3], savep = profile;
			for (int i = 0; i < 12; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					profile_array[i][j] = 0;
				}
			}
			for (int k = 9; k >= 1; k--)
			{
				if (savep % 3 == 0)
				{
					profile_array[k][0] = profile_array[k][1] = profile_array[k][2] = 0;
				}
				else if (savep % 3 == 1)
				{
					profile_array[k][0] = 1;
					profile_array[k][1] = profile_array[k][2] = 0;
				}
				else
				{
					profile_array[k][0] = profile_array[k][1] = 1;
					profile_array[k][2] = 0;
				}
				savep /= 3;
			}
			// generating a next profile
			next_profile(profile_array, 1, profile, row_num, dp);
		}
	}
	printf("%lld\n", dp[12][0]);
	for (int i = 0; i < 13; i++)
	{
		free(dp[i]);
	}
	free(dp);
	return 0;
}