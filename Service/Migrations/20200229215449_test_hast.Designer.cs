﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Service;

namespace Service.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200229215449_test_hast")]
    partial class test_hast
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.Entities.Condition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Condition");
                });

            modelBuilder.Entity("DataLayer.Entities.FAQs.FAQ", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnswerText");

                    b.Property<int>("FaqGroupId");

                    b.Property<bool>("IsActive");

                    b.Property<string>("QuestionText");

                    b.HasKey("Id");

                    b.HasIndex("FaqGroupId");

                    b.ToTable("FAQ");
                });

            modelBuilder.Entity("DataLayer.Entities.FAQs.FaqGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Icon")
                        .HasMaxLength(60);

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("FaqGroup");
                });

            modelBuilder.Entity("DataLayer.Entities.FactorAndPackage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsFactor");

                    b.Property<int>("TotalAmount");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("FactorAndPackage");
                });

            modelBuilder.Entity("DataLayer.Entities.FactorItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<int>("FactorId");

                    b.Property<int>("Number");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("FactorId");

                    b.HasIndex("ProductId");

                    b.ToTable("FactorItem");
                });

            modelBuilder.Entity("DataLayer.Entities.Favorites", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId");

                    b.Property<DateTime>("SubmitDate");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("DataLayer.Entities.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FeatureType");

                    b.Property<bool>("IsRequired");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("Feature");
                });

            modelBuilder.Entity("DataLayer.Entities.FeatureItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FeatureId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId");

                    b.ToTable("FeatureItem");
                });

            modelBuilder.Entity("DataLayer.Entities.LogoManufactory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressImg")
                        .HasMaxLength(500);

                    b.Property<string>("Title")
                        .HasMaxLength(150);

                    b.Property<string>("URL")
                        .HasMaxLength(1000);

                    b.HasKey("Id");

                    b.ToTable("LogoManufactory");
                });

            modelBuilder.Entity("DataLayer.Entities.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("ImageAddress")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<bool>("IsActive");

                    b.Property<int>("NewsGroupId");

                    b.Property<string>("SummeryNews")
                        .IsRequired()
                        .HasMaxLength(600);

                    b.Property<string>("Tags")
                        .HasMaxLength(400);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("UserId");

                    b.Property<int>("ViewCount");

                    b.HasKey("Id");

                    b.HasIndex("NewsGroupId");

                    b.HasIndex("UserId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("DataLayer.Entities.NewsComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("NewsId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("NewsId");

                    b.ToTable("NewsComment");
                });

            modelBuilder.Entity("DataLayer.Entities.NewsGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("NewsGroup");
                });

            modelBuilder.Entity("DataLayer.Entities.NewsTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("NewsTag");
                });

            modelBuilder.Entity("DataLayer.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateDate");

                    b.Property<int>("DisLike");

                    b.Property<string>("IndexPic");

                    b.Property<bool?>("IsActive");

                    b.Property<bool>("IsSpecialSell");

                    b.Property<int>("Like");

                    b.Property<long>("Price");

                    b.Property<int>("ProductGroupId");

                    b.Property<int?>("ProductUnitId");

                    b.Property<string>("ShortDescription")
                        .HasMaxLength(2000);

                    b.Property<string>("Tags");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<int>("Visit");

                    b.HasKey("Id");

                    b.HasIndex("ProductGroupId");

                    b.HasIndex("ProductUnitId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("DataLayer.Entities.ProductDiscount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Discount");

                    b.Property<int>("DiscountType");

                    b.Property<DateTime>("EndDate");

                    b.Property<int?>("ProductGroupId");

                    b.Property<int?>("ProductId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("ProductGroupId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductDiscount");
                });

            modelBuilder.Entity("DataLayer.Entities.ProductFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FeatureId");

                    b.Property<string>("FeatureValue");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductFeature");
                });

            modelBuilder.Entity("DataLayer.Entities.ProductGallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Pic");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Gallery");
                });

            modelBuilder.Entity("DataLayer.Entities.ProductGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ParentId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("ProductGroup");
                });

            modelBuilder.Entity("DataLayer.Entities.ProductGroupDependencies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConditionId");

                    b.Property<int>("Feature1");

                    b.Property<int>("Feature2");

                    b.Property<int>("GroupId1");

                    b.Property<int>("GroupId2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("Value1");

                    b.Property<string>("Value2");

                    b.HasKey("Id");

                    b.HasIndex("ConditionId");

                    b.ToTable("ProductGroupDependencies");
                });

            modelBuilder.Entity("DataLayer.Entities.ProductGroupFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FeatureId");

                    b.Property<int>("ProductGroupId");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId");

                    b.HasIndex("ProductGroupId");

                    b.ToTable("ProductGroupFeature");
                });

            modelBuilder.Entity("DataLayer.Entities.ProductPackage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("DisLike");

                    b.Property<long>("Discount");

                    b.Property<DateTime>("EndDiscount");

                    b.Property<string>("IndexPic");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsSpecialPackage");

                    b.Property<int>("Like");

                    b.Property<string>("ShortDiscription");

                    b.Property<DateTime>("StartDiscount");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.Property<int>("Visit");

                    b.HasKey("Id");

                    b.ToTable("ProductPackage");
                });

            modelBuilder.Entity("DataLayer.Entities.ProductPackageDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PackageId");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductPackageDetails");
                });

            modelBuilder.Entity("DataLayer.Entities.ProductPackageGallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("File");

                    b.Property<int>("PackageId");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.ToTable("ProductPackageGallery");
                });

            modelBuilder.Entity("DataLayer.Entities.ProductUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("ProductUnit");
                });

            modelBuilder.Entity("DataLayer.Entities.SlideShow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alt")
                        .HasMaxLength(200);

                    b.Property<string>("ImgAddress")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Title")
                        .HasMaxLength(100);

                    b.Property<string>("URL")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.ToTable("SlideShow");
                });

            modelBuilder.Entity("DataLayer.Entities.StoreRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(200);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("StoreRoom");
                });

            modelBuilder.Entity("DataLayer.Entities.Users.RoleClams", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("DataLayer.Entities.Users.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.Property<string>("RoleTitle");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("DataLayer.Entities.Users.UserClams", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("DataLayer.Entities.Users.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("DataLayer.Entities.Users.UserRoles", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("DataLayer.Entities.Users.UserTokens", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DataLayer.Entities.Users.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsModerator");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("ProfilePic");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DataLayer.Entities.Users.UsersAccess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Actions");

                    b.Property<string>("Controller");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("UsersAccess");
                });

            modelBuilder.Entity("DataLayer.Entities.Warehouse.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActivity");

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("Region");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("DataLayer.Entities.Warehouse.WarehouseProductCheck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Factor");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int>("ProductId");

                    b.Property<int>("TypeSSOt");

                    b.Property<int>("WarehouseId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("WarehouseProductCheck");
                });

            modelBuilder.Entity("DataLayer.Entities.FAQs.FAQ", b =>
                {
                    b.HasOne("DataLayer.Entities.FAQs.FaqGroup", "FaqGroup")
                        .WithMany()
                        .HasForeignKey("FaqGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.FactorItem", b =>
                {
                    b.HasOne("DataLayer.Entities.FactorAndPackage", "Factor")
                        .WithMany()
                        .HasForeignKey("FactorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.Favorites", b =>
                {
                    b.HasOne("DataLayer.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Entities.Users.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.FeatureItem", b =>
                {
                    b.HasOne("DataLayer.Entities.Feature", "Feature")
                        .WithMany("Features")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.News", b =>
                {
                    b.HasOne("DataLayer.Entities.NewsGroup", "NewsGroup")
                        .WithMany()
                        .HasForeignKey("NewsGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Entities.Users.Users", "Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.NewsComment", b =>
                {
                    b.HasOne("DataLayer.Entities.News", "News")
                        .WithMany()
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.Product", b =>
                {
                    b.HasOne("DataLayer.Entities.ProductGroup", "ProductGroup")
                        .WithMany("Products")
                        .HasForeignKey("ProductGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Entities.ProductUnit", "ProductUnit")
                        .WithMany()
                        .HasForeignKey("ProductUnitId");
                });

            modelBuilder.Entity("DataLayer.Entities.ProductDiscount", b =>
                {
                    b.HasOne("DataLayer.Entities.ProductGroup", "ProductGroup")
                        .WithMany()
                        .HasForeignKey("ProductGroupId");

                    b.HasOne("DataLayer.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("DataLayer.Entities.ProductFeature", b =>
                {
                    b.HasOne("DataLayer.Entities.Feature", "Feature")
                        .WithMany()
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Entities.Product", "Product")
                        .WithMany("ProductFeatures")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.ProductGallery", b =>
                {
                    b.HasOne("DataLayer.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.ProductGroup", b =>
                {
                    b.HasOne("DataLayer.Entities.ProductGroup", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("DataLayer.Entities.ProductGroupDependencies", b =>
                {
                    b.HasOne("DataLayer.Entities.Condition", "Condition")
                        .WithMany()
                        .HasForeignKey("ConditionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.ProductGroupFeature", b =>
                {
                    b.HasOne("DataLayer.Entities.Feature", "Feature")
                        .WithMany()
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Entities.ProductGroup", "ProductGroup")
                        .WithMany()
                        .HasForeignKey("ProductGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.ProductPackageDetails", b =>
                {
                    b.HasOne("DataLayer.Entities.ProductPackage", "ProductPackage")
                        .WithMany("ProductPackageDetails")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.ProductPackageGallery", b =>
                {
                    b.HasOne("DataLayer.Entities.ProductPackage", "ProductPackage")
                        .WithMany()
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.Users.RoleClams", b =>
                {
                    b.HasOne("DataLayer.Entities.Users.Roles")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.Users.UserClams", b =>
                {
                    b.HasOne("DataLayer.Entities.Users.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.Users.UserLogin", b =>
                {
                    b.HasOne("DataLayer.Entities.Users.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.Users.UserRoles", b =>
                {
                    b.HasOne("DataLayer.Entities.Users.Roles")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Entities.Users.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.Users.UserTokens", b =>
                {
                    b.HasOne("DataLayer.Entities.Users.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.Users.UsersAccess", b =>
                {
                    b.HasOne("DataLayer.Entities.Users.Roles", "Roles")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.Warehouse.WarehouseProductCheck", b =>
                {
                    b.HasOne("DataLayer.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Entities.Warehouse.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
