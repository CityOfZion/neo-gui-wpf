﻿namespace Neo.Gui.Base.Services.Interfaces
{
    public interface INotificationService
    {
        void ShowSuccessNotification(string message);

        void ShowErrorNotification(string message);

        void ShowWarningNotification(string message);

        void ShowInformationNotification(string message);
    }
}