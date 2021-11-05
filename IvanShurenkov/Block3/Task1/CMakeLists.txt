cmake_minimum_required(VERSION 3.20)
project(Task1 C)

set(CMAKE_C_STANDARD 11)

if (UNIX)
    message("Linux")
    add_compile_definitions(IS_LINUX)
else(WIN32 || CYGWIN || MINGW || MSYS)
    message("Windows")
    add_compile_definitions(IS_WINDOWS)
    option(DCMAKE_SYSTEM_NAME WindowsStore)
    option(DCMAKE_SYSTEM_VERSION 10)
endif()

add_executable(Task1 main.c)
#-DCMAKE_SYSTEM_NAME=WindowsStore -DCMAKE_SYSTEM_VERSION=10