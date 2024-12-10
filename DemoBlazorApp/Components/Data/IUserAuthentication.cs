using DemoBlazorApp.Components.Model;

namespace DemoBlazorApp.Components.Data
{
    public interface IUserAuthentication
    {
        Task<bool> RegisterDomain(string name,string domain);
        Task<bool> LoginUser(string name);
        Task<List<StudentData>> GetStudent(string key);
        Task<bool> AddStudent(StudentData student, string key);
    }
}
