using System.ComponentModel.DataAnnotations.Schema;

namespace SimbirsfotStaging10.DAL.Entities
{
    [Table("CardPlatforms")]
    public class CardPlatformItem
    {
        public int Id { get; set; }

        public int CardId { get; set; }

        public int PlatformId { get; set; }

        [ForeignKey("CardId")]
        public Card Card { get; set; }

        public Platform Platform { get; set; }
    }
}
