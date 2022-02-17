cmake_minimum_required(VERSION 3.20)
project(. C)

set(CMAKE_C_STANDARD 11)
add_executable(. main.c main.c big_integer.c big_integer.h)