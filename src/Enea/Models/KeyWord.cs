namespace Enea.Models
{
    public class KeyWord
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public bool IsOnline { get; set; } = true;
    }
}