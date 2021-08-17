using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Noteapp.Models
{
    public partial class NoteWebdbContext : DbContext
    {
        public NoteWebdbContext()
        {
        }

        public NoteWebdbContext(DbContextOptions<NoteWebdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Notes> Notes { get; set; }

       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       {
           if (!optionsBuilder.IsConfigured)
          {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
              optionsBuilder.UseSqlServer("Server=HP\\SQLEXPRESS;Database=NoteWebdb;Trusted_Connection=True;user id=Pavithra;password=admin;");
           }
       }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<Notes>(entity =>
            {
                entity.HasKey(e => e.Noteid);

                entity.Property(e => e.Noteid).ValueGeneratedNever();

                entity.Property(e => e.Notedate).HasColumnType("smalldatetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
