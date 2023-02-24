using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

public class SearchContext : DbContext{

    public SearchContext(DbContextOptions<SearchContext> options) : base(options){
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:uppgift3db.database.windows.net,1433;Initial Catalog=uppgift3db;Persist Security Info=False;User ID=CloudSAb18fe369;Password=ARW5yQYcUi8NNWr;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Search>()
        .Property(s => s.SearchString)
        .IsRequired();
        
        modelBuilder.Entity<Search>()
        .HasKey(s => s.SearchID);
    }

    public DbSet<Search> SearchSet {get;set;}
}
        