using System;
using Cwiczenia5.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace Cwiczenia5.Migrations;

[DbContext(typeof(AppDbContext))]
partial class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.0")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        modelBuilder.Entity("Cwiczenia5.Models.Component", entity =>
        {
            entity.Property<string>("Code")
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnType("char(10)");

            entity.Property<int>("ComponentManufacturersId")
                .HasColumnType("int");

            entity.Property<int>("ComponentTypesId")
                .HasColumnType("int");

            entity.Property<string>("Description")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            entity.Property<string>("Name")
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnType("nvarchar(300)");

            entity.HasKey("Code")
                .HasName("Components_pk");

            entity.HasIndex("ComponentManufacturersId");
            entity.HasIndex("ComponentTypesId");

            entity.ToTable("Components");

            entity.HasData(
                new
                {
                    Code = "CPU0000001",
                    ComponentManufacturersId = 1,
                    ComponentTypesId = 1,
                    Description = "8-core gaming processor",
                    Name = "Ryzen 7 7800X3D"
                },
                new
                {
                    Code = "GPU0000001",
                    ComponentManufacturersId = 2,
                    ComponentTypesId = 2,
                    Description = "High-end gaming graphics card",
                    Name = "RTX 4080 Super"
                },
                new
                {
                    Code = "RAM0000001",
                    ComponentManufacturersId = 3,
                    ComponentTypesId = 3,
                    Description = "DDR5 RAM module 16GB",
                    Name = "Corsair Vengeance DDR5 16GB"
                });
        });

        modelBuilder.Entity("Cwiczenia5.Models.ComponentManufacturer", entity =>
        {
            entity.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasAnnotation("SqlServer:Identity", "1, 1");

            entity.Property<string>("Abbreviation")
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnType("nvarchar(30)");

            entity.Property<DateOnly>("FoundationDate")
                .HasColumnType("date");

            entity.Property<string>("FullName")
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnType("nvarchar(300)");

            entity.HasKey("Id")
                .HasName("ComponentManufacturers_pk");

            entity.ToTable("ComponentManufacturers");

            entity.HasData(
                new
                {
                    Id = 1,
                    Abbreviation = "AMD",
                    FoundationDate = new DateOnly(1969, 5, 1),
                    FullName = "Advanced Micro Devices"
                },
                new
                {
                    Id = 2,
                    Abbreviation = "NV",
                    FoundationDate = new DateOnly(1993, 4, 5),
                    FullName = "NVIDIA Corporation"
                },
                new
                {
                    Id = 3,
                    Abbreviation = "COR",
                    FoundationDate = new DateOnly(1994, 1, 1),
                    FullName = "Corsair Gaming Inc."
                });
        });

        modelBuilder.Entity("Cwiczenia5.Models.ComponentType", entity =>
        {
            entity.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasAnnotation("SqlServer:Identity", "1, 1");

            entity.Property<string>("Abbreviation")
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnType("nvarchar(30)");

            entity.Property<string>("Name")
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("nvarchar(150)");

            entity.HasKey("Id")
                .HasName("ComponentTypes_pk");

            entity.ToTable("ComponentTypes");

            entity.HasData(
                new
                {
                    Id = 1,
                    Abbreviation = "CPU",
                    Name = "Processor"
                },
                new
                {
                    Id = 2,
                    Abbreviation = "GPU",
                    Name = "Graphics Card"
                },
                new
                {
                    Id = 3,
                    Abbreviation = "RAM",
                    Name = "Memory"
                });
        });

        modelBuilder.Entity("Cwiczenia5.Models.PC", entity =>
        {
            entity.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasAnnotation("SqlServer:Identity", "1, 1");

            entity.Property<DateTime>("CreatedAt")
                .HasColumnType("datetime");

            entity.Property<string>("Name")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            entity.Property<int>("Stock")
                .HasColumnType("int");

            entity.Property<int>("Warranty")
                .HasColumnType("int");

            entity.Property<double>("Weight")
                .HasColumnType("float(5)");

            entity.HasKey("Id")
                .HasName("PCs_pk");

            entity.ToTable("PCs");

            entity.HasData(
                new
                {
                    Id = 1,
                    CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0),
                    Name = "Gaming Beast X",
                    Stock = 5,
                    Warranty = 36,
                    Weight = 12.5
                },
                new
                {
                    Id = 2,
                    CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0),
                    Name = "Office Mini Pro",
                    Stock = 12,
                    Warranty = 24,
                    Weight = 4.2
                },
                new
                {
                    Id = 3,
                    CreatedAt = new DateTime(2026, 3, 20, 10, 15, 0),
                    Name = "Creator Workstation",
                    Stock = 3,
                    Warranty = 36,
                    Weight = 9.8
                });
        });

        modelBuilder.Entity("Cwiczenia5.Models.PCComponent", entity =>
        {
            entity.Property<int>("PCId")
                .HasColumnType("int");

            entity.Property<string>("ComponentCode")
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnType("char(10)");

            entity.Property<int>("Amount")
                .HasColumnType("int");

            entity.HasKey("PCId", "ComponentCode")
                .HasName("PCComponents_pk");

            entity.HasIndex("ComponentCode");

            entity.ToTable("PCComponents");

            entity.HasData(
                new
                {
                    PCId = 1,
                    ComponentCode = "CPU0000001",
                    Amount = 1
                },
                new
                {
                    PCId = 1,
                    ComponentCode = "GPU0000001",
                    Amount = 1
                },
                new
                {
                    PCId = 1,
                    ComponentCode = "RAM0000001",
                    Amount = 2
                },
                new
                {
                    PCId = 2,
                    ComponentCode = "CPU0000001",
                    Amount = 1
                },
                new
                {
                    PCId = 2,
                    ComponentCode = "RAM0000001",
                    Amount = 1
                },
                new
                {
                    PCId = 3,
                    ComponentCode = "CPU0000001",
                    Amount = 1
                },
                new
                {
                    PCId = 3,
                    ComponentCode = "GPU0000001",
                    Amount = 1
                },
                new
                {
                    PCId = 3,
                    ComponentCode = "RAM0000001",
                    Amount = 4
                });
        });

        modelBuilder.Entity("Cwiczenia5.Models.Component", entity =>
        {
            entity.HasOne("Cwiczenia5.Models.ComponentManufacturer", "Manufacturer")
                .WithMany("Components")
                .HasForeignKey("ComponentManufacturersId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired()
                .HasConstraintName("Components_ComponentManufacturers");

            entity.HasOne("Cwiczenia5.Models.ComponentType", "Type")
                .WithMany("Components")
                .HasForeignKey("ComponentTypesId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired()
                .HasConstraintName("Components_ComponentTypes");

            entity.Navigation("Manufacturer");
            entity.Navigation("Type");
        });

        modelBuilder.Entity("Cwiczenia5.Models.PCComponent", entity =>
        {
            entity.HasOne("Cwiczenia5.Models.Component", "Component")
                .WithMany("PCComponents")
                .HasForeignKey("ComponentCode")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired()
                .HasConstraintName("PCComponents_Components");

            entity.HasOne("Cwiczenia5.Models.PC", "PC")
                .WithMany("PCComponents")
                .HasForeignKey("PCId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired()
                .HasConstraintName("PCComponents_PCs");

            entity.Navigation("Component");
            entity.Navigation("PC");
        });

        modelBuilder.Entity("Cwiczenia5.Models.Component", entity =>
        {
            entity.Navigation("PCComponents");
        });

        modelBuilder.Entity("Cwiczenia5.Models.ComponentManufacturer", entity =>
        {
            entity.Navigation("Components");
        });

        modelBuilder.Entity("Cwiczenia5.Models.ComponentType", entity =>
        {
            entity.Navigation("Components");
        });

        modelBuilder.Entity("Cwiczenia5.Models.PC", entity =>
        {
            entity.Navigation("PCComponents");
        });
    }
}
