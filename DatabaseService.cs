using SQLite;
using System.Collections.ObjectModel;

public class DatabaseService
{
    private SQLiteAsyncConnection _db;

    public async Task Init()
    {
        if (_db != null) return;

        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "shopping.db");
        _db = new SQLiteAsyncConnection(databasePath);
        await _db.CreateTableAsync<ShoppingItem>();
    }

    // CRUD Operations
    public async Task AddItem(ShoppingItem item)
    {
        await Init();
        await _db.InsertAsync(item);
    }

    public async Task<List<ShoppingItem>> GetItems()
    {
        await Init();
        return await _db.Table<ShoppingItem>().ToListAsync();
    }

    public async Task UpdateItem(ShoppingItem item)
    {
        await Init();
        await _db.UpdateAsync(item);
    }

    public async Task DeleteItem(int id)
    {
        await Init();
        await _db.DeleteAsync<ShoppingItem>(id);
    }
    public async Task ClearAllItems()
    {
        await Init();
        await _db.DeleteAllAsync<ShoppingItem>();
    }
}