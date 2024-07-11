using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;


namespace BusinessLayer
{
    public class clsPerson
    {
        public event EventHandler<PersonErrorsEventArgs> OnPerssonOperationErrors;
        private enum enMode { AddNewPerson, UpdatePerson };
        private enMode Mode { set; get; }
        public long? PersonID { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public string Email { get; set; }
        // will be on add only 
        // it will not return if i want to find a peron 
        public string Password { get; set; }

        // use this constructor in find person
        private clsPerson(long personID, string name, DateTime birthday, string email)
        {
            PersonID = personID;
            Name = name;
            Birthday = birthday;
            Email = email;
            Password = null;
            Mode = enMode.UpdatePerson;

        }
        // inital condition
        public clsPerson()
        {
            PersonID = null;
            Name = null;
            Birthday = DateTime.Now;
            Email = null;
            Mode = enMode.AddNewPerson;

        }

        protected virtual void RaiseOnPersonOperationsError(string Message, Exception ex)
        {

            OnPerssonOperationErrors?.Invoke(this, new PersonErrorsEventArgs(Message, ex));
        }


        private async Task<long?> AddNewPeronsAsync()
        {
            long? personID = null;
            try
            {
                bool IsEmailExist = await clsPeopleData.IsEmailExistAsync(Email);
                if (!IsEmailExist)
                {
                    personID = await clsPeopleData.AddNewPersonAsync(Name, Email, Password, Birthday);
                }
                else
                {
                    RaiseOnPersonOperationsError("This Email Is olready exists", null);
                    return null;

                }
            }
            catch (Exception ex)
            {
                RaiseOnPersonOperationsError(ex.Message, ex);
                throw;
            }
            return PersonID;

        }

        // add Validation  for email 
        private async Task<bool> UpdatePersonInfoAsync()
        {
            if (!clsUtil.CheckEmail(this.Email.Trim()))
            {
                RaiseOnPersonOperationsError("Invalid Email", null);
                return false;
            }
            try
            {
                bool resutl = await clsPeopleData.UpdatePersonInfoAsync((long)this.PersonID, Name, Email, Birthday);
                if (resutl) { return true; }

            }
            catch (Exception ex)
            {
                RaiseOnPersonOperationsError(ex.Message, ex);
            }
            return false;
        }

        public async Task<bool> UpdatePersonPasswordAsync(string NewPassword)
        {
            try
            {
                bool result = await clsPeopleData.UpdatePersonPasswordAsync((long)this.PersonID, NewPassword);
                if (result) { return true; }
            }
            catch (Exception ex)
            {

                RaiseOnPersonOperationsError(ex.Message, ex);
            }
            return false;
        }

        // check email
        public static clsPerson Find(string email, string Password)
        {
            if (!clsUtil.CheckEmail(email.Trim()))
            {
                // check it will not continue
                throw new Exception("Invalid Email");
            }
            try
            {
                long PersonID = default;
                string Name = default;
                DateTime BirthDate = default;

                if (clsPeopleData.Find(email, Password, ref PersonID, ref Name, ref BirthDate))
                {
                    // return new object 
                    return new clsPerson(PersonID, Name, BirthDate, email);
                }
                else
                    return null;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        // it is not async it will block thread 

        public static clsPerson Find(long PersonID)
        {
            try
            {
                string Email = default;
                string Name = default;
                DateTime BirthDate = default;

                if (clsPeopleData.Find(ref Email, PersonID, ref Name, ref BirthDate))
                {
                    // return new object 
                    return new clsPerson(PersonID, Name, BirthDate, Email);
                }
                else
                    return null;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }


    }



    public class PersonErrorsEventArgs : EventArgs
    {
        public PersonErrorsEventArgs(string errorMessage, Exception exception)
        {
            ErrorMessage = errorMessage;
            Exception = exception;
        }

        public string ErrorMessage { get; }
        public Exception Exception { get; }


    }




}