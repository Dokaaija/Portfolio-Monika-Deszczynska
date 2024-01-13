using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using UserLogin.Enums;
using UserLogin.JsonRequest;
using UserLogin.JsonResponse;
using UserLogin.Languages;
using UserLogin.Models;
using UserLogin.Settings;
using UserLogin.Tools;

namespace UserLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : UserBaseController
    {


        [ProducesResponseType(typeof(ProjectDeleteResponse), 200)]
        [HttpDelete]
        public JsonResult Delete(ProjectsDeleteRequest projectsDeleteRequest)
        {
            return PerformRequest(() =>
            {
                ValidateParameters(projectsDeleteRequest);

                Project project = DataBase.Projects.FirstOrDefault(up => up.Id == projectsDeleteRequest.ProjectId);
                if (project == null)
                    return new JsonResult(new ProjectDeleteResponse() { Success = false, ErrorMessage = LanguageManager.GetLabelValue(Request, "projectNotFound") });

                if (DataBase.ProjectUsers.FirstOrDefault(pu => pu.UserId == LoggedUser.Login && pu.ProjectId == projectsDeleteRequest.ProjectId) == null)
                    return new JsonResult(new ProjectDeleteResponse() { Success = false, ErrorMessage = LanguageManager.GetLabelValue(Request, "noProjectDeleteAccess") });

                DataBase.Projects.Remove(project);
                DataBase.SaveChanges();

                return new JsonResult(new ProjectDeleteResponse() { Success = true });
            });
        }



        [ProducesResponseType(typeof(ProjectCompetenceAddResponse), 200)]
        [Route("AddCompetence")]
        [HttpPost]
        public JsonResult Post2(ProjectsAddCompetenceRequest projectCompetenceAddRequest)
        {
            return PerformRequest(() =>
            {
                ValidateParameters(projectCompetenceAddRequest);

                Project Project = DataBase.Projects.FirstOrDefault(p => p.Id == projectCompetenceAddRequest.ProjectId);
                if (Project == null)
                    return new JsonResult(new ProjectCompetenceAddResponse() { Success = false, ErrorMessage = String.Format(LanguageManager.GetLabelValue(Request, "projectNotFound")) });
                Competence Competence = DataBase.Competences.FirstOrDefault(c => c.Name == projectCompetenceAddRequest.CompetenceName);
                if (Competence == null)
                    return new JsonResult(new ProjectCompetenceAddResponse() { Success = false, ErrorMessage = LanguageManager.GetLabelValue(Request, "competenceNotFound") });

                ProjectCompetence competence = new ProjectCompetence() { ProjectId = projectCompetenceAddRequest.ProjectId, CompetenceId = projectCompetenceAddRequest.CompetenceName };
                DataBase.ProjectCompetences.Add(competence);
                DataBase.SaveChanges();
                return new JsonResult(new ProjectCompetenceAddResponse() { Success = true });
            });
        }


    }
}
