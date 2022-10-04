using UnityEngine;

namespace ShopTown.Data
{
public class UpgradeRowData : ScriptableObject
{
    public string[] FirstLevelNames =
    {
        "Bouquets",
        "Delivery",
        "Takeaway",
        "Homemade Candy",
        "Milk Shake",
        "Cakes",
        "Steaks",
        "Delivery",
        "Combo Sets",
        "Cocktails",
        "Wine Card",
        "Skin Care",
        "Fashion Bags",
        "Women's Shoes",
        "Stylish Dresses",
        "Gift Box",
        "Bestsellers",
        "Antique Instruments",
        "Delivery",
        "Teddy Bears",
        "Stylish Sofas",
        "Puzzles",
        "Balls",
        "Modern Equipments",
        "Necklaces",
        "Tech Support",
        "Brand Watches"
    };

    public string[] SecondLevelNames =
    {
        "Ornamental Plants",
        "Quality Control",
        "New Varieties of Coffee",
        "Aromatic Tea",
        "New Ice Cream Machine",
        "Modern Equipments",
        "Delivery",
        "24/7",
        "BBQ",
        "Snacks",
        "Sea Food Dishes",
        "Brand Perfume",
        "Suitcases",
        "Shoe Repair Service",
        "Eco Fabrics",
        "Postcards and souvenirs",
        "E-books",
        "Musical Equipments",
        "Grooming Services",
        "Radio Control Toys",
        "Home Decor",
        "Video Games",
        "Sportswear",
        "Sports Programmes",
        "Jeweller",
        "Warranty Service",
        "Rolex"
    };

    public string[] ThirdLevelNames =
    {
        "Florist Services",
        "Exotic Fruits",
        "New Coffee Machine",
        "Colorful Decor",
        "Italian Gelato",
        "Professional Baker",
        "Butcher",
        "Children's Parties",
        "Online Ordering",
        "Bar Activities",
        "Service Staff",
        "Make Up Artist",
        "Wallets",
        "Accessories",
        "Tailoring Service",
        "Online Ordering",
        "Book Club",
        "Musical Evenings",
        "Veterinarian Services",
        "Equipment for Hobbies",
        "Lighting",
        "Board Games",
        "Sport Equipment",
        "Sauna",
        "Online Ordering",
        "YouTube Channel",
        "Repair Service"
    };

    public double[] MoneyBaseCost =
    {
        250000,
        400000,
        700000,
        1200000,
        1500000,
        2000000,
        5000000,
        10000000,
        15000000,
        20000000,
        30000000,
        40000000,
        100000000,
        500000000,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    };

    public double[] GoldBaseCost =
    {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        3,
        5,
        10,
        20,
        40,
        80,
        120,
        200,
        250,
        300,
        400,
        600,
        1000
    };
}
}
