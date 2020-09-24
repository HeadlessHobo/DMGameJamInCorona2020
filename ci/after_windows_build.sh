absolute_path_to_built_zip_file=$(pwd)/$WINDOWS_BUILD_DIRECTORY/${BUILD_NAME}.zip

# Rename the files to match the drone repo name
mv $(pwd)/${BUILD_PATH}/${BUILD_NAME}.exe $(pwd)/${BUILD_PATH}/${DRONE_REPO_NAME}.exe
mv $(pwd)/${BUILD_PATH}/${BUILD_NAME}_Data $(pwd)/${BUILD_PATH}/${DRONE_REPO_NAME}_Data

# -p create the directory(along with the directories that lead to the directory you want to create)
mkdir -p $(pwd)/$WINDOWS_BUILD_DIRECTORY

cd $(pwd)/$BUILD_PATH
zip -r "$absolute_path_to_built_zip_file" .