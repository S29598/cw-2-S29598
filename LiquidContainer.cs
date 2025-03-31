using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LiquidContainer : Container { 
    
    public bool IsHazardous { get; private set; }
    
    public LiquidContainer(bool isHazardous) : base("L") {
        Height = 259;
        Width = 244;
        Depth = 606;
        TareWeight = 4000;
        MaxPayload = 26000;
        IsHazardous = isHazardous;
    }

    public override void Load(double weight) {
        double maxFillPercentage = IsHazardous ? 0.5 : 0.9;
        if (weight > MaxPayload * maxFillPercentage) {
            NotifyHazard($"Próba przelania kontenera {SerialNumber}");
            throw new OverfillException("Kontenery na płyny mogą być wypełnione do 90%.");
        }

        base.Load(weight);
    }

    public void Unload()
    {
        CargoWeight = 0;
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"Powiadomienie o niebezpieczeństwie dla {SerialNumber} : {message}");
    }

    public override string ToString()
    {
        return base.ToString() + $", Niebezpieczny = {IsHazardous}";
    }
}
