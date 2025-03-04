﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WayMatcherBL.Models;

public partial class WayMatcherContext : DbContext
{
    public WayMatcherContext(DbContextOptions<WayMatcherContext> options) : base(options)
    {

    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Audit> Audits { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<ConfirmationStatus> ConfirmationStatuses { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventMember> EventMembers { get; set; }

    public virtual DbSet<EventMemberType> EventMemberTypes { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Invite> Invites { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Stop> Stops { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleMapping> VehicleMappings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("WayMatcher");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Address__03BDEBDABB144E85");

            entity.ToTable("Address");

            entity.Property(e => e.AddressId).HasColumnName("Address_ID");
            entity.Property(e => e.AddressLine1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Address_Line1");
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Address_Line2");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CountryCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Country_Code");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Postal_Code");
            entity.Property(e => e.Region)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Status).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Address__Status___3A6CA48E");
        });

        modelBuilder.Entity<Audit>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK__Audit__EDBEC75952CBFBED");

            entity.ToTable("Audit");

            entity.Property(e => e.AuditId).HasColumnName("Audit_ID");
            entity.Property(e => e.EntityId).HasColumnName("Entity_ID");
            entity.Property(e => e.EntityType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Entity_Type");
            entity.Property(e => e.Message).IsUnicode(false);
            entity.Property(e => e.Timestamp).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Audits)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Audit__User_ID__71BCD978");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.ChatMessageId).HasName("PK__Chat_Mes__A4F8A363F0F4AA1A");

            entity.ToTable("Chat_Message");

            entity.Property(e => e.ChatMessageId).HasColumnName("Chat_Message_ID");
            entity.Property(e => e.EventId).HasColumnName("Event_ID");
            entity.Property(e => e.Message).IsUnicode(false);
            entity.Property(e => e.Timestamp).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.Event).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Chat_Mess__Event__5F9E293D");

            entity.HasOne(d => d.User).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Chat_Mess__User___5EAA0504");
        });

        modelBuilder.Entity<ConfirmationStatus>(entity =>
        {
            entity.HasKey(e => e.ConfirmationStatusId).HasName("PK__Confirma__1B10A687CBE8A23D");

            entity.ToTable("ConfirmationStatus");

            entity.Property(e => e.ConfirmationStatusId).HasColumnName("ConfirmationStatus_ID");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Event__FD6BEFE45FB2E0F4");

            entity.ToTable("Event");

            entity.Property(e => e.EventId).HasColumnName("Event_ID");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.EventTypeId).HasColumnName("EventType_ID");
            entity.Property(e => e.FreeSeats).HasColumnName("Free_Seats");
            entity.Property(e => e.ScheduleId).HasColumnName("Schedule_ID");
            entity.Property(e => e.StartTimestamp).HasColumnName("Start_Timestamp");
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");

            entity.HasOne(d => d.EventType).WithMany(p => p.Events)
                .HasForeignKey(d => d.EventTypeId)
                .HasConstraintName("FK__Event__EventType__515009E6");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Events)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK__Event__Schedule___52442E1F");

            entity.HasOne(d => d.Status).WithMany(p => p.Events)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Event__Status_ID__53385258");
        });

        modelBuilder.Entity<EventMember>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Event_Me__42A68F27D735EAFA");

            entity.ToTable("Event_Member");

            entity.Property(e => e.MemberId).HasColumnName("Member_ID");
            entity.Property(e => e.EventId).HasColumnName("Event_ID");
            entity.Property(e => e.EventMemberTypeId).HasColumnName("EventMemberType_ID");
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.Event).WithMany(p => p.EventMembers)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Event_Mem__Event__59E54FE7");

            entity.HasOne(d => d.EventMemberType).WithMany(p => p.EventMembers)
                .HasForeignKey(d => d.EventMemberTypeId)
                .HasConstraintName("FK__Event_Mem__Event__57FD0775");

            entity.HasOne(d => d.Status).WithMany(p => p.EventMembers)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Event_Mem__Statu__5AD97420");

            entity.HasOne(d => d.User).WithMany(p => p.EventMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Event_Mem__User___58F12BAE");
        });

        modelBuilder.Entity<EventMemberType>(entity =>
        {
            entity.HasKey(e => e.EventMemberTypeId).HasName("PK__EventMem__08F37EBA482F4895");

            entity.ToTable("EventMemberType");

            entity.Property(e => e.EventMemberTypeId).HasColumnName("EventMemberType_ID");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("PK__EventTyp__CCBAA9DCDB5AF138");

            entity.ToTable("EventType");

            entity.Property(e => e.EventTypeId).HasColumnName("EventType_ID");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Invite>(entity =>
        {
            entity.HasKey(e => e.InviteId).HasName("PK__Invite__D280FE625E146EA2");

            entity.ToTable("Invite");

            entity.Property(e => e.InviteId).HasColumnName("Invite_ID");
            entity.Property(e => e.ConfirmationStatusId).HasColumnName("ConfirmationStatus_ID");
            entity.Property(e => e.EventId).HasColumnName("Event_ID");
            entity.Property(e => e.IsRequest).HasColumnName("Is_Request");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.ConfirmationStatus).WithMany(p => p.Invites)
                .HasForeignKey(d => d.ConfirmationStatusId)
                .HasConstraintName("FK__Invite__Confirma__6462DE5A");

            entity.HasOne(d => d.Event).WithMany(p => p.Invites)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Invite__Event_ID__65570293");

            entity.HasOne(d => d.User).WithMany(p => p.Invites)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Invite__User_ID__664B26CC");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__8C1160B51877FA08");

            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId).HasColumnName("Notification_ID");
            entity.Property(e => e.EntityId).HasColumnName("Entity_ID");
            entity.Property(e => e.EntityType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Entity_Type");
            entity.Property(e => e.Message).IsUnicode(false);
            entity.Property(e => e.Read).HasDefaultValue(false);
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__User___6A1BB7B0");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__Rating__BE48C8257A812707");

            entity.ToTable("Rating");

            entity.Property(e => e.RatingId).HasColumnName("Rating_ID");
            entity.Property(e => e.RatedUserId).HasColumnName("Rated_user_ID");
            entity.Property(e => e.RatingText)
                .IsUnicode(false)
                .HasColumnName("Rating_Text");
            entity.Property(e => e.RatingValue).HasColumnName("Rating_Value");
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.UserWhoRatedId).HasColumnName("User_who_rated_ID");

            entity.HasOne(d => d.RatedUser).WithMany(p => p.RatingRatedUsers)
                .HasForeignKey(d => d.RatedUserId)
                .HasConstraintName("FK__Rating__Rated_us__758D6A5C");

            entity.HasOne(d => d.Status).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Rating__Status_I__7775B2CE");

            entity.HasOne(d => d.UserWhoRated).WithMany(p => p.RatingUserWhoRateds)
                .HasForeignKey(d => d.UserWhoRatedId)
                .HasConstraintName("FK__Rating__User_who__76818E95");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__D80AB49BF51B3764");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("Role_ID");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Schedule__8C4D3BBBDDB8A6FA");

            entity.ToTable("Schedule");

            entity.Property(e => e.ScheduleId).HasColumnName("Schedule_ID");
            entity.Property(e => e.CronSchedule)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Cron_Schedule");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Schedule__User_I__4C8B54C9");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__519009AC75A66F34");

            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.StatusDescription)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Status_Description");
        });

        modelBuilder.Entity<Stop>(entity =>
        {
            entity.HasKey(e => e.StopId).HasName("PK__Stop__B509D9FA9AB4CD1C");

            entity.ToTable("Stop");

            entity.Property(e => e.StopId).HasColumnName("Stop_ID");
            entity.Property(e => e.AddressId).HasColumnName("Address_ID");
            entity.Property(e => e.EventId).HasColumnName("Event_ID");
            entity.Property(e => e.StopSequenceNumber).HasColumnName("Stop_sequence_number");

            entity.HasOne(d => d.Address).WithMany(p => p.Stops)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK__Stop__Address_ID__6CF8245B");

            entity.HasOne(d => d.Event).WithMany(p => p.Stops)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Stop__Event_ID__6DEC4894");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__206D9190FC60A966");

            entity.ToTable("User");

            entity.HasIndex(e => e.Username, "UQ__User__536C85E44C860875").IsUnique();

            entity.HasIndex(e => e.EMail, "UQ__User__F692CF072516A299").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("User_ID");
            entity.Property(e => e.AdditionalDescription)
                .IsUnicode(false)
                .HasColumnName("Additional_Description");
            entity.Property(e => e.AddressId).HasColumnName("Address_ID");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.EMail)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("E-Mail");
            entity.Property(e => e.Firstname)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LicenseVerified).HasColumnName("License_Verified");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePicture).HasColumnName("Profile_Picture");
            entity.Property(e => e.RoleId).HasColumnName("Role_ID");
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.Telephone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Address).WithMany(p => p.Users)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK__User__Address_ID__40257DE4");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__User__Role_ID__4119A21D");

            entity.HasOne(d => d.Status).WithMany(p => p.Users)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__User__Status_ID__420DC656");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicle__CE6D7CB5F6EC6532");

            entity.ToTable("Vehicle");

            entity.Property(e => e.VehicleId).HasColumnName("Vehicle_ID");
            entity.Property(e => e.ManufacturerName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Manufacturer_Name");
            entity.Property(e => e.Model)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.YearOfManufacture).HasColumnName("Year_of_manufacture");

            entity.HasOne(d => d.Status).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Vehicle__Status___44EA3301");
        });

        modelBuilder.Entity<VehicleMapping>(entity =>
        {
            entity.HasKey(e => e.VehicleMappingId).HasName("PK__Vehicle___34B2F5A761341A8F");

            entity.ToTable("Vehicle_Mapping");

            entity.Property(e => e.VehicleMappingId).HasColumnName("Vehicle_Mapping_ID");
            entity.Property(e => e.AdditionalInfo)
                .IsUnicode(false)
                .HasColumnName("Additional_Info");
            entity.Property(e => e.FuelMilage)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("Fuel_Milage");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("License_Plate");
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
            entity.Property(e => e.VehicleId).HasColumnName("Vehicle_ID");

            entity.HasOne(d => d.Status).WithMany(p => p.VehicleMappings)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Vehicle_M__Statu__49AEE81E");

            entity.HasOne(d => d.User).WithMany(p => p.VehicleMappings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Vehicle_M__User___48BAC3E5");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleMappings)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__Vehicle_M__Vehic__47C69FAC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}