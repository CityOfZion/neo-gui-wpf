﻿using System.Collections.Generic;
using System.Linq;

namespace Neo.UI.Core.Data
{
    internal class WalletInfo
    {
        private readonly IDictionary<UInt160, AccountItem> accounts;
        private readonly IList<AssetItem> assets;
        private readonly IList<TransactionItem> transactions;

        public WalletInfo()
        {
            this.accounts = new Dictionary<UInt160, AccountItem>();
            this.assets = new List<AssetItem>();
            this.transactions = new List<TransactionItem>();
        }

        public IEnumerable<AccountItem> GetAccounts()
        {
            return this.accounts.Values;
        }

        public bool ContainsAccount(UInt160 scriptHash)
        {
            return this.accounts.ContainsKey(scriptHash);
        }

        public AccountItem GetAccount(UInt160 scriptHash)
        {
            return this.ContainsAccount(scriptHash) ? this.accounts[scriptHash] : null;
        }

        public void AddAccount(AccountItem account)
        {
            this.accounts.Add(UInt160.Parse(account.ScriptHash), account);
        }

        public void RemoveAccount(UInt160 scriptHash)
        {
            this.accounts.Remove(scriptHash);
        }

        public IEnumerable<FirstClassAssetItem> GetFirstClassAssets()
        {
            return this.assets.Where(item => item is FirstClassAssetItem).Cast<FirstClassAssetItem>();
        }

        public IEnumerable<NEP5AssetItem> GetNEP5Assets()
        {
            return this.assets.Where(item => item is NEP5AssetItem).Cast<NEP5AssetItem>();
        }

        public FirstClassAssetItem GetFirstClassAsset(UInt256 assetId)
        {
            if (assetId == null) return null;

            var assetIdStr = assetId.ToString();

            return this.assets.FirstOrDefault(asset => asset is FirstClassAssetItem &&
                assetIdStr.Equals(((FirstClassAssetItem) asset).AssetId)) as FirstClassAssetItem;
        }

        public NEP5AssetItem GetNEP5Asset(UInt160 scriptHash)
        {
            if (scriptHash == null) return null;

            var scriptHashStr = scriptHash.ToString();

            return this.assets.FirstOrDefault(asset => asset is NEP5AssetItem &&
                scriptHashStr.Equals(((NEP5AssetItem) asset).ScriptHash)) as NEP5AssetItem;
        }

        public void AddAsset(AssetItem asset)
        {
            this.assets.Add(asset);
        }

        public void RemoveAsset(AssetItem asset)
        {
            this.assets.Remove(asset);
        }

        public void AddTransaction(TransactionItem transaction)
        {
            // Add transaction to beginning of list
            this.transactions.Insert(0, transaction);
        }

        public void UpdateTransactionConfirmations(uint blockHeight)
        {
            foreach (var transactionItem in this.transactions)
            {
                var confirmations = blockHeight - transactionItem.Height + 1;

                transactionItem.Confirmations = confirmations;
            }
        }

        public IEnumerable<TransactionItem> GetTransactions()
        {
            return this.transactions;
        }
    }
}
