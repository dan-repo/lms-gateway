using LmsGateway.Domain.Medias;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Data.Mappings.Medias
{
    public class MediaMap
    {
        public MediaMap(EntityTypeBuilder<Media> entityBuilder)
        {
            entityBuilder.ToTable("MEDIA");

            entityBuilder.HasKey(x => x.Id);
            
            entityBuilder.Property(x => x.Height);
            entityBuilder.Property(x => x.Width);
            entityBuilder.Property(u => u.MimeType).HasColumnName("Mime_Type").HasMaxLength(20).IsRequired();
            entityBuilder.Property(x => x.Url).HasMaxLength(500);


            //    public byte[] File { get; set; }
            //public int Height { get; set; }
            //public int Width { get; set; }
            //public string MimeType { get; set; }
            //public string Url { get; set; }


        }

    }
}
