cmake_minimum_required(VERSION 3.20)
project(Long_arithmetic C)

set(CMAKE_C_STANDARD 99)

add_executable(Long_arithmetic main.c big_int.c big_int.h)
