using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortfolioOpenApi.Models;

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




    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);


        builder.Entity<Section>().HasData(
            new Section { Id = 1, Title = "About Me", Description = "Full-stack developer passionate about combining technology and design for seamless user experiences.", Image = "about.png" },
            new Section { Id = 2, Title = "Education", Description = "Formal education and certifications in development and design.", Image = "education.png" },
            new Section { Id = 3, Title = "Experience", Description = "Professional roles in front-end development, prepress, and lab work.", Image = "experience.png" },
            new Section { Id = 4, Title = "Technical Skills", Description = "Tools and technologies used in development and deployment.", Image = "techskills.png" },
            new Section { Id = 5, Title = "Soft Skills", Description = "Key interpersonal and problem-solving strengths.", Image = "softskills.png" },
            new Section { Id = 6, Title = "Hobbies", Description = "Interests outside of work.", Image = "hobbies.png" }
        );

        builder.Entity<WorkExperience>().HasData(
            new WorkExperience { Id = 1, Company = "ZAPFLOOR", Position = "Front-End Developer", Description = "Maintained platforms, integrated APIs, handled SSO and payment systems, designed UIs and wireframes.", StartDate = new DateTime(2020, 1, 1), EndDate = new DateTime(2024, 1, 1), Image = "zapfloor.png" },
            new WorkExperience { Id = 2, Company = "Gazelle Printing House", Position = "Prepress Coordinator", Description = "Design, layout, Adobe Suite, and prepress operations.", StartDate = new DateTime(2011, 1, 1), EndDate = new DateTime(2019, 1, 1), Image = "gazelle.png" },
            new WorkExperience { Id = 3, Company = "Umicore", Position = "Lab Technician", Description = "Quality control and analytics in chemistry lab.", StartDate = new DateTime(2007, 1, 1), EndDate = new DateTime(2009, 1, 1), Image = "umicore.png" }
        );

        builder.Entity<Education>().HasData(
            new Education { Id = 1, School = "VDAB", Degree = "Full-stack Developer", StartDate = new DateTime(2024, 1, 1), EndDate = null, Image = "vdab-fullstack.png" },
            new Education { Id = 2, School = "BeCode", Degree = "Front-end Developer", StartDate = new DateTime(2019, 1, 1), EndDate = new DateTime(2020, 1, 1), Image = "becode.png" },
            new Education { Id = 3, School = "VDAB", Degree = "DTP-Prepress", StartDate = new DateTime(2010, 1, 1), EndDate = new DateTime(2011, 1, 1), Image = "vdab-prepress.png" },
            new Education { Id = 4, School = "AP Hoge School", Degree = "Chemistry", StartDate = new DateTime(2007, 1, 1), EndDate = new DateTime(2009, 1, 1), Image = "ap-chemistry.png" }
        );

        builder.Entity<SoftSkill>().HasData(
            new SoftSkill { Id = 1, Name = "Communication", Image = "communication.png" },
            new SoftSkill { Id = 2, Name = "Problem Solving", Image = "problem-solving.png" },
            new SoftSkill { Id = 3, Name = "Critical Thinking", Image = "critical-thinking.png" },
            new SoftSkill { Id = 4, Name = "Growth Mindset", Image = "growth.png" },
            new SoftSkill { Id = 5, Name = "Attention to Detail", Image = "detail.png" }
        );

        builder.Entity<TechnicalSkill>().HasData(
            new TechnicalSkill { Id = 1, Name = "Vue.js / Nuxt.js", Level = "Advanced", Image = "vue.png" },
            new TechnicalSkill { Id = 2, Name = "React", Level = "Intermediate", Image = "react.png" },
            new TechnicalSkill { Id = 3, Name = "C# / .NET", Level = "Advanced", Image = "dotnet.png" },
            new TechnicalSkill { Id = 4, Name = "Razor / Blazor", Level = "Intermediate", Image = "razor.png" },
            new TechnicalSkill { Id = 5, Name = "HTML / CSS", Level = "Advanced", Image = "htmlcss.png" },
            new TechnicalSkill { Id = 6, Name = "Figma / Adobe XD", Level = "Intermediate", Image = "figma.png" },
            new TechnicalSkill { Id = 7, Name = "MSSQL", Level = "Intermediate", Image = "mssql.png" },
            new TechnicalSkill { Id = 8, Name = "Git / GitHub", Level = "Intermediate", Image = "git.png" },
            new TechnicalSkill { Id = 9, Name = "OAuth / SSO", Level = "Intermediate", Image = "oauth.png" },
            new TechnicalSkill { Id = 10, Name = "Stripe / Mollie / Paypal", Level = "Intermediate", Image = "payments.png" }
        );

        builder.Entity<Hobby>().HasData(
            new Hobby { Id = 1, Name = "Bass", Image = "bass.png" },
            new Hobby { Id = 2, Name = "TTRPG", Image = "ttrpg.png" },
            new Hobby { Id = 3, Name = "Games", Image = "games.png" },
            new Hobby { Id = 4, Name = "Reading", Image = "reading.png" }
        );

        builder.Entity<Project>().HasData(
            new Project { Id = 1, Name = "Project1", Description = "Project1",Image = "project1.png" },
            new Project { Id = 2, Name = "Project2", Description = "Project2", Image = "project2.png" }
        );

    }
}