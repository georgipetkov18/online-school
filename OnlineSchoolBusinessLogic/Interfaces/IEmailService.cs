namespace OnlineSchoolBusinessLogic.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string text, string from = "online.school.project.2022@gmail.com");
    }
}
