#define _CRT_SECURE_NO_WARNINGS

#include "bmp.h"
#include "../lib/error.h"

#include <stdio.h>
#include <stdlib.h>

bmp_image_t* read_image(char* filename)
{
	FILE* input;

	if ((input = fopen(filename, "rb")) == NULL)
		return error("Unable to open input image file.\n");

	bmp_image_t* image = (bmp_image_t*)malloc(sizeof(bmp_image_t));
	if (image == NULL)
		return error("Unable to allocate memory for image structure.\n");

	// reading file header and file info
	fread(&image->header, sizeof(image->header), 1, input);
	fread(&image->info, sizeof(image->info), 1, input);
	//

	if (image->info.bits_per_pixel != 24 && image->info.bits_per_pixel != 32)
		return error("Invalid image format.\n");

	// defining properties
	image->width = image->info.width;
	image->height = image->info.height;
	image->size = image->header.size;
	//

	// reading pixels
	image->data = (pixel*)malloc(image->size);
	if (image->data == NULL)
		return error("Unable to allocate memory for image pixels.\n");

	fseek(input, image->header.offset, SEEK_SET);

	if (image->info.bits_per_pixel == 24)
	{
		fread(image->data, image->size, 1, input);
	}
	else
	{
		for (int i = 0; i * 4 < image->info.image_size; i++)
			fread(&image->data[i], 4, 1, input);
	}
	//

	fclose(input);

	return image;
}

void close_image(bmp_image_t* image)
{
	free(image->data);
	free(image);
}

int write_image(char* filename, bmp_image_t* image)
{
	FILE* output;

	if ((output = fopen(filename, "wb")) == NULL)
		return error("Unable to create output image file.\n");
	
	// writing file header and file info
	if (fwrite(&image->header, sizeof(image->header), 1, output) == NULL)
		return 0;
	if (fwrite(&image->info, sizeof(image->info), 1, output) == NULL)
		return 0;
	//

	// writing pixels
	fseek(output, image->header.offset, SEEK_SET);

	if (image->info.bits_per_pixel == 24)
	{
		if (fwrite(image->data, image->size, 1, output) == NULL)
			return 0;
	}
	else
	{
		for (int i = 0; i * 4 < image->info.image_size; i++)
			if (fwrite(&image->data[i], 4, 1, output) == NULL)
				return 0;
	}
	//

	fclose(output);

	return 1;
}