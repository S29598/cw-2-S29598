using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConsoleInterface { 
    private List<ContainerShip> ships = new List<ContainerShip>();
    
    private List<Container> containers = new List<Container>();

    public void Run() { 
        bool exit = false;
        while (!exit) {
            DisplayMainMenu();
            string choice = Console.ReadLine();
            switch (choice) {
                case "1":
                    AddShip();
                    break;
                case "2":
                    AddContainer();
                    break;
                case "3":
                    LoadContainerToShip();
                    break;
                case "4":
                    UnloadContainerFromShip();
                    break;
                case "5":
                    DisplayShipInfo();
                    break;
                case "6":
                    DisplayContainerInfo();
                    break;
                case "7":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                    break;
            }
        }
    }

    private void DisplayMainMenu() {
        Console.Clear();
        Console.WriteLine("System zarządzania kontenerami");
        Console.WriteLine("1. Dodaj statek");
        Console.WriteLine("2. Dodaj kontener");
        Console.WriteLine("3. Załaduj kontener na statek");
        Console.WriteLine("4. Rozładuj kontener ze statku");
        Console.WriteLine("5. Wyświetl informacje o statku");
        Console.WriteLine("6. Wyświetl informacje o kontenerze");
        Console.WriteLine("7. Wyjście");
        Console.Write("Wybierz opcję: ");
    }

    private void AddShip() {
        Console.Write("Podaj nazwę statku: ");
        string name = Console.ReadLine();

        Console.Write("Podaj maksymalną prędkość (w węzłach): ");
        double maxSpeed = double.Parse(Console.ReadLine());

        Console.Write("Podaj maksymalną liczbę kontenerów: ");
        int maxContainers = int.Parse(Console.ReadLine());

        Console.Write("Podaj maksymalną ładowność (w tonach): ");
        double maxWeight = double.Parse(Console.ReadLine());

        ships.Add(new ContainerShip(name, maxSpeed, maxContainers, maxWeight * 1000));
        Console.WriteLine("Statek dodany pomyślnie.");
        Console.ReadKey();
    }

    private void AddContainer() { 
        Console.WriteLine("Wybierz typ kontenera:");
        Console.WriteLine("1. Kontener na płyny");
        Console.WriteLine("2. Kontener na gaz");
        Console.WriteLine("3. Kontener chłodniczy");
        string choice = Console.ReadLine();

        Container newContainer = null;

        switch (choice) {
            case "1":
                Console.Write("Czy kontener ma przewozić ładunek niebezpieczny? (tak/nie)");
                bool isHazardous = Console.ReadLine().ToLower() == "tak";
                newContainer = new LiquidContainer(isHazardous);
                break;
            case "2":
                Console.Write("Podaj ciśnienie (w atmosferach)");
                double pressure = double.Parse(Console.ReadLine());
                newContainer = new GasContainer(pressure);
                break;
            case "3":
                Console.Write("Podaj rodzaj produktu");
                string productType = Console.ReadLine();
                newContainer = new RefrigeratedContainer(productType);
                break;
            default:
                Console.WriteLine("Nieprawidłowy wybór.");
                return;
        }

        containers.Add(newContainer);
        Console.WriteLine($"Kontener {newContainer.SerialNumber} dodany pomyślnie.");
        Console.ReadKey();
    }

    private void LoadContainerToShip() { 
        if (ships.Count == 0 || containers.Count == 0) {
            Console.WriteLine("Brak statków lub kontenerów.");
            Console.ReadKey();
            return;
        }

        DisplayShips();
        Console.Write("Wybierz numer statku: ");
        int shipIndex = int.Parse(Console.ReadLine()) - 1;

        DisplayContainers();
        Console.Write("Wybierz numer kontenera: ");
        int containerIndex = int.Parse(Console.ReadLine()) - 1;

        try {
            ships[shipIndex].AddContainer(containers[containerIndex]);
            containers.RemoveAt(containerIndex);
            Console.WriteLine("Kontener załadowany na statek.");
        }
        catch(Exception e) {
            Console.WriteLine($"Błąd: {e.Message}");
        }
        Console.ReadKey();
    }

    private void UnloadContainerFromShip() {
        if (ships.Count == 0){
            Console.WriteLine("Brak statków.");
            Console.ReadKey();
            return;
        }

        DisplayShips();
        Console.Write("Wybierz numer statku: ");
        int shipIndex = int.Parse(Console.ReadLine()) - 1;

        var shipContainers = ships[shipIndex].GetAllContainers();
        if (shipContainers.Count == 0) {
            Console.WriteLine("Brak kontenerów na statku.");
            Console.ReadKey(); 
            return;
        }

        for (int i = 0; i < shipContainers.Count; i++) {
            Console.WriteLine($"{i + 1}.{shipContainers[i]}");
        }

        Console.Write("Wybierz numer kontenera do rozładunku: ");
        int containerIndex = int.Parse(Console.ReadLine()) - 1;

        var containerToUnload = shipContainers[containerIndex];

        if 
            (ships[shipIndex].RemoveContainer(containerToUnload.SerialNumber)) {
                containers.Add(containerToUnload);
                Console.WriteLine("Kontener rozładowany ze statku.");
            }
        else {
            Console.WriteLine("Nie udało się rozładować kontenera.");
        }
        Console.ReadKey();
    }

    private void DisplayShipInfo() {
        if (ships.Count == 0) { 
            Console.WriteLine("Brak statków.");
            Console.ReadKey();
            return;
        }

        DisplayShips();
        Console.Write("Wybierz numer statku : ");
        int shipIndex = int.Parse(Console.ReadLine()) - 1;

        Console.WriteLine(ships[shipIndex].GetShipInfo());
        Console.WriteLine(ships[shipIndex].GetContainerList());
        Console.ReadKey();
    }

    private void DisplayContainerInfo() {
        if (containers.Count == 0) { 
            Console.WriteLine("Brak kontenerów.");
            Console.ReadKey();
            return;
        }

        DisplayContainers();
        Console.Write("Wybierz numer kontenera: ");
        int containerIndex = int.Parse(Console.ReadLine()) - 1;

        Console.WriteLine(containers[containerIndex]);
        Console.ReadKey();
    }

    private void DisplayShips() {
        for (int i = 0; i < ships.Count; i++) {
            Console.WriteLine($"{i + 1}.{ships[i]}");
        }
    }

    private void DisplayContainers(){
        if (containers.Count == 0) {
            Console.WriteLine("Brak kontenerów.");
            return;
        }

        Console.WriteLine("Lista kontenerów: ");
        for (int i = 0; i < containers.Count; i++) { 
            Console.WriteLine($"{i + 1}.{containers[i]}");
        }
    }
}