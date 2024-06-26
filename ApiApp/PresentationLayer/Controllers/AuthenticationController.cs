﻿using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ApiApp.BusinessLogicLayer.TokenBLL;
using ApiApp.BusinessLogicLayer.UserBLL;
using ApiApp.DataAccessLayer.ObjectModel;
using ApiApp.DataAccessLayer.Model;
using Microsoft.AspNetCore.Authorization;

namespace ApiApp.PresentationLayer.Controllers;

[Route("api/Authenticate")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _authService;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public AuthenticationController(
        IUserService userService,
        IConfiguration config,
        ITokenService authService,
        IMapper mapper)
    {
        _userService = userService;
        _authService = authService;
        _config = config;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginInformation inputUser)
    {
        if (inputUser is null)
        {
            return BadRequest("Invalid client request");
        }

        var _decKey = _config.GetValue<int>("DecryptionKey");

        inputUser.Name = _authService.Decrypt(inputUser.Name, _decKey);
        inputUser.Password = _authService.Decrypt(inputUser.Password, _decKey);
        // Get the list of users
        var userList = await _userService.GetUsersAsync();

        var foundUser = userList.FirstOrDefault(user =>
            user.Name != null &&
            user.Name.Equals(inputUser.Name) &&
            user.Password.Equals(inputUser.Password));

        if (foundUser == null)
        {
            return Unauthorized();
        }

        var user = _mapper.Map<User>(foundUser);
        var tokenString = _authService.CreateTokenOptions(user);

        return Ok(new AuthenticationInformation { Token = tokenString });

    }
}