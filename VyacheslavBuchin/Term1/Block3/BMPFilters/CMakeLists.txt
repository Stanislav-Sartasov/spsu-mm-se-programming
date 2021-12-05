cmake_minimum_required(VERSION 3.20)
project(BMPFilters C)

set(CMAKE_C_STANDARD 99)

add_executable(BMPFilters main.c bmp/bmp.c bmp/bmp.h filter/filter.c filter/filter.h filter/kernel/kernel.c filter/kernel/kernel.h bmp_filter.c bmp_filter.h)
