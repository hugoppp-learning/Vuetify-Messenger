using System.Diagnostics;
using backend.Controllers;
using backend.Model;
using backend.Repository;
using backend.Services.Security;
using Exception = System.Exception;

namespace backend.Services;

public class DevelopmentDataService
{
    private readonly UserRepo _users;
    private readonly PostRepo _postsRepo;
    private readonly AuthService _authService;
    private readonly MockEmailSendingService _mockEmailSendingService;

    public DevelopmentDataService(UserRepo userRepo, PostRepo postsRep, ILogger<AuthService> authServiceLogger,
        JwtEmailVerificationService jwtEmailVerificationService)
    {
        _users = userRepo;
        _postsRepo = postsRep;
        _mockEmailSendingService = new MockEmailSendingService();
        _authService = new AuthService(authServiceLogger, userRepo, jwtEmailVerificationService,
            _mockEmailSendingService);
    }

    public void SeedData()
    {
        CreateTestAccount().Wait();
        CreatePosts().Wait();
    }

    private async Task CreateTestAccount()
    {
        if (await _users.FindByUsername("string") is not null)
        {
            return;
        }

        await _authService.SignupAsync(new SignupDto("a@b.c", "string", "string"));
        Debug.Assert(_mockEmailSendingService.EmailVerificationToken is not null);
        if (! await _authService.VerifyEmail(_mockEmailSendingService.EmailVerificationToken))
            throw new Exception("Could not create test account");
    }

    private async Task CreatePosts()
    {
        try
        {
            await Task.WhenAll(
                Posts.Select(p => _postsRepo.Add(p)
                )
            );
        }
        catch (Exception)
        {
            //ignore
        }
    }

    private static readonly List<Post> Posts = new()
    {
        new Post()
        {
            Id = new Guid("7f4b8926-2b2e-4018-8ceb-c711cd5b6a35"),
            Username = "hugop",
            ProfilePicture = "https://i.pravatar.cc/300",
            Message = "This is the first post on this site, Yay!! :)",
        },
        new Post()
        {
            Id = new Guid("69a7c562-1de7-4d1c-a1ad-462f8611ef83"),
            Username = "hugop",
            ProfilePicture = "https://i.pravatar.cc/300",
            Message = "This is the second post on this site",
            LikesCount = 0,
        },
        new Post()
        {
            Id = new Guid("8cc205f7-757c-4203-8a7b-cb3747e6b79b"),
            Username = "hugop",
            ProfilePicture = "https://i.pravatar.cc/300",
            Message = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.Ut enim ad minim veniam,
                quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.Duis aute irure dolor
                in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat
                cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum",
        },
    };
}