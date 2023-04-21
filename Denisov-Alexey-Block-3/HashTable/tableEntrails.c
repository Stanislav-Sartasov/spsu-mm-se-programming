#include <stdio.h>
#include <stdlib.h>
#include "tableEntrails.h"

pointer* rebalance(pointer** p)
{
	pointer* copy = (pointer*)malloc(sizeof(pointer));
	copy->arrChains = (table*)malloc(REBALANCE_MULTIPLIER * (*p)->arrChains->divisor * sizeof(table));
	copy->arrChains->divisor = REBALANCE_MULTIPLIER * (*p)->arrChains->divisor;

	for (int i = 0; i < copy->arrChains->divisor; i++)
	{
		copy->arrChains[i].divisor = copy->arrChains->divisor;
		copy->arrChains[i].chainsFirst = NULL;
	}

	element* head = NULL;
	for (int i = 0; i < (*p)->arrChains->divisor; i++)
	{
		head = (*p)->arrChains[i].chainsFirst;
		while (head != NULL)
		{
			add(&copy, head->value);
			head = head->next;
		}
	}

	return copy;
}