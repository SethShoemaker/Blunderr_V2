using System.Security.Claims;
using System.Text.Encodings.Web;
using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Features.Security.Authenticate;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Blunderr.Web.Authentication
{
    public class TokenAuthenticationHandler : AuthenticationHandler<TokenAuthenticationSchemeOptions>
    {
        private readonly IMediator _mediator;

        public TokenAuthenticationHandler(IOptionsMonitor<TokenAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IMediator mediator
        ) : base(options, logger, encoder, clock){
            _mediator = mediator;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string? token = Request.Cookies[TokenAuthenticationSchemeOptions.CookieName];

            if(string.IsNullOrWhiteSpace(token))
                return AuthenticateResult.NoResult();

            AuthenticateResponse response = await _mediator.Send(new AuthenticateRequest(token));
            if(!response.isSuccessful)
                return AuthenticateResult.Fail("Token is invalid");

            return AuthenticateResult.Success(CreateTicket(response.User));
        }

        private AuthenticationTicket CreateTicket(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, nameof(TokenAuthenticationHandler));
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return new AuthenticationTicket(claimsPrincipal, TokenAuthenticationSchemeOptions.Name);
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.Redirect("/Login");
            return Task.CompletedTask;
        }
    }
}