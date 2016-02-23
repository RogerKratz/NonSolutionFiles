# NonSolutionFiles
Finds c# files removed from csproj file but still on disk.

Usage:</br>
NonSolutionFiles.exe [absolute path to your c# solution file] [excludeFileContainingString1] [excludeFileContainingString2] [...]

Example:</br>
NonSolutionFiles.exe c:\stuff\mysolution.sln \aFolderWithGeneratedFiles\
