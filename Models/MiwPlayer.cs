using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiwBotApi.Models
{
    public class MiwPlayer
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MiwPlayerID { get; set; }
        public string UUID { get; set; }
        public string Username { get; set; }

        public int Wins { get; set; }
        public int Kills { get; set; }
        public int Finals { get; set; }
        public int WitherDamage { get; set; }
        public int WitherKills { get; set; }
        public int Deaths { get; set; }
        public int ArrowsHit { get; set; }
        public int ArrowsShot { get; set; }

        public decimal KD { get; set; }
        public decimal FKD { get; set; }
        public decimal TKD { get; set; }
        public decimal WDD { get; set; }
        public decimal WKD { get; set; }
        public decimal AA { get; set; }
        public decimal RATE { get; set; }

        public decimal KPW { get; set; }
        public decimal FPW { get; set; }
        public decimal TKPW { get; set; }
        public decimal WDPW { get; set; }
        public decimal WKPW { get; set; }
        public decimal DPW { get; set; }
        public decimal SPW { get; set; }

        public int RankWins { get; set; }
        public int RankKills { get; set; }
        public int RankFinals { get; set; }
        public int RankWitherDamage { get; set; }
        public int RankWitherKills { get; set; }
        public int RankDeaths { get; set; }
        public int RankArrowsHit { get; set; }
        public int RankArrowsShot { get; set; }

        public int RankKD { get; set; }
        public int RankFKD { get; set; }
        public int RankTKD { get; set; }
        public int RankWDD { get; set; }
        public int RankWKD { get; set; }
        public int RankAA { get; set; }
        public int RankRATE { get; set; }

        public int RankKPW { get; set; }
        public int RankFPW { get; set; }
        public int RankTKPW { get; set; }
        public int RankWDPW { get; set; }
        public int RankWKPW { get; set; }
        public int RankDPW { get; set; }
        public int RankSPW { get; set; }
    }
}
