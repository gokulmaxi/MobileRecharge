using MobileRecharge.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace MobileRecharge.Models
{
    public enum ResolvedStatus
    {
        Yes,
        No
    }
    public class ContactForm
    {
        [Key]
        public int ContactId { get; set; }
        public string Subject { get; set; }
        public string  Message { get; set; }
        public ResolvedStatus Status { get; set; } = ResolvedStatus.No;
        public MobileRechargeUser User { get; set; }
        public string AdminComment { get; set; } = "";
    }
}
