using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TECHNOVA.Data;
using TECHNOVA.Helpers;
using TECHNOVA.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authentication.Google;


namespace TECHNOVA.Controllers
{
    public class CustomerController : Controller
    {
        private readonly TechnovaContext db;
        private readonly IMapper _mapper;

        public CustomerController(TechnovaContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM model)
        {
            // Kiểm tra trùng tài khoản/email
            if (db.Customers.Any(c => c.CustomerId == model.customerID))
            {
                ModelState.AddModelError("customerID", "Username already exists.");
                return View(model);
            }

            if (db.Customers.Any(c => c.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email already registered.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var customer = _mapper.Map<Customer>(model);
                    customer.RandomKey = Util.GenerateRandomKey();
                    customer.Password = model.password.ToMd5Hash(customer.RandomKey);
                    customer.IsActive = true;
                    customer.Role = 0;
                    db.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Item");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Register Fail: " + ex.Message);
                }
            }
            return View();
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            if (ModelState.IsValid)
            {
                var customer = db.Customers
                    .FirstOrDefault(c =>
                        (c.CustomerId == model.UsernameOrEmail || c.Email == model.UsernameOrEmail));

                if (customer == null || customer.Password != model.password.ToMd5Hash(customer.RandomKey))
                {
                    ModelState.AddModelError("", "Tên đăng nhập/email hoặc mật khẩu không đúng");
                    return View(model);
                }

                if (!customer.IsActive)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa. Vui lòng liên hệ Admin.");
                    return View(model);
                }

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, customer.CustomerId),
                                            new Claim(MySetting.CLAIM_CUSTOMERID, customer.CustomerId),
            new Claim(ClaimTypes.Email, customer.Email ?? ""),
            new Claim(ClaimTypes.Name, customer.FullName ?? ""),
            new Claim(ClaimTypes.Role, "Customer")
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7) // hoặc thời gian bạn muốn
                });

                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return Redirect("/");
                }
            }
            return View();
        }
        public IActionResult LoginGoogle(string returnUrl = "/")
        {
            var redirectUrl = Url.Action("GoogleResponse", "Customer", new { returnUrl });
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> GoogleResponse(string returnUrl = "/")
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
                return RedirectToAction("Login");

            var claims = result.Principal.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            // Kiểm tra hoặc tạo user
            var user = db.Customers.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                user = new Customer
                {
                    CustomerId = Guid.NewGuid().ToString().Substring(0, 8),
                    Email = email,
                    FullName = name,
                    IsActive = true,
                    Role = 0
                };
                try
                {
                    db.Customers.Add(user);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Ghi log hoặc hiển thị lỗi
                    Console.WriteLine(ex.Message);
                    return Content("Lỗi khi lưu người dùng: " + ex.Message);
                }
            }

            // Tạo claims đăng nhập
            var userClaims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Name, user.FullName),
        new Claim(MySetting.CLAIM_CUSTOMERID, user.CustomerId),
        new Claim(ClaimTypes.Role, "Customer")
    };

            var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);

            return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl : "/");
        }

        public IActionResult LoginFacebook(string returnUrl = "/")
        {
            var redirectUrl = Url.Action("FacebookResponse", "Customer", new { returnUrl });
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, "Facebook");
        }

        public async Task<IActionResult> FacebookResponse(string returnUrl = "/")
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
                return RedirectToAction("Login");

            var claims = result.Principal.Identities.FirstOrDefault()?.Claims;
            var facebookId = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (facebookId == null)
                return RedirectToAction("Login");

            // Tìm theo FacebookID chứ không dùng email nữa
            var user = db.Customers.FirstOrDefault(u => u.FacebookId == facebookId);

            if (user == null)
            {
                user = new Customer
                {
                    CustomerId = Guid.NewGuid().ToString("N").Substring(0, 8),
                    FullName = name,
                    Email = email,              // nếu có thì lưu, không bắt buộc
                    FacebookId = facebookId,    // thêm cột này trong bảng Customer
                    IsActive = true,
                    Role = 0
                };
                db.Customers.Add(user);
                await db.SaveChangesAsync();
            }

            var userClaims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.FullName),
        new Claim(MySetting.CLAIM_CUSTOMERID, user.CustomerId),
        new Claim(ClaimTypes.Role, "Customer")
    };

            var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);

            return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl : "/");
        }


        #endregion

        [Authorize]
        public IActionResult MyAccount()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");

        }
    }
}
