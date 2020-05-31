﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationMicroservice.Database;
using AuthenticationMicroservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        DatabaseContext db;
        IConfiguration config;
        public AuthenticationController(DatabaseContext _db, IConfiguration _config)
        {
            db = _db;
            config = _config;
        }

        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //For JWT Cliams help : https://tools.ietf.org/html/rfc7519#section-5
            var claims = new Claim[] {
                             new Claim(JwtRegisteredClaimNames.Sub, userInfo.Name),
                             new Claim(JwtRegisteredClaimNames.Email, userInfo.Username),
                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                             new Claim("Roles", userInfo.Roles.First()), //claim for authorization
                             new Claim("CreatedDate", DateTime.Now.ToString()),
                             };

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                                            config["Jwt:Audience"],
                                            claims,
                                            expires: DateTime.Now.AddMinutes(120),
                                            signingCredentials: credentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedJwt;
        }

        [HttpPost]
        public IActionResult ValidateUser(LoginModel model)
        {
            UserModel data = (from user in db.Users
                              join userrole in db.UserRoles
                              on user.UserId equals userrole.UserId
                              where user.Username == model.Username && user.Password == model.Password
                              select new UserModel
                              {
                                  UserId = user.UserId,
                                  Username = user.Username,
                                  Name = user.Name,
                                  Roles = db.Roles.Where(r => r.RoleId == userrole.RoleId).Select(r => r.Name).ToArray()
                              }).FirstOrDefault();
            data.Token = GenerateJSONWebToken(data);
            return Ok(data);
        }

        public IActionResult GetUsers()
        {
            var data = db.Users.Select(u => new { u.Name, u.UserId, u.Username }).ToList();
            return Ok(data);
        }
    }
}