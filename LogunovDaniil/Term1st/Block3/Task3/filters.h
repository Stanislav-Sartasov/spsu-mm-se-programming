#pragma once

typedef unsigned char byte;

int grayscale(byte** img, int height, int width, int byteCount);

int gauss(byte** img, int height, int width, int byteCount);

int median(byte** img, int height, int width, int byteCount);

int sobelX(byte** img, int height, int width, int byteCount);

int sobelY(byte** img, int height, int width, int byteCount);
