using System;
using System.Collections.Generic;

namespace Imtihani.Interfaces
{
    public interface IAuthResponse
    {
        bool Success { get; set; }
        string? Token { get; set; }
        string? RefreshToken { get; set; }
        DateTime RefreshTokenExpires { get; set; }
        string? UserId { get; set; }
        DateTime TokenExpires { get; set; }
        string Message { get; set; }
        IEnumerable<string> Errors { get; set; }
    }
}
