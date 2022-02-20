#include <stdio.h>
#include <math.h>

struct device {
	unsigned active : 1;
	unsigned ready : 9;
	unsigned xmt_error : 1;
};

int main()
{
	struct device asd;

}