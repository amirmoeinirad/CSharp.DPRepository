
// Amir Moeini Rad
// May 3, 2025

// Main Concept: Repository Design Pattern
// In this pattern, a repository class mediates between the domain and data mapping layers,
// acting like an in-memory collection of domain objects.
// This pattern is useful for decoupling the business logic from data access logic,
// making the code more maintainable and testable.


namespace RepositoryDemo
{
    // Entity class
    // In a real application, this would be a model representing a database table.
    public class Product
    {
        // Mapping to the primary key column in the 'Product' table.
        public int Id { get; set; }

        // Mapping to the 'Name' column in the 'Product' table.
        public string? Name { get; set; }
    }


    //////////////////////////////////////


    // Generic repository interface
    public interface IRepository<T>
    {
        // CRUD operations

        // Create one item or record
        void Add(T item);

        // Read one item or record by ID
        T GetById(int id);

        // Read all items or records
        // 'IEnumerable' is used to return a collection of in-memory items.
        // 'IQueryable' is used to return a collection of items from a database.
        IEnumerable<T> GetAll();

        // Delete one item or record by ID
        void Remove(int id);
    }

    
    //////////////////////////////////////
    

    // Product repository implementation (in-memory)
    // The repository class performs the CRUD operations (here in-memory list).
    // Therefore, the main app does not directly call the list.
    // The repo class acts as a mediator between the main app and the data source.
    public class ProductRepository : IRepository<Product>
    {
        // In-memory list to simulate a data source.
        private readonly List<Product> _products = new List<Product>();

        // CRUD operations implementation

        // Adding a product to the in-memory list using the lambda expression syntax.
        public void Add(Product product) => _products.Add(product);

        // Using lambda expression and LINQ syntax to find the product by ID.
        public Product GetById(int id) => _products.First(p => p.Id == id);

        // Returning all products from the in-memory list using the lambda expression syntax.
        public IEnumerable<Product> GetAll() => _products;

        // Removing a product by ID using the traditional method syntax.
        public void Remove(int id)
        {
            var product = GetById(id);
            
            // If the product is found, remove it from the list.
            if (product != null)
                _products.Remove(product);
        }
    }


    //////////////////////////////////////


    // Main App
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Repository Design Pattern in C#.NET.");
            Console.WriteLine("------------------------------------\n");


            // Create a repository instance
            IRepository<Product> productRepo = new ProductRepository();

            // Add products
            productRepo.Add(new Product { Id = 1, Name = "Laptop" });
            productRepo.Add(new Product { Id = 2, Name = "Smartphone" });

            // List all products
            foreach (var product in productRepo.GetAll())
                Console.WriteLine($"Product {product.Id} - {product.Name}");

            // Get product by ID
            var singleProduct = productRepo.GetById(1);
            Console.WriteLine($"\nRetrieved: {singleProduct.Name}");

            // Remove a product
            productRepo.Remove(1);

            // List again
            Console.WriteLine("\nAfter removal:");
            foreach (var product in productRepo.GetAll())
                Console.WriteLine($"Product {product.Id} - {product.Name}");


            Console.WriteLine("\nDone.");
        }
    }
}
