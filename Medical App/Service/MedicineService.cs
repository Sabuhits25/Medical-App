using Medical_App.Exceptions;
using Medical_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_App.Service
{
    public class MedicineService
    {
        public void CreateMedicine(Medicine medicine)
        {
            var category = Array.Find(DB.Categories,c=> c.Id== medicine.CategoryId);
            if (category != null)
            {
                Array.Resize(ref DB.Medicines, DB.Medicines.Length + 1);
                DB.Medicines[DB.Medicines.Length - 1] = medicine;

            }
            else
            {
              
                throw new NotFoundException("Category not found");
            }


         
        }
        public Medicine[] GetAllMedicines()
        {
            return DB.Medicines;
        }

        public Medicine GetMedicineById(int id)
        {
            var medicine = Array.Find(DB.Medicines, m => m.Id == id);
            if (medicine == null)
                throw new NotFoundException("Medicine not found.");
            return medicine;
        }
        public Medicine GetMedicineByName(string name)
        {
            var medicine = Array.Find(DB.Medicines, m => m.Name == name);
            if (medicine == null)
                throw new NotFoundException("Medicine not found.");
            return medicine;
        }

        public Medicine[] GetMedicineByCategory(int categoryId)
        {
            var medicines = Array.FindAll(DB.Medicines, m => m.CategoryId == categoryId);
            return medicines;
        }
        public void RemoveMedicine(int id)
        {
            int index = Array.FindIndex(DB.Medicines, m => m.Id == id);
            if (index == -1)
                throw new NotFoundException("Medicine not found.");

            for (int i = index; i < DB.Medicines.Length - 1; i++)
            {
                DB.Medicines[i] = DB.Medicines[i + 1];
            }

            Array.Resize(ref DB.Medicines, DB.Medicines.Length - 1);
        }
        public void UpdateMedicine(int id, Medicine updatedMedicine)
        {
            int index = Array.FindIndex(DB.Medicines, m => m.Id == id);
            if (index == -1)
                throw new NotFoundException("Medicine not found.");

            DB.Medicines[index] = updatedMedicine;
        }
    }

}


