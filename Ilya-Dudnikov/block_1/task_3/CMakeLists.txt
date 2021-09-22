cmake_minimum_required(VERSION 3.0.0)
project(task_3 VERSION 0.1.0)

include(CTest)
enable_testing()

add_executable(task_3 main.c)

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)

target_link_libraries(task_3 PRIVATE m)
