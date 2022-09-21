using System;
using System.Collections.Generic;
using VContainer.Unity;

public class DataController : IInitializable
{
    public GameDataModel GameData;

    public DataController(GameDataModel gameData)
    {
        GameData = gameData;
    }

    public void Initialize()
    {
        CreateDefaultGameData();
    }

    public void CreateDefaultGameData()
    {
        GameData = new GameDataModel
        {
            CurrentMoneyBalance = 2,
            CurrentGoldBalance = 0,
            TotalMoneyBalance = 0,
            TotalGoldBalance = 0,
            MaxOpenedLevel = 1,
            NumberOfLevels = 27,
            TimeStamp = DateTime.Now,

            Settings = new GameSettingModel()
            {
                Music = true,
                Sound = true,
                Notifications = true,
                Ads = true
            },

            GameBoardModel = new GameBoardModel()
            {
                Rows = 4,
                Columns = 3,
            },

            Businesses = new List<GameCellModel>()
            {
                new GameCellModel
                {
                    Name = "Flower shop",
                    Level = 1,
                    BaseCost = 2,
                    BaseProfit = 1,
                    TimerInSeconds = 2,
                },

                new GameCellModel
                {
                    Name = "Fruit shop",
                    Level = 2,
                    BaseCost = 12,
                    BaseProfit = 6,
                    TimerInSeconds = 5,
                },

                new GameCellModel
                {
                    Name = "Coffee",
                    Level = 3,
                    BaseCost = 32,
                    BaseProfit = 14,
                    TimerInSeconds = 10,
                },

                new GameCellModel
                {
                    Name = "Sweets shop",
                    Level = 4,
                    BaseCost = 60,
                    BaseProfit = 25,
                    TimerInSeconds = 20,
                },

                new GameCellModel
                {
                    Name = "Ice cream shop",
                    Level = 5,
                    BaseCost = 180,
                    BaseProfit = 80,
                    TimerInSeconds = 40,
                },

                new GameCellModel
                {
                    Name = "Bakery",
                    Level = 6,
                    BaseCost = 320,
                    BaseProfit = 140,
                    TimerInSeconds = 80,
                },

                new GameCellModel
                {
                    Name = "Butcher shop",
                    Level = 7,
                    BaseCost = 720,
                    BaseProfit = 300,
                    TimerInSeconds = 160,
                },

                new GameCellModel
                {
                    Name = "Pizza",
                    Level = 8,
                    BaseCost = 1440,
                    BaseProfit = 720,
                    TimerInSeconds = 210,
                },

                new GameCellModel
                {
                    Name = "Burger",
                    Level = 9,
                    BaseCost = 3860,
                    BaseProfit = 1930,
                    TimerInSeconds = 360,
                },

                new GameCellModel
                {
                    Name = "Bar",
                    Level = 10,
                    BaseCost = 8640,
                    BaseProfit = 4320,
                    TimerInSeconds = 480,
                },

                new GameCellModel
                {
                    Name = "Restaurant",
                    Level = 11,
                    BaseCost = 26340,
                    BaseProfit = 10320,
                    TimerInSeconds = 620,
                },

                new GameCellModel
                {
                    Name = "Beauty shop",
                    Level = 12,
                    BaseCost = 54640,
                    BaseProfit = 20420,
                    TimerInSeconds = 960,
                },

                new GameCellModel
                {
                    Name = "Beg shop",
                    Level = 13,
                    BaseCost = 103680,
                    BaseProfit = 51840,
                    TimerInSeconds = 1240,
                },

                new GameCellModel
                {
                    Name = "Shoe shop",
                    Level = 14,
                    BaseCost = 320340,
                    BaseProfit = 154080,
                    TimerInSeconds = 1510,
                },

                new GameCellModel
                {
                    Name = "Clothing shop",
                    Level = 15,
                    BaseCost = 780560,
                    BaseProfit = 390250,
                    TimerInSeconds = 1880,
                },

                new GameCellModel
                {
                    Name = "Gift shop",
                    Level = 16,
                    BaseCost = 1244160,
                    BaseProfit = 622030,
                    TimerInSeconds = 2300,
                },

                new GameCellModel
                {
                    Name = "Book shop",
                    Level = 17,
                    BaseCost = 4688020,
                    BaseProfit = 2644010,
                    TimerInSeconds = 2940,
                },

                new GameCellModel
                {
                    Name = "Music shop",
                    Level = 18,
                    BaseCost = 9824600,
                    BaseProfit = 4912300,
                    TimerInSeconds = 3660,
                },

                new GameCellModel
                {
                    Name = "Pet shop",
                    Level = 19,
                    BaseCost = 14929920,
                    BaseProfit = 7464000,
                    TimerInSeconds = 4320,
                },

                new GameCellModel
                {
                    Name = "Toy shop",
                    Level = 20,
                    BaseCost = 36246020,
                    BaseProfit = 18123010,
                    TimerInSeconds = 5450,
                },

                new GameCellModel
                {
                    Name = "Furniture shop",
                    Level = 21,
                    BaseCost = 84562000,
                    BaseProfit = 42281000,
                    TimerInSeconds = 6810,
                },

                new GameCellModel
                {
                    Name = "Game shop",
                    Level = 22,
                    BaseCost = 179159040,
                    BaseProfit = 89579520,
                    TimerInSeconds = 8020,
                },

                new GameCellModel
                {
                    Name = "Sport shop",
                    Level = 23,
                    BaseCost = 548000200,
                    BaseProfit = 294000100,
                    TimerInSeconds = 10000,
                },

                new GameCellModel
                {
                    Name = "Gym",
                    Level = 24,
                    BaseCost = 1040684000,
                    BaseProfit = 5203422000,
                    TimerInSeconds = 18000,
                },

                new GameCellModel
                {
                    Name = "Jew shop",
                    Level = 25,
                    BaseCost = 2149908480,
                    BaseProfit = 1074954240,
                    TimerInSeconds = 24000,
                },

                new GameCellModel
                {
                    Name = "Tech shop",
                    Level = 26,
                    BaseCost = 14744601820,
                    BaseProfit = 7372300910,
                    TimerInSeconds = 30000,
                },

                new GameCellModel
                {
                    Name = "Watch shop",
                    Level = 27,
                    BaseCost = 25798901760,
                    BaseProfit = 12899450880,
                    TimerInSeconds = 36000,
                }
            },

            Managers = new List<ManagerRowModel>(){
                new ManagerRowModel{
                    Level = 1,
                    BusinessName = "Flower shop",
                    Name = "Flora",
                    MoneyCost = 1000,
                    GoldCost = 0,
                },

                new ManagerRowModel{
                    Level = 2,
                    BusinessName = "Fruit shop",
                    Name = "Jackie",
                    MoneyCost = 5000,
                    GoldCost = 0,
                },

                new ManagerRowModel{
                    Level = 3,
                    BusinessName = "Coffee",
                    Name = "Ruby",
                    MoneyCost = 10000,
                    GoldCost = 0,
                },

                new ManagerRowModel{
                    Level = 4,
                    BusinessName = "Sweets shop",
                    Name = "Zoe",
                    MoneyCost = 30000,
                    GoldCost = 0,
                },

                new ManagerRowModel{
                    Level = 5,
                    BusinessName = "Ice cream shop",
                    Name = "Candice",
                    MoneyCost = 80000,
                    GoldCost = 0,
                },

                new ManagerRowModel{
                    Level = 6,
                    BusinessName = "Bakery",
                    Name = "Ann",
                    MoneyCost = 150000,
                    GoldCost = 0,
                },

                new ManagerRowModel{
                    Level = 7,
                    BusinessName = "Butcher shop",
                    Name = "Henry",
                    MoneyCost = 300000,
                    GoldCost = 0,
                },

                new ManagerRowModel{
                    Level = 8,
                    BusinessName = "Pizza",
                    Name = "Maggie",
                    MoneyCost = 600000,
                    GoldCost = 0,
                },

                new ManagerRowModel{
                    Level = 9,
                    BusinessName = "Burger",
                    Name = "Kevin",
                    MoneyCost = 900000,
                    GoldCost = 0,
                },

                new ManagerRowModel{
                    Level = 10,
                    BusinessName = "Bar",
                    Name = "Mike",
                    MoneyCost = 1500000,
                    GoldCost = 0,
                },

                new ManagerRowModel{
                    Level = 11,
                    BusinessName = "Restaurant",
                    Name = "Oliver",
                    MoneyCost = 5000000,
                    GoldCost = 0,
                },

                new ManagerRowModel{
                    Level = 12,
                    BusinessName = "Beauty shop",
                    Name = "Page",
                    MoneyCost = 10000000,
                    GoldCost = 0,
                },

                new ManagerRowModel{
                    Level = 13,
                    BusinessName = "Beg shop",
                    Name = "Emily",
                    MoneyCost = 50000000,
                    GoldCost = 0,
                },

                new ManagerRowModel{
                    Level = 14,
                    BusinessName = "Shoe shop",
                    Name = "Sally",
                    MoneyCost = 100000000,
                    GoldCost = 0,
                },

                new ManagerRowModel{
                    Level = 15,
                    BusinessName = "Clothing shop",
                    Name = "Nicole",
                    MoneyCost = 0,
                    GoldCost = 3,
                },

                new ManagerRowModel{
                    Level = 16,
                    BusinessName = "Gift shop",
                    Name = "Chloe",
                    MoneyCost = 0,
                    GoldCost = 5,
                },

                new ManagerRowModel{
                    Level = 17,
                    BusinessName = "Book shop",
                    Name = "Brian",
                    MoneyCost = 0,
                    GoldCost = 10,
                },

                new ManagerRowModel{
                    Level = 18,
                    BusinessName = "Music shop",
                    Name = "Carl",
                    MoneyCost = 0,
                    GoldCost = 20,
                },

                new ManagerRowModel{
                    Level = 19,
                    BusinessName = "Pet shop",
                    Name = "Melissa",
                    MoneyCost = 0,
                    GoldCost = 40,
                },

                new ManagerRowModel{
                    Level = 20,
                    BusinessName = "Toy shop",
                    Name = "Jesse",
                    MoneyCost = 0,
                    GoldCost = 80,
                },

                new ManagerRowModel{
                    Level = 21,
                    BusinessName = "Furniture shop",
                    Name = "Kevin",
                    MoneyCost = 0,
                    GoldCost = 120,
                },

                new ManagerRowModel{
                    Level = 22,
                    BusinessName = "Game shop",
                    Name = "Penny",
                    MoneyCost = 0,
                    GoldCost = 200,
                },

                new ManagerRowModel{
                    Level = 23,
                    BusinessName = "Sport shop",
                    Name = "Mark",
                    MoneyCost = 0,
                    GoldCost = 250,
                },

                new ManagerRowModel{
                    Level = 24,
                    BusinessName = "Gym",
                    Name = "Steve",
                    MoneyCost = 0,
                    GoldCost = 300,
                },

                new ManagerRowModel{
                    Level = 25,
                    BusinessName = "Jew shop",
                    Name = "Barbara",
                    MoneyCost = 0,
                    GoldCost = 400,
                },

                new ManagerRowModel{
                    Level = 26,
                    BusinessName = "Tech shop",
                    Name = "Diana",
                    MoneyCost = 0,
                    GoldCost = 600,
                },

                new ManagerRowModel{
                    Level = 27,
                    BusinessName = "Watch shop",
                    Name = "Irene",
                    MoneyCost = 0,
                    GoldCost = 1000,
                },
            },

            FirstUpgrades = new List<UpgradeRowModel>(){
                new UpgradeRowModel{
                    Level = 1,
                    BusinessName = "Flower shop",
                    UpgradeLevel = 1,
                    Name = "Bouquets",
                    MoneyCost = 250000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 2,
                    BusinessName = "Fruit shop",
                    UpgradeLevel = 1,
                    Name = "Delivery",
                    MoneyCost = 400000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 3,
                    BusinessName = "Coffee",
                    UpgradeLevel = 1,
                    Name = "Takeaway",
                    MoneyCost = 700000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 4,
                    BusinessName = "Sweets shop",
                    UpgradeLevel = 1,
                    Name = "Homemade Candy",
                    MoneyCost = 1200000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 5,
                    BusinessName = "Ice cream shop",
                    UpgradeLevel = 1,
                    Name = "Milk Shake",
                    MoneyCost = 1500000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 6,
                    BusinessName = "Bakery",
                    UpgradeLevel = 1,
                    Name = "Cakes",
                    MoneyCost = 2000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 7,
                    BusinessName = "Butcher shop",
                    UpgradeLevel = 1,
                    Name = "Steaks",
                    MoneyCost = 5000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 8,
                    BusinessName = "Pizza",
                    UpgradeLevel = 1,
                    Name = "Delivery",
                    MoneyCost = 10000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 9,
                    BusinessName = "Burger",
                    UpgradeLevel = 1,
                    Name = "Combo Sets",
                    MoneyCost = 15000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 10,
                    BusinessName = "Bar",
                    UpgradeLevel = 1,
                    Name = "Cocktails",
                    MoneyCost = 20000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 11,
                    BusinessName = "Restaurant",
                    UpgradeLevel = 1,
                    Name = "Wine Card",
                    MoneyCost = 30000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 12,
                    BusinessName = "Beauty shop",
                    UpgradeLevel = 1,
                    Name = "Skin Care",
                    MoneyCost = 40000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 13,
                    BusinessName = "Beg shop",
                    UpgradeLevel = 1,
                    Name = "Fashion Bags",
                    MoneyCost = 100000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 14,
                    BusinessName = "Shoe shop",
                    UpgradeLevel = 1,
                    Name = "Women's Shoes",
                    MoneyCost = 500000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 15,
                    BusinessName = "Clothing shop",
                    UpgradeLevel = 1,
                    Name = "Stylish Dresses",
                    MoneyCost = 0,
                    GoldCost = 3,
                },

                new UpgradeRowModel{
                    Level = 16,
                    BusinessName = "Gift shop",
                    UpgradeLevel = 1,
                    Name = "Gift Box",
                    MoneyCost = 0,
                    GoldCost = 5,
                },

                new UpgradeRowModel{
                    Level = 17,
                    BusinessName = "Book shop",
                    UpgradeLevel = 1,
                    Name = "Bestsellers",
                    MoneyCost = 0,
                    GoldCost = 10,
                },

                new UpgradeRowModel{
                    Level = 18,
                    BusinessName = "Music shop",
                    UpgradeLevel = 1,
                    Name = "Antique Instruments",
                    MoneyCost = 0,
                    GoldCost = 20,
                },

                new UpgradeRowModel{
                    Level = 19,
                    BusinessName = "Pet shop",
                    UpgradeLevel = 1,
                    Name = "Delivery",
                    MoneyCost = 0,
                    GoldCost = 40,
                },

                new UpgradeRowModel{
                    Level = 20,
                    BusinessName = "Toy shop",
                    UpgradeLevel = 1,
                    Name = "Teddy Bears",
                    MoneyCost = 0,
                    GoldCost = 80,
                },

                new UpgradeRowModel{
                    Level = 21,
                    BusinessName = "Furniture shop",
                    UpgradeLevel = 1,
                    Name = "Stylish Sofas",
                    MoneyCost = 0,
                    GoldCost = 120,
                },

                new UpgradeRowModel{
                    Level = 22,
                    BusinessName = "Game shop",
                    UpgradeLevel = 1,
                    Name = "Puzzles",
                    MoneyCost = 0,
                    GoldCost = 200,
                },

                new UpgradeRowModel{
                    Level = 23,
                    BusinessName = "Sport shop",
                    UpgradeLevel = 1,
                    Name = "Balls",
                    MoneyCost = 0,
                    GoldCost = 250,
                },

                new UpgradeRowModel{
                    Level = 24,
                    BusinessName = "Gym",
                    UpgradeLevel = 1,
                    Name = "Modern Equipments",
                    MoneyCost = 0,
                    GoldCost = 300,
                },

                new UpgradeRowModel{
                    Level = 25,
                    BusinessName = "Jew shop",
                    UpgradeLevel = 1,
                    Name = "Necklaces",
                    MoneyCost = 0,
                    GoldCost = 400,
                },

                new UpgradeRowModel{
                    Level = 26,
                    BusinessName = "Tech shop",
                    UpgradeLevel = 1,
                    Name = "Tech Support",
                    MoneyCost = 0,
                    GoldCost = 600,
                },

                new UpgradeRowModel{
                    Level = 27,
                    BusinessName = "Watch shop",
                    UpgradeLevel = 1,
                    Name = "Brand Watches",
                    MoneyCost = 0,
                    GoldCost = 1000,
                }
            },

            SecondUpgrades = new List<UpgradeRowModel>(){
                new UpgradeRowModel{
                    Level = 1,
                    BusinessName = "Flower shop",
                    UpgradeLevel = 2,
                    Name = "Ornamental Plants",
                    MoneyCost = 500000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 2,
                    BusinessName = "Fruit shop",
                    UpgradeLevel = 2,
                    Name = "Quality Control",
                    MoneyCost = 800000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 3,
                    BusinessName = "Coffee",
                    UpgradeLevel = 2,
                    Name = "New Varieties of Coffee",
                    MoneyCost = 1400000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 4,
                    BusinessName = "Sweets shop",
                    UpgradeLevel = 2,
                    Name = "Aromatic Tea",
                    MoneyCost = 2400000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 5,
                    BusinessName = "Ice cream shop",
                    UpgradeLevel = 2,
                    Name = "New Ice Cream Machine",
                    MoneyCost = 3000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 6,
                    BusinessName = "Bakery",
                    UpgradeLevel = 2,
                    Name = "Modern Equipments",
                    MoneyCost = 4000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 7,
                    BusinessName = "Butcher shop",
                    UpgradeLevel = 2,
                    Name = "Delivery",
                    MoneyCost = 10000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 8,
                    BusinessName = "Pizza",
                    UpgradeLevel = 2,
                    Name = "24/7",
                    MoneyCost = 20000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 9,
                    BusinessName = "Burger",
                    UpgradeLevel = 2,
                    Name = "BBQ",
                    MoneyCost = 30000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 10,
                    BusinessName = "Bar",
                    UpgradeLevel = 2,
                    Name = "Snacks",
                    MoneyCost = 40000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 11,
                    BusinessName = "Restaurant",
                    UpgradeLevel = 2,
                    Name = "Sea Food Dishes",
                    MoneyCost = 60000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 12,
                    BusinessName = "Beauty shop",
                    UpgradeLevel = 2,
                    Name = "Brand Parfume",
                    MoneyCost = 80000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 13,
                    BusinessName = "Beg shop",
                    UpgradeLevel = 2,
                    Name = "Suitcases",
                    MoneyCost = 200000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 14,
                    BusinessName = "Shoe shop",
                    UpgradeLevel = 2,
                    Name = "Shoe Repair Service",
                    MoneyCost = 0,
                    GoldCost = 3,
                },

                new UpgradeRowModel{
                    Level = 15,
                    BusinessName = "Clothing shop",
                    UpgradeLevel = 2,
                    Name = "Eco Fabrics",
                    MoneyCost = 0,
                    GoldCost = 6,
                },

                new UpgradeRowModel{
                    Level = 16,
                    BusinessName = "Gift shop",
                    UpgradeLevel = 2,
                    Name = "Postcards and souvenirs",
                    MoneyCost = 0,
                    GoldCost = 10,
                },

                new UpgradeRowModel{
                    Level = 17,
                    BusinessName = "Book shop",
                    UpgradeLevel = 2,
                    Name = "E-books",
                    MoneyCost = 0,
                    GoldCost = 20,
                },

                new UpgradeRowModel{
                    Level = 18,
                    BusinessName = "Music shop",
                    UpgradeLevel = 2,
                    Name = "Musical Equipments",
                    MoneyCost = 0,
                    GoldCost = 40,
                },

                new UpgradeRowModel{
                    Level = 19,
                    BusinessName = "Pet shop",
                    UpgradeLevel = 2,
                    Name = "Grooming Services",
                    MoneyCost = 0,
                    GoldCost = 80,
                },

                new UpgradeRowModel{
                    Level = 20,
                    BusinessName = "Toy shop",
                    UpgradeLevel = 2,
                    Name = "Radio Control Toys",
                    MoneyCost = 0,
                    GoldCost = 160,
                },

                new UpgradeRowModel{
                    Level = 21,
                    BusinessName = "Furniture shop",
                    UpgradeLevel = 2,
                    Name = "Home Decor",
                    MoneyCost = 0,
                    GoldCost = 240,
                },

                new UpgradeRowModel{
                    Level = 22,
                    BusinessName = "Game shop",
                    UpgradeLevel = 2,
                    Name = "Video Games",
                    MoneyCost = 0,
                    GoldCost = 400,
                },

                new UpgradeRowModel{
                    Level = 23,
                    BusinessName = "Sport shop",
                    UpgradeLevel = 2,
                    Name = "Sportswears",
                    MoneyCost = 0,
                    GoldCost = 500,
                },

                new UpgradeRowModel{
                    Level = 24,
                    BusinessName = "Gym",
                    UpgradeLevel = 2,
                    Name = "Sports Programmes",
                    MoneyCost = 0,
                    GoldCost = 600,
                },

                new UpgradeRowModel{
                    Level = 25,
                    BusinessName = "Jew shop",
                    UpgradeLevel = 2,
                    Name = "Jeweller",
                    MoneyCost = 0,
                    GoldCost = 800,
                },

                new UpgradeRowModel{
                    Level = 26,
                    BusinessName = "Tech shop",
                    UpgradeLevel = 2,
                    Name = "Warranty Service",
                    MoneyCost = 0,
                    GoldCost = 1200,
                },

                new UpgradeRowModel{
                    Level = 27,
                    BusinessName = "Watch shop",
                    UpgradeLevel = 2,
                    Name = "Rolex",
                    MoneyCost = 0,
                    GoldCost = 2000,
                }
            },

            ThirdUpgrades = new List<UpgradeRowModel>(){
                new UpgradeRowModel{
                    Level = 1,
                    BusinessName = "Flower shop",
                    UpgradeLevel = 3,
                    Name = "Florist Services",
                    MoneyCost = 750000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 2,
                    BusinessName = "Fruit shop",
                    UpgradeLevel = 3,
                    Name = "Exotic Fruits",
                    MoneyCost = 1200000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 3,
                    BusinessName = "Coffee",
                    UpgradeLevel = 3,
                    Name = "New Coffee Machine",
                    MoneyCost = 2100000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 4,
                    BusinessName = "Sweets shop",
                    UpgradeLevel = 3,
                    Name = "Colorful Decor",
                    MoneyCost = 3600000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 5,
                    BusinessName = "Ice cream shop",
                    UpgradeLevel = 3,
                    Name = "Italian Gelato",
                    MoneyCost = 4500000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 6,
                    BusinessName = "Bakery",
                    UpgradeLevel = 3,
                    Name = "Professional Baker",
                    MoneyCost = 6000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 7,
                    BusinessName = "Butcher shop",
                    UpgradeLevel = 3,
                    Name = "Butcher",
                    MoneyCost = 15000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 8,
                    BusinessName = "Pizza",
                    UpgradeLevel = 3,
                    Name = "Children's Parties",
                    MoneyCost = 30000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 9,
                    BusinessName = "Burger",
                    UpgradeLevel = 3,
                    Name = "Online Ordering",
                    MoneyCost = 45000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 10,
                    BusinessName = "Bar",
                    UpgradeLevel = 3,
                    Name = "Bar Activities",
                    MoneyCost = 60000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 11,
                    BusinessName = "Restaurant",
                    UpgradeLevel = 3,
                    Name = "Service Staff",
                    MoneyCost = 90000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 12,
                    BusinessName = "Beauty shop",
                    UpgradeLevel = 3,
                    Name = "Make Up Artist",
                    MoneyCost = 120000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 13,
                    BusinessName = "Beg shop",
                    UpgradeLevel = 3,
                    Name = "Wallets",
                    MoneyCost = 300000000,
                    GoldCost = 0,
                },

                new UpgradeRowModel{
                    Level = 14,
                    BusinessName = "Shoe shop",
                    UpgradeLevel = 3,
                    Name = "Accessories",
                    MoneyCost = 0,
                    GoldCost = 9,
                },

                new UpgradeRowModel{
                    Level = 15,
                    BusinessName = "Clothing shop",
                    UpgradeLevel = 3,
                    Name = "Tailoring Service",
                    MoneyCost = 0,
                    GoldCost = 15,
                },

                new UpgradeRowModel{
                    Level = 16,
                    BusinessName = "Gift shop",
                    UpgradeLevel = 3,
                    Name = "Online Ordering",
                    MoneyCost = 0,
                    GoldCost = 30,
                },

                new UpgradeRowModel{
                    Level = 17,
                    BusinessName = "Book shop",
                    UpgradeLevel =3,
                    Name = "Book Club",
                    MoneyCost = 0,
                    GoldCost = 60,
                },

                new UpgradeRowModel{
                    Level = 18,
                    BusinessName = "Music shop",
                    UpgradeLevel = 3,
                    Name = "Musical Evenings",
                    MoneyCost = 0,
                    GoldCost = 120,
                },

                new UpgradeRowModel{
                    Level = 19,
                    BusinessName = "Pet shop",
                    UpgradeLevel = 3,
                    Name = "Veterinarian Services",
                    MoneyCost = 0,
                    GoldCost = 240,
                },

                new UpgradeRowModel{
                    Level = 20,
                    BusinessName = "Toy shop",
                    UpgradeLevel = 3,
                    Name = "Equipment for Hobbies",
                    MoneyCost = 0,
                    GoldCost = 360,
                },

                new UpgradeRowModel{
                    Level = 21,
                    BusinessName = "Furniture shop",
                    UpgradeLevel = 3,
                    Name = "Lighting",
                    MoneyCost = 0,
                    GoldCost = 480,
                },

                new UpgradeRowModel{
                    Level = 22,
                    BusinessName = "Game shop",
                    UpgradeLevel = 3,
                    Name = "Board Games",
                    MoneyCost = 0,
                    GoldCost = 600,
                },

                new UpgradeRowModel{
                    Level = 23,
                    BusinessName = "Sport shop",
                    UpgradeLevel = 3,
                    Name = "Sport Equipment",
                    MoneyCost = 0,
                    GoldCost = 750,
                },

                new UpgradeRowModel{
                    Level = 24,
                    BusinessName = "Gym",
                    UpgradeLevel = 3,
                    Name = "Sauna",
                    MoneyCost = 0,
                    GoldCost = 900,
                },

                new UpgradeRowModel{
                    Level = 25,
                    BusinessName = "Jew shop",
                    UpgradeLevel = 3,
                    Name = "Online Ordering",
                    MoneyCost = 0,
                    GoldCost = 1200,
                },

                new UpgradeRowModel{
                    Level = 26,
                    BusinessName = "Tech shop",
                    UpgradeLevel = 3,
                    Name = "YouTube Channel",
                    MoneyCost = 0,
                    GoldCost = 1800,
                },

                new UpgradeRowModel{
                    Level = 27,
                    BusinessName = "Watch shop",
                    UpgradeLevel = 3,
                    Name = "Repair Service",
                    MoneyCost = 0,
                    GoldCost = 3000,
                }
            },
        };
    }
}