cmake_minimum_required(VERSION 2.8)
project(openglShader) 

set(CMAKE_CXX_STANDARD 17)      # 设置C++标准为C++11
set(OpenGL_GL_PREFERENCE "GLVND")
find_package(OpenGL REQUIRED)   # 查找系统安装的OpenGL库
find_package(glfw3 REQUIRED)    # 查找系统安装的glfw库

include_directories(${OPENGL_INCLUDE_DIRS} ${GLFW_INCLUDE_DIRS}  ./ src/glad/include/glad)

add_executable(openglShader main.cpp src/glad/src/glad.c) 

target_link_libraries(openglShader ${OPENGL_LIBRARIES} glfw) # 链接所需要的库文件