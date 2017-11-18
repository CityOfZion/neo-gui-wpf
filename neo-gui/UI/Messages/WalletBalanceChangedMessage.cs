﻿namespace Neo.UI.Messages
{
    public class WalletBalanceChangedMessage
    {
        public bool BalanceChanged { get; private set; }

        public WalletBalanceChangedMessage(bool balanceChanged)
        {
            this.BalanceChanged = balanceChanged;
        }
    }
}