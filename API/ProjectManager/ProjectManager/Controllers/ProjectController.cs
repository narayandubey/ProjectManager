using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectManager.Models;
using ProjectManager.BC;
using System.Web.Http;
using ProjectManager.ActionFilters;

namespace ProjectManager.Controllers
{
    public class ProjectController : ApiController
    {
        ProjectBC projObjBC = null;

        public ProjectController()
        {
            projObjBC = new ProjectBC();
        }

        public ProjectController(ProjectBC projectBc)
        {
            projObjBC = projectBc;
        }

        [HttpGet]
        [ProjectManagerLogFilter]
        [ProjectManagerExceptionFilter]
        [Route("api/project")]
        public JSonResponse RetrieveProjects()
        {
            List<Project> Projects = projObjBC.RetrieveProjects();

            return new JSonResponse()
            {
                Data = Projects
            };

        }

        [HttpPost]
        [ProjectManagerLogFilter]
        [ProjectManagerExceptionFilter]
        [Route("api/project/add")]
        public JSonResponse InsertProjectDetails(Project project)
        {
            if(project == null)
            {
                throw new ArgumentNullException("Project is null");
            }
            if(project.ProjectId < 0)
            {
                throw new ArithmeticException("Project ID cannot be negative");
            }
            if(project.User == null)
            {
                throw new ArgumentNullException("User related to the project cannot be null");
            }
            if(project.User.ProjectId < 0)
            {
                throw new ArithmeticException("User object project Id cannot be negative");
            }
            if(project.NoOfCompletedTasks > project.NoOfTasks)
            {
                throw new ArgumentException("Completed tasks cannot be greater than total tasks");
            }
            return new JSonResponse()
            {
                Data = projObjBC.InsertProjectDetails(project)
            };

        }


        [HttpPost]
        [Route("api/project/update")]
        [ProjectManagerLogFilter]
        [ProjectManagerExceptionFilter]
        public JSonResponse UpdateProjectDetails(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("Project is null");
            }
            if (project.ProjectId < 0)
            {
                throw new ArithmeticException("Project ID cannot be negative");
            }
            if (project.User == null)
            {
                throw new ArgumentNullException("User related to the project cannot be null");
            }
            if (project.User.ProjectId < 0)
            {
                throw new ArithmeticException("User object project Id cannot be negative");
            }
            if (project.NoOfCompletedTasks > project.NoOfTasks)
            {
                throw new ArgumentException("Completed tasks cannot be greater than total tasks");
            }
            return new JSonResponse()
            {
                Data = projObjBC.UpdateProjectDetails(project)
            };
        }

        [HttpPost]
        [Route("api/project/delete")]
        public JSonResponse DeleteProjectDetails(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("Project is null");
            }
            if (project.ProjectId < 0)
            {
                throw new ArithmeticException("Project ID cannot be negative");
            }
            if (project.User == null)
            {
                throw new ArgumentNullException("User related to the project cannot be null");
            }
            if (project.User.ProjectId < 0)
            {
                throw new ArithmeticException("User object project Id cannot be negative");
            }
            if (project.NoOfCompletedTasks > project.NoOfTasks)
            {
                throw new ArgumentException("Completed tasks cannot be greater than total tasks");
            }
            return new JSonResponse()
            {
                Data = projObjBC.DeleteProjectDetails(project)
            };
        }

    }
}