using Imtihani.Interfaces;
using System;
using System.Collections.Generic;

namespace Imtihani.Models.Responses
{
    public class AuthResponse : IAuthResponse
    {
        public AuthResponse(string token, string refreshToken, int type)
        {
            Success = true;
            Token = token;
            RefreshToken = refreshToken;
            Type = type;
        }

        public AuthResponse(string token, string refreshToken, DateTime tokenExpires, DateTime refreshTokenExpires)
        {
            Success = true;
            Token = token;
            TokenExpires = tokenExpires;
            RefreshToken = refreshToken;
            RefreshTokenExpires = refreshTokenExpires;
        }

        public AuthResponse(string message, IEnumerable<string> errors)
        {
            Success = false;
            Message = message;
            Errors = errors;
        }

        public bool Success { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public int? Type { get; set; }
        public DateTime RefreshTokenExpires { get; set; }
        public string? UserId { get; set; }
        public DateTime TokenExpires { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }

    }
}
