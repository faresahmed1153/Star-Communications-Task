using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Star_Communications.Core.Models;

namespace Star_Communications.Repository.Data.Config
{
	public class UserFollowConfigurations : IEntityTypeConfiguration<UserFollow>
	{
		public void Configure(EntityTypeBuilder<UserFollow> builder)
		{
			builder.HasKey(uf => new { uf.FollowerId, uf.FollowingId });

			builder.HasOne(uf => uf.Following)
				.WithMany(u => u.Followers)
				.HasForeignKey(uf => uf.FollowingId);


			builder.HasOne(uf => uf.Follower)
				.WithMany(u => u.Followings)
				.HasForeignKey(uf => uf.FollowerId);


		}
	}


}
