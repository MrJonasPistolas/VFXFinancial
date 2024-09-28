namespace VFXFinancial.Infrastructure.Contexts;
public class ApplicationDbContext : AuditableContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    DbSet<ForeignExchangeRate> ForeignExchangeRates { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "System";
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedOn = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = "System";
                    break;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ForeignExchangeRate>(ConfigureForeignExhcangeRates);
    }

    private void ConfigureForeignExhcangeRates(EntityTypeBuilder<ForeignExchangeRate> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.FromCurrencyCode)
            .IsRequired()
            .HasMaxLength(3);
        builder.Property(x => x.FromCurrencyName)
            .IsRequired()
            .HasMaxLength(30);
        builder.Property(x => x.ToCurrencyCode)
            .IsRequired()
            .HasMaxLength(3);
        builder.Property(x => x.ToCurrencyName)
            .IsRequired()
            .HasMaxLength(30);
        builder.Property(x => x.ExchangeRate)
            .IsRequired()
            .HasColumnType("decimal(19,6)");
        builder.Property(x => x.BidPrice)
            .IsRequired()
            .HasColumnType("decimal(19,6)");
        builder.Property(x => x.AskPrice)
            .IsRequired()
            .HasColumnType("decimal(19,6)");
        builder.ToTable(name: "ForeignExchangeRates", "Financial");
    }
}