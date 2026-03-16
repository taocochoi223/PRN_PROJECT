using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Message
{
    public static class AppMessages
    {
        public const string LOGIN_SUCCESS = "Login successful. Welcome back!";
        public const string LOGIN_FAILED = "Invalid username or password. Please try again.";
        public const string ACCESS_DENIED = "Access Denied! You do not have permission to access this feature.";
        public const string SESSION_EXPIRED = "Your session has expired. Please login again.";

        public const string CREATE_SUCCESS = "The new record has been added successfully.";
        public const string CREATE_FAILED = "Failed to add the record. Please check your data.";

        public const string UPDATE_SUCCESS = "The record has been updated successfully.";
        public const string UPDATE_FAILED = "Failed to update the record. It may have been deleted or modified by another user.";

        public const string DELETE_SUCCESS = "The record has been deleted successfully.";
        public const string DELETE_FAILED = "Failed to delete the record. It might be linked to other data.";
        public const string DELETE_CONFIRM = "Are you sure you want to delete this item?";

        public const string NOT_FOUND = "The requested information could not be found.";

        public const string TITLE_REQ = "Contract Title is required (5-100 characters).";
        public const string TITLE_SPECIAL_CHARS = "Contract Title cannot contain special characters like @, #, $, %, ^";
        public const string DATE_FUTURE = "Signing Date cannot be a future date.";
        public const string DATE_EXPIRATION = "Expiration Date must be after the Signing Date.";
        public const string VALUE_POSITIVE = "Value must be a positive number and greater than 1000.";
        public const string BROKER_REQ = "Please select a valid Broker from the list.";
    }
}
