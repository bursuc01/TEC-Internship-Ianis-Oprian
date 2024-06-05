namespace ApiApp.DataAccessLayer.ObjectModel
{
    public class PersonCreation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int PositionId { get; set; }
        public int SalaryId { get; set; }
    }
}
