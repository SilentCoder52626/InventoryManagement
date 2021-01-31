﻿using System;

namespace InventoryLibrary.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(string message = "Customer not found!") : base(message)
        {

        }
    }
}
