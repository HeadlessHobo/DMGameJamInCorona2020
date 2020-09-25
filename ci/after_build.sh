absolute_path_to_built_zip_nas_file=$(pwd)/$NAS_BUILD_DIRECTORY/$DRONE_REPO_NAME/${DRONE_BUILD_NUMBER}-${BUILD_NAME}-${DRONE_COMMIT_AUTHOR_NAME}-${DRONE_COMMIT_MESSAGE}.zip
absolute_path_to_built_zip_itch_file=$(pwd)/$ITCH_BUILD_DIRECTORY/${BUILD_NAME}.zip
mkdir -p $NAS_BUILD_DIRECTORY/$DRONE_REPO_NAME
mkdir -p $ITCH_BUILD_DIRECTORY/$DRONE_REPO_NAME

mv ${BUILD_PATH}${BUILD_NAME}.exe ${BUILD_PATH}${DRONE_REPO_NAME}.exe
mv ${BUILD_PATH}${BUILD_NAME}_Data ${BUILD_PATH}${DRONE_REPO_NAME}_Data

# Removes spaces from zip file path
absolute_path_to_built_zip_nas_file = ${absolute_path_to_built_zip_nas_file// /_}
absolute_path_to_built_zip_itch_file = ${absolute_path_to_built_zip_itch_file// /_}

# Removes anything that's not alphanumeric or an underscore
absolute_path_to_built_zip_nas_file=${absolute_path_to_built_zip_nas_file//[^a-zA-Z0-9_]/}
absolute_path_to_built_zip_itch_file=${absolute_path_to_built_zip_itch_file//[^a-zA-Z0-9_]/}

cd $BUILD_PATH
zip -r "$absolute_path_to_built_zip_nas_file" .
zip -r "$absolute_path_to_built_zip_itch_file" .