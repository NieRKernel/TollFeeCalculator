using Toll;
using TollFeeCalculator;

var tollDates = new TollDates();
var townPerTimeIntervalCost = new TownPerTimeIntervalCost();

var tollCalculator = new TollCalculator(tollDates, townPerTimeIntervalCost);

// Normally I would create a test project, but for simplicity we test it here unconvetionally with prints to console
var tollFreeDate1 = DateTime.Parse("2013-01-01 06:25:00"); // New years
var tollFreeDate2 = DateTime.Parse("2013-03-29 08:20:00"); // Easter
var tollFreeDate3 = DateTime.Parse("2013-02-23 13:00:00"); // Saturday
var tollFreeDate4 = DateTime.Parse("2013-02-24 13:00:00"); // Sunday
var tollFreeDate5 = DateTime.Parse("2013-07-15 12:00:00"); // July free
var tollFreeDate6 = DateTime.Parse("2013-07-01 08:00:00"); // July free
var tollFreeDate7 = DateTime.Parse("2013-07-31 16:00:00"); // July free

var tollDateWithInSameHour1 = DateTime.Parse("2013-09-09 06:00:00"); // 8
var tollDateWithInSameHour2 = DateTime.Parse("2013-09-09 06:25:00"); // 8
var tollDateWithInSameHour3 = DateTime.Parse("2013-09-09 06:30:00"); // 13
var tollDateWithInSameHour4 = DateTime.Parse("2013-09-09 06:45:00"); // 13
var tollDateWithInSameHour5 = DateTime.Parse("2013-09-09 06:59:00"); // 13

var tollDateWithInSameHourAndNextHour1 = DateTime.Parse("2013-09-09 06:00:00"); // 8
var tollDateWithInSameHourAndNextHour2 = DateTime.Parse("2013-09-09 06:25:00"); // 8
var tollDateWithInSameHourAndNextHour3 = DateTime.Parse("2013-09-09 06:30:00"); // 13
var tollDateWithInSameHourAndNextHour4 = DateTime.Parse("2013-09-09 07:10:00"); // 18
var tollDateWithInSameHourAndNextHour5 = DateTime.Parse("2013-09-09 07:30:00"); // 18
var tollDateWithInSameHourAndNextHour6 = DateTime.Parse("2013-09-09 08:10:00"); // 13

var noMoreThan60TotalCostInADay1 = DateTime.Parse("2013-09-09 06:00:00"); // 8
var noMoreThan60TotalCostInADay2 = DateTime.Parse("2013-09-09 06:25:00"); // 8
var noMoreThan60TotalCostInADay3 = DateTime.Parse("2013-09-09 06:30:00"); // 13
var noMoreThan60TotalCostInADay4 = DateTime.Parse("2013-09-09 07:10:00"); // 18
var noMoreThan60TotalCostInADay5 = DateTime.Parse("2013-09-09 07:30:00"); // 18
var noMoreThan60TotalCostInADay6 = DateTime.Parse("2013-09-09 08:10:00"); // 13
var noMoreThan60TotalCostInADay7 = DateTime.Parse("2013-09-09 09:12:00"); // 8
var noMoreThan60TotalCostInADay8 = DateTime.Parse("2013-09-09 10:15:00"); // 8
var noMoreThan60TotalCostInADay9 = DateTime.Parse("2013-09-09 15:00:00"); // 13
var noMoreThan60TotalCostInADay10 = DateTime.Parse("2013-09-09 16:10:00"); // 18
var noMoreThan60TotalCostInADay11 = DateTime.Parse("2013-09-09 17:15:00"); // 13

var listTollFreeDates = new List<DateTime> { tollFreeDate1, tollFreeDate2, tollFreeDate3, tollFreeDate4, tollFreeDate5, tollFreeDate6, tollFreeDate7 };
var listTollDateWithInSameHour = new List<DateTime> { tollDateWithInSameHour1, tollDateWithInSameHour2, tollDateWithInSameHour3, tollDateWithInSameHour4, tollDateWithInSameHour5 };
var listTollDateWithInSameHourAndNextHour = new List<DateTime>
{ 
    tollDateWithInSameHourAndNextHour1, tollDateWithInSameHourAndNextHour2, tollDateWithInSameHourAndNextHour3,
    tollDateWithInSameHourAndNextHour4, tollDateWithInSameHourAndNextHour5, tollDateWithInSameHourAndNextHour6
};
var listNoMoreThan60TotalCostInADay = new List<DateTime>
{
    noMoreThan60TotalCostInADay1 , noMoreThan60TotalCostInADay2, noMoreThan60TotalCostInADay3,
    noMoreThan60TotalCostInADay4, noMoreThan60TotalCostInADay5, noMoreThan60TotalCostInADay6,
    noMoreThan60TotalCostInADay7, noMoreThan60TotalCostInADay8, noMoreThan60TotalCostInADay9,
    noMoreThan60TotalCostInADay10, noMoreThan60TotalCostInADay11
};


var car = new Car();
var bike = new Motorbike();

// Test category bike
foreach (var freeDate in listTollFreeDates)
{
    Console.WriteLine(tollCalculator.GetTollFee(bike, [freeDate]) + " Bike"); // 0
}
Console.WriteLine(tollCalculator.GetTollFee(bike, listTollFreeDates.ToArray()) + " Bike"); // 0
Console.WriteLine(tollCalculator.GetTollFee(bike, listTollDateWithInSameHour.ToArray()) + " Bike"); // 0
Console.WriteLine(tollCalculator.GetTollFee(bike, listTollDateWithInSameHourAndNextHour.ToArray()) + " Bike"); // 0
Console.WriteLine(tollCalculator.GetTollFee(bike, listNoMoreThan60TotalCostInADay.ToArray()) + " Bike"); // 0


// Test category car
foreach (var freeDate in listTollFreeDates)
{
    Console.WriteLine(tollCalculator.GetTollFee(car, [freeDate]) + " Car"); // 0
}
Console.WriteLine(tollCalculator.GetTollFee(car, listTollDateWithInSameHour.ToArray()) + " Car"); // 13
Console.WriteLine(tollCalculator.GetTollFee(car, listTollDateWithInSameHourAndNextHour.ToArray()) + " Car"); // 13 + 18 = 31
Console.WriteLine(tollCalculator.GetTollFee(car, listNoMoreThan60TotalCostInADay.ToArray()) + " Car"); // 60
