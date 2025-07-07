using SQLite;

[Table("ShoppingItems")]
public class ShoppingItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; }
    public bool IsBought { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Category { get; set; } = "Inne";
}