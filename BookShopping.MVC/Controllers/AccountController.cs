using System;
using System.Net;
using BookShopping.Application.Interfaces;
using BookShopping.Application.Models.Account.Register;
using BookShopping.MVC.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BookShopping.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IEmailSender _emailSender;
        private readonly ITransactionHandler _transactionHandler;
        public AccountController(IAccountService accountService,
                                IEmailSender emailSender,
                                ITransactionHandler transactionHandler)
        {
            _accountService = accountService;
            _transactionHandler = transactionHandler;
            _emailSender = emailSender;
        }
        [HttpGet]
        public IActionResult Auth()
        {
            return View(new AuthModel());
        }


        [HttpPost]
        public async Task<IActionResult> Auth(AuthModel model)
        {
            try
            {
                if (model.RegisterRequest != null)
                {
                    ModelState.Remove("LoginRequest");

                    if (!ModelState.IsValid) return View(model);


                    await _transactionHandler.BeginTransactionAsync();




                    var registerResponse = await _accountService.RegisterAsync(model.RegisterRequest);
                    if (registerResponse.StatusCode == (int)HttpStatusCode.BadRequest)
                    {
                        foreach (var item in registerResponse.Errors)
                        {
                            ModelState.AddModelError(item.Key, item.Value);
                        }

                        return View(model);
                    }



                    var emailConfirmationModel = await _accountService.CreateEmailValidationModel(registerResponse.Response.UserId);




                    await _emailSender.SendAsync(model.RegisterRequest.Email, "Confirmation", emailConfirmationModel.Response.URL);

                    HttpContext.Session.SetString("userId", registerResponse.Response.UserId.ToString());
                    HttpContext.Session.SetString("token", emailConfirmationModel.Response.Token);


                    TempData["register"] = true;

                    await _transactionHandler.CommitTransactionAsync();
                    return RedirectToAction("Auth", "Account");
                }
                else if (model.LoginRequest != null)
                {

                    ModelState.Remove("RegisterRequest");

                    if (!ModelState.IsValid) return View(model);


                    var loginResponse = await _accountService.LoginAsync(model.LoginRequest);

                    if (loginResponse.StatusCode == (int)HttpStatusCode.BadRequest)
                    {
                        foreach (var item in loginResponse.Errors)
                        {
                            ModelState.AddModelError(item.Key, item.Value);
                        }

                        return View(model);
                    }


                    return RedirectToAction("Index", "Home");
                }

                //todo: eger her ikisi nulldursa bad request falan sehifesine getsin
                return RedirectToAction("Index", "Home");

            }
            catch (Exception exp)
            {
                var transactionId = _transactionHandler.GetTransactionId();

                if (transactionId != Guid.Empty)
                    await _transactionHandler.RoleBackTransactionAsync();

                return RedirectToAction("Index", "Home");
            }




        }


        [HttpGet]
        public async Task<IActionResult> Validate(string userId, string token)
        {
            var sessionToken = HttpContext.Session.GetString("token");
            var sessionUserId = HttpContext.Session.GetString("userId");


            if (sessionUserId == null || sessionToken == null)
            {
                TempData["emailconfirmed"] = false;
                TempData["emailconfirmederror"] = "Sessiya muddeti bitmisdir.";
                return RedirectToAction("Index", "Home");
            }


            if (sessionToken == token && sessionUserId == userId)
            {
                var result = await _accountService.ConfirmEmailAsync(userId);
                if (result.StatusCode == (int)HttpStatusCode.BadRequest)
                {
                    TempData["emailconfirmed"] = false;
                    TempData["emailconfirmederror"] = result.Errors.FirstOrDefault().Value;
                    return RedirectToAction("Index", "Home");
                }


                TempData["emailconfirmed"] = true;
                return RedirectToAction("Index", "Home");

            }
            else
            {
                TempData["emailconfirmed"] = false;
                TempData["emailconfirmederror"] = "Xeta bas verdi.";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}

