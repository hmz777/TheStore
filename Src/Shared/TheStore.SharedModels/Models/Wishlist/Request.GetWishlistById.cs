using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.Wishlist
{
    [DisplayName("Wishlist." + nameof(GetWishlistByIdRequest))]
    public class GetWishlistByIdRequest : RequestBase
    {
        public const string RouteName = "Wishlists.Id";
        public const string RouteTemplate = "wishlist/{WishlistId}";
        public override string Route => RouteTemplate.Replace("{WishlistId}", WishlistId.ToString());

        [FromRoute(Name = nameof(WishlistId))]
        public Guid WishlistId { get; set; }
    }
}