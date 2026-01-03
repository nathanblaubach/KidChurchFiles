# KidChurchFiles

Organize files from The Gospel Project for intuitive use on a TV

## Quickstart

* Download the following content to your Downloads folder
  * Older Preschool
  * Volume Resources
  * Presentation Files
  * Bible Story Videos
  * Missions Videos
  * Key Passage Songs
* Unzip the folders and the sub folders of the Presentation Files
* Run the commands below

```shell
# Clone the repository
git clone https://github.com/nathanblaubach/KidChurchFiles.git

# Run the console application
cd KidChurchFiles/KidChurchFiles
dotnet run <Volume Number>

# Run the tests
dotnet test
```

## Testing Notes

Note: Currently these tests assume that you have the content in your Downloads folder, because they are integration tests

## Roadmap

This is a very rudimentary slice that processes 6 files for each session. Down the road there will be additional features

* Supporting base functionality for "Kids", not just pre-school
* Supporting Missions videos
* Supporting Volume resource organization
