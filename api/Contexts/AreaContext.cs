using Microsoft.EntityFrameworkCore;

using area.Models;

namespace area.Contexts
{
	public partial class AreaContext : DbContext
	{
		public AreaContext()
		{
		}

		public AreaContext(DbContextOptions<AreaContext> options)
			: base(options)
		{
		}

		public virtual DbSet<UserModel> User { get; set; }
		public virtual DbSet<AccountModel> Account { get; set; }
		public virtual DbSet<ProviderModel> Provider { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

			modelBuilder.Entity<UserModel>(entity =>
			{
				entity.ToTable("user", "area");

				entity.HasKey(e => e.Id);

				entity.HasIndex(e => new { e.Username })
					.HasName("username")
					.IsUnique();

				entity.HasIndex(e => new { e.Email })
					.HasName("email")
					.IsUnique();

				entity.HasIndex(e => new { e.Username, e.Email })
					.HasName("UNIQUE")
					.IsUnique();

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.HasColumnType("int(10) unsigned");

				entity.Property(e => e.Date)
					.HasColumnName("date_created")
					.HasDefaultValueSql("current_timestamp()");

				entity.Property(e => e.Email)
					.IsRequired()
					.HasColumnName("email")
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasDefaultValueSql("''");

				entity.Property(e => e.Password)
					.IsRequired()
					.HasColumnName("password")
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasDefaultValueSql("''");

				entity.Property(e => e.Username)
					.IsRequired()
					.HasColumnName("username")
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasDefaultValueSql("''");
			});

			modelBuilder.Entity<AccountModel>(entity =>
			{
				entity.ToTable("account", "area");

				entity.HasKey(e => e.Id);

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.HasColumnType("int(10) unsigned");

				entity.Property(e => e.Token)
					.IsRequired()
					.HasColumnName("token")
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasDefaultValueSql("''");

				entity.Property(e => e.ProvId)
					.IsRequired()
					.HasColumnName("prov_id")
					.HasColumnType("int(10) unsigned")
					.HasDefaultValueSql("0");

				entity.Property(e => e.UserId)
					.IsRequired()
					.HasColumnName("user_id")
					.HasColumnType("int(10) unsigned")
					.HasDefaultValueSql("0");

				entity.Property(e => e.LastUpdated)
					.HasColumnName("last_updated")
					.HasDefaultValueSql("current_timestamp()");
			});

			modelBuilder.Entity<ProviderModel>(entity =>
			{
				entity.ToTable("provider", "area");

				entity.HasKey(e => e.Id);

				entity.HasIndex(e => new { e.Name })
					.HasName("name")
					.IsUnique();

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.HasColumnType("int(10) unsigned");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name")
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasDefaultValueSql("''");

				entity.Property(e => e.Url)
					.IsRequired()
					.HasColumnName("url")
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasDefaultValueSql("''");
			});
		}
	}
}
