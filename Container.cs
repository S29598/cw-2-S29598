using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Container : IContainer
    {
        public double CargoWeight { get; set; }
        public double Height { get; protected set; }
        public double Depth { get; protected set; }
        public double Width { get; protected set; }
        public double TareWeight { get; protected set; }
        public double MaxPayload { get; protected set; }
        public string SerialNumber { get; protected set; }

        protected static int serialNumberCounter = 1;

    protected Container(string containerType) { 
        SerialNumber = $"KON-{containerType}-{serialNumberCounter++}";
    }

        public virtual void Load(double weight)
            {
                if (weight > MaxPayload)
                    throw new OverfillException("Waga przewyższa maksymalną ładowność.");

                CargoWeight = weight;
        
        }

        public virtual void Unload()
        {
        CargoWeight = 0;
        }

    public override string ToString()
    {
        return $"Kontener {SerialNumber}: Typ : {GetType().Name}, Waga towaru : {CargoWeight} kg, Maksymalne załadowanie : {MaxPayload} kg";
    }
}

