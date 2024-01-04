namespace SwiftParcel.ExternalAPI.Lecturer.Application.DTO
{
    public class AccessTokenDto
    {
        public string Access_Token { get; set; }
        public int Expires_In { get; set; }
        public string Token_Type { get; set; }
        public string Scope { get; set; }
    }
}