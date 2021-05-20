using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Database relations
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Movie Watchlist M:N cardinality configuration
            builder.Entity<MovieWatchlist>()
                    .HasKey(mwl => new { mwl.MovieId, mwl.WatchlistId });

            builder.Entity<MovieWatchlist>()
                    .HasOne(mwl => mwl.Movie)
                    .WithMany(mwl => mwl.Watchlists)
                    .HasForeignKey(mwl => mwl.MovieId);

            builder.Entity<MovieWatchlist>()
                    .HasOne(mwl => mwl.Watchlist)
                    .WithMany(mwl => mwl.Movies)
                    .HasForeignKey(mwl => mwl.WatchlistId);

            // Series Watchlist M:N cardinality configuration
            builder.Entity<SeriesWatchlist>()
                    .HasKey(swl => new { swl.SeriesId, swl.WatchlistId });

            builder.Entity<SeriesWatchlist>()
                    .HasOne(swl => swl.Series)
                    .WithMany(swl => swl.Watchlists)
                    .HasForeignKey(swl => swl.SeriesId);

            builder.Entity<SeriesWatchlist>()
                    .HasOne(swl => swl.Watchlist)
                    .WithMany(swl => swl.Series)
                    .HasForeignKey(swl => swl.WatchlistId);
        }
    }
}
