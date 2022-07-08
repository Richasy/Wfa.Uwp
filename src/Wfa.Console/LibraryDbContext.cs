// Copyright (c) Richasy. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Wfa.Models.Community;
using Wfa.Models.Data.Center;

namespace Wfa.Console
{
    /// <summary>
    /// 资料库上下文.
    /// </summary>
    public sealed class LibraryDbContext : DbContext
    {
        private readonly string _dbPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryDbContext"/> class.
        /// </summary>
        public LibraryDbContext()
            => _dbPath = "lib.db";

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryDbContext"/> class.
        /// </summary>
        /// <param name="dbPath">数据库文件路径.</param>
        public LibraryDbContext(string dbPath)
            => _dbPath = dbPath;

        /// <summary>
        /// 空战装甲数据集.
        /// </summary>
        public DbSet<Archwing> Archwing { get; set; }

        /// <summary>
        /// 空战枪械数据集.
        /// </summary>
        public DbSet<ArchGun> ArchGun { get; set; }

        /// <summary>
        /// 空战近战武器数据集.
        /// </summary>
        public DbSet<ArchMelee> ArchMelee { get; set; }

        /// <summary>
        /// 近战武器数据集.
        /// </summary>
        public DbSet<Melee> Melee { get; set; }

        /// <summary>
        /// Mod 数据集.
        /// </summary>
        public DbSet<Mod> Mods { get; set; }

        /// <summary>
        /// 主要武器数据集.
        /// </summary>
        public DbSet<Primary> Primaries { get; set; }

        /// <summary>
        /// 次要武器数据集.
        /// </summary>
        public DbSet<Secondary> Secondaries { get; set; }

        /// <summary>
        /// 装甲数据集.
        /// </summary>
        public DbSet<Warframe> Warframes { get; set; }

        /// <summary>
        /// 物品翻译数据集.
        /// </summary>
        public DbSet<Dict> Dicts { get; set; }

        /// <summary>
        /// 来自维基的翻译数据集.
        /// </summary>
        public DbSet<Translate> Translates { get; set; }

        /// <summary>
        /// 增补翻译集.
        /// </summary>
        public DbSet<Patch> Patches { get; set; }

        /// <summary>
        /// 元数据集.
        /// </summary>
        public DbSet<Meta> Metas { get; set; }

        /// <summary>
        /// WM商品数据集.
        /// </summary>
        public DbSet<MarketItem> MarketItems { get; set; }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={_dbPath};")
                             .EnableSensitiveDataLogging();

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Archwing>()
                .HasMany(p => p.Abilities)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ArchGun>()
                .HasMany(p => p.Attacks)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ArchMelee>()
                .HasMany(p => p.Attacks)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Melee>()
                .HasMany(p => p.Attacks)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Mod>()
                .HasMany(p => p.Effects)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Primary>()
                .HasMany(p => p.Attacks)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Secondary>()
                .HasMany(p => p.Attacks)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Warframe>()
                .HasMany(p => p.Abilities)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
