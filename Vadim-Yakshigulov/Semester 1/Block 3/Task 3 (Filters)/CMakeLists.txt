cmake_minimum_required(VERSION 3.20)
project(Task_3__Filters_ C)

set(CMAKE_C_STANDARD 11)

include_directories(.)

add_executable(Task_3__Filters_
        bmp.c
        bmp.h
        filters.c
        filters.h
        main.c)
