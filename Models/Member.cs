namespace LibrarySystem.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public ICollection<Checkout> Checkouts { get; set; }
    }
}
