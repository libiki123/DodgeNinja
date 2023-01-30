#!/usr/bin/env bash
CI_PROJECT_DIR=$1
set -e #  use to exit when the command exits with a non-zero status
set -x # Turn on debug
mkdir -p $CI_PROJECT_DIR/.cache/unity3d
mkdir -p $CI_PROJECT_DIR/.local/share/unity3d/Unity/
set +x # Turn off debug

unity_license_destination=/root/.local/share/unity3d/Unity/Unity_lic.ulf
android_keystore_destination=keystore.keystore


upper_case_build_target=${BUILD_TARGET^^};

if [ "$upper_case_build_target" = "ANDROID" ]
then
    if [ -n $ANDROID_KEYSTORE_BASE64 ]
    then
        echo "'\$ANDROID_KEYSTORE_BASE64' found, decoding content into ${android_keystore_destination}"
        echo $ANDROID_KEYSTORE_BASE64 | base64 --decode > ${android_keystore_destination}
    else
        echo '$ANDROID_KEYSTORE_BASE64'" env var not found, building with Unity's default debug keystore"
    fi
fi

if [ -n "$UNITY_LICENSE" ]
then
    echo "Writing '\$UNITY_LICENSE' to license file ${unity_license_destination}"
    echo "${UNITY_LICENSE}" | tr -d '\r' > ${unity_license_destination}
else
    echo "'\$UNITY_LICENSE' env var not found"
fi
