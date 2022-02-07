using OnlineSchoolApi.RequestModels;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolApi
{
    public static class Mapper
    {
        public static Subject ToSubject(this SubjectInputModel subjectInputModel)
        {
            if (subjectInputModel.Id is null)
            {
                return new Subject
                {
                    Name = subjectInputModel.Name,
                    Code = subjectInputModel.Code,
                };
            }

            return new Subject
            {
                Id = subjectInputModel.Id.Value,
                Name = subjectInputModel.Name,
                Code = subjectInputModel.Code,
            };
        }
    }
}
