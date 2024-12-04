/*
    Program Author: Bibas Kandel

    USM ID:  W10170085

    Assignment: Programming Project Part 2- Password Manager Back End

    Description: Paraphrase : This program implements a secure passord manager that allows users to store, view, edit and manage their platform password with encryption.

*/
namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class AddPasswordView : ContentPage
{
    Color baseColorHighlight;
    bool generatedPassword;

    public AddPasswordView()
    {
        InitializeComponent();
        //Stores the original color of the text boxes. Used to revert a text box back
        //to its original color if it was "highlighted" during input validation.
        baseColorHighlight = (Color)Application.Current.Resources["ErrorEntryHighlightBG"];
        
        //Determines if a password was generated at least once.
        generatedPassword = false;
    }

    private void ButtonCancel(object sender, EventArgs e)
    {
       Navigation.PopAsync();
    }

    private async void ButtonSubmitExisting(object sender, EventArgs e)
    {
       //Called when the Submit button is clicked for a password manually
       //entered.  (i.e., existing password).
       if (string.IsNullOrWhiteSpace(txtNewUserName.Text)||
            string.IsNullOrWhiteSpace(txtNewPlatform.Text)||
            string.IsNullOrWhiteSpace(txtNewPassword.Text)||
            string.IsNullOrWhiteSpace(txtNewPasswordVerify.Text)||
            txtNewPassword.Text != txtNewPasswordVerify.Text )
            {
                await DisplayAlert("Error", "Please ensure all fields are filled and passwod match!", "Ok");
                return;
            }
        App.PasswordController.AddPassword(txtNewPlatform.Text, txtNewUserName.Text, txtNewPassword.Text);
        Navigation.PopAsync();
    }

    private async void ButtonSubmitGenerated(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNewUserName.Text)||
            string.IsNullOrWhiteSpace(txtNewPlatform.Text)||
            !generatedPassword)
            {
                await DisplayAlert("Error", "Please ensure all fields are filled and passwod match!", "Ok");
                return;
            }

        App.PasswordController.AddPassword(txtNewPlatform.Text, txtNewUserName.Text, lblGenPassword.Text);
        Navigation.PopAsync();
        
    }

    private void ButtonGeneratePassword(object sender, EventArgs e)
    {
        bool upper = chkUpperLetter.IsChecked;
        bool digit = chkDigit.IsChecked;
        string symbol = txtReqSymbols.Text;
        int minlength = Convert.ToInt32(minlen.Text);
       lblGenPassword.Text = PasswordGeneration.BuildPassword(upper, digit, symbol, minlength);   
       generatedPassword = true;
    }
}