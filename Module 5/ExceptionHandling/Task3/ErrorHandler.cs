using System;
using Task3.DoNotChange;

namespace Task3
{
    public class InvalidUserIdException : Exception
    {
        public InvalidUserIdException() : base("Invalid userId") { }
    }

    public class UserNotFoundException : Exception 
    {
        public UserNotFoundException() : base("User not found") { }
    }

    public class TaskAlreadyExistsException : Exception
    {
        public TaskAlreadyExistsException() : base("The task already exists") { }
    }

    public static class ErrorHandler
    {
        public static void Handle(Exception ex, IResponseModel model)
        {
            switch (ex)
            {
                case InvalidUserIdException:
                case UserNotFoundException:
                case TaskAlreadyExistsException:
                    model.AddAttribute("action_result", ex.Message);
                    break;
                default:
                    model.AddAttribute("action_result", "Unknown error");
                    break;
            }
        }
    }
}
