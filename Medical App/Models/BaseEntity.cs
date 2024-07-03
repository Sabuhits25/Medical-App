using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_App.Models
{
    public abstract class BaseEntity
    {
        private static int _idCounter = 0;
        public int Id { get; set; }

        protected BaseEntity()
        {
            Id = ++_idCounter;
        }
    }
}
