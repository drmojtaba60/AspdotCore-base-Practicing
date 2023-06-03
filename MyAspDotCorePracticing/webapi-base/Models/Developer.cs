namespace webapi_base.Models
{
    public class Developer
    {
        
        public string Mail { get; set; } = "";
        public string FullName { get; set; } = "";
        public DateTime BrithDate { get; set; }
        public List<string> Skills { get; set; } =new List<string>();
    }
}
