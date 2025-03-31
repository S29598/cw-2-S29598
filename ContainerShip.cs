using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ContainerShip {
    public string Name { get; set; }
    public double MaxSpeed { get; set; }
    public int MaxContainerCapacity { get; set; }
    public double MaxWeightCapacity { get; set; }
    private List<Container> containers;

    public ContainerShip(string name, double maxSpeed, int maxContainerCapacity, double maxWeightCapacity) { 
        Name = name;
        MaxSpeed = maxSpeed;
        MaxContainerCapacity = maxContainerCapacity;
        MaxWeightCapacity = maxWeightCapacity;
        containers = new List<Container>();
    }

    public bool AddContainer(Container container) {
        if (containers.Count >= MaxContainerCapacity) {
            throw new OverfillException("Nie można dodać kolejnego kontenera. Statek jest zapelniony.");
        }

        double totalWeight = containers.Sum(c => c.CargoWeight + c.TareWeight) + container.CargoWeight + container.TareWeight;

        if (totalWeight > MaxWeightCapacity) { 
            throw new OverfillException("Nie można dodać kolejnego kontenera. Przewyższyłoby to maksymalną ładowność statku.");
        }

        containers.Add(container);
        return true;
    }

    public bool RemoveContainer(string serialNumber) { 
        Container containerToRemove = containers.FirstOrDefault(c => c.SerialNumber == serialNumber);

        if (containerToRemove != null) { 
            return containers.Remove(containerToRemove);
        }
        return false;
    }

    public Container GetContainer(string serialNumber) {
        return containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
    }

    public List<Container> GetAllContainers() { 
        return new List<Container>(containers);
    }

    public double GetTotalWeight() { 
        return containers.Sum(c => c.CargoWeight + c.TareWeight);
    }

    public void AddContainers(List<Container> containersToAdd) {
        foreach (var container in containersToAdd) { 
            AddContainer(container);
        }
    }

    public bool ReplaceContainer(string oldContainerSerialNumber, Container container, Container newContainer) { 
        int index = containers.FindIndex(c => c.SerialNumber == oldContainerSerialNumber);

        if (index != -1) {
            double weightDifference = (newContainer.CargoWeight + newContainer.TareWeight) - (containers[index].CargoWeight + containers[index].TareWeight);

            if (GetTotalWeight() + weightDifference > MaxWeightCapacity) {
                throw new OverfillException("Podmiana tego kontenera spowoduje przeładowanie statku.");
            }

            containers[index] = newContainer;
            return true;
        }

        return false ;
    }

    public static bool TransferContainer(ContainerShip sourceShip, ContainerShip destinationShip, string containerSerialNumber) { 
        Container containerToTransfer = sourceShip.GetContainer(containerSerialNumber);

        if (containerToTransfer != null) {
            if (sourceShip.RemoveContainer(containerSerialNumber)) {
                try {
                    destinationShip.AddContainer(containerToTransfer);
                    return true;
                }
                catch(OverfillException) {
                    sourceShip.AddContainer(containerToTransfer);
                    throw;
                }
            }
        }

        return false ;
    }

    public string GetShipInfo() { 
        return $"Statek : {Name}\n" +
            $"Maksymalna prędkość : {MaxSpeed} węzłów\n" +
            $"Maksymalna pojemność kontenera : {MaxContainerCapacity}\n" +
            $"Maksymalny załadunek kontenera : {MaxWeightCapacity}\n" +
            $"Aktualna ilość kontenerów : {containers.Count}\n" +
            $"Aktualna waga ładunków : {GetTotalWeight()} kg";
    }

    public string GetContainerList() {
        if (containers.Count == 0)
            return "Brak kontenerów na pokładzie.";

        return string.Join("\n", containers.Select(c => c.ToString()));
    }

    public string GetContainerInfo(string serialNumber) {
        var container = GetContainer(serialNumber);
        if (container == null)
            return "Nie znaleziono kontenera.";

        return container.ToString();
    }
}
