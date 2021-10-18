cmake_minimum_required(VERSION 3.20)
project(Task4 C)

set(CMAKE_C_STANDARD 11)

set(CMAKE_EXE_LINKER_FLAGS "-lm")

add_executable(Task4 main.c)

if (CMAKE_C_COMPILER_ID MATCHES "MSVC")
    add_compile_definitions(IS_MSVC=1)
endif()
