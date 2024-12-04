/*
    Program Author: Bibas Kandel

    USM ID:  W10170085

    Assignment: Programming Project Part 2- Password Manager Back End

    Description: Paraphrase : This program implements a secure passord manager that allows users to store, view, edit and manage their platform password with encryption.

*/
using CSC317PassManagerP2Starter.Modules.Controllers;

namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class LoginView : ContentPage
{
    public LoginView()
    {
        InitializeComponent();
    }

    private async void ProcessLogin(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Text)){
             ShowErrorMessage("Please fill all fields");
        }

        var loginStatus = App.LoginController.Authenticate(txtUserName.Text,txtPassword.Text);
        if (loginStatus == AuthenticationError.INVALIDUSERNAME){
             ShowErrorMessage("Invalid username");
        }
        else if (loginStatus == AuthenticationError.INVALIDPASSWORD){
             ShowErrorMessage("Invalid password");
        }
        else if(loginStatus == AuthenticationError.NONE){
            await Navigation.PushAsync(new PasswordListView());
        }
    }

    private async void ShowErrorMessage(string message)
    {
        await DisplayAlert("Login Error", message, "Ok");
    }
}