using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using sepending.Application.DTOs;
using sepending.Infrastructure.Repositories;
using sepending.Share;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using sepending.Domain.Entities;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;


namespace sepending.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IConfiguration _config;

    public UserService(IUserRepository repository, IConfiguration config)
    {
        _repository = repository;
        _config = config;
    }
    
    public async Task<Result<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _repository.GetAll();

        var list = users.Select(u => new UserDto
        {
            Id = u.Id,
            Username = u.Username,
            Role = u.Role,
        });

        return await Result<IEnumerable<UserDto>>.SuccessAsync(list, "Lấy danh sách user thành công");
    }

    public async Task<Result<string>> RegisterAsync(string username, string email, string password)
    {
        var existingUser = await _repository.GetByUsernameAsync(username);
        
        if (existingUser != null)
            return Result<string>.Failure("Username đã tồn tại");

        var existingEmail = await _repository.GetByEmailAsync(email);
        if (existingEmail != null)
            return Result<string>.Failure("Email đã tồn tại");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        
        var newUser = new User
        {
            Username = username,
            Email = email,
            Password = passwordHash
        };
        
        await  _repository.AddAsync(newUser);
        return Result<string>.Success("Đăng ký thành công");
    }

    public async Task<Result<string>> LoginAsync(string username, string password)
    {
        var existingUser = await _repository.GetByUsernameAsync(username);
        if (existingUser == null)
            return Result<string>.Failure("Username không tồn tại");
        
        bool validPassword  = BCrypt.Net.BCrypt.Verify(password, existingUser.Password);
        
        if (!validPassword)
            return Result<string>.Failure("Sai username hoặc password");

        string token = GenerateJwtToken(existingUser);

        return Result<string>.Success(token, "Đăng nhập thành công");
    }   

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes((_config["Jwt:Key"])));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}