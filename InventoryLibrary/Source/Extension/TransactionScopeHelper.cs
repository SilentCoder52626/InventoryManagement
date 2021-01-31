using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace InventoryLibrary.Source.Extension
{
    public class TransactionScopeHelper
    {
        public static TransactionScope GetInstance()
        {
            return new TransactionScope(
            TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
