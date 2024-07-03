using Medical_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_App.Service
{
    public class CategoryService
    {
        public void CreateCategory(Category category)
        {
            Array.Resize(ref DB.Categories, DB.Categories.Length + 1);
            DB.Categories[DB.Categories.Length - 1] = category;
        }
    }
}
