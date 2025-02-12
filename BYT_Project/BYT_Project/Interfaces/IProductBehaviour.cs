namespace BYT_Project.Interfaces;

public interface IProduct
{
    string Name { get; set; }
    float Price { get; set; }
    void DisplayProductDetails();
}