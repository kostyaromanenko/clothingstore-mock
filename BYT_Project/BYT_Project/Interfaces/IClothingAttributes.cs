namespace BYT_Project.Interfaces;

public interface IClothing
{
    string Size { get; set; }
    string Color { get; set; }
    string Fabric { get; set; }

    void DisplayClothingAttributes();
}