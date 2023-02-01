#!/usr/bin/env sh
set -x

export UNITY_EXECUTABLE=${UNITY_EXECUTABLE:-"/Applications/Unity/Hub/Editor/2021.3.15f1/Unity.app"}

BUILD_TARGET=StandaloneLinux64 ./ci/build.sh

