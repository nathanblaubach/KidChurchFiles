# KidChurchFiles

Organize files from The Gospel Project for intuitive use on a TV via Thumb Drive

## Quickstart

* Download the following content to your Downloads folder
  * Older Preschool
  * Volume Resources
  * Presentation Files
  * Bible Story Videos
  * Missions Videos
  * Key Passage Songs
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

* Organize "Kids" files in addition to "Preschool"
* Automate session title population
* Automate missions video placement
* Automate volume resources placement
