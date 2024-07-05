using Medical_App.Exceptions;
using Medical_App.Models;
using Medical_App.Service;

namespace Medical_App
{
    class Program
    {
        static void Main()
        {
            UserService userService = new UserService();
            CategoryService categoryService = new CategoryService();
            MedicineService medicineService = new MedicineService();

            bool running = true;

            while (running)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. User Register");
                Console.WriteLine("2. User Login");
                Console.WriteLine("0. Exit");

                string initialChoice = Console.ReadLine();

                switch (initialChoice)
                {
                    case "1":
                        Console.WriteLine("Enter full name:");
                        string fullname = Console.ReadLine();
                        string email;
                        while (true)
                        {
                            Console.WriteLine("Enter email:");
                            email = Console.ReadLine();
                            try
                            {
                                var addr = new System.Net.Mail.MailAddress(email);
                                if (addr.Address == email)
                                {
                                    break;
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Invalid email format.");
                            }
                        }

                        string password;
                        while (true)
                        {
                            Console.WriteLine("Enter password (at least 8 characters, both uppercase and lowercase):");
                            password = Console.ReadLine();
                            try
                            {
                                User newUser = new User { Fullname = fullname, Email = email, Password = password };
                                userService.AddUser(newUser);
                                Console.WriteLine("User registered successfully.");
                                break;
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                                break;
                            }
                        }
                        break;
                    case "2":
                        User loggedInUser = null;

                        while (loggedInUser == null)
                        {
                            Console.WriteLine("Enter email:");
                            string loginEmail = Console.ReadLine();
                            Console.WriteLine("Enter password:");
                            string loginPassword = Console.ReadLine();

                            try
                            {
                                loggedInUser = userService.Login(loginEmail, loginPassword);
                                Console.WriteLine("Login successful.");
                            }
                            catch (NotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }

                        bool userLoggedIn = true;
                        while (userLoggedIn)
                        {
                            Console.WriteLine("Select an option:");
                            Console.WriteLine("1. Create a new Category");
                            Console.WriteLine("2. Create a new Medicine");
                            Console.WriteLine("3. Remove a Medicine");
                            Console.WriteLine("4. List all medicines");
                            Console.WriteLine("5. Update a medicine");
                            Console.WriteLine("6. Find medicine by ID");
                            Console.WriteLine("7. Find medicine by Name");
                            Console.WriteLine("8. Find medicine by Category");
                            Console.WriteLine("9. View Medicines");
                            Console.WriteLine("10. List all Categories");
                            Console.WriteLine("0. Logout");

                            string choice = Console.ReadLine();
                            switch (choice)
                            {
                                case "1":
                                    Console.WriteLine("Enter category name:");
                                    string categoryName = Console.ReadLine();
                                    categoryService.CreateCategory(new Category { Name = categoryName });
                                    Console.WriteLine("Category added.");
                                    break;
                                case "2":
                                    Console.WriteLine("Enter medicine name:");
                                    string medicineName = Console.ReadLine();
                                    Console.WriteLine("Enter price:");
                                    double price = Convert.ToDouble(Console.ReadLine());
                                    Console.WriteLine("Enter category ID:");
                                    int categoryId = Convert.ToInt32(Console.ReadLine());
                                    Medicine medicine = new Medicine
                                    {
                                        Name = medicineName,
                                        Price = price,
                                        CategoryId = categoryId,
                                        UserId = loggedInUser.Id
                                    };
                                    try
                                    {
                                        medicineService.CreateMedicine(medicine);
                                        Console.WriteLine("Medicine added.");
                                    }
                                    catch (NotFoundException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case "3":
                                    Console.WriteLine("Enter medicine ID to remove:");
                                    int removeId = Convert.ToInt32(Console.ReadLine());
                                    try
                                    {
                                        medicineService.RemoveMedicine(removeId);
                                        Console.WriteLine("Medicine removed.");
                                    }
                                    catch (NotFoundException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case "4":
                                    Medicine[] allMedicines = medicineService.GetAllMedicines();
                                    foreach (var med in allMedicines)
                                    {
                                        Console.WriteLine($"ID: {med.Id}, Name: {med.Name}, Price: {med.Price}, Category ID: {med.CategoryId}");
                                    }
                                    break;
                                case "5":
                                    Console.WriteLine("Enter medicine ID to update:");
                                    int updateId = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter new medicine name:");
                                    string newName = Console.ReadLine();
                                    Console.WriteLine("Enter new price:");
                                    double newPrice = Convert.ToDouble(Console.ReadLine());
                                    Console.WriteLine("Enter new category ID:");
                                    int newCategoryId = Convert.ToInt32(Console.ReadLine());
                                    Medicine updatedMedicine = new Medicine
                                    {
                                        Id = updateId,
                                        Name = newName,
                                        Price = newPrice,
                                        CategoryId = newCategoryId,
                                        UserId = loggedInUser.Id
                                    };
                                    try
                                    {
                                        medicineService.UpdateMedicine(updateId, updatedMedicine);
                                        Console.WriteLine("Medicine updated.");
                                    }
                                    catch (NotFoundException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case "6":
                                    Console.WriteLine("Enter medicine ID:");
                                    int id = Convert.ToInt32(Console.ReadLine());
                                    try
                                    {
                                        var medicineById = medicineService.GetMedicineById(id);
                                        Console.WriteLine($"ID: {medicineById.Id}, Name: {medicineById.Name}, Price: {medicineById.Price}, Category ID: {medicineById.CategoryId}");
                                    }
                                    catch (NotFoundException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case "7":
                                    Console.WriteLine("Enter medicine name:");
                                    string name = Console.ReadLine();
                                    try
                                    {
                                        var medicineByName = medicineService.GetMedicineByName(name);
                                        Console.WriteLine($"ID: {medicineByName.Id}, Name: {medicineByName.Name}, Price: {medicineByName.Price}, Category ID: {medicineByName.CategoryId}");
                                    }
                                    catch (NotFoundException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case "8":
                                    Console.WriteLine("Enter category ID:");
                                    int catId = Convert.ToInt32(Console.ReadLine());
                                    var medicinesByCategory = medicineService.GetMedicineByCategory(catId);
                                    foreach (var med in medicinesByCategory)
                                    {
                                        Console.WriteLine($"ID: {med.Id}, Name: {med.Name}, Price: {med.Price}, Category ID: {med.CategoryId}");
                                    }
                                    break;
                                case "9":
                                    Medicine[] medicines = medicineService.GetAllMedicines();
                                    foreach (var med in medicines)
                                    {
                                        Console.WriteLine($"ID: {med.Id}, Name: {med.Name}, Price: {med.Price}, Category ID: {med.CategoryId}");
                                    }
                                    break;
                                case "10":
                                    var allCategories = DB.Categories;
                                    foreach (var cat in allCategories)
                                    {
                                        Console.WriteLine($"ID: {cat.Id}, Name: {cat.Name}");
                                    }
                                    break;
                                case "0":
                                    userLoggedIn = false;
                                    Console.WriteLine("Logged out.");
                                    break;
                                default:
                                    Console.WriteLine("Invalid option.");
                                    break;
                            }
                        }
                        break;
                    case "0":
                        Console.WriteLine("Exiting to main menu.");
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
                 
