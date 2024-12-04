/*
    Program Author: Bibas Kandel

    USM ID:  W10170085

    Assignment: Programming Project Part 2- Password Manager Back End

    Description: Paraphrase : This program implements a secure passord manager that allows users to store, view, edit and manage their platform password with encryption.

*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSC317PassManagerP2Starter.Modules.Models;



/* This module contains the class definition for Password Row.
 * 
 * The methods are missing their bodies.  Fill in the method definitions below.
 * 
 */
namespace CSC317PassManagerP2Starter.Modules.Views
{
    public class PasswordRow : BindableObject, INotifyPropertyChanged
    {
        private PasswordModel _pass;
        private bool _isVisible = false;
        private bool _editing = false;


        public PasswordRow(PasswordModel source)
        {
            _pass = source;
        }

        //Create your Binding Properties here, which should reflect the front-end bindings.
        //See the example of "Platform" below.
        public string Platform
        {
            get
            {
                return _pass.PlatformName;
            }
            set
            {
                _pass.PlatformName = value;
                RefreshRow();
            }
        }

        public string PlatformUserName
        {
            get
            {
                return _pass.PlatformUserName;
            }
            set
            {
                _pass.PlatformUserName = value;
                RefreshRow();
            }
        }

        public string PlatformPassword
        {
            get
            {
               var curerntUser = App.LoginController.GetCurrentUser();
               if (_isVisible == true){
                return PasswordCrypto.Decrypt(_pass.PasswordText, Tuple.Create(curerntUser.Key,curerntUser.IV));
               }
               else{
                return "<hidden>";
               }
            }
            set
            {
                var curerntUser = App.LoginController.GetCurrentUser();
                _pass.PasswordText = PasswordCrypto.Encrypt(value,Tuple.Create(curerntUser.Key,curerntUser.IV));
                RefreshRow();
            }
        }

        public int PasswordID
        {
            get
            {
                return _pass.ID;
            }
        }

        public bool IsShown
        {
            get
            {
               return _isVisible;
            }
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(PlatformPassword));
                RefreshRow();
            }
        }

        public bool Editing
        {
            get
            {
                return _editing;
            }
            set
            {
                _editing = value;
                RefreshRow();
            }
        }


        //This is called when a bound property is changed on the front-end.  Causes the 
        //front-end to update the collection view.
        private void RefreshRow()
        {
            OnPropertyChanged(nameof(Platform));
            OnPropertyChanged(nameof(PlatformUserName));
            OnPropertyChanged(nameof(PlatformPassword));
            OnPropertyChanged(nameof(IsShown));
            OnPropertyChanged(nameof(Editing));
        }

        public void SavePassword()
        {
            App.PasswordController.UpdatePassword(_pass);
        }
    }

}
