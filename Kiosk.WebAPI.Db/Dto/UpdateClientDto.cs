namespace Kiosk.WebAPI.Db.Dto
{
    public class UpdateClientDto
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
