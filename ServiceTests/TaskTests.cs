using NUnit.Framework;
using Service.DAL;
using ServiceTests.Extensions;

namespace ServiceTests
{
    [TestFixture]
    public class TaskTests : AbstractTest
    {
        [Test]
        public void NewUserDoesNotHaveAnyTasks()
        {
            SignUpConfirmationNotifier.Reset();
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty123").Ok();
            Service.ConfirmSignUpRequest(SignUpConfirmationNotifier.LastSecret).Ok();

            var sessionToken = Service.SignIn("loki2302@loki2302.loki2302", "qwerty123")
                .Ok()
                .HasSessionToken()
                .SessionToken;
            Service.GetWorkspace(sessionToken).Ok().HasNoTasks();
        }

        [Test]
        public void CanCreateTask()
        {
            SignUpConfirmationNotifier.Reset();
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty123").Ok();
            Service.ConfirmSignUpRequest(SignUpConfirmationNotifier.LastSecret).Ok();

            var sessionToken = Service.SignIn("loki2302@loki2302.loki2302", "qwerty123")
                .Ok()
                .SessionToken;
            var task = Service.CreateTask(sessionToken, "My task description")
                .Ok()
                .HasTaskId()
                .HasTaskDescription("My task description")
                .HasTaskStatus(TaskStatus.NotStarted);
            Service.GetWorkspace(sessionToken).Ok().HasOnlyTasks(task);
        }

        [Test]
        public void CanUpdateTask()
        {
            SignUpConfirmationNotifier.Reset();
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty123").Ok();
            Service.ConfirmSignUpRequest(SignUpConfirmationNotifier.LastSecret).Ok();

            var sessionToken = Service.SignIn("loki2302@loki2302.loki2302", "qwerty123")
                .Ok()
                .SessionToken;
            var task = Service.CreateTask(sessionToken, "My task description").Ok();
            Service.GetWorkspace(sessionToken).Ok().HasOnlyTasks(task);
            task = Service.UpdateTask(sessionToken, task.TaskId, "My other description")
                .Ok()
                .HasTaskId(task.TaskId)
                .HasTaskDescription("My other description")
                .HasTaskStatus(task.TaskStatus);
            Service.GetWorkspace(sessionToken).Ok().HasOnlyTasks(task);
        }

        [Test]
        public void CanDeleteTask()
        {
            SignUpConfirmationNotifier.Reset();
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty123").Ok();
            Service.ConfirmSignUpRequest(SignUpConfirmationNotifier.LastSecret).Ok();

            var sessionToken = Service.SignIn("loki2302@loki2302.loki2302", "qwerty123")
                .Ok()
                .SessionToken;
            var task = Service.CreateTask(sessionToken, "My task description").Ok();
            Service.GetWorkspace(sessionToken).Ok().HasOnlyTasks(task);
            Service.DeleteTask(sessionToken, task.TaskId).Ok();
            Service.GetWorkspace(sessionToken).Ok().HasNoTasks();
        }

        [Test]
        public void CanProgressTask()
        {
            SignUpConfirmationNotifier.Reset();
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty123").Ok();
            Service.ConfirmSignUpRequest(SignUpConfirmationNotifier.LastSecret).Ok();

            var sessionToken = Service.SignIn("loki2302@loki2302.loki2302", "qwerty123")
                .Ok()
                .SessionToken;
            var task = Service.CreateTask(sessionToken, "My task description").Ok().HasTaskStatus(TaskStatus.NotStarted);
            Service.GetWorkspace(sessionToken).Ok().HasOnlyTasks(task);

            task = Service.ProgressTask(sessionToken, task.TaskId).Ok().HasTaskStatus(TaskStatus.InProgress);
            Service.GetWorkspace(sessionToken).Ok().HasOnlyTasks(task);

            task = Service.ProgressTask(sessionToken, task.TaskId).Ok().HasTaskStatus(TaskStatus.Done);
            Service.GetWorkspace(sessionToken).Ok().HasOnlyTasks(task);

            task = Service.ProgressTask(sessionToken, task.TaskId).Ok().HasTaskStatus(TaskStatus.Complete);
            Service.GetWorkspace(sessionToken).Ok().HasNoTasks();
        }

        [Test]
        public void CanUnprogressTask()
        {
            SignUpConfirmationNotifier.Reset();
            Service.SignUp("loki2302@loki2302.loki2302", "qwerty123").Ok();
            Service.ConfirmSignUpRequest(SignUpConfirmationNotifier.LastSecret).Ok();

            var sessionToken = Service.SignIn("loki2302@loki2302.loki2302", "qwerty123")
                .Ok()
                .SessionToken;
            var task = Service.CreateTask(sessionToken, "My task description").Ok().HasTaskStatus(TaskStatus.NotStarted);
            Service.GetWorkspace(sessionToken).Ok().HasOnlyTasks(task);

            task = Service.ProgressTask(sessionToken, task.TaskId).Ok().HasTaskStatus(TaskStatus.InProgress);
            Service.GetWorkspace(sessionToken).Ok().HasOnlyTasks(task);

            task = Service.ProgressTask(sessionToken, task.TaskId).Ok().HasTaskStatus(TaskStatus.Done);
            Service.GetWorkspace(sessionToken).Ok().HasOnlyTasks(task);

            task = Service.UnprogressTask(sessionToken, task.TaskId).Ok().HasTaskStatus(TaskStatus.InProgress);
            Service.GetWorkspace(sessionToken).Ok().HasOnlyTasks(task);

            task = Service.UnprogressTask(sessionToken, task.TaskId).Ok().HasTaskStatus(TaskStatus.NotStarted);
            Service.GetWorkspace(sessionToken).Ok().HasOnlyTasks(task);
        }
    }
}