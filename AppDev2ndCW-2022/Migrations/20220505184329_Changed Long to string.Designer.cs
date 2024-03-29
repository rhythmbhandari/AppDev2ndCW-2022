﻿// <auto-generated />
using System;
using AppDev2ndCW_2022.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppDev2ndCW_2022.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20220505184329_Changed Long to string")]
    partial class ChangedLongtostring
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AppDev2ndCW_2022.Models.Actor", b =>
                {
                    b.Property<long>("ActorNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ActorNumber"), 1L, 1);

                    b.Property<string>("ActorFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ActorSurname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActorNumber");

                    b.ToTable("Actor");
                });

            modelBuilder.Entity("AppDev2ndCW_2022.Models.CastMember", b =>
                {
                    b.Property<long>("DvdNumber")
                        .HasColumnType("bigint");

                    b.Property<long>("ActorNumber")
                        .HasColumnType("bigint");

                    b.HasKey("DvdNumber", "ActorNumber");

                    b.HasIndex("ActorNumber");

                    b.ToTable("CastMember");
                });

            modelBuilder.Entity("AppDev2ndCW_2022.Models.DvdCategory", b =>
                {
                    b.Property<long>("CategoryNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CategoryNumber"), 1L, 1);

                    b.Property<string>("AgeRestricted")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryNumber");

                    b.ToTable("DvdCategory");
                });

            modelBuilder.Entity("AppDev2ndCW_2022.Models.DvdCopy", b =>
                {
                    b.Property<long>("CopyNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CopyNumber"), 1L, 1);

                    b.Property<DateTime>("DatePurchased")
                        .HasColumnType("datetime2");

                    b.Property<long>("DvdNumber")
                        .HasColumnType("bigint");

                    b.HasKey("CopyNumber");

                    b.HasIndex("DvdNumber");

                    b.ToTable("DvdCopy");
                });

            modelBuilder.Entity("AppDev2ndCW_2022.Models.DvdTitle", b =>
                {
                    b.Property<long>("DvdNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("DvdNumber"), 1L, 1);

                    b.Property<long>("CategoryNumber")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateReleased")
                        .HasColumnType("datetime2");

                    b.Property<int>("PenaltyCharge")
                        .HasColumnType("int");

                    b.Property<long>("ProducerNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("StandardCharge")
                        .HasColumnType("int");

                    b.Property<long>("StudioNumber")
                        .HasColumnType("bigint");

                    b.HasKey("DvdNumber");

                    b.HasIndex("CategoryNumber");

                    b.HasIndex("ProducerNumber");

                    b.HasIndex("StudioNumber");

                    b.ToTable("DvdTitle");
                });

            modelBuilder.Entity("AppDev2ndCW_2022.Models.Loan", b =>
                {
                    b.Property<long>("LoanNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LoanNumber"), 1L, 1);

                    b.Property<long>("CopyNumber")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateDue")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateReturned")
                        .HasColumnType("datetime2");

                    b.Property<long>("LoanTypeNumber")
                        .HasColumnType("bigint");

                    b.Property<long>("MemberNumber")
                        .HasColumnType("bigint");

                    b.HasKey("LoanNumber");

                    b.HasIndex("CopyNumber");

                    b.HasIndex("LoanTypeNumber");

                    b.HasIndex("MemberNumber");

                    b.ToTable("Loan");
                });

            modelBuilder.Entity("AppDev2ndCW_2022.Models.LoanTypes", b =>
                {
                    b.Property<long>("LoanTypeNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LoanTypeNumber"), 1L, 1);

                    b.Property<int>("LoanDuration")
                        .HasColumnType("int");

                    b.Property<string>("LoanType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoanTypeNumber");

                    b.ToTable("LoanTypes");
                });

            modelBuilder.Entity("AppDev2ndCW_2022.Models.Member", b =>
                {
                    b.Property<long>("MemberNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("MemberNumber"), 1L, 1);

                    b.Property<string>("MemberAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MemberDOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("MemberFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MembershipCategoryNumber")
                        .HasColumnType("bigint");

                    b.HasKey("MemberNumber");

                    b.HasIndex("MembershipCategoryNumber");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("AppDev2ndCW_2022.Models.MembershipCategory", b =>
                {
                    b.Property<long>("MembershipCategoryNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("MembershipCategoryNumber"), 1L, 1);

                    b.Property<string>("MembershipCategoryDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MembershipCategoryTotalLoans")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MembershipCategoryNumber");

                    b.ToTable("MembershipCategory");
                });

            modelBuilder.Entity("AppDev2ndCW_2022.Models.Producer", b =>
                {
                    b.Property<long>("ProducerNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ProducerNumber"), 1L, 1);

                    b.Property<string>("ProducerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProducerNumber");

                    b.ToTable("Producer");
                });

            modelBuilder.Entity("AppDev2ndCW_2022.Models.Studio", b =>
                {
                    b.Property<long>("StudioNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("StudioNumber"), 1L, 1);

                    b.Property<string>("StudioName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudioNumber");

                    b.ToTable("Studio");
                });

            modelBuilder.Entity("AppDev2ndCW_2022.Models.User", b =>
                {
                    b.Property<long>("UserNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserNumber"), 1L, 1);

                    b.Property<string>("UserPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserNumber");

                    b.ToTable("User");
                });

            modelBuilder.Entity("AppDev2ndCW_2022.Models.CastMember", b =>
                {
                    b.HasOne("AppDev2ndCW_2022.Models.Actor", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppDev2ndCW_2022.Models.DvdTitle", "DvdTitle")
                        .WithMany()
                        .HasForeignKey("DvdNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("DvdTitle");
                });

            modelBuilder.Entity("AppDev2ndCW_2022.Models.DvdCopy", b =>
                {
                    b.HasOne("AppDev2ndCW_2022.Models.DvdTitle", "DvdTitle")
                        .WithMany()
                        .HasForeignKey("DvdNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DvdTitle");
                });

            modelBuilder.Entity("AppDev2ndCW_2022.Models.DvdTitle", b =>
                {
                    b.HasOne("AppDev2ndCW_2022.Models.DvdCategory", "DvdCategory")
                        .WithMany()
                        .HasForeignKey("CategoryNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppDev2ndCW_2022.Models.Producer", "Producer")
                        .WithMany()
                        .HasForeignKey("ProducerNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppDev2ndCW_2022.Models.Studio", "Studio")
                        .WithMany()
                        .HasForeignKey("StudioNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DvdCategory");

                    b.Navigation("Producer");

                    b.Navigation("Studio");
                });

            modelBuilder.Entity("AppDev2ndCW_2022.Models.Loan", b =>
                {
                    b.HasOne("AppDev2ndCW_2022.Models.DvdCopy", "DvdCopy")
                        .WithMany()
                        .HasForeignKey("CopyNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppDev2ndCW_2022.Models.LoanTypes", "LoanType")
                        .WithMany()
                        .HasForeignKey("LoanTypeNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppDev2ndCW_2022.Models.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DvdCopy");

                    b.Navigation("LoanType");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("AppDev2ndCW_2022.Models.Member", b =>
                {
                    b.HasOne("AppDev2ndCW_2022.Models.MembershipCategory", "MembershipCategory")
                        .WithMany()
                        .HasForeignKey("MembershipCategoryNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MembershipCategory");
                });
#pragma warning restore 612, 618
        }
    }
}
