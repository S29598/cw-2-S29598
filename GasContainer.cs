using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GasContainer : Container, IHazardNotifier { 
    public double Pressure { get; private set; }

    public GasContainer(double pressure) : base ("G") {
        Pressure = pressure;
        Height = 259;
        Width = 244;
        Depth = 606;
        TareWeight = 4000;
        MaxPayload = 26000;
    }

    public override void Load(double weight)
    {
        if (weight > MaxPayload) {
            NotifyHazard($"Próba przeładowania kontenera {SerialNumber}");
            throw new OverfillException("Waga przewyższa maksymalną ładowność.");
        }
        base.Load(weight);
    }

    public override void Unload() {
        CargoWeight *= 0.05;
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"Powiadomienie o niebezpieczeństwie dla {SerialNumber} : {message}");
    }

    public override string ToString() {
        return base.ToString() + $", Ciśnienie = {Pressure}";
    }
}
