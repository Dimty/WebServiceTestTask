using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace WebServiceTestTask
{
    /// <summary>
    /// An automatically generated class describing interaction with the database.
    /// </summary>
    public partial class MessagesDbContext : DbContext
    {
        static MessagesDbContext()=> NpgsqlConnection.GlobalTypeMapper.MapEnum<PostgresContext.Result>();

        public MessagesDbContext()
        {
            Database.EnsureCreated();
        }

        public MessagesDbContext(DbContextOptions<MessagesDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<MessageProperty> MessageProperties { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conn = System.Configuration.ConfigurationManager.ConnectionStrings["Authentication"].ConnectionString;
                optionsBuilder.UseNpgsql(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<PostgresContext.Result>();

            modelBuilder.Entity<MessageProperty>(entity =>
            {
                entity.ToTable("messageProperties");
                
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();
                entity.Property(e => e.Result)
                    .HasColumnName("Result")
                    .HasColumnOrder(7);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        /// <summary>
        /// Saves the message in the database
        /// </summary>
        /// <param name="message">Message to save</param>
        /// <param name="status">Message status</param>
        public async Task SaveMessageAsync(LetterPostRequest message,LetterPostResponseStatus status)
        {
            var mess = new MessageProperty()
            {
                Subject = message.Subject,
                Body = message.Body,
                DateOfCreation = DateOnly.FromDateTime(DateTime.Now),
                Result =  status.Status,
                Recipients = message.Recipients,
                FailedMessage = status.Description
            };
            Add(mess);
            await SaveChangesAsync();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
