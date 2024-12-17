namespace Sunglass_ecom.Models
{
    public class Admins
    {
        public required int Id { get; set; }
        public required int AdmiID { get; set; }
      
        public string  AgentName { get; set; }
        public bool IsActive { get; set; }
        public string StatusOfAcc { get; set; }
        public DateTime CreayedAt { get; set; }
        public string PassWord { get; set; }


    }
}
