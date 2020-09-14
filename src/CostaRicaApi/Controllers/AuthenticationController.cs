using System.Threading.Tasks;
using AutoMapper;
using CostaRicaApi.Dtos;
using CostaRicaApi.Exceptions;
using CostaRicaApi.Models;
using CostaRicaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CostaRicaApi.Controllers {

    [Route("api/v1/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase {
        private readonly IAuthenticationService _authService;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] AuthRegisterRequestDto registerDto) {
            var user = _mapper.Map<User>(registerDto);
            
            if(!registerDto.Password.Equals(registerDto.PasswordVerify)) {
                return BadRequest("Password mismatch");
            }

            if(await _authService.IsUserPresent(user.Username, user.Email)) {
                return BadRequest("User already exists with this username or email");
            }

            var userId = await _authService.Register(user, registerDto.Password);

            var responseDto = new AuthLoginResponseDto() {Success = false};
            if(userId > 0) {
                responseDto.Success = true;
                responseDto.JwtToken = await _authService.Login(user.Username, registerDto.Password);
            }

            return Ok(responseDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthLoginResponseDto>> Login([FromBody] AuthLoginResquestDto loginDto) {
            loginDto.usernameOrEmail = loginDto.usernameOrEmail.Trim().ToLower();
            loginDto.password = loginDto.password.Trim();

            var responseDto = new AuthLoginResponseDto();

            try {
                responseDto.JwtToken = await _authService.Login(loginDto.usernameOrEmail, loginDto.password);
                responseDto.Success = true;
            } catch (UserNotFoundException) {
                responseDto.Success = false;
            } catch (IncorrectPasswordException) {
                responseDto.Success = false;
            }

            return Ok(responseDto);
        }
    }
}