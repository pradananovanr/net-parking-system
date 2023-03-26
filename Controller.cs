public enum VehicleType
{
    Motor,
    Mobil
}

public class Vehicle
{
    public string RegistrationNumber { get; set; } = "";
    public string Color { get; set; } = "";
    public VehicleType Type { get; set; }
    public DateTime EntryTime { get; set; }
}

public class ParkingLot
{
    public int Capacity { get; set; }
    public int AvailableLots { get; set; }
    public Dictionary<int, Vehicle?> OccupiedLots { get; set; }

    public ParkingLot(int capacity)
    {
        Capacity = capacity;
        AvailableLots = capacity;
        OccupiedLots = new Dictionary<int, Vehicle?>();

        for (int i = 1; i <= Capacity; i++)
        {
            OccupiedLots.Add(i, null);
        }
    }

    public decimal CalculateParkingFee(Vehicle vehicle)
    {
        if (vehicle.EntryTime == DateTime.MinValue)
        {
            return 0;
        }

        TimeSpan duration = DateTime.Now - vehicle.EntryTime;
        int hours = (int)Math.Ceiling(duration.TotalHours);

        decimal feePerHour = (vehicle.Type == VehicleType.Motor) ? 5000 : 10000;
        decimal totalFee = hours * feePerHour;

        return totalFee;
    }
}

public class ParkSystem
{
    private ParkingLot parkingLot;

    public ParkSystem(int capacity)
    {
        parkingLot = new ParkingLot(capacity);
    }

    public void ProcessCommand(string command)
    {
        string[] parts = command.Split(' ');
        string action = parts[0];

        switch (action)
        {
            case "create_parking_lot":
                int capacity = int.Parse(parts[1]);
                parkingLot = new ParkingLot(capacity);
                Console.WriteLine($"Created a parking lot with {capacity} slots");
                break;
            case "park":
                string registrationNumber = parts[1];
                string color = parts[2];
                VehicleType type;

                if (Enum.IsDefined(typeof(VehicleType), parts[3]))
                {
                    type = (VehicleType)Enum.Parse(typeof(VehicleType), parts[3]);
                    ParkVehicle(new Vehicle { RegistrationNumber = registrationNumber, Color = color, Type = type, EntryTime = DateTime.Now });
                }
                else
                {
                    Console.WriteLine("Vehicle type not registered");
                }
                break;
            case "leave":
                int lotNumber = int.Parse(parts[1]);
                LeaveLot(lotNumber);
                break;
            case "status":
                PrintStatus();
                break;
            case "type_of_vehicles":
                string vehicleType = parts[1];
                if (Enum.IsDefined(typeof(VehicleType), parts[1]))
                {
                    PrintTypeOfVehicles(vehicleType);
                }
                else
                {
                    Console.WriteLine("Vehicle type not registered");
                }
                break;
            case "registration_numbers_for_vehicles_with_ood_plate":
                PrintRegistrationNumbersForOddPlate();
                break;
            case "registration_numbers_for_vehicles_with_even_plate":
                PrintRegistrationNumbersForEvenPlate();
                break;
            case "registration_numbers_for_vehicles_with_colour":
                string colorFilter = parts[1];
                PrintRegistrationNumbersForColor(colorFilter);
                break;
            case "slot_numbers_for_vehicles_with_colour":
                colorFilter = parts[1];
                PrintSlotNumbersForColor(colorFilter);
                break;
            case "slot_number_for_registration_number":
                registrationNumber = parts[1];
                PrintSlotNumberForRegistrationNumber(registrationNumber);
                break;
            case "exit":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Unknown command");
                break;
        }
    }

    private void ParkVehicle(Vehicle vehicle)
    {
        if (parkingLot.Capacity == 0)
        {
            Console.WriteLine("Parking lot not initialized");
            return;
        }
        else if (parkingLot.AvailableLots == 0)
        {
            Console.WriteLine("Sorry, parking lot is full");
            return;
        }
        else if (vehicle.Type != VehicleType.Motor && vehicle.Type != VehicleType.Mobil)
        {
            Console.WriteLine("Vehicle type not recognized");
            return;
        }

        int lotNumber = -1;
        foreach (var slot in parkingLot.OccupiedLots)
        {
            if (slot.Value == null)
            {
                lotNumber = slot.Key;
                break;
            }
        }

        if (lotNumber == -1)
        {
            lotNumber = parkingLot.OccupiedLots.Count + 1;
        }

        parkingLot.OccupiedLots[lotNumber] = vehicle;
        parkingLot.AvailableLots--;

        Console.WriteLine($"Allocated slot number: {lotNumber}");
    }

    private void LeaveLot(int lotNumber)
    {
        Vehicle? vehicle = parkingLot.OccupiedLots[lotNumber];
        if (vehicle == null)
        {
            Console.WriteLine($"Slot number {lotNumber} is already empty");
            return;
        }

        decimal parkingFee = parkingLot.CalculateParkingFee(vehicle);
        Console.WriteLine($"Parking fee: {parkingFee}");

        parkingLot.OccupiedLots[lotNumber] = null;
        parkingLot.AvailableLots++;

        Console.WriteLine($"Slot number {lotNumber} is free");
    }

    private void PrintStatus()
    {
        Console.WriteLine("Slot\tNo.\t\tType\tRegistration No. Color\tParking Time");
        foreach (var lot in parkingLot.OccupiedLots)
        {
            if (lot.Value != null)
            {
                Console.WriteLine($"{lot.Key}\t{lot.Value.RegistrationNumber}\t{lot.Value.Type}\t{lot.Value.Color}\t\t\t{lot.Value.EntryTime}");
            }
        }
    }

    private void PrintTypeOfVehicles(string vehicleType)
    {
        if (!vehicleType.Equals("Motor", StringComparison.InvariantCultureIgnoreCase) && !vehicleType.Equals("Mobil", StringComparison.InvariantCultureIgnoreCase))
        {
            Console.WriteLine("Input tidak terdaftar.");
            return;
        }

        int count = 0;

        foreach (var lot in parkingLot.OccupiedLots)
        {
            if (lot.Value != null)
            {
                if (lot.Value.Type.ToString().Equals(vehicleType, StringComparison.InvariantCultureIgnoreCase))
                {
                    count++;
                }
            }
        }

        Console.WriteLine($"Number of vehicles type {vehicleType} : {count}");
    }

    private void PrintRegistrationNumbersForOddPlate()
    {
        Console.Write("Registration numbers of vehicles with odd plate numbers: \n");
        bool isFirst = true;

        foreach (var lot in parkingLot.OccupiedLots)
        {
            if (lot.Value != null)
            {
                string registrationNumber = lot.Value.RegistrationNumber;
                string[] registParts = registrationNumber.Split('-');

                if (registParts.Length > 1)
                {
                    int registrationNumberValue = int.Parse(registParts[1]);

                    if (registrationNumberValue % 2 != 0)
                    {
                        if (!isFirst)
                        {
                            Console.Write(", ");
                        }

                        Console.Write(lot.Value.RegistrationNumber);
                        isFirst = false;
                    }
                }
            }
        }

        if (isFirst)
        {
            Console.WriteLine("No vehicle with odd plate number found");
        }
        else
        {
            Console.WriteLine();
        }
    }

    private void PrintRegistrationNumbersForEvenPlate()
    {
        Console.Write("Registration numbers of vehicles with even plate numbers: \n");
        bool isFirst = true;

        foreach (var lot in parkingLot.OccupiedLots)
        {
            if (lot.Value != null)
            {
                string registrationNumber = lot.Value.RegistrationNumber;
                string[] registParts = registrationNumber.Split('-');

                if (registParts.Length > 1)
                {
                    int registrationNumberValue = int.Parse(registParts[1]);

                    if (registrationNumberValue % 2 == 0)
                    {
                        if (!isFirst)
                        {
                            Console.Write(", ");
                        }

                        Console.Write(lot.Value.RegistrationNumber);
                        isFirst = false;
                    }
                }
            }
        }

        if (isFirst)
        {
            Console.WriteLine("No vehicle with even plate number found");
        }
        else
        {
            Console.WriteLine();
        }
    }

    private void PrintRegistrationNumbersForColor(string color)
    {
        Console.Write($"Registration numbers of vehicles with color {color}: \n");
        bool isFirst = true;

        foreach (var lot in parkingLot.OccupiedLots)
        {
            if (lot.Value != null)
            {
                if (lot.Value.Color.Equals(color, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!isFirst)
                    {
                        Console.Write(", ");
                    }

                    Console.Write(lot.Value.RegistrationNumber);
                    isFirst = false;
                }
            }
        }

        if (isFirst)
        {
            Console.WriteLine($"No vehicle with color {color} found");
        }
        else
        {
            Console.WriteLine();
        }
    }

    private void PrintSlotNumbersForColor(string color)
    {
        Console.Write($"Slot numbers of vehicles with color {color}: \n");
        bool isFirst = true;

        foreach (var lot in parkingLot.OccupiedLots)
        {
            if (lot.Value != null)
            {
                if (lot.Value.Color.Equals(color, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!isFirst)
                    {
                        Console.Write(", ");
                    }

                    Console.Write(lot.Key);
                    isFirst = false;
                }
            }
        }

        if (isFirst)
        {
            Console.WriteLine($"No vehicle with color {color} found");
        }
        else
        {
            Console.WriteLine();
        }
    }

    private void PrintSlotNumberForRegistrationNumber(string registrationNumber)
    {
        bool found = false;

        foreach (var lot in parkingLot.OccupiedLots)
        {
            if (lot.Value != null)
            {
                if (lot.Value.RegistrationNumber.Equals(registrationNumber, StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine($"Slot number of vehicle with registration number {registrationNumber}: {lot.Key}");
                    found = true;
                    break;
                }
            }
        }

        if (!found)
        {
            Console.WriteLine($"No vehicle with registration number {registrationNumber} found");
        }
    }
}

