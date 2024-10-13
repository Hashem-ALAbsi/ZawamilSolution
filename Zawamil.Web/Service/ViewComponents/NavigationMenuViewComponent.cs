//using Microsoft.AspNetCore.Mvc;
//using Zawamil.Web.Services;

//namespace Zawamil.Web.Service.ViewComponents
//{
//    public class NavigationMenuViewComponent : ViewComponent
//    {
//        private readonly IDataAccessService _dataAccessService;

//        public NavigationMenuViewComponent(IDataAccessService dataAccessService)
//        {
//            _dataAccessService = dataAccessService;
//        }

//        public async Task<IViewComponentResult> InvokeAsync()
//        {
//            var items = await _dataAccessService.GetMenuItemsAsync(HttpContext.User);

//            return View(items);
//        }
//    }
//}