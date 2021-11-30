#include "bmp_filter.h"

int main(int argc, char** argv)
{
	if (validate_args(argc, argv))
		return -1;

	bmp_t* file;
	int open_code = bmp_load(argv[1], &file);
	process_bmp_error(open_code, argv[1]);
	if (open_code)
		return -1;

	if (process_bmp_filter(file, argv[2]) == NO_SUCH_FILTER)
	{
		print_error("Invalid filter name");
		return -1;
	}

	int write_code = bmp_write(argv[3], file);
	process_bmp_error(write_code, argv[3]);
	if (write_code)
		return -1;

	bmp_free(file);

	return 0;
}
