cmake_minimum_required(VERSION 3.20)
project(Task3 C)

set(CMAKE_C_STANDARD 11)

SET( CMAKE_EXE_LINKER_FLAGS "-lm")

add_executable(Task3 main.c)
