
// Amir Moeini Rad
// May, 2025

// The Repository Design Pattern

// In this pattern, a repository class mediates between the business logic and data mapping layers.

namespace RepositoryDemo
{
    // Domain Entity
    public class Product
    {        
        public int Id { get; set; }        
        public string? Name { get; set; }
    }


    // Repository
    public interface IRepository<TEntity>
    {                
        void Add(TEntity entity);        
        TEntity GetById(int id);               
        IEnumerable<TEntity> GetAll(); // 'IEnumerable' is used to return a collection of in-memory items.
        void Remove(int id);
    }
          

    public class Repository : IRepository<Product>
    {
        private readonly List<Product> _products = [];        

        public void Add(Product product) => _products.Add(product);

        public Product GetById(int id) => _products.First(p => p.Id == id);

        public IEnumerable<Product> GetAll() => _products;
        
        public void Remove(int id)
        {
            var product = GetById(id);                        
            if (product != null)
                _products.Remove(product);
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("The Repository Design Pattern in C#.NET.");
            Console.WriteLine("----------------------------------------\n");

            
            Repository productRepo = new();

            productRepo.Add(new Product { Id = 1, Name = "Laptop" });
            productRepo.Add(new Product { Id = 2, Name = "Smartphone" });
       
            foreach (var product in productRepo.GetAll())
                Console.WriteLine($"Product {product.Id} - {product.Name}");
            
            var singleProduct = productRepo.GetById(1);
            Console.WriteLine($"\nRetrieved: {singleProduct.Name}");
            
            productRepo.Remove(1);
            
            Console.WriteLine("\nAfter removal:");
            foreach (var product in productRepo.GetAll())
                Console.WriteLine($"Product {product.Id} - {product.Name}");

            Console.WriteLine("\nDone.");
        }
    }
}
