
// Amir Moeini Rad
// May, 2025

// Main Concept: The Repository Design Pattern

// In this pattern, a repository class mediates between the business logic and data mapping layers.
// This pattern is useful for decoupling the business logic from data access logic.


namespace RepositoryDemo
{
    // Entity class
    // In a real application, this would be a model representing a database table called 'Product'.
    public class Product
    {
        // Mapping to the primary key column in the 'Product' table.
        public int Id { get; set; }

        // Mapping to the 'Name' column in the 'Product' table.
        public string? Name { get; set; }
    }


    //////////////////////////////////////


    // Generic repository interface
    public interface IRepository<TEntity>
    {        
        // Create and add one item or record
        void Add(TEntity item);

        // Read one item or record by ID
        TEntity GetById(int id);

        // Read all items or records
        // 'IEnumerable' is used to return a collection of in-memory items.
        // 'IQueryable' is used to return a collection of items from a database.
        IEnumerable<TEntity> GetAll();

        // Delete one item or record by ID
        void Remove(int id);
    }

    
    //////////////////////////////////////
    

    // Product repository implementation (in-memory)        
    // The repo class acts as a mediator between the main app and the data source.
    public class Repository : IRepository<Product>
    {
        // In-memory list to simulate a data source.
        private readonly List<Product> _products = [];        

        // Adding a product to the in-memory list.
        public void Add(Product product) => _products.Add(product);

        // Using a lamda expression to find a product by ID.
        public Product GetById(int id) => _products.First(p => p.Id == id);

        // Returning all products from the in-memory list.
        public IEnumerable<Product> GetAll() => _products;

        // Removing a product by ID.
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
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("The Repository Design Pattern in C#.NET.");
            Console.WriteLine("----------------------------------------\n");


            // Create a repository instance
            Repository productRepo = new();

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
