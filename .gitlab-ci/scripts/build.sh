#!/usr/bin/env bash

set -e
set -x

echo "Building for $BUILD_TARGET"

export BUILD_PATH=$UNITY_DIR/Builds/$BUILD_TARGET/
export UNITY_EXECUTABLE="/Applications/Unity/Hub/Editor/2021.3.15f1/Unity.app/Contents/MacOS/Unity"
mkdir -p $BUILD_PATH


mkdir -p $UNITY_DIR/Log
touch $UNITY_DIR/Log/log.txt
# echo "test artifact" > $UNITY_DIR/Log/log.txt

/Applications/Unity/Hub/Editor/2021.3.15f1/Unity.app/Contents/MacOS/Unity \
  -projectPath $UNITY_DIR \
  -quit \
  -batchmode \
  -nographics \
  -buildTarget $BUILD_TARGET \
  -customBuildTarget $BUILD_TARGET \
  -customBuildName $BUILD_NAME \
  -customBuildPath $BUILD_PATH \
  -executeMethod BuildCommand.PerformBuild \
  -logFile $UNITY_DIR/Log/log.txt

UNITY_EXIT_CODE=$?

if [ $UNITY_EXIT_CODE -eq 0 ]; then
  echo "Run succeeded, no failures occurred";
elif [ $UNITY_EXIT_CODE -eq 2 ]; then
  echo "Run succeeded, some tests failed";
elif [ $UNITY_EXIT_CODE -eq 3 ]; then
  echo "Run failure (other failure)";
else
  echo "Unexpected exit code $UNITY_EXIT_CODE";
fi

ls -la $BUILD_PATH
[ -n "$(ls -A $BUILD_PATH)" ] # fail job if build folder is empty
