#include <stdio.h>
#include <string.h>

void binSystem(char* binRep, int size, long long nameLen)
{
	while (size >= 0)
	{
		if (nameLen & 1) binRep[size] = '1';
		else binRep[size] = '0';
		nameLen = nameLen >> 1;
		size--;
	}
}


int main()
{
	printf("The program counts the product of the lengths of the surname, name and patronymic\nand gives its representation in negative binomial 32 bytes system,\npositive Single Precision IEEE 754 Floating-Point Standard\nand negative Double Precision IEEE 754 Floating-Point Standard.\n\n");
	long long nameLen = strlen("Харитонов") * strlen("Александр") * strlen("Алексеевич");
	printf("The product of the lengths of my surname, name and patronymic is %d.\n\n", nameLen);
	char res_32bytes[33] = "\0", res_single_IEEE754[33] = "\0", res_double_IEEE754[65] = "\0";
	binSystem((char *) &res_32bytes, 31, -nameLen);
	printf("The negative binomial representation of the product in 32 bytes is %s.\n", res_32bytes);
	nameLen = *(long int*)&nameLen;
	binSystem((char *) &res_single_IEEE754, 31, (long int)nameLen);
	printf("The positive binomial representation of the product in Single Precision IEEE 754 Floating-Point Standard is\n%s.\n", res_single_IEEE754);
	nameLen = -nameLen;
	binSystem((char *) &res_double_IEEE754, 63, nameLen);
	printf("The negative binomial representation of the product in Double Precision IEEE 754 Floating-Point Standard is\n%s.\n", res_double_IEEE754);
	return 0;
}