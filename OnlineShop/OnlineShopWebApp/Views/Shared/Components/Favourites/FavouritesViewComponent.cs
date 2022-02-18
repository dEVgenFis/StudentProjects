using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApp.Views.Shared.Component.Favourites
{
    public class FavouritesViewComponent : ViewComponent
    {
        private readonly IFavouritesStorage favourites;
        public FavouritesViewComponent(IFavouritesStorage favourites)
        {
            this.favourites = favourites;
        }
        public IViewComponentResult Invoke()
        {
            var item = favourites.TryGetByUserId(Constants.UserId);
            return View("Favourites", item.Amount);
        }
    }
}