using OnlineSchoolApi.RequestModels;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolApi
{
    public static class Mapper
    {
        public static Subject ToSubject(this SubjectInputModel subjectInputModel)
        {
            return new Subject
            {
                Name = subjectInputModel.Name,
                Code = subjectInputModel.Code,
            };
        }
    }
}
