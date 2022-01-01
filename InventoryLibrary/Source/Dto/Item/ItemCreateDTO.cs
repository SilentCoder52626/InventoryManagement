using InventoryLibrary.Source.Entity;

namespace InventoryLibrary.Source.DTO.Item
{
    public class ItemCreateDTO
    {

        public long ItemId { get; set; }

        public string ItemName { get; set; }

        public long Price { get; set; }
        public Unit Unit { get; set; }
    }
}
