using System.ComponentModel.DataAnnotations;

namespace MobileRecharge.Models
{
    public enum PlanType
    {
        PopularPlans,
        DataBooster,
        AnnualPlans,
        DataPacks,
        NoDailyLimit,
        TopUp
    }
    public class RechargePlans
    {
        [Key] public int Id { get; set; }
        public String PlanName { get; set; }
        public int Price { get; set; }
        public int DataBenfits { get; set; }
        public int CallBenifits { get; set; }
        public string OtherBenifits { get; set; }
        public int Validity { get; set; }
        public PlanType PlanType { get; set; }
    }
}
