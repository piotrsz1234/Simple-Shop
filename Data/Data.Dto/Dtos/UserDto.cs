namespace Data.Dto.Dtos
{
    public sealed class UserDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginCode { get; set; }
        public bool IsAdmin { get; set; }
    }
}