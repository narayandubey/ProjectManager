using ProjectManager.Models;
using ProjectManager.BC;
using System.Web.Http;

using ProjectManager.ActionFilters;
using System.Collections.Generic;
using System;

namespace ProjectManager.Controllers
{
    public class TaskController : ApiController
    {
        TaskBC taskObj = null;

        public TaskController()
        {
            taskObj = new TaskBC();
        }

        public TaskController(TaskBC taskBc)
        {
            taskObj = taskBc;
        }

        [HttpGet]
        [Route("api/task")]
        [ProjectManagerLogFilter]
        [ProjectManagerExceptionFilter]
        public JSonResponse RetrieveTaskByProjectId(int projectId)
        {
            if(projectId < 0)
            {
                throw new ArithmeticException("ProjectId cannot be negative");
            }

            List<Task> Tasks = taskObj.RetrieveTaskByProjectId(projectId);

            return new JSonResponse()
            {
                Data = Tasks
            };

        }

        [HttpGet]
        [Route("api/task/parent")]
        [ProjectManagerLogFilter]
        [ProjectManagerExceptionFilter]
        public JSonResponse RetrieveParentTasks()
        {
            List<ParentTask> ParentTasks = taskObj.RetrieveParentTasks();

            return new JSonResponse()
            {
                Data = ParentTasks
            };

        }

        [HttpPost]
        [ProjectManagerLogFilter]
        [ProjectManagerExceptionFilter]
        [Route("api/task/add")]
        public JSonResponse InsertTaskDetails(Task task)
        {
            if(task == null)
            {
                throw new ArgumentNullException("Task object is null");
            }
            if(task.Parent_ID < 0)
            {
                throw new ArithmeticException("Parent Id of task cannot be negative");
            }
            if(task.Project_ID < 0)
            {
                throw new ArithmeticException("Project Id cannot be negative");
            }
            if(task.TaskId < 0)
            {
                throw new ArithmeticException("Task id cannot be negative");
            }
            return new JSonResponse()
            {
                Data = taskObj.InsertTaskDetails(task)
            };

        }

        [HttpPost]
        [ProjectManagerLogFilter]
        [ProjectManagerExceptionFilter]
        [Route("api/task/update")]
        public JSonResponse UpdateTaskDetails(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException("Task object is null");
            }
            if (task.Parent_ID < 0)
            {
                throw new ArithmeticException("Parent Id of task cannot be negative");
            }
            if (task.Project_ID < 0)
            {
                throw new ArithmeticException("Project Id cannot be negative");
            }
            if (task.TaskId < 0)
            {
                throw new ArithmeticException("Task id cannot be negative");
            }
            return new JSonResponse()
            {
                Data = taskObj.UpdateTaskDetails(task)
            };

        }
        [HttpPost]
        [ProjectManagerLogFilter]
        [ProjectManagerExceptionFilter]
        [Route("api/task/delete")]
        public JSonResponse DeleteTaskDetails(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException("Task object is null");
            }
            if (task.Parent_ID < 0)
            {
                throw new ArithmeticException("Parent Id of task cannot be negative");
            }
            if (task.Project_ID < 0)
            {
                throw new ArithmeticException("Project Id cannot be negative");
            }
            if (task.TaskId < 0)
            {
                throw new ArithmeticException("Task id cannot be negative");
            }
            return new JSonResponse()
            {
                Data = taskObj.DeleteTaskDetails(task)
            };
        }


    }
}