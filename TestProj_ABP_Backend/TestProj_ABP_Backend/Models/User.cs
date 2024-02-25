using System.ComponentModel.DataAnnotations;

namespace TestProj_ABP_Backend.Models;

public class User
{
    public User() { }

    [Key]
    public Guid UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string DeviceToken { get; set; }

    public virtual ColorTestModel ColorExperiment { get; set; }

    public virtual BrowserFingerprint BrowserFingerprint { get; set; }

    [Timestamp]
    public byte[] Version { get; set; }
}

