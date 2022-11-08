using System;
using Data.Dto.Dtos;
using TextInterface.UI;

namespace TextInterface
{
    public static class EventManager
    {
        public delegate void RequestLogin(string code);
        public delegate void LoginFailed();
        public delegate void SuccessfulLogin(UserDto user);
        public delegate void ShowProductList();
        public delegate void RequestLogout();
        public delegate void SuccessfulLogout();
        public delegate void ProductSaved();
        public delegate void GoBack(Type requestor);
        public delegate void ShowProductDetails(ProductDto? product);

        public static event RequestLogin? OnRequestLogin;
        public static event LoginFailed? OnLoginFailed;
        public static event SuccessfulLogin? OnSuccessfulLogin;
        public static event ShowProductList? OnShowProductList;
        public static event RequestLogout? OnRequestLogout;
        public static event SuccessfulLogout? OnSuccessfulLogout;
        public static event ShowProductDetails? OnShowProductDetails;
        public static event ProductSaved? OnProductSaved;
        public static event GoBack? OnGoBack;

        public static void RaiseRequestLoginEvent(string? code)
        {
            if (!string.IsNullOrWhiteSpace(code)) {
                OnRequestLogin?.Invoke(code.Trim());
            }
        }

        public static void RaiseLoginFailedEvent()
        {
            OnLoginFailed?.Invoke();
        }

        public static void RaiseSuccessfulLoginEvent(UserDto user)
        {
            OnSuccessfulLogin?.Invoke(user);
        }

        public static void RaiseShowProductListEvent()
        {
            OnShowProductList?.Invoke();
        }

        public static void RaiseRequestLogoutEvent()
        {
            OnRequestLogout?.Invoke();
        }

        public static void RaiseSuccessfulLogoutEvent()
        {
            OnSuccessfulLogout?.Invoke();
        }

        public static void RaiseShowProductDetailsEvent(ProductDto? product)
        {
            OnShowProductDetails?.Invoke(product);
        }

        public static void RaiseProductSavedEvent()
        {
            OnProductSaved?.Invoke();
        }

        public static void RaiseGoBackEvent(ProductListWindow productListWindow)
        {
            OnGoBack?.Invoke(productListWindow.GetType());
        }
    }
}