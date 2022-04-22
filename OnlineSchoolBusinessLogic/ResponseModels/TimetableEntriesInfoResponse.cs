namespace OnlineSchoolBusinessLogic.ResponseModels
{
    public class TimetableEntriesInfoResponse
    {
        public TimetableEntryResponse? Current { get; set; }
        public TimetableEntryResponse? Next { get; set; }
    }
}
