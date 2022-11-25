using Application;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class RedditContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set;  }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/Reddit.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserInfoRetrieving>();
        modelBuilder.Entity<Post>().HasKey(post => post.Id);
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<Comment>().HasKey(comment => comment.Id);
        
        modelBuilder.Entity<Comment>().Property(todo => todo.CommentTitle).HasMaxLength(50);
        modelBuilder.Entity<Comment>().Property(todo => todo.CommentDescription).HasMaxLength(199);
    }
}