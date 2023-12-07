namespace Colors.API.Models;

public class ColorContrastRatio(Color color1, Color color2)
{
    public Color Color1 { get; } = color1;
    public Color Color2 { get; } = color2;
    public double Ratio { get; } = ColorServices.GetContrastRatio(color1, color2);
}