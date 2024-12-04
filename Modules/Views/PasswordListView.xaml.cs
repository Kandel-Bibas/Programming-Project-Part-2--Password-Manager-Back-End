/*
    Program Author: Bibas Kandel

    USM ID:  W10170085

    Assignment: Programming Project Part 2- Password Manager Back End

    Description: Paraphrase : This program implements a secure passord manager that allows users to store, view, edit and manage their platform password with encryption.

*/
using System.Collections.ObjectModel;
using CSC317PassManagerP2Starter.Modules.Models;
using Security;

namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class PasswordListView : ContentPage
{
    private ObservableCollection<PasswordRow> _rows = new ObservableCollection<PasswordRow>();

    public PasswordListView()
    {
        InitializeComponent();

        //once logged in, generate a set of test passwords for the user.
        App.PasswordController.GenTestPasswords();

        //Populates the list of shown passwords  This should also be called in the search
        //bar event method to implement the search filter.
        App.PasswordController.PopulatePasswordView(_rows);

        //Binds the Collection View to the password rows.
        collPasswords.ItemsSource = _rows;
    }

    private void RefreshPasswordlist(){
        App.PasswordController.PopulatePasswordView(_rows);
    }

    private void TextSearchBar(object sender, TextChangedEventArgs e)
    {
       App.PasswordController.PopulatePasswordView(_rows, e.NewTextValue);
    }

    private void CopyPassToClipboard(object sender, EventArgs e)
    {
        //Is called when Copy is clicked.  Looks up the password by its ID
        //and copies it to the clipboard.

        //Example of how to get the ID of the password selected.
        int ID = Convert.ToInt32((sender as Button).CommandParameter);
        PasswordModel pass = App.PasswordController.GetPassword(ID);
        
        var curerntUser = App.LoginController.GetCurrentUser();
        if(pass != null){
            string password = PasswordCrypto.Decrypt(pass.PasswordText, Tuple.Create(curerntUser.Key,curerntUser.IV));
            MainThread.BeginInvokeOnMainThread(() =>
                {
                    Clipboard.Default.SetTextAsync(password); 
                });
        } 
        
    }

    private void ShowPassword(object sender, EventArgs e){
        this.Appearing += OnPageAppearing;
    }

    private void EditPassword(object sender, EventArgs e)
    {
        int ID = Convert.ToInt32((sender as Button).CommandParameter);
        var row = _rows.FirstOrDefault(X => X.PasswordID == ID);
        if (row != null){
            if (row.Editing){
                row.Editing = false;
                row.SavePassword();
                (sender as Button).Text = "Edit";
            }
            else{
                row.Editing = true;
                (sender as Button).Text = "Save";
            }
        }
    }

    private void DeletePassword(object sender, EventArgs e)
    {
        int ID = Convert.ToInt32((sender as Button).CommandParameter);
        if (App.PasswordController.RemovePassword(ID)){
            var row = _rows.FirstOrDefault(X => X.PasswordID == ID);
            if (row != null){
                _rows.Remove(row);
            }
        }
    }

    private async void ButtonAddPassword(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddPasswordView()); 
        this.Appearing += OnPageAppearing;
            
    }

    private void OnPageAppearing(object sender, EventArgs e){
        RefreshPasswordlist();
        this.Appearing -= OnPageAppearing;
    }
}