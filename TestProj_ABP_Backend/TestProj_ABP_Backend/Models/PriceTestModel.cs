using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestProj_ABP_Backend.AB_Tests;

namespace TestProj_ABP_Backend.Models;
public class PriceTestModel
{
    public PriceTestModel() { }

    [ForeignKey("User")]
    public Guid Id { get; set; }

    public string DeviceToken { get; set; }

    [Required]
    public PriceTest.PriceTestEnum Group { get; set; }
    public virtual User User { get; set; }
}
