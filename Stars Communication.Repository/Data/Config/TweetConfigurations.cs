using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stars_Communication.Core.Models;

namespace Stars_Communication.Repository.Data.Config
{
	public class TweetConfigurations : IEntityTypeConfiguration<Tweet>
	{
		public void Configure(EntityTypeBuilder<Tweet> builder)
		{

			builder.HasOne(t => t.User)
				.WithMany(u => u.Tweets)
				.HasForeignKey(t => t.UserId);


			builder.Property(u => u.CreatedAt)
				.HasDefaultValueSql("Now()");

		}
	}
}
