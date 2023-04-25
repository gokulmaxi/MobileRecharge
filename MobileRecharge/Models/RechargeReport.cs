using MobileRecharge.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace MobileRecharge.Models
{
    public class RechargeReport
    {
        [Key]
        public int RechargeId { get; set; }
        public MobileRechargeUser User { get; set; }
        public RechargePlans Plan { get; set; }
        public DateTime RechargedDate { get; set; }
    }
}
