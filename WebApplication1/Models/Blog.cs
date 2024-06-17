using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Blog:BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }

        public string ImageUrl{ get; set;}

        [NotMapped]
        public string ShortDesc => Desc.Length > 50 ? Desc.Substring(0, 40) : Desc;
    }
}
