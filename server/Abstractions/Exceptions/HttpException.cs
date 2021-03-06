using System;

namespace Abstractions.Exceptions
{
  public class HttpException : Exception
  {
    public int StatusCode { get; set; }

    public HttpException(string message) : base(message)
    { }

    public HttpException(int statusCode, string message) : base(message)
    {
      StatusCode = statusCode;
    }
  }
}