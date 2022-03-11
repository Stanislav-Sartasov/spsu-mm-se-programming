#define _CRT_SECURE_NO_WARNINGS
#define byte unsigned char

#include <stdio.h>
#include <string.h>
#include "file_utils.h"
#include "filters.h"

int main(int argc, char* argv[])
{
	printf("This program applies filters to given bmp images.\n");

	// Inputs
	if (argc != 4)
	{
		printf("Too many / not enough arguments.\nProgram shhould be called like this:\n name.exe input.bmp filter output.bmp\n");
		printf("Available filters: SobelX, SobelY, Gauss3, Gray, Median");
		return -1;
	}

	// Input file opening
	FILE* ptr_in;
	ptr_in = fopen(argv[1], "rb+");
	if (ptr_in == NULL)
	{
		printf("Error opening input file");
		return -1;
	}

	// Output file creation;
	FILE* ptr_out;
	ptr_out = fopen(argv[3], "wb+");
	if (ptr_out == NULL)
	{
		printf("Error creating/opening output file");
		return -1;
	}

	// Parsing the image 
	struct bmp_file* inp_file = parse(ptr_in);
	if (inp_file == NULL)
	{
		printf("File format is either non 24 or 32 bit color or is outdated.\n Please, try another file.");
		return -1;
	}

	// Applying the filter
	if (strcmp(argv[2], "SobelX") == 0)
	{
		sobel_x(inp_file);
	}
	else if (strcmp(argv[2], "SobelY") == 0)
	{
		sobel_y(inp_file);
	}
	else if (strcmp(argv[2], "Gray") == 0)
	{
		monochrome_filter(inp_file);
	}
	else if (strcmp(argv[2], "Median") == 0) 
	{
		med_filter(inp_file);
	}
	else if (strcmp(argv[2], "Gauss3") == 0)
	{
		gauss_filter(inp_file);
	}
	else 
	{
		printf("The filter you entered does not exist.\n");
		printf("Available filters: SobelX, SobelY, Gauss3, Gray, Median");
		return -1;
	}


	// Output file handling
	fseek(ptr_in, 0, SEEK_SET);
	byte next;
	for (int i = 0; i < parse_int(&(inp_file->header_info[10])); i++)
	{
		fread(&next, 1, 1, ptr_in);
		fwrite(&next, 1, 1, ptr_out);
	}
	for (int i = 0; i < inp_file->size - 54; i++)
		fwrite(&(inp_file->data[i]), 1, 1, ptr_out);

	fclose(ptr_in);
	fclose(ptr_out);
	free_bmp_file(inp_file);

	printf("\n\nDone V\n");
	return 0;
}