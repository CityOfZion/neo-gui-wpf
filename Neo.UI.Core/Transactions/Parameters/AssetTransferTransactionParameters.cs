﻿using System.Collections.Generic;
using Neo.UI.Core.Data;

namespace Neo.UI.Core.Transactions.Parameters
{
    public class AssetTransferTransactionParameters : TransactionParameters
    {
        public IEnumerable<string> AccountScriptHashes { get; }

        public IEnumerable<TransactionOutputItem> TransactionOutputItems { get; }

        public string Remark { get; }

        public string TransferChangeAddress { get; }

        public string TransferFee { get; }

        public AssetTransferTransactionParameters(IEnumerable<string> accountScriptHashes, IEnumerable<TransactionOutputItem> transactionOutputItems, string transferChangeAddress, string remark, string transferFee)
        {
            this.AccountScriptHashes = accountScriptHashes;
            this.TransactionOutputItems = transactionOutputItems;
            this.TransferChangeAddress = transferChangeAddress;
            this.Remark = remark;
            this.TransferFee = transferFee;
        }
    }
}
