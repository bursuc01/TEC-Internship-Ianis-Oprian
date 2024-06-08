namespace ApiApp.DataAccessLayer.ObjectModel
{
    public class UserInformation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool? IsAdmin { get; set; } = false;
        public int PersonId { get; set; }
    }
}
