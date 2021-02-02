using InventoryLibrary.Source.Entity;

namespace InventoryLibrary.Source.DTO.Item
{
    public class ItemUpdateDTO
    {

        public long ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Unit Unit { get; set; }
    }
}
