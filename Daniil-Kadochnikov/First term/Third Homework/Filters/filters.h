
struct rgb
{
	unsigned char blue;
	unsigned char green;
	unsigned char red;
};

struct image
{
	struct rgb** pixels;
	int height;
	int width;
};

void allocationError();
void greyFilter(struct image* image);
int sobelYFunction(int* massive);
int sobelXFunction(int* massive);
int medianFunction(int* massive);
int gauss3Function(int* massive);
int gauss5Function(int* massive);
int filter(struct image* image, int (*filterFunction)(int*), int number);