﻿// <auto-generated />
using System;
using Blunderr.Core.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Blunderr.Core.Data.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230406164138_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Blunderr.Core.Data.Entities.FileItems.FileItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("FileItem");
                });

            modelBuilder.Entity("Blunderr.Core.Data.Entities.Projects.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Blunderr.Core.Data.Entities.Tickets.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("DeveloperId")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("SubmitterId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SubmitterId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Blunderr.Core.Data.Entities.Tickets.TicketAttachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FileItemId")
                        .HasColumnType("int");

                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FileItemId");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketAttachment");
                });

            modelBuilder.Entity("Blunderr.Core.Data.Entities.Tickets.TicketComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("SubmitterId")
                        .HasColumnType("int");

                    b.Property<int?>("TicketId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubmitterId");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketComment");
                });

            modelBuilder.Entity("Blunderr.Core.Data.Entities.Tickets.TicketCommentAttachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FileItemId")
                        .HasColumnType("int");

                    b.Property<int>("TicketCommentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FileItemId");

                    b.HasIndex("TicketCommentId");

                    b.ToTable("TicketCommentAttachment");
                });

            modelBuilder.Entity("Blunderr.Core.Data.Entities.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("Phone")
                        .HasColumnType("int");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Blunderr.Core.Data.Entities.Users.UserToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("Blunderr.Core.Data.Entities.Projects.Project", b =>
                {
                    b.HasOne("Blunderr.Core.Data.Entities.Users.User", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Blunderr.Core.Data.Entities.Tickets.Ticket", b =>
                {
                    b.HasOne("Blunderr.Core.Data.Entities.Users.User", "Developer")
                        .WithMany()
                        .HasForeignKey("DeveloperId");

                    b.HasOne("Blunderr.Core.Data.Entities.Projects.Project", "Project")
                        .WithMany("Tickets")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blunderr.Core.Data.Entities.Users.User", "Submitter")
                        .WithMany()
                        .HasForeignKey("SubmitterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developer");

                    b.Navigation("Project");

                    b.Navigation("Submitter");
                });

            modelBuilder.Entity("Blunderr.Core.Data.Entities.Tickets.TicketAttachment", b =>
                {
                    b.HasOne("Blunderr.Core.Data.Entities.FileItems.FileItem", "FileItem")
                        .WithMany()
                        .HasForeignKey("FileItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blunderr.Core.Data.Entities.Tickets.Ticket", "Ticket")
                        .WithMany("Attachments")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FileItem");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("Blunderr.Core.Data.Entities.Tickets.TicketComment", b =>
                {
                    b.HasOne("Blunderr.Core.Data.Entities.Users.User", "Submitter")
                        .WithMany()
                        .HasForeignKey("SubmitterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blunderr.Core.Data.Entities.Tickets.Ticket", null)
                        .WithMany("Comments")
                        .HasForeignKey("TicketId");

                    b.Navigation("Submitter");
                });

            modelBuilder.Entity("Blunderr.Core.Data.Entities.Tickets.TicketCommentAttachment", b =>
                {
                    b.HasOne("Blunderr.Core.Data.Entities.FileItems.FileItem", "FileItem")
                        .WithMany()
                        .HasForeignKey("FileItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blunderr.Core.Data.Entities.Tickets.TicketComment", "TicketComment")
                        .WithMany("Attachments")
                        .HasForeignKey("TicketCommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FileItem");

                    b.Navigation("TicketComment");
                });

            modelBuilder.Entity("Blunderr.Core.Data.Entities.Users.UserToken", b =>
                {
                    b.HasOne("Blunderr.Core.Data.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Blunderr.Core.Data.Entities.Projects.Project", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Blunderr.Core.Data.Entities.Tickets.Ticket", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Blunderr.Core.Data.Entities.Tickets.TicketComment", b =>
                {
                    b.Navigation("Attachments");
                });
#pragma warning restore 612, 618
        }
    }
}