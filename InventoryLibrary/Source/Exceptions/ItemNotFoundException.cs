using System;

namespace InventoryLibrary.Exceptions
{
    class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message = "Item Has not been Found!") : base(message)
        {

        }
    }
}
