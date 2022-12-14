// <auto-generated />
using System;
using GuessIngGameWithEfCoreAndCosmoDb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GuessIngGameWithEfCoreAndCosmoDb.Migrations
{
    [DbContext(typeof(GameDbContext))]
    [Migration("20220717014208_MovedPrizeToContestant")]
    partial class MovedPrizeToContestant
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GuessIngGameWithEfCoreAndCosmoDb.Models.Contest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UId")
                        .HasColumnType("char(36)");

                    b.Property<int>("WinningNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Contest");
                });

            modelBuilder.Entity("GuessIngGameWithEfCoreAndCosmoDb.Models.Contestant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ContestId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("PrizeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContestId");

                    b.ToTable("Contestant");
                });

            modelBuilder.Entity("GuessIngGameWithEfCoreAndCosmoDb.Models.Guess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ContestId")
                        .HasColumnType("int");

                    b.Property<int>("ContestantId")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContestId");

                    b.HasIndex("ContestantId");

                    b.ToTable("Guess");
                });

            modelBuilder.Entity("GuessIngGameWithEfCoreAndCosmoDb.Models.Prize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ContestId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsWon")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Place")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContestId");

                    b.ToTable("Prize");
                });

            modelBuilder.Entity("GuessIngGameWithEfCoreAndCosmoDb.Models.Contestant", b =>
                {
                    b.HasOne("GuessIngGameWithEfCoreAndCosmoDb.Models.Contest", "Contest")
                        .WithMany("Contestants")
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contest");
                });

            modelBuilder.Entity("GuessIngGameWithEfCoreAndCosmoDb.Models.Guess", b =>
                {
                    b.HasOne("GuessIngGameWithEfCoreAndCosmoDb.Models.Contest", "Contest")
                        .WithMany("Guesses")
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuessIngGameWithEfCoreAndCosmoDb.Models.Contestant", "Contestant")
                        .WithMany()
                        .HasForeignKey("ContestantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contest");

                    b.Navigation("Contestant");
                });

            modelBuilder.Entity("GuessIngGameWithEfCoreAndCosmoDb.Models.Prize", b =>
                {
                    b.HasOne("GuessIngGameWithEfCoreAndCosmoDb.Models.Contest", "Contest")
                        .WithMany("Prizes")
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contest");
                });

            modelBuilder.Entity("GuessIngGameWithEfCoreAndCosmoDb.Models.Contest", b =>
                {
                    b.Navigation("Contestants");

                    b.Navigation("Guesses");

                    b.Navigation("Prizes");
                });
#pragma warning restore 612, 618
        }
    }
}
