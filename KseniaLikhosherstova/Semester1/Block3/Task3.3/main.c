#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#define SWAP(type, a, b) type tmp = a; a = b; b = tmp
#define MIN(a,b) a < b ? a : b 



typedef unsigned short WORD;
typedef unsigned long DWORD;
typedef long LONG;
typedef unsigned char BYTE;



typedef struct bmp_file_header 
{
    BYTE type_f, type_s; 
    DWORD size_f; 
    WORD reserv_one;
    WORD reserv_two; 
    DWORD off_bits; 
} bmp_file_header;

typedef struct bmp_info_header 
{
    DWORD size; 
    LONG width; 
    LONG hight; 
    WORD planes; 
    WORD bit_count; 
    DWORD compression; 
    DWORD size_image; 
    LONG XPPM;
    LONG YPPM; 
    DWORD color_used; 
    DWORD color_imp; 
} bmp_info_header;

typedef struct pixel 
{
    BYTE red;
    BYTE green;
    BYTE blue;
    BYTE reserv;
} pixel;

void rat(bmp_file_header* BMPFH , bmp_info_header* BMPIH, pixel** mas)
{ 

    mas[0][0] = mas[2][2];
    mas[1][1] = mas[2][2];
    mas[0][(*BMPIH).width + 3] = mas[2][(*BMPIH).width + 1];
    mas[1][(*BMPIH).width + 2] = mas[2][(*BMPIH).width + 1];
    mas[(*BMPIH).hight + 3][0] = mas[(*BMPIH).hight + 1][2];
    mas[(*BMPIH).hight + 2][1] = mas[(*BMPIH).hight + 1][2];
    mas[(*BMPIH).hight + 3][(*BMPIH).width + 3] = mas[(*BMPIH).hight + 1][(*BMPIH).width + 1];
    mas[(*BMPIH).hight + 2][(*BMPIH).width + 2] = mas[(*BMPIH).hight + 1][(*BMPIH).width + 1];


    for (int i = 2; i < (*BMPIH).hight + 2; ++i) 
    {
        mas[i][1] = mas[i][2];
    }
    for (int i = 1; i < (*BMPIH).hight + 3; ++i) 
    {
        mas[i][0] = mas[i][2];
    }
    
    for (int i = 2; i < (*BMPIH).hight + 2; ++i) 
    {
        mas[i][(*BMPIH).width + 2] = mas[i][(*BMPIH).width + 1];
    }
    for (int i = 1; i < (*BMPIH).hight + 3; ++i) 
    {
        mas[i][(*BMPIH).width + 3] = mas[i][(*BMPIH).width + 1];
    }
    
    for (int j = 2; j < (*BMPIH).width + 2; ++j) 
    {
        mas[1][j] = mas[2][j];
    }
    for (int j = 1; j < (*BMPIH).width + 3; ++j) {
        mas[0][j] = mas[2][j];
    }
    
    for (int j = 2; j < (*BMPIH).width + 2; ++j) 
    {
        mas[(*BMPIH).hight + 2][j] = mas[(*BMPIH).hight + 1][j];
    }
    for (int j = 1; j < (*BMPIH).width + 3; ++j) 
    {
        mas[(*BMPIH).hight + 3][j] = mas[(*BMPIH).hight + 1][j];
    }

}

void read_h(bmp_file_header* BMPFH, bmp_info_header* BMPIH, FILE* file) 
{

    rewind(file); 

    fread(&BMPFH->type_f, sizeof((*BMPFH).type_f), 1, file);
    fread(&BMPFH->type_s, sizeof((*BMPFH).type_s), 1, file);
    fread(&BMPFH->size_f, sizeof((*BMPFH).size_f), 1, file);
    fread(&BMPFH->reserv_one, sizeof((*BMPFH).reserv_one), 1, file);
    fread(&BMPFH->reserv_two, sizeof((*BMPFH).reserv_two), 1, file);
    fread(&BMPFH->off_bits, sizeof((*BMPFH).off_bits), 1, file);



    fread(&BMPIH->size, sizeof((*BMPIH).size), 1, file);
    fread(&BMPIH->width, sizeof((*BMPIH).width), 1, file);
    fread(&BMPIH->hight, sizeof((*BMPIH).hight), 1, file);
    fread(&BMPIH->planes, sizeof((*BMPIH).planes), 1, file);
    fread(&BMPIH->bit_count, sizeof((*BMPIH).bit_count), 1, file);
    fread(&BMPIH->compression, sizeof((*BMPIH).compression), 1, file);
    fread(&BMPIH->size_image, sizeof((*BMPIH).size_image), 1, file);
    fread(&BMPIH->XPPM, sizeof((*BMPIH).XPPM), 1, file);
    fread(&BMPIH->YPPM, sizeof((*BMPIH).YPPM), 1, file);
    fread(&BMPIH->color_used, sizeof((*BMPIH).color_used), 1, file);
    fread(&BMPIH->color_imp, sizeof((*BMPIH).color_imp), 1, file);

    

    if ((*BMPFH).type_f != 'B' || (*BMPFH).type_s != 'M') 
    {
        printf("Unsupported file format....\n");
        fclose(file);
        exit(1.1);
    } 

    if ((*BMPFH).reserv_one != 0 || (*BMPFH).reserv_two != 0) 
    {
        printf("This is not a BMP file...\n");
        fclose(file);
        exit(1.2);
    }      

}

void read_p(bmp_file_header* BMPFH, bmp_info_header* BMPIH, pixel** mas, FILE* file) 
{

    fseek(file, BMPFH->off_bits, 0); 

    if (BMPIH->bit_count == 32) 
    { 
        for (int i = 0; i < BMPIH->hight + 4; ++i) 
        {
            for (int j = 0; j < BMPIH->width + 4; ++j) 
            {
                if (i <= 1 || j <= 1 || i >= BMPIH->hight + 2 || j >= BMPIH->width + 2) 
                {
                    mas[i][j].blue = 0;
                    mas[i][j].green = 0;
                    mas[i][j].red = 0;
                }
                else 
                {
                    fread(&mas[i][j].blue, sizeof(BYTE), 1, file);
                    fread(&mas[i][j].green, sizeof(BYTE), 1, file);
                    fread(&mas[i][j].red, sizeof(BYTE), 1, file);
                    fread(&mas[i][j].reserv, sizeof(BYTE), 1, file);
                }
            }
        }
    }
    else 
    { 
        for (int i = 0; i < BMPIH->hight + 4; ++i) 
        {
            for (int j = 0; j < BMPIH->width + 4; ++j) 
            {
                if (i <= 1 || j <= 1 || i >= BMPIH->hight + 2 || j >= BMPIH->width + 2) 
                {
                    mas[i][j].blue = 0;
                    mas[i][j].green = 0;
                    mas[i][j].red = 0;
                }
                else 
                {
                    fread(&mas[i][j].blue, sizeof(BYTE), 1, file);
                    fread(&mas[i][j].green, sizeof(BYTE), 1, file);
                    fread(&mas[i][j].red, sizeof(BYTE), 1, file);
                }
            }
        }
    }
    rat(BMPFH, BMPIH, mas);
}

void save(bmp_file_header* BMPFH, bmp_info_header* BMPIH, pixel** mas, FILE* file) 
{

    fwrite(&BMPFH->type_f, sizeof((*BMPFH).type_f), 1, file);
    fwrite(&BMPFH->type_s, sizeof((*BMPFH).type_s), 1, file);
    fwrite(&BMPFH->size_f, sizeof((*BMPFH).size_f), 1, file);
    fwrite(&BMPFH->reserv_one, sizeof((*BMPFH).reserv_one), 1, file);
    fwrite(&BMPFH->reserv_two, sizeof((*BMPFH).reserv_two), 1, file);
    fwrite(&BMPFH->off_bits, sizeof((*BMPFH).off_bits), 1, file);

 

    fwrite(&BMPIH->size, sizeof((*BMPIH).size), 1, file);
    fwrite(&BMPIH->width, sizeof(BMPIH->width), 1, file);
    fwrite(&BMPIH->hight, sizeof(BMPIH->hight), 1, file);
    fwrite(&BMPIH->planes, sizeof(BMPIH->planes), 1, file);
    fwrite(&BMPIH->bit_count, sizeof(BMPIH->bit_count), 1, file);
    fwrite(&BMPIH->compression, sizeof(BMPIH->compression), 1, file);
    fwrite(&BMPIH->size_image, sizeof(BMPIH->size_image), 1, file);
    fwrite(&BMPIH->XPPM, sizeof(BMPIH->XPPM), 1, file);
    fwrite(&BMPIH->YPPM, sizeof(BMPIH->YPPM), 1, file);
    fwrite(&BMPIH->color_used, sizeof(BMPIH->color_used), 1, file);
    fwrite(&BMPIH->color_imp, sizeof(BMPIH->color_imp), 1, file);

  

    if (BMPIH->bit_count == 32) 
    {
        for (int i = 2; i < BMPIH->hight + 2; ++i) 
        {
            for (int j = 2; j < BMPIH->width + 2; ++j) 
            {
                fwrite(&mas[i][j].blue, sizeof(BYTE), 1, file);
                fwrite(&mas[i][j].green, sizeof(BYTE), 1, file);
                fwrite(&mas[i][j].red, sizeof(BYTE), 1, file);
                fwrite(&mas[i][j].reserv, sizeof(BYTE), 1, file);
            }
        }
    }
    else 
    {
        for (int i = 2; i < BMPIH->hight + 2; ++i) 
        {
            for (int j = 2; j < BMPIH->width + 2; ++j) 
            {
                fwrite(&mas[i][j].blue, sizeof(BYTE), 1, file);
                fwrite(&mas[i][j].green, sizeof(BYTE), 1, file);
                fwrite(&mas[i][j].red, sizeof(BYTE), 1, file);
            }
        }
    }


    fclose(file);
}

void median_f(bmp_file_header* BMPFH, bmp_info_header* BMPIH, pixel** mas) 
{
    pixel buf[9];
    for (int i = 2; i < BMPIH->hight + 2; ++i) 
    {
        for (int j = 2; j < BMPIH->width + 2; ++j) 
        { 
            int k = 0;
            for (int i1 = i - 1; (i1 < i1 + 1) && (k < 9); ++i1) 
            {
                for (int j1 = j - 1; j1 <= j + 1; ++j1, ++k) 
                {
                    buf[k] = mas[i1][j1]; 
                }
            }
            for (int k = 0; k < 9; ++k) 
            {
                for (int k1 = 0; k1 < 9; ++k1) 
                {
                    if ((buf[k].red * 0.212 + buf[k].green * 0.715 + buf[k].blue * 0.072) <
                        (buf[k1].red * 0.212 + buf[k1].green * 0.715 + buf[k1].blue * 0.072)) 
                    {
                        SWAP(pixel, buf[k], buf[k1]); 
                    }
                }
            }
            mas[i][j] = buf[4]; 
        }
    }
}

void gauss_f(bmp_file_header* BMPFH, bmp_info_header* BMPIH, pixel** mas) 
{

    BYTE apl[9] = { 1,2,1,2,4,2,1,2,1 }; 
    int k = 0;
    float green = 0, red = 0, blue = 0;

    for (int i = 2; i < BMPIH->hight + 2; ++i) 
    {
        for (int j = 2; j < BMPIH->width + 2; ++j) 
        { 
            k = 0, green = 0, red = 0, blue = 0;
            for (int i1 = i - 1; (i1 < i1 + 1) && (k < 9); ++i1) 
            {
                for (int j1 = j - 1; j1 <= j + 1; ++j1, ++k) 
                {
                    green += mas[i1][j1].green * apl[k] / 16;
                    blue += mas[i1][j1].blue * apl[k] / 16;
                    red += mas[i1][j1].red * apl[k] / 16;
                }
            }
            mas[i][j].blue = (BYTE)blue;
            mas[i][j].red = (BYTE)red;
            mas[i][j].green = (BYTE)green; 
        }
    }

}


void gray_f(bmp_file_header* BMPFH, bmp_info_header* BMPIH, pixel** mas) 
{
    BYTE average = 0;
    for (int i = 0; i < BMPIH->hight + 4; ++i) 
    {
        for (int j = 0; j < BMPIH->width + 4; ++j) 
        {
            average = (mas[i][j].red + mas[i][j].green + mas[i][j].blue) / 3;
            mas[i][j].red = average;
            mas[i][j].green = average;
            mas[i][j].blue = average;
        }
    }
} 

void sobel_fX(bmp_file_header* BMPFH, bmp_info_header* BMPIH, pixel** mas) 
{

    gray_f(BMPFH, BMPIH, mas); 

    int apl[9] = { -1, 0, 1, -2, 0, 2, -1, 0, 1 }; 
    int k = 0;
    float green = 0, red = 0, blue = 0;

    for (int i = 2; i < BMPIH->hight + 2; ++i) 
    {
        for (int j = 2; j < BMPIH->width + 2; ++j) 
        { 
            k = 0, green = 0, red = 0, blue = 0;
            for (int i1 = i - 1; (i1 < i1 + 1) && (k < 9); ++i1) 
            {
                for (int j1 = j - 1; j1 <= j + 1; ++j1, ++k) 
                {
                    green += mas[i1][j1].green * apl[k];
                    blue += mas[i1][j1].blue * apl[k];
                    red += mas[i1][j1].red * apl[k]; 
                }
            }
            mas[i][j].blue = MIN(abs(blue), 255); 
            mas[i][j].red = MIN(abs(red), 255);
            mas[i][j].green = MIN(abs(green), 255);
        }
    }
}

void sobel_fY(bmp_file_header* BMPFH, bmp_info_header* BMPIH, pixel** mas) 
{

    gray_f(BMPFH, BMPIH, mas);

    int apl[9] = { -1, -2, -1, 0, 0, 0, 1, 2, 1 }; 
    int k = 0;
    float green = 0, red = 0, blue = 0;

    for (int i = 2; i < BMPIH->hight + 2; ++i) 
    {
        for (int j = 2; j < BMPIH->width + 2; ++j) 
        { 
            k = 0, green = 0, red = 0, blue = 0;
            for (int i1 = i - 1; (i1 < i1 + 1) && (k < 9); ++i1) 
            {
                for (int j1 = j - 1; j1 <= j + 1; ++j1, ++k) 
                {
                    green += mas[i1][j1].green * apl[k];
                    blue += mas[i1][j1].blue * apl[k];
                    red += mas[i1][j1].red * apl[k];
                }
            }
            mas[i][j].blue = MIN(abs(blue), 255);
            mas[i][j].red = MIN(abs(red), 255);
            mas[i][j].green = MIN(abs(green), 255);
        }
    } 
}

int main()
{

    bmp_file_header BMPFH;
    bmp_info_header BMPIH;

    char file_rep[80]; 
    char file_out_rep[80]; 

    printf("Enter a file path: ");

    scanf("%s", file_rep);

    FILE* file = fopen(file_rep, "rb");
    if (file == NULL) 
    {
        printf("Error while trying to open file \n");
        exit(1.0);
    }   

    read_h(&BMPFH, &BMPIH, file);

    pixel** mas = (pixel**)malloc((BMPIH.hight + 4) * sizeof(pixel*));

    for (int i = 0; i < BMPIH.hight + 4; ++i) 
    {
        mas[i] = (pixel*)malloc((BMPIH.width + 4) * sizeof(pixel));
    }

    read_p(&BMPFH, &BMPIH, mas, file);

    fclose(file);

    int t = 1;

    printf("Select a filter: \n 1) greyscale \n 2) median 3x3 \n 3) gaussian 3õ3 \n 4) sobelX \n 5) sobelY \nInput: ");

    scanf("%d", &t);

    if (t == 1) 
    {
        gray_f(&BMPFH, &BMPIH, mas);
    }
    else if (t == 2) 
    {
        median_f(&BMPFH, &BMPIH, mas);
    }
    else if (t == 3) 
    {
        gauss_f(&BMPFH, &BMPIH, mas);
    }
    else if (t == 4) 
    {
        sobel_fX(&BMPFH, &BMPIH, mas);
    }
    else if (t == 5) 
    {
        sobel_fY(&BMPFH, &BMPIH, mas);
    }
    else 
    {
        printf("Invalid input");
        exit(-21213);
    }

    printf("Enter the path to save the file: ");
    scanf("%s", file_out_rep);

    FILE* out = fopen(file_out_rep, "wb");

    save(&BMPFH, &BMPIH, mas, out);

    return 0;
}
