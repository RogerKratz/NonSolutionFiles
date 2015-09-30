using System.Collections.Generic;
using System.Linq;

namespace NonSolutionFiles
{
	public class FindNonSolutionFiles
	{
		private readonly IFilesOnDisk _filesOnDisk;
		private readonly IFilesInProject _filesInProject;
		private readonly IProjectsInSolution _projectsInSolution;

		public FindNonSolutionFiles(IFilesOnDisk filesOnDisk, 
															IFilesInProject filesInProject, 
															IProjectsInSolution projectsInSolution)
		{
			_filesOnDisk = filesOnDisk;
			_filesInProject = filesInProject;
			_projectsInSolution = projectsInSolution;
		}

		public IEnumerable<string> Find(string solutionPath)
		{
			var allFilesOnDiskInProjectFolders = new List<string>();
			var allFilesInProjects = new List<string>();
			foreach (var projectPath in _projectsInSolution.ProjectPaths(solutionPath))
			{
				allFilesInProjects.AddRange(_filesInProject.FilePaths(projectPath));
				allFilesOnDiskInProjectFolders.AddRange(_filesOnDisk.CSharpFilesInSamePathAsProjectFileRecursive(projectPath));
			}
			return allFilesOnDiskInProjectFolders.Except(allFilesInProjects);
		}
	}
}