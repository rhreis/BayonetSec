using ScarletSec.Api.DTOs;
using ScarletSec.Application.Interfaces;
using ScarletSec.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ScarletSec.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        // For now, return a mock response
        // TODO: Implement proper authentication with password hashing
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null || !user.IsActive)
        {
            return Unauthorized(new { message = "Invalid credentials" });
        }

        // Mock token generation - replace with proper JWT implementation
        var response = new LoginResponseDto
        {
            Token = $"mock-jwt-token-{user.Id}",
            RefreshToken = $"mock-refresh-token-{user.Id}",
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            User = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName ?? "",
                LastName = user.LastName ?? "",
                Role = user.Role,
                TenantId = user.TenantId,
                IsActive = user.IsActive,
                LastLoginAt = null
            }
        };

        return Ok(response);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        // Mock refresh token implementation
        // TODO: Implement proper refresh token logic
        return Ok(new
        {
            token = "new-mock-jwt-token",
            expiresAt = DateTime.UtcNow.AddHours(1)
        });
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        // TODO: Implement token blacklisting if needed
        return Ok(new { message = "Logged out successfully" });
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        // TODO: Get user from JWT token claims
        // For now, return mock user
        var user = new UserDto
        {
            Id = 1,
            Username = "admin",
            Email = "admin@scarletsec.com",
            FirstName = "Admin",
            LastName = "User",
            Role = "Admin",
            TenantId = 1,
            IsActive = true,
            LastLoginAt = DateTime.UtcNow
        };

        return Ok(user);
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
    {
        // TODO: Implement password change logic
        return Ok(new { message = "Password changed successfully" });
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword([FromBody] string email)
    {
        // TODO: Implement forgot password logic
        return Ok(new { message = "Password reset email sent" });
    }

    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
    {
        // TODO: Implement password reset logic
        return Ok(new { message = "Password reset successfully" });
    }
}

public class ResetPasswordRequestDto
{
    [Required]
    public string Token { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string NewPassword { get; set; } = string.Empty;
}