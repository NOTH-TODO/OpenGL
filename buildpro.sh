#!/bin/sh
argument1=${1:-make}
if [ "$argument1" = "cleanbuild" ]; then
    if [ -d "build" ]; then
        rm -r build
    fi
    exit 0
fi

nproc=8
if [ ! -d "build" ]; then mkdir build; fi
cd build
cmake ../openglShader/
make -j"$nproc"
