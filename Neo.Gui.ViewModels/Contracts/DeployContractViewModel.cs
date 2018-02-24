﻿using System;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Neo.Gui.Dialogs.Interfaces;
using Neo.Gui.Dialogs.LoadParameters.Contracts;
using Neo.UI.Core.Services.Interfaces;
using Neo.UI.Core.Transactions.Parameters;
using Neo.UI.Core.Wallet;

namespace Neo.Gui.ViewModels.Contracts
{
    public class DeployContractViewModel : 
        ViewModelBase, 
        IDialogViewModel<DeployContractLoadParameters>
    {
        #region Private Fields 
        private readonly IDialogManager dialogManager;
        private readonly IFileManager fileManager;
        private readonly IFileDialogService fileDialogService;
        private readonly IWalletController walletController;

        private string name;
        private string version;
        private string author;
        private string email;
        private string description;
        private string parameterList;
        private string returnTypeStr;

        private string script;
        private bool needsStorage;
        #endregion

        #region Public Properties 
        public string Name
        {
            get => this.name;
            set
            {
                if (this.name == value) return;

                this.name = value;

                RaisePropertyChanged();

                // Update dependent property
                RaisePropertyChanged(nameof(this.DeployEnabled));
            }
        }

        public string Version
        {
            get => this.version;
            set
            {
                if (this.version == value) return;

                this.version = value;

                RaisePropertyChanged();

                // Update dependent property
                RaisePropertyChanged(nameof(this.DeployEnabled));
            }
        }

        public string Author
        {
            get => this.author;
            set
            {
                if (this.author == value) return;

                this.author = value;

                RaisePropertyChanged();

                // Update dependent property
                RaisePropertyChanged(nameof(this.DeployEnabled));
            }
        }

        public string Email
        {
            get => this.email;
            set
            {
                if (this.email == value) return;

                this.email = value;

                RaisePropertyChanged();

                // Update dependent property
                RaisePropertyChanged(nameof(this.DeployEnabled));
            }
        }

        public string Description
        {
            get => this.description;
            set
            {
                if (this.description == value) return;

                this.description = value;

                RaisePropertyChanged();

                // Update dependent property
                RaisePropertyChanged(nameof(this.DeployEnabled));
            }
        }

        public string ParameterList
        {
            get => this.parameterList;
            set
            {
                if (this.parameterList == value) return;

                this.parameterList = value;

                RaisePropertyChanged();

                // Update dependent property
                RaisePropertyChanged(nameof(this.DeployEnabled));
            }
        }

        public string ReturnType
        {
            get => this.returnTypeStr;
            set
            {
                if (this.returnTypeStr == value) return;

                this.returnTypeStr = value;

                RaisePropertyChanged();

                // Update dependent property
                RaisePropertyChanged(nameof(this.DeployEnabled));
            }
        }

        public string Script
        {
            get => this.script;
            set
            {
                if (this.script == value) return;

                this.script = value;

                RaisePropertyChanged();

                // Update dependent properties
                RaisePropertyChanged(nameof(this.ScriptHash));
                RaisePropertyChanged(nameof(this.DeployEnabled));
            }
        }

        public string ScriptHash
        {
            get
            {
                if (string.IsNullOrEmpty(this.Script)) return string.Empty;

                try
                {
                    var scriptBytes = this.Script.HexToBytes();

                    return this.walletController.BytesToScriptHash(scriptBytes);
                }
                catch (FormatException)
                {
                    return string.Empty;
                }
            }
        }

        public bool NeedsStorage
        {
            get => this.needsStorage;
            set
            {
                if (this.needsStorage == value) return;

                this.needsStorage = value;

                RaisePropertyChanged();
            }
        }

        public bool DeployEnabled =>
            !string.IsNullOrEmpty(this.Name) &&
            !string.IsNullOrEmpty(this.Version) &&
            !string.IsNullOrEmpty(this.Author) &&
            !string.IsNullOrEmpty(this.Email) &&
            !string.IsNullOrEmpty(this.Description) &&
            !string.IsNullOrEmpty(this.Script);

        public RelayCommand LoadCommand => new RelayCommand(this.HandleLoadCommand);

        public RelayCommand DeployCommand => new RelayCommand(this.HandleDeployCommand);

        public RelayCommand CancelCommand => new RelayCommand(() => this.Close(this, EventArgs.Empty));
        #endregion

        #region Constructor 
        public DeployContractViewModel(
            IDialogManager dialogManager,
            IFileManager fileManager,
            IFileDialogService fileDialogService,
            IWalletController walletController)
        {
            this.dialogManager = dialogManager;
            this.fileManager = fileManager;
            this.fileDialogService = fileDialogService;
            this.walletController = walletController;
        }
        #endregion

        #region IDialogViewModel implementation 
        public event EventHandler Close;

        public void OnDialogLoad(DeployContractLoadParameters parameters)
        {
        }
        #endregion

        #region Private Methods 
        private void HandleLoadCommand()
        {
            var filePath = this.fileDialogService.OpenFileDialog("AVM File|*.avm", "avm");

            if (string.IsNullOrEmpty(filePath)) return;

            byte[] loadedBytes;
            try
            {
                loadedBytes = this.fileManager.ReadAllBytes(filePath);
            }
            catch
            {
                // TODO Show error message
                return;
            }

            var hexString = string.Empty;

            if (loadedBytes != null)
            {
                hexString = loadedBytes.ToHexString();
            }

            this.Script = hexString;
        }

        private void HandleDeployCommand()
        {
            var transactionParameters = new DeployContractTransactionParameters(
                this.Script,
                this.parameterList,
                this.ReturnType,
                this.NeedsStorage,
                this.Name,
                this.Version,
                this.Author,
                this.Email,
                this.Description);

            this.walletController.BuildSignAndRelayTransaction(transactionParameters);

            this.Close(this, EventArgs.Empty);
        }
        #endregion
    }
}