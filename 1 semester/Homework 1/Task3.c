#include <stdio.h>
#include <math.h>

double radian(double a, double b, double c)
{
    return (acos((b*b+c*c-a*a)/(2*b*c))*180)/M_PI;
}

double degree(double d)
{
    int D, M, S;
    D=(int)d;
    M=(int)(60*(d-D));
    S=(int)((3600*(d-D))-60*M);
    printf("%d° %d' %d'' \n", D, M, S);
}

int main()
{
    int a, b, c;
    printf("Enter numbers: ");
    scanf("%d", &a);
    scanf("%d", &b);
    scanf("%d", &c);
    if ((a+b>c)&(a+c>b)&(c+b>a))
    {
		degree(radian(a, b, c));
		degree(radian(b, a, c));
		degree(radian(c, b, a));
    }
    else
        {
            printf("Degenerate triangle\n");
        }
    return 0;
}
