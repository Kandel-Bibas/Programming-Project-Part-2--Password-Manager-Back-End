/*
    Program Author: Bibas Kandel

    USM ID:  W10170085

    Assignment: Programming Project Part 2- Password Manager Back End

    Description: Paraphrase : This program implements a secure passord manager that allows users to store, view, edit and manage their platform password with encryption.

*/

using CSC317PassManagerP2Starter.Modules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC317PassManagerP2Starter.Modules.Controllers
{
    public enum AuthenticationError { NONE, INVALIDUSERNAME, INVALIDPASSWORD }
    public class LoginController
    {

        /*
         * This class is incomplete.  Fill in the method definitions below.
         */
        private User _user;

        public LoginController()
        {
            var key = PasswordCrypto.GenKey();
            _user = new User(
                id:  1,
                firstname: "John",
                lastname: "Doe",
                username: "test",
                passwordhash: PasswordCrypto.GetHash("ab1234"),
                key: key.Item1,
                iv: key.Item2
            );
        }
       

        public User? GetCurrentUser()
        {
            return new User(_user.ID, _user.FirstName, _user.LastName, _user.UserName, _user.PasswordHash, _user.Key, _user.IV);
        }

        public AuthenticationError Authenticate(string username, string password)
        {
            //determines whether the inputted username/password matches the stored

            if (username != _user.UserName){
                return AuthenticationError.INVALIDUSERNAME;
            }

            if (!PasswordCrypto.CompareBytes(PasswordCrypto.GetHash(password), _user.PasswordHash))
                return AuthenticationError.INVALIDPASSWORD;

            return AuthenticationError.NONE;

        }
    }

}
