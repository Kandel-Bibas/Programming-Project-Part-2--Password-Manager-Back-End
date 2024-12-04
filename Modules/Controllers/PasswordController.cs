/*
    Program Author: Bibas Kandel

    USM ID:  W10170085

    Assignment: Programming Project Part 2- Password Manager Back End

    Description: Paraphrase : This program implements a secure passord manager that allows users to store, view, edit and manage their platform password with encryption.

*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSC317PassManagerP2Starter.Modules.Controllers;
using CSC317PassManagerP2Starter.Modules.Models;
using CSC317PassManagerP2Starter.Modules.Views;

namespace CSC317PassManagerP2Starter.Modules.Controllers
{
    public class PasswordController
    {
        //Stores a list of sample passwords for the test user.
        public List<PasswordModel> _passwords = new List<PasswordModel>();
        private int counter = 0;


        /*
         * The following functions need to be completed.
         */
        //Used to copy the passwords over to the Row Binders.
        public void PopulatePasswordView(ObservableCollection<PasswordRow> source, string search_criteria = "")
        {
            //Complete definition of PopulatePasswordView here.
            source.Clear();
            foreach(var _pass in _passwords){
                if (string.IsNullOrWhiteSpace(search_criteria) ||
                _pass.PlatformName.Contains(search_criteria) ||
                _pass.PlatformUserName.Contains(search_criteria)){
                    source.Add(new PasswordRow(_pass));
                }
            }
        }

        //CRUD operations for the password list.
        public void AddPassword(string platform, string username, string password)
        {
            counter++;
            var currentUser = App.LoginController.GetCurrentUser();
            var encrypted_password = PasswordCrypto.Encrypt(password, Tuple.Create(currentUser.Key, currentUser.IV));
            _passwords.Add(new PasswordModel(counter,currentUser.ID,platform,username,encrypted_password));
        }

        public PasswordModel? GetPassword(int ID)
        {
           return _passwords.Find(x => x.ID == ID);
        }

        public bool UpdatePassword(PasswordModel changes)
        {
           var current = GetPassword(changes.ID);
           var currentUser = App.LoginController.GetCurrentUser();

           if (current != null){
            current.PlatformName = changes.PlatformName;
            current.PlatformUserName = changes.PlatformUserName;
            // current.PasswordText = PasswordCrypto.Encrypt(changes.PasswordText, Tuple.Create(currentUser.Key, currentUser.IV));
            current.PasswordText = changes.PasswordText;
            return true;
           }

            return false;
        }

        public bool RemovePassword(int ID)
        {
           var password = GetPassword(ID);
           if (password != null){
            _passwords.Remove(password);
            return true; 
           }

            return false;
        }

        public void GenTestPasswords()
        {

            var currentUser = App.LoginController.GetCurrentUser();
            counter++;
            _passwords.Add(new PasswordModel(counter,currentUser.ID,"Facebook","John.Doe",PasswordCrypto.Encrypt("facebook123", Tuple.Create(currentUser.Key,currentUser.IV))));
            counter ++;
            _passwords.Add(new PasswordModel(counter,currentUser.ID,"Google","John.Doe",PasswordCrypto.Encrypt("Google123", Tuple.Create(currentUser.Key,currentUser.IV))));
        }
    }
}
