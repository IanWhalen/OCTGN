// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Register.xaml.cs" company="OCTGN">
//   GNU Stuff
// </copyright>
// <summary>
//   Interaction logic for Register.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Octgn.Launcher
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;

    using Skylabs.Lobby;

    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public partial class Register : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Register"/> class.
        /// </summary>
        public Register()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Fires when the register button is clicked.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnRegisterClick(object sender, RoutedEventArgs e)
        {
            this.lblErrors.Content = string.Empty;
            const string Pattern = @"^[a-zA-Z0-9.\-_]{2,30}$";
            if (!Regex.Match(tbUsername.Text, Pattern).Success)
            {
                lblErrors.Content = "Usernames must only contain letters, numbers and . - _";
                return;
            }

            if (tbPass1.Password == string.Empty)
            {
                lblErrors.Content = "Password cannot be empty.";
                return;
            }

            if (tbPass1.Password != tbPass2.Password || tbPass1.Password == string.Empty)
            {
                lblErrors.Content = "Passwords do not match";
                return;
            }

            progressBar1.Visibility = Visibility.Visible;
            Program.OctgnInstance.LobbyClient.OnRegisterComplete += this.LobbyClientOnOnRegisterComplete;
            Program.OctgnInstance.LobbyClient.BeginRegister(tbUsername.Text, tbPass1.Password, tbEmail.Text);
        }

        /// <summary>
        /// Fires when the lobby registration is complete.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="results">
        /// The results.
        /// </param>
        private void LobbyClientOnOnRegisterComplete(object sender, RegisterResults results)
        {
            Program.OctgnInstance.LobbyClient.OnRegisterComplete -= this.LobbyClientOnOnRegisterComplete;
            Dispatcher.Invoke(new Action(()=>
                {
                    progressBar1.Visibility = Visibility.Hidden;
                    switch (results)
                    {
                        case RegisterResults.ConnectionError:
                            lblErrors.Content = "There was a connection error. Please try again.";
                            break;
                        case RegisterResults.Success:
                            // TODO This message box needsta go
                            MessageBox.Show(
                                "Registration Success!", "Octgn", MessageBoxButton.OK, MessageBoxImage.Information);
                            var l = new Login();
                            l.textBox1.Text = Program.OctgnInstance.LobbyClient.Username;
                            l.passwordBox1.Password = Program.OctgnInstance.LobbyClient.Password;
                            if (NavigationService != null)
                            {
                                NavigationService.Navigate(l);
                            }

                            break;
                        case RegisterResults.UsernameTaken:
                            lblErrors.Content = "That username is already taken.";
                            break;
                        case RegisterResults.UsernameInvalid:
                            lblErrors.Content = "That username is invalid.";
                            break;
                        case RegisterResults.PasswordFailure:
                            lblErrors.Content = "That password is invalid.";
                            break;
                    }
                }));
        }

        /// <summary>
        /// Fires when the cancel button is clicked.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            Program.OctgnInstance.LobbyClient.OnRegisterComplete -= this.LobbyClientOnOnRegisterComplete;
            if (this.NavigationService != null)
            {
                NavigationService.Navigate(new Login());
            }
        }
    }
}
