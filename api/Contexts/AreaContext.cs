using Microsoft.EntityFrameworkCore;

using area.Models.Provider;
using area.Models.Service;
using area.Models.User;
using area.Models.Widget;
using area.Models.WidgetConf;
using area.Models.WidgetParam;

namespace area.Contexts
{
	public class AreaContext : DbContext
	{
		public AreaContext()
		{
		}

		public AreaContext(DbContextOptions<AreaContext> options)
			: base(options)
		{
		}

		public virtual DbSet<UserModel> User { get; set; }
		public virtual DbSet<ProviderModel> Provider { get; set; }
		public virtual DbSet<ServiceModel> Service { get; set; }
		public virtual DbSet<WidgetModel> Widget { get; set; }
		public virtual DbSet<WidgetConfModel> WidgetConf { get; set; }
		public virtual DbSet<WidgetParamModel> WidgetParam { get; set; }

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
					.HasColumnName("created_at")
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
				
				entity.Property(e => e.Key)
					.IsRequired()
					.HasColumnName("key")
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasDefaultValueSql("''");
			});
			
			modelBuilder.Entity<ServiceModel>(entity =>
			{
				entity.ToTable("service", "area");

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

				entity.Property(e => e.ProvId)
					.IsRequired()
					.HasColumnName("prov_id")
					.HasColumnType("int(10) unsigned")
					.HasDefaultValueSql("0");
			});

			modelBuilder.Entity<WidgetModel>(entity =>
			{
				entity.ToTable("widget", "area");

				entity.HasKey(e => e.Id);

				entity.HasIndex(e => new {e.Name})
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

				entity.Property(e => e.ParamId)
					.IsRequired()
					.HasColumnName("param_id")
					.HasColumnType("int(10) unsigned")
					.HasDefaultValueSql("0");
				
				entity.Property(e => e.ServiceId)
					.IsRequired()
					.HasColumnName("service_id")
					.HasColumnType("int(10) unsigned")
					.HasDefaultValueSql("0");
			});

			modelBuilder.Entity<WidgetParamModel>(entity =>
			{
				entity.ToTable("widget_param", "area");

				entity.HasKey(e => e.Id);

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.HasColumnType("int(10) unsigned");

				entity.Property(e => e.Param)
					.IsRequired()
					.HasColumnName("param")
					.IsUnicode(false)
					.HasDefaultValueSql("''");
			});
			
			modelBuilder.Entity<WidgetConfModel>(entity =>
			{
				entity.ToTable("widget_conf", "area");

				entity.HasKey(e => e.Id);

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.HasColumnType("int(10) unsigned");

				entity.Property(e => e.Conf)
					.IsRequired()
					.HasColumnName("conf")
					.IsUnicode(false)
					.HasDefaultValueSql("''");
				
				entity.Property(e => e.UserId)
					.IsRequired()
					.HasColumnName("user_id")
					.HasColumnType("int(10) unsigned")
					.HasDefaultValueSql("0");
				
				entity.Property(e => e.WidgetId)
					.IsRequired()
					.HasColumnName("widget_id")
					.HasColumnType("int(10) unsigned")
					.HasDefaultValueSql("0");
				
				entity.Property(e => e.UpdatedAt)
					.HasColumnName("updated_at")
					.HasDefaultValueSql("current_timestamp()");
				
				entity.Property(e => e.CreatedAt)
					.HasColumnName("created_at")
					.HasDefaultValueSql("current_timestamp()");
			});
		}
	}
}
