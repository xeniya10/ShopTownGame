using ShopTown.ModelComponent;
using UnityEngine;

namespace ShopTown.Data
{
[CreateAssetMenu(fileName = "UpgradeData", menuName = "UpgradeData")]
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

    public MoneyModel[] BaseCost =
    {
        new MoneyModel(250000, Currency.Dollar),
        new MoneyModel(400000, Currency.Dollar),
        new MoneyModel(700000, Currency.Dollar),
        new MoneyModel(1200000, Currency.Dollar),
        new MoneyModel(1500000, Currency.Dollar),
        new MoneyModel(2000000, Currency.Dollar),
        new MoneyModel(5000000, Currency.Dollar),
        new MoneyModel(10000000, Currency.Dollar),
        new MoneyModel(15000000, Currency.Dollar),
        new MoneyModel(20000000, Currency.Dollar),
        new MoneyModel(30000000, Currency.Dollar),
        new MoneyModel(40000000, Currency.Dollar),
        new MoneyModel(100000000, Currency.Dollar),
        new MoneyModel(500000000, Currency.Dollar),
        new MoneyModel(3, Currency.Gold),
        new MoneyModel(5, Currency.Gold),
        new MoneyModel(10, Currency.Gold),
        new MoneyModel(20, Currency.Gold),
        new MoneyModel(40, Currency.Gold),
        new MoneyModel(80, Currency.Gold),
        new MoneyModel(120, Currency.Gold),
        new MoneyModel(200, Currency.Gold),
        new MoneyModel(250, Currency.Gold),
        new MoneyModel(300, Currency.Gold),
        new MoneyModel(400, Currency.Gold),
        new MoneyModel(600, Currency.Gold),
        new MoneyModel(1000, Currency.Gold)
    };
}
}
