namespace Sample.WebApp.Services
{
    using Sample.WebApp.Helpers;
    using Sample.WebApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class EmployeeService : HttpClientBase, IEmployeeService
    {
        private readonly ISecurityTokenHelper _securityTokenHelper;

        public EmployeeService(IHttpClientFactory httpClientFactory, ISecurityTokenHelper securityTokenHelper) : base(httpClientFactory)
        {
            _securityTokenHelper = securityTokenHelper;
        }

        public async Task<IEnumerable<Employee>> Get()
        {
            string relativeUri = "/api/employee";
            var result = await GetAsync<EmployeesResult>(relativeUri);
            return result.Employees;
        }

        public async Task<Employee> Get(Guid id)
        {
            string relativeUri = $"/api/employee/{id}";
            var result = await GetAsync<Employee>(relativeUri);
            return result;
        }

        public async Task Add(Employee employee)
        {
            string relativeUri = $"/api/employee";
            await PostAsync(relativeUri, employee);
        }

        public async Task Update(Employee employee)
        {
            string relativeUri = $"/api/employee/{employee.EmployeeId}";
            await PutAsync(relativeUri, employee);
        }

        public async Task Delete(Guid id)
        {
            string relativeUri = $"/api/employee/{id}";
            await DeleteAsync(relativeUri);
        }

        protected override HttpClient GetScopedHttpClient()
        {
            var httpClient = _httpClientFactory.CreateClient(NamedHttpClients.EmployeeServiceClient);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _securityTokenHelper.GetSessionToken().Result);
            return httpClient;
        }
    }
}
