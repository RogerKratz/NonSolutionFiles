# NonSolutionFiles
Finds c# files removed from csproj file but still on disk.

Usage:
NonSolutionFiles.exe [absolute path to your c# solution file] [excludeFileContaingString1] [excludeFileContainingString2] [...]

Example
NonSolutionFiles.exe c:\stuff\mysolution.sln \aFolderWithGeneratedFiles\
