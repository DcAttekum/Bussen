namespace Bussen.Services
{
    /// <summary>
    /// The alert service interface.
    /// </summary>
    public interface IAlertService
    {
        /// <summary>
        /// Show alert async to use with await, must be on dispatcher thread.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="cancel">The cancel text.</param>
        /// <returns>Success.</returns>
        Task ShowAlertAsync(string title, string message, string cancel = "OK");

        /// <summary>
        /// Show confirmation async to use with await, must be on dispatcher thread.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="accept">The accept text.</param>
        /// <param name="cancel">The cancel text.</param>
        /// <returns>Whether or not the confirmation was accepted.</returns>
        Task<bool> ShowConfirmationAsync(string title, string message, string accept = "Yes", string cancel = "No");

        /// <summary>
        /// Fire and forget call, method returns before showing the alert.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="cancel">The cancel text.</param>
        void ShowAlert(string title, string message, string cancel = "OK");

        /// <summary>
        /// Fire and forget call, method returns before showing the confirmation.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="callback">The action to perform afterwards.</param>
        /// <param name="accept">The accept text.</param>
        /// <param name="cancel">The cancel text.</param>
        void ShowConfirmation(string title, string message, Action<bool> callback,
                              string accept = "Yes", string cancel = "No");
    }

    /// <summary>
    /// The alert service.
    /// </summary>
    public class AlertService : IAlertService
    {
        /// <summary>
        /// Show alert async to use with await, must be on dispatcher thread.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="cancel">The cancel text.</param>
        /// <returns>Success.</returns>
        public Task ShowAlertAsync(string title, string message, string cancel = "OK")
        {
            return Application.Current!.Windows[0].Page!.DisplayAlertAsync(title, message, cancel);
        }

        /// <summary>
        /// Show confirmation async to use with await, must be on dispatcher thread.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="accept">The accept text.</param>
        /// <param name="cancel">The cancel text.</param>
        /// <returns>Whether or not the confirmation was accepted.</returns>
        public Task<bool> ShowConfirmationAsync(string title, string message, string accept = "Yes", string cancel = "No")
        {
            return Application.Current!.Windows[0].Page!.DisplayAlertAsync(title, message, accept, cancel);
        }

        /// <summary>
        /// Fire and forget call, method returns before showing the alert.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="cancel">The cancel text.</param>
        public void ShowAlert(string title, string message, string cancel = "OK")
        {
            Application.Current!.Windows[0].Dispatcher.Dispatch(async () =>
                await ShowAlertAsync(title, message, cancel)
            );
        }

        /// <summary>
        /// Fire and forget call, method returns before showing the confirmation.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="callback">The action to perform afterwards.</param>
        /// <param name="accept">The accept text.</param>
        /// <param name="cancel">The cancel text.</param>
        public void ShowConfirmation(string title, string message, Action<bool> callback,
                                     string accept = "Yes", string cancel = "No")
        {
            Application.Current!.Windows[0].Dispatcher.Dispatch(async () =>
            {
                bool answer = await ShowConfirmationAsync(title, message, accept, cancel);
                callback(answer);
            });
        }
    }
}
