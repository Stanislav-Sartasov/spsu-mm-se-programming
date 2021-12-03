#include <stdio.h>
#include "error.h"

int error(char* message)
{
	printf(message);
	return NULL;
}