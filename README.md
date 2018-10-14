Extensions.Authentication
=========================

Lean token extensions for issuing/validating tokens, automatically creating a private key and creating password salts.
Compatible with Asp.Net Core 2.1.

Usage Guide
-----------

Install the NuGet package

>   Buzzytom.Extensions.Authentication

Add using statements to get the extensions.

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ c#
using Extensions.Authentication;
using Extensions.Authentication.JwtBearer;
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

In the ConfigureServices of your application (or equivalent) add the following
calls to register the required services and middleware.

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ c#
public void ConfigureServices(IServiceCollection services)
{
    // Register other services here, like entity framework
    
	// Adds a ITokenService to the dependency services
	services.AddTokenService();
	
    // Adds a IValidatorService to the dependency services
	services.AddValidationService();
	
	// Adds:
	// - JWT middleware services supporting the Authorize attribute
	// - Adds a ISymmetricKeyProvider to the dependency services
	services.AddJwt();
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Make sure your application is configured to use authentication.

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ c#
public void Configure(IApplicationBuilder application)
{
	// Configure other services here

	// Enable authentication, JWT middleware does not work without this
	application.UseAuthentication();
	
	// Configure other services here. E.g. services.UseMvc();
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Example controller to issue authentication tokens.

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ c#
[Route("/api/account")]
public class AuthenticationController : ControllerBase
{
	private readonly ITokenService tokenService;
	private readonly IBearerTokenService bearerTokenService;

	public AuthenticationController(ITokenService tokenService,
									IBearerTokenService, bearerTokenService)
	{
		this.tokenService = tokenService;
		this.bearerTokenService = bearerTokenService;
	}

	[HttpPost]
	[Route("authenticate")]
	public string Authenticate([FromBody] AuthenticateRequest request)
	{
		// DON'T ACTUALLY DO IT LIKE THIS
		// THIS IS ONLY AN EXAMPLE
		
		YourUserType user = GetUserFromYourPersistentStore(request.Email);
	
		string requestHash = tokenService.Hash(request.Password, user.Salt);
		if (requestHash == user.PasswordHash)
			return bearerTokenService.CreateAuthenticationToken(new Claim(ClaimTypes.Email, request.Email.ToLower()));
		else
			return null;
	}
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Example registration controller

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ c#
[Route("/api/account")]
public class RegisterController : ControllerBase
{
	private readonly ITokenService service;

	public AuthenticationController(ITokenService service)
	{
		this.service = service;
	}

	[HttpPost]
	[Route("register")]
	public void Register([FromBody] RegisterRequest request)
	{
		// DON'T ACTUALLY DO IT LIKE THIS
		// THIS IS ONLY AN EXAMPLE
		
		// Perform request validation checks
		
		bool exists = CheckUserDoesNotExistInYourPersistentStore(request.Email);
		if (exists)
			return false;
			
		// Create the password
        string salt = service.GenerateSalt();
        string hash = service.Hash(request.Password, salt);
	
		// Create the new user
		CreateNewUserInYourPersistentStore(new YourUser
		{
			Email = request.Email,
			Salt = salt,
			PasswordHash = hash
			// Other properties
		});
	}
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Improvements
------------

Raise them as an issue
