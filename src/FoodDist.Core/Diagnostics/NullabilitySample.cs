#nullable enable
namespace FoodDist.Core.Diagnostics;

public class NullabilitySample
{
    public string GetValue()
    {
        string value = null;
        return value;
    }
}
