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

            return Ok(userId);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthLoginResponseDto>> Login([FromBody] AuthLoginResquestDto loginDto) {
            loginDto.usernameOrEmail = loginDto.usernameOrEmail.Trim().ToLower();
            loginDto.password = loginDto.password.Trim();

            var response = new AuthLoginResponseDto();

            try {
                response.JwtToken = await _authService.Login(loginDto.usernameOrEmail, loginDto.password);
                response.Success = true;
            } catch (UserNotFoundException) {
                response.Success = false;
            } catch (IncorrectPasswordException) {
                response.Success = false;
            }

            return Ok(response);
        }
    }
}