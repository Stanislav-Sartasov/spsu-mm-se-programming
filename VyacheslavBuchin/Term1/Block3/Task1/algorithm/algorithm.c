//
// Created by Вячеслав Бучин on 30.10.2021.
//

#include "algorithm.h"

int count(char* begin, const char* end, char symbol)
{
	int answer = 0;
	while (begin < end)
	{
		answer += *begin == symbol;
		begin++;
	}
	return answer;
}

char* find(char* begin, const char* end, char symbol)
{
	while (begin < end && *begin != symbol)
		begin++;
	return begin;
}
