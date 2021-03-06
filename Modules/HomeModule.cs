using Nancy;
using Carlist.Objects;
using System.Collections.Generic;
using System;

namespace CarDealer
{
  public class HomeModule : NancyModule
{
    public HomeModule()
    {
      Get["/"] = _ =>
      {
        Console.WriteLine("Home Directory");
        List<Cars> car = Cars.ViewCars();
        return View["view_all_cars.cshtml", car];
      };
      Get["/view_all_cars"] = _ =>
      {
        List<Cars> car = Cars.ViewCars();
        return View["view_all_cars.cshtml", car];
      };
      Post["/car_added"] = _ =>
      {
        int carYear;
         int.TryParse(Request.Form["new-year"], out carYear);
        string carMake = Request.Form["new-make"];
        string carModel = Request.Form["new-model"];
        int carPrice;
          int.TryParse(Request.Form["new-price"], out carPrice);
        int carMiles;
          int.TryParse(Request.Form["new-miles"], out carMiles);

          Cars newCar = new Cars(carYear, carMake, carModel, carPrice, carMiles);
          newCar.AddCars();
          return View["/car_added.cshtml", newCar];
      };
      Post["/cars_cleared"] = _ => {
        Cars.DeleteAll();
        return View["cars_cleared.cshtml"];
      };
      Get["/search_cars"] = _ => View["search_cars.cshtml"];
      Post["/view_result"] = _ =>
      {
        int maxPrice;
        int.TryParse(Request.Form["max-price"], out maxPrice);
        int maxMiles;
        int.TryParse(Request.Form["max-miles"], out maxMiles);
        List<Cars> results = Cars.searchCars(maxPrice, maxMiles);
        return View["view_result.cshtml", results];
      };
    }
  }
}
