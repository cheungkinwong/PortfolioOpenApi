using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PortfolioApi.Models;



public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Section> Sections { get; set; }
    public DbSet<WorkExperience> WorkExperiences { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<SoftSkill> SoftSkills { get; set; }
    public DbSet<TechnicalSkill> TechnicalSkills { get; set; }
    public DbSet<Hobby> Hobbies { get; set; }
    public DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<WorkExperience>()
            .HasOne(w => w.Section)
            .WithMany(s => s.WorkExperiences)
            .HasForeignKey(w => w.SectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Education>()
            .HasOne(e => e.Section)
            .WithMany(s => s.Educations)
            .HasForeignKey(e => e.SectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<TechnicalSkill>()
            .HasOne(t => t.Section)
            .WithMany(s => s.TechnicalSkills)
            .HasForeignKey(t => t.SectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<SoftSkill>()
            .HasOne(s => s.Section)
            .WithMany(sec => sec.SoftSkills)
            .HasForeignKey(s => s.SectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Hobby>()
            .HasOne(h => h.Section)
            .WithMany(s => s.Hobbies)
            .HasForeignKey(h => h.SectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "admin-role-id", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "reader-role-id", Name = "Reader", NormalizedName = "READER" }
        );

        builder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = "00000000-0000-0000-0000-000000000001",
                UserName = "ckadmin",
                NormalizedUserName = "CKADMIN",
                PasswordHash = "AQAAAAIAAYagAAAAEM/3FzxDvGid1y+gVx1qEIxWj64Rqqlp3vqX5EOqN+pBFE0zzPaMZvBqC/y0c6DfIw==", 
                SecurityStamp = "00000000-0000-0000-0000-0000000000AA"
            },
            new ApplicationUser
            {
                Id = "00000000-0000-0000-0000-000000000002",
                UserName = "reader",
                NormalizedUserName = "READER",
                PasswordHash = "AQAAAAEAACcQAAAAEOG+srRNrPTnR4y2hvBowdppe2k6JMzU3OmGQoPu/zYtPR7KQWIRFwIGc8i6uY/QGQ==", 
                SecurityStamp = "00000000-0000-0000-0000-0000000000BB"
            }
        );


        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = "00000000-0000-0000-0000-000000000001", RoleId = "admin-role-id" },
            new IdentityUserRole<string> { UserId = "00000000-0000-0000-0000-000000000002", RoleId = "reader-role-id" }
        );

        builder.Entity<Section>().HasData(
            new Section { Id = 1, Title = "About Me", Description = "Full-stack developer passionate about combining technology and design for seamless user experiences.", Image = "about.png" },
            new Section { Id = 2, Title = "Education", Description = "Formal education and certifications in development and design.", Image = "education.png" },
            new Section { Id = 3, Title = "Experience", Description = "Professional roles in front-end development, prepress, and lab work.", Image = "experience.png" },
            new Section { Id = 4, Title = "Technical Skills", Description = "Tools and technologies used in development and deployment.", Image = "techskills.png" },
            new Section { Id = 5, Title = "Soft Skills", Description = "Key interpersonal and problem-solving strengths.", Image = "softskills.png" },
            new Section { Id = 6, Title = "Hobbies", Description = "Interests outside of work.", Image = "hobbies.png" }
        );

        builder.Entity<WorkExperience>().HasData(
            new WorkExperience { Id = 1, Company = "ZAPFLOOR", Position = "Front-End Developer", StartDate = new DateTime(2020, 1, 1), EndDate = new DateTime(2024, 1, 1), SectionId = 3 },
            new WorkExperience { Id = 2, Company = "Gazelle Printing House", Position = "Prepress Coordinator", StartDate = new DateTime(2011, 1, 1), EndDate = new DateTime(2019, 1, 1), SectionId = 3 },
            new WorkExperience { Id = 3, Company = "Umicore", Position = "Lab Technician", StartDate = new DateTime(2007, 1, 1), EndDate = new DateTime(2009, 1, 1), SectionId = 3 }
        );

        builder.Entity<Education>().HasData(
            new Education { Id = 1, School = "VDAB", Course = "Full-stack developer", StartDate = new DateTime(2024, 1, 1), EndDate = null, SectionId = 2 },
            new Education { Id = 2, School = "BeCode", Course = "Front-end developer", StartDate = new DateTime(2019, 1, 1), EndDate = new DateTime(2020, 1, 1), SectionId = 2 },
            new Education { Id = 3, School = "VDAB", Course = "DTP-Prepress", StartDate = new DateTime(2010, 1, 1), EndDate = new DateTime(2011, 1, 1), SectionId = 2 },
            new Education { Id = 4, School = "AP Hoge School", Course = "Chemistry", StartDate = new DateTime(2007, 1, 1), EndDate = new DateTime(2009, 1, 1), SectionId = 2 }
        );

        builder.Entity<SoftSkill>().HasData(
            new SoftSkill { Id = 1, Name = "Communication", SectionId = 5 },
            new SoftSkill { Id = 2, Name = "Problem Solving", SectionId = 5 },
            new SoftSkill { Id = 3, Name = "Critical Thinking", SectionId = 5 },
            new SoftSkill { Id = 4, Name = "Growth Mindset", SectionId = 5 },
            new SoftSkill { Id = 5, Name = "Attention to Detail", SectionId = 5 }
        );

        builder.Entity<TechnicalSkill>().HasData(
            new TechnicalSkill { Id = 1, Name = "Vue.js / Nuxt.js", SectionId = 4 },
            new TechnicalSkill { Id = 2, Name = "React", SectionId = 4 },
            new TechnicalSkill { Id = 3, Name = "C# / .NET", SectionId = 4 },
            new TechnicalSkill { Id = 4, Name = "Razor / Blazor", SectionId = 4 },
            new TechnicalSkill { Id = 5, Name = "HTML / CSS", SectionId = 4 },
            new TechnicalSkill { Id = 6, Name = "Figma / Adobe XD", SectionId = 4 },
            new TechnicalSkill { Id = 7, Name = "MSSQL", SectionId = 4 },
            new TechnicalSkill { Id = 8, Name = "Git / GitHub", SectionId = 4 },
            new TechnicalSkill { Id = 9, Name = "OAuth / SSO", SectionId = 4 },
            new TechnicalSkill { Id = 10, Name = "Stripe / Mollie / Paypal", SectionId = 4 }
        );

        builder.Entity<Hobby>().HasData(
            new Hobby { Id = 1, Name = "Bass", SectionId = 6 },
            new Hobby { Id = 2, Name = "TTRPG", SectionId = 6 },
            new Hobby { Id = 3, Name = "Games", SectionId = 6 },
            new Hobby { Id = 4, Name = "Reading", SectionId = 6 }
        );

        builder.Entity<Project>().HasData(
            new Project { Id = 1, Name = "SWApi", Description = "Vue client that consumes Starwars API", Link = "https://www.github.com/cheungkinwong/javascript-framework-test", Image = "project1.jpg" },
            new Project { Id = 2, Name = "Memory game", Description = "Game project using js and css", Link = "https://www.github.com/cheungkinwong/memory-game", Image = "project2.jpg" }
        );
    }
}
