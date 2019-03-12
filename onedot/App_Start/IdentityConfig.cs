using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using one.Data.Entities;
using one.Data;
using System.Configuration;
using System.Net.Mail;
using System.Net;


namespace one.OneDot
{

 //   public class ApplicationUserStore :
 //UserStore<ApplicationUser, IdentityRole, string, IdentityUserLogin, ApplicationUserRole, ApplicationUserClaim>,
 //IUserStore<ApplicationUser>,
 //IDisposable
 //   {
 //       public ApplicationUserStore(ApplicationDbContext context) : base(context) { }
 //   }


    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {

            var email = new MailMessage(Convert.ToString(ConfigurationManager.AppSettings["FromMailAddress"]), message.Destination)
            {
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true
            };

            var mailClient = new SmtpClient(
                Convert.ToString(ConfigurationManager.AppSettings["MailHost"]),
                Convert.ToInt32(ConfigurationManager.AppSettings["EmailPort"]))
            {
                Credentials =
                    new NetworkCredential(
                        Convert.ToString(ConfigurationManager.AppSettings["EmailUsername"]),
                        Convert.ToString(ConfigurationManager.AppSettings["EmailPassword"])),
                EnableSsl = true
            };

            return mailClient.SendMailAsync(email);
            
            
            
            // 在此处插入电子邮件服务可发送电子邮件。
           // return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 在此处插入 SMS 服务可发送短信。
            return Task.FromResult(0);
        }
    }

    // 配置此应用程序中使用的应用程序用户管理器。UserManager 在 ASP.NET Identity 中定义，并由此应用程序使用。
    public class ApplicationUserManager : UserManager<Auth_Users>
    {
        public ApplicationUserManager(IUserStore<Auth_Users> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            //var manager = new ApplicationUserManager(new IdentityUserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            var manager = new ApplicationUserManager(new one.Data.IdentityUserStore(context.Get<ApplicationDbContext>()));
            // 配置用户名的验证逻辑
            manager.UserValidator = new UserValidator<Auth_Users>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // 配置密码的验证逻辑
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // 配置用户锁定默认值
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;



            // 注册双重身份验证提供程序。此应用程序使用手机和电子邮件作为接收用于验证用户的代码的一个步骤
            // 你可以编写自己的提供程序并将其插入到此处。
            manager.RegisterTwoFactorProvider("电话代码", new PhoneNumberTokenProvider<Auth_Users>
            {
                MessageFormat = "你的安全代码是 {0}"
            });
            manager.RegisterTwoFactorProvider("电子邮件代码", new EmailTokenProvider<Auth_Users>
            {
                Subject = "安全代码",
                BodyFormat = "你的安全代码是 {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<Auth_Users>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }




    // 配置要在此应用程序中使用的应用程序登录管理器。
    public class ApplicationSignInManager : SignInManager<Auth_Users, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(Auth_Users user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

        

    }
}
