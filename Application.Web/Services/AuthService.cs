﻿using Application.Web.Common;
using Application.Web.InputModel;
using Application.Web.Services.Interface;
using Application.Web.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private ApplicationUser _applicationUser;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _applicationUser = new();
        }

       
        public async Task<IEnumerable<IdentityError>> Register(Register register)
        {
            _applicationUser.FirstName = register.FirstName;
            _applicationUser.LastName = register.LastName;
            _applicationUser.Email = register.Email;
            _applicationUser.UserName = register.Email;

            var result = await _userManager.CreateAsync(_applicationUser, register.Password );

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_applicationUser, "CUSTOMER");
            }

            return result.Errors;
        }

        public async Task<object> Login(Login login)
        {
            _applicationUser = await _userManager.FindByEmailAsync(login.Email);

            if (_applicationUser == null)
            {
                return "Invalid Email Address";
            }

            var result = await _signInManager.PasswordSignInAsync(_applicationUser, login.Password, isPersistent: true, lockoutOnFailure: true);

            var isValidCredential = await _userManager.CheckPasswordAsync(_applicationUser, login.Password);

            if (result.Succeeded)
            {
                var token = await GenerateToken();

                LoginResponse loginResponse = new LoginResponse
                {
                    UserId = _applicationUser.Id,
                    Token = token
                };

                return loginResponse;
            }

            else
            {
                if (result.IsLockedOut)
                {
                    return "Your Account is Locked , Contact System Admin";
                }
                            

                if (result.IsNotAllowed)
                {
                    return "Please Verify Email Address";
                }

                if (isValidCredential == false)
                {
                    return "Invalid Password";
                }

                else
                {
                    return "Login Failed";
                }
            }
        }


        public async Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(_applicationUser);

            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,_applicationUser.Email)
            }.Union(roleClaims).ToList();

            var token = new JwtSecurityToken
                (
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["JwtSettings:DurationInMinutes"]))
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

}


