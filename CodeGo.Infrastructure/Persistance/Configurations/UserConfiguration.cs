
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.Enums;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGo.Infrastructure.Persistance.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUserTable(builder);
        ConfigureCourseIdsTable(builder);
        ConfigureFriendIdsTable(builder);
        ConfigureBlockedIdsTable(builder);
        ConfigureFriendshipRequestTable(builder);
    }

    private static void ConfigureFriendshipRequestTable(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(u => u.FriendshipRequests, frb =>
        {
            frb.ToTable("friendshipRequests");
            frb.WithOwner().HasForeignKey("UserId");
            frb.HasKey("Id", "UserId");
            frb.Property(fr => fr.Id)
                .HasColumnName("FriendshipRequestId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => FriendshipRequestId.Create(value));
            frb.Property(fr => fr.RequesterId)
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value)
                );
            frb.Property(fr => fr.RequesterEmail)
                .HasMaxLength(254);
            frb.Property(fr => fr.RequesterPhoto);
            frb.Property(fr => fr.Message)
                .HasMaxLength(500);
            frb.Property(fr => fr.Status)
                .HasConversion(
                    status => status.Value,
                    value => FriendshipRequestStatus.FromValue(value));
        });
    }

    private static void ConfigureBlockedIdsTable(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(u => u.BlockedUserIds, bib =>
        {
            bib.ToTable("userBlockedIds");
            bib.WithOwner().HasForeignKey("UserId");
            bib.HasKey("Id");
            bib.Property(fi => fi.Value)
                .HasColumnName("BlockedId")
                .ValueGeneratedNever();
        });
        builder.Metadata.FindNavigation(nameof(User.BlockedUserIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureFriendIdsTable(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(u => u.FriendIds, fib =>
        {
            fib.ToTable("userFriendIds");
            fib.WithOwner().HasForeignKey("ReceiverId");
            fib.Property(fi => fi.Value)
                .HasColumnName("RequesterId")
                .ValueGeneratedNever();
        });
        builder.Metadata.FindNavigation(nameof(User.FriendIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureCourseIdsTable(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(u => u.CourseIds, cib =>
        {
            cib.ToTable("userCourseIds");
            cib.WithOwner().HasForeignKey("UserId");
            cib.HasKey("Id");
            cib.Property(ci => ci.Value)
                .HasColumnName("CourseId")
                .ValueGeneratedNever();
        });
        builder.Metadata.FindNavigation(nameof(User.CourseIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureUserTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value)
            );
        builder.OwnsOne(u => u.Points);
        builder.OwnsOne(u => u.DayStreak);
        builder.Property(u => u.FirstName)
            .HasMaxLength(50);
        builder.Property(u => u.LastName)
            .HasMaxLength(50);
        builder.Property(u => u.Password)
            .HasMaxLength(80);
        builder.Property(u => u.Email)
            .HasMaxLength(254);
        builder.Property(u => u.Visibility)
            .HasConversion(
                visibility => visibility.Value,
                value => ProfileVisibility.FromValue(value));
        builder.Property(u => u.Role)
            .HasConversion(
                visibility => visibility.Value,
                value => UserRole.FromValue(value));
        builder.Property(u => u.ProfilePicture);
        builder.Property(u => u.Bio)
            .HasMaxLength(500);
    }
}
