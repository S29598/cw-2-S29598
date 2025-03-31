using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RefrigeratedContainer : Container { 
    public string ProductType { get; private set; }
    public double Temperature { get; private set; }

    private static Dictionary<string, double> ProductTemperatures = new Dictionary<string, double>{
        {"Bananas", 13.3},
        {"Chocolate", 18},
        {"Fish", 2},
        {"Meat", -15},
        {"Ice cream", -18},
        {"Frozen pizza", -30},
        {"Cheese", 7.2},
        {"Sausages", 5},
        {"Butter", 20.5},
        {"Eggs", 19}
    };

    public RefrigeratedContainer(string productType) : base("C") {
        Height = 259;
        Width = 244;
        Depth = 1219;
        TareWeight = 4500;
        MaxPayload = 27500;
        SetProductType(productType);
    }

    private void SetProductType(string productType) {
        if (!ProductTemperatures.ContainsKey(productType))
            throw new ArgumentException("Niepoprawny typ produktu");

        ProductType = productType;
        Temperature = ProductTemperatures[productType];
    }

    public override void Load(double weight)
    {
        if (weight > MaxPayload)
            throw new OverfillException("Waga przewyższa maksymalną ładowność.");

        base.Load(weight);
    }

    public override string ToString()
    {
        return base.ToString() + $", Produkt : {ProductType}, Temperatura : {Temperature}°C";
    }
}
