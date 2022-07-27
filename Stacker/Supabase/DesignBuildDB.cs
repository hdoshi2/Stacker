using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Postgrest;
using Stacker.Models;
using Supabase;
using Client = Supabase.Client;

namespace Stacker.Supabase
{
    class DesignBuildDB : ConnectionBase
    {
        /// <summary>
        ///     Adds the new project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        public async Task<bool> AddNewProject(Project project)
        {
            return await AddNewRecord(project);
        }

        /// <summary>
        ///     Adds the new project details.
        /// </summary>
        /// <param name="projectDetail">The project detail.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Not initialized</exception>
        public async Task<bool> AddNewProjectDetails(ProjectDetail projectDetail)
        {
            return await AddNewRecord(projectDetail);
        }

        /// <summary>
        ///     Deletes the project.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Not initialized</exception>
        public async Task<bool> DeleteProject(int projectId)
        {
            if (!Connected)
            {
                throw new Exception("Not initialized");
            }

            await Client.Instance.From<Project>()
                .Filter("id", Constants.Operator.Equals, projectId)
                .Delete();

            return !await IsProjectExist(projectId);
        }

        /// <summary>
        ///     Deletes the project detail.
        /// </summary>
        /// <param name="projectDetailId">The project detail identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Not initialized</exception>
        public async Task<bool> DeleteProjectDetail(int projectDetailId)
        {
            if (!Connected)
            {
                throw new Exception("Not initialized");
            }

            await Client.Instance.From<ProjectDetail>()
                .Filter("id", Constants.Operator.Equals, projectDetailId)
                .Delete();

            return !await IsProjectDetailsExist(projectDetailId);
        }

        /// <summary>
        ///     Deletes the project details by project identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Not initialized</exception>
        public async Task<bool> DeleteProjectDetailsByProjectId(int projectId)
        {
            if (!Connected)
            {
                throw new Exception("Not initialized");
            }

            await Client.Instance.From<ProjectDetail>()
                .Filter("project_ID", Constants.Operator.Equals, projectId)
                .Delete();

            return !await IsProjectDetailsExistByProjectId(projectId);
        }

        /// <summary>
        ///     Gets the last project detail record by project identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Not initialized</exception>
        public async Task<ProjectDetail> GetLastProjectDetailByProjectId(int projectId)
        {
            if (!Connected)
            {
                throw new Exception("Not initialized");
            }

            var result = await Client.Instance.From<ProjectDetail>()
                .Filter("project_ID", Constants.Operator.Equals, projectId)
                .Order("created_at", Constants.Ordering.Descending)
                .Limit(1)
                .Get();

            if (result.ResponseMessage.StatusCode == HttpStatusCode.OK && result.Models.Count > 0)
            {
                return result.Models[0];
            }

            return null;
        }

        /// <summary>
        ///     Gets the new name of the project detail.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns></returns>
        public async Task<string> GetNewProjectDetailName(int projectId)
        {
            var project = await GetProject(projectId);

            if (project is null)
            {
                return null;
            }

            var lastProjectDetail = await GetLastProjectDetailByProjectId(projectId);

            if (lastProjectDetail is null)
            {
                return $"{project.Name}_Report 1";
            }

            if (int.TryParse(lastProjectDetail.Name.Substring($"{project.Name}_Report".Length),
                    out var lastReportNumber))
            {
                return $"{project.Name}_Report {lastReportNumber + 1}";
            }

            return $"{project.Name}_Report 1";
        }

        /// <summary>
        ///     Gets the project.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not initialized</exception>
        public async Task<Project> GetProject(int projectId)
        {
            if (!Connected)
            {
                throw new Exception("Not initialized");
            }

            var result = await Client.Instance.From<Project>()
                .Filter("id", Constants.Operator.Equals, projectId)
                .Limit(1)
                .Get();

            if (result.ResponseMessage.StatusCode != HttpStatusCode.OK || result.Models.Count == 0)
            {
                return null;
            }

            return result.Models[0];
        }

        /// <summary>
        ///     Gets the project details by project identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Not initialized</exception>
        public async Task<List<ProjectDetail>> GetProjectDetailsByProjectId(int projectId)
        {
            var projectDetails = new List<ProjectDetail>();

            if (!Connected)
            {
                throw new Exception("Not initialized");
            }

            var result = await Client.Instance.From<ProjectDetail>()
                .Filter("project_ID", Constants.Operator.Equals, projectId).Get();

            if (result.ResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                projectDetails.AddRange(result.Models);
            }

            return projectDetails;
        }

        /// <summary>
        ///     Gets the projects from Supabase.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Project>> GetProjects()
        {
            var projects = new List<Project>();

            if (!Connected)
            {
                throw new Exception("Not initialized");
            }

            var result = await Client.Instance.From<Project>().Get();

            if (result.ResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                projects.AddRange(result.Models);
            }

            return projects;
        }

        /// <summary>
        ///     Checks weather the Projects details exists or not.
        /// </summary>
        /// <param name="projectDetailsId"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Not initialized</exception>
        public async Task<bool> IsProjectDetailsExist(int projectDetailsId)
        {
            if (!Connected)
            {
                throw new Exception("Not initialized");
            }

            var result = await GetRecordsCount<ProjectDetail>("id", projectDetailsId);

            return result > 0;
        }

        /// <summary>
        ///     Checks weather the Projects details for a specific project exists or not.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Not initialized</exception>
        public async Task<bool> IsProjectDetailsExistByProjectId(int projectId)
        {
            if (!Connected)
            {
                throw new Exception("Not initialized");
            }

            var result = await GetRecordsCount<ProjectDetail>("project_ID", projectId);

            return result > 0;
        }

        /// <summary>
        ///     Checks weather the Projects exists or not.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>
        ///     <c>true</c> if [is project exist] [the specified project identifier]; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.Exception">Not initialized</exception>
        public async Task<bool> IsProjectExist(int projectId)
        {
            if (!Connected)
            {
                throw new Exception("Not initialized");
            }

            var result = await GetRecordsCount<Project>("id", projectId);

            return result > 0;
        }

        /// <summary>
        ///     Updates the project details.
        /// </summary>
        /// <param name="projectDetail">The project detail.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Not initialized</exception>
        public async Task<bool> UpdateProjectDetails(ProjectDetail projectDetail)
        {
            return await UpdateRecord(projectDetail);
        }

        /// <summary>
        ///     A generic method to add a new record.
        /// </summary>
        /// <typeparam name="TTable">The type of the record.</typeparam>
        /// <param name="recordObject">The record object.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Not initialized</exception>
        private async Task<bool> AddNewRecord<TTable>(TTable recordObject)
            where TTable : SupabaseTableBase, new()
        {
            if (!Connected)
            {
                throw new Exception("Not initialized");
            }

            var result = await Client.Instance.From<TTable>().Insert(recordObject);

            if (result.ResponseMessage.StatusCode != HttpStatusCode.Created)
            {
                return false;
            }

            recordObject.Id = result.Models[0].Id;

            return true;
        }

        /// <summary>
        ///     A generic method to get the table records count.
        /// </summary>
        /// <param name="filterField">The filter field.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private async Task<int> GetRecordsCount<TTable>(string filterField, object value)
            where TTable : SupabaseModel, new()
        {
            return await Client.Instance.From<TTable>()
                .Filter(filterField, Constants.Operator.Equals, value)
                .Count(Constants.CountType.Exact);
        }

        /// <summary>
        ///     A generic method to update a record.
        /// </summary>
        /// <typeparam name="TTable">The type of the record.</typeparam>
        /// <param name="recordObject">The record object.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Not initialized</exception>
        private async Task<bool> UpdateRecord<TTable>(TTable recordObject)
            where TTable : SupabaseTableBase, new()
        {
            if (!Connected)
            {
                throw new Exception("Not initialized");
            }

            var result = await recordObject.Update<TTable>();

            if (result.ResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            return true;
        }
    }
}
