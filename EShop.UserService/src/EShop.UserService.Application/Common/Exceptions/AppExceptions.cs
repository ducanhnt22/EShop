using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.UserService.Application.Common.Exceptions;
public class AppExceptions : Exception
{
    public List<string> Errors { get; }
    public List<FieldError> FieldErrors { get; }
    public int StatusCode { get; } = 400;

    public AppExceptions() : base()
    {
        Errors = new List<string>();
        FieldErrors = new List<FieldError>();
    }

    public AppExceptions(string message) : base(message)
    {
        Errors = new List<string> { message };
        FieldErrors = new List<FieldError>();
        StatusCode = 400;
    }

    public AppExceptions(List<string> errors) : base("One or more errors occurred.")
    {
        Errors = errors ?? new List<string>();
        FieldErrors = new List<FieldError>();
        StatusCode = 400;
    }

    public AppExceptions(string message, List<string> errors) : base(message)
    {
        Errors = errors ?? new List<string>();
        FieldErrors = new List<FieldError>();
        StatusCode = 400;
    }

    public AppExceptions(string message, Exception innerException) : base(message, innerException)
    {
        Errors = new List<string> { message };
        FieldErrors = new List<FieldError>();
        StatusCode = 400;
    }

    public AppExceptions(string message, List<string> errors, Exception innerException) : base(message, innerException)
    {
        Errors = errors ?? new List<string>();
        FieldErrors = new List<FieldError>();
        StatusCode = 400;

    }

    public AppExceptions(string message, List<FieldError> fieldErrors, int statusCode) : base(message)
    {
        Errors = new List<string>();
        FieldErrors = fieldErrors ?? new List<FieldError>();
        StatusCode = statusCode;
    }

    public AppExceptions(string message, List<FieldError> fieldErrors) : base(message)
    {
        Errors = new List<string>();
        FieldErrors = fieldErrors ?? new List<FieldError>();
    }

    public AppExceptions(string message, List<FieldError> fieldErrors, Exception innerException) : base(message, innerException)
    {
        Errors = new List<string>();
        FieldErrors = fieldErrors ?? new List<FieldError>();
        StatusCode = 400;
    }
}
