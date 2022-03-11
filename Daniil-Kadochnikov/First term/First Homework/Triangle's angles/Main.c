#define _CRT_SECURE_NO_WARNINGS
#define M_PI 3.14159265358979323846
#include <stdio.h>
#include <stdlib.h>
#include <math.h>



int userInput(long double* pa, long double* pb, long double* pc)
{
	int operation;
	do
	{
		fseek(stdin, 0, 0);
		printf("\nEnter the positive a value, the length of the first triangle's side, please.\na = ");
		operation = scanf("%lf", pa);
	} while (operation != 1 || *pa <= 0);
	do
	{
		fseek(stdin, 0, 0);
		printf("\nEnter the positive b value, the length of the second triangle's side, please.\nb = ");
		operation = scanf("%lf", pb);
	} while (operation != 1 || *pb <= 0);
	do
	{
		fseek(stdin, 0, 0);
		printf("\nEnter the positive c value, the length of the third triangle's side, please.\nc = ");
		operation = scanf("%lf", pc);
	} while (operation != 1 || *pc <= 0);
	return *pa, *pb, *pc;
}

long double angleCalc(long double *op, long double *ad1, long double* ad2)
{
	return (acos((pow(*ad1, 2) + pow(*ad2, 2) - pow(*op, 2)) / (2 * (*ad1) * (*ad2))) / M_PI * 180); //in degrees
}

void convertionAndPrint(long double* angle)
{

	int degrees, minutes, seconds;
	degrees = *angle;
	*angle -= degrees;
	minutes = *angle * 60;
	*angle = *angle *60 - minutes;
	seconds = round(*angle * 60);
	printf(">>> %d*%d'%ld''\n", degrees, minutes, seconds);
}

void output(long double* anga, long double* angb, long double* angc)
{
	convertionAndPrint(anga);
	convertionAndPrint(angb);
	convertionAndPrint(angc);
}

void triangleBuilding(long double* pa, long double* pb, long double* pc)
{
	if ((*pa + *pb > *pc) && (*pa + *pc > *pb) && (*pc + *pb > *pa))   //if true - triangle exists
	{
		long double anga, angb, angc;   //angles of a triangle
		anga = angleCalc(pa, pb, pc); 
		angb = angleCalc(pb, pa, pc);
		angc = angleCalc(pc, pb, pa);
		printf("\n>>> The triangle with the sides %f, %f, %f has the following angles:\n", *pa, *pb, *pc);
		output(&anga, &angb, &angc);
		
	}
	else
	{
		printf("\n>>> There is no non - degenerate triangle with sides %lf, %lf, %lf.<<<\n", *pa, *pb, *pc);
		exit(EXIT_SUCCESS);
	}
}

int main()
{
	printf("The program takes three positive numbers and tells whether it is possible to build a non degenerate triangle with the entered lengthes.\n If it is, the program gives the values of the triangle's angles in degrees, minutes and seconds.");
	long double a, b, c;
	userInput(&a, &b, &c);
	triangleBuilding(&a, &b, &c);
	return 0;
}
