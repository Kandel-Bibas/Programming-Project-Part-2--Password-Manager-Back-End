/*
    Program Author: Bibas Kandel

    USM ID:  W10170085

    Assignment: Programming Project Part 2- Password Manager Back End

    Description: Paraphrase : This program implements a secure passord manager that allows users to store, view, edit and manage their platform password with encryption.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC317PassManagerP2Starter.Modules.Models
{
    public class User
    {

        //Implement the User Model here.

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }

        public User(int id, string firstname, string lastname, string username, byte[] passwordhash, byte[] key, byte[] iv){
            ID = id;
            FirstName = firstname;
            LastName = lastname;
            UserName = username;
            PasswordHash = passwordhash;
            Key = key;
            IV = iv;
        }

    }
}
