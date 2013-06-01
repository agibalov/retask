using System.Linq;
using NUnit.Framework;
using Service.DTO;

namespace ServiceTests.Extensions
{
    public static class WorkspaceDTOAssertExtensions
    {
        public static WorkspaceDTO Ok(this ServiceResult<WorkspaceDTO> result)
        {
            var workspace = result.Ok<WorkspaceDTO>();
            Assert.IsNotNull(workspace);
            return workspace;
        }

        public static WorkspaceDTO HasNoTasks(this WorkspaceDTO workspace)
        {
            Assert.AreEqual(0, workspace.Tasks.Count);
            return workspace;
        }

        public static WorkspaceDTO HasOnlyTasks(this WorkspaceDTO workspace, params TaskDTO[] tasks)
        {
            Assert.AreEqual(tasks.Length, workspace.Tasks.Count);
            var workspaceTasks = workspace.Tasks;
            foreach (var task in tasks)
            {
                Assert.IsTrue(workspaceTasks.Any(workspaceTask => workspaceTask.BoolSameAs(task)));
            }
            return workspace;
        }
    }
}