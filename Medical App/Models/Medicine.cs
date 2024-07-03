using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_App.Models
{
    public class Medicine : BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public Medicine()
        {
            CreatedDate = DateTime.Now;
        }
    }




}
