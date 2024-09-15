using JWT.Models;
using Microsoft.EntityFrameworkCore;

namespace JWT.Data
{
    /// <summary>
    /// Represents the database context for User entities using Entity Framework Core.
    /// This class is responsible for configuring the database connection and defining the shape of your data as it is represented in the database.
    /// </summary>
    public class UserContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the UserContext class.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the DbSet<User> that can be used to query and save instances of User.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Configures the model that was discovered by convention from the entity types
        /// exposed in DbSet<TEntity> properties on your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                // Configures the email to be unique in the database
                entity.HasIndex(e => e.Email).IsUnique();

                // Configures the Id to be an identity column and sets it to auto-increment
                entity.Property(e => e.Id)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();
            });
        }
    }
}

// Usage Example:
/*
// In Startup.cs or Program.cs (depending on your .NET version)
services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

// In a repository or service class
public class UserRepository : IUserRepository
{
    private readonly UserContext _context;

    public UserRepository(UserContext context)
    {
        _context = context;
    }

    public User Create(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    // Other methods...
}
*/

// Notes:
// 1. This context class is specific to User entities. If you have other entities, consider creating a more general ApplicationDbContext.
// 2. The OnModelCreating method is used to configure the User entity, setting up a unique index on the Email property and configuring the Id as an identity column.
// 3. Ensure that you've properly set up your connection string in your application's configuration.
// 4. Use migrations to keep your database schema in sync with your entity models.

// Potential Improvements:
// 1. Add configuration for other entities if your application grows.
// 2. Consider moving entity configurations to separate classes (IEntityTypeConfiguration<T>) for better organization.
// 3. Implement soft delete functionality if needed (e.g., IsDeleted flag).
// 4. Add global query filters if you need to always apply certain conditions (e.g., for multi-tenancy).
// 5. Override SaveChanges() method to implement auditing or other cross-cutting concerns.