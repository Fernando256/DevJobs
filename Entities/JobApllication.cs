namespace DevJobs.API.Entities {
    public class JobApplication {
        public JobApplication(string apllicantName, string apllicantEmail, int idJobVacancy)
        {
            ApllicantName = apllicantName;
            ApllicantEmail = apllicantEmail;
            IdJobVacancy = idJobVacancy;
        }

        public int Id { get; private set; }
        public string ApllicantName { get; private set; }
        public string ApllicantEmail { get; private set; }
        public int IdJobVacancy { get; private set; }

    }
}