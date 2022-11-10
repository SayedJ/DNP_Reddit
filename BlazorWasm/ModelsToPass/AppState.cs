using Domain.Models;

namespace BlazorWasm.ModelsToPass;

public class AppState
{
    public static User User { get; set; }
    public static Post Post { get; set; }
    public static Comment Comment { get; set; }
    public static Vote Vote { get; set; }
}