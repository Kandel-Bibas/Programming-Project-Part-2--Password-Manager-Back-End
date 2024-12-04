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
    public class PasswordModel
    {
       public int ID { get; set; }
       public int UserID { get; set; }
       public string PlatformName {get; set; }
       public string PlatformUserName { get; set; }
       public byte[] PasswordText { get; set; }

       public PasswordModel(int id, int userId, string platformName, string platformUserName, byte[] passwordText)
       {
            ID = id;
            UserID = userId;
            PlatformName = platformName;
            PlatformUserName = platformUserName;
            PasswordText = passwordText;
       }
    }
}
