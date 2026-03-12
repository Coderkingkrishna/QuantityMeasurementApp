using System;
using QuantityMeasurementApp.Business;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.UI
{
    public static class ConsoleMenu
    {
        public static void Run()
        {
            var service = new QuantityMeasurementService();
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Quantity Measurement - Interactive Menu");
                Console.WriteLine("1) Compare two quantities");
                Console.WriteLine("2) Convert a quantity");
                Console.WriteLine("3) Add two quantities");
                Console.WriteLine("4) Show available unit types and names");
                Console.WriteLine("0) Exit");
                Console.Write("Select an option: ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    continue;
                switch (input.Trim())
                {
                    case "1":
                        RunCompare(service);
                        break;
                    case "2":
                        RunConvert(service);
                        break;
                    case "3":
                        RunAdd(service);
                        break;
                    case "4":
                        PrintAvailableUnits();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private static void RunCompare(QuantityMeasurementService service)
        {
            try
            {
                Console.Write(
                    "Enter unit enum type (LengthUnit/WeightUnit/VolumeUnit/TemperatureUnit): "
                );
                var typeName = Console.ReadLine()?.Trim();
                Console.Write("Enter first value: ");
                var v1 = double.Parse(Console.ReadLine() ?? "0");
                Console.Write("Enter first unit name (e.g., Feet, Inch, Kilogram): ");
                var u1 = Console.ReadLine()?.Trim();
                Console.Write("Enter second value: ");
                var v2 = double.Parse(Console.ReadLine() ?? "0");
                Console.Write("Enter second unit name: ");
                var u2 = Console.ReadLine()?.Trim();

                if (string.Equals(typeName, "LengthUnit", StringComparison.OrdinalIgnoreCase))
                {
                    var a = new Quantity<LengthUnit>(
                        v1,
                        (LengthUnit)Enum.Parse(typeof(LengthUnit), u1!, true)
                    );
                    var b = new Quantity<LengthUnit>(
                        v2,
                        (LengthUnit)Enum.Parse(typeof(LengthUnit), u2!, true)
                    );
                    Console.WriteLine($"Result: {service.AreEqual(a, b)}");
                }
                else if (string.Equals(typeName, "WeightUnit", StringComparison.OrdinalIgnoreCase))
                {
                    var a = new Quantity<WeightUnit>(
                        v1,
                        (WeightUnit)Enum.Parse(typeof(WeightUnit), u1!, true)
                    );
                    var b = new Quantity<WeightUnit>(
                        v2,
                        (WeightUnit)Enum.Parse(typeof(WeightUnit), u2!, true)
                    );
                    Console.WriteLine($"Result: {service.AreEqual(a, b)}");
                }
                else if (string.Equals(typeName, "VolumeUnit", StringComparison.OrdinalIgnoreCase))
                {
                    var a = new Quantity<VolumeUnit>(
                        v1,
                        (VolumeUnit)Enum.Parse(typeof(VolumeUnit), u1!, true)
                    );
                    var b = new Quantity<VolumeUnit>(
                        v2,
                        (VolumeUnit)Enum.Parse(typeof(VolumeUnit), u2!, true)
                    );
                    Console.WriteLine($"Result: {service.AreEqual(a, b)}");
                }
                else if (
                    string.Equals(typeName, "TemperatureUnit", StringComparison.OrdinalIgnoreCase)
                )
                {
                    var a = new Quantity<TemperatureUnit>(
                        v1,
                        (TemperatureUnit)Enum.Parse(typeof(TemperatureUnit), u1!, true)
                    );
                    var b = new Quantity<TemperatureUnit>(
                        v2,
                        (TemperatureUnit)Enum.Parse(typeof(TemperatureUnit), u2!, true)
                    );
                    Console.WriteLine($"Result: {service.AreEqual(a, b)}");
                }
                else
                {
                    Console.WriteLine("Unknown type");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void RunConvert(QuantityMeasurementService service)
        {
            try
            {
                Console.Write(
                    "Enter unit enum type (LengthUnit/WeightUnit/VolumeUnit/TemperatureUnit): "
                );
                var typeName = Console.ReadLine()?.Trim();
                Console.Write("Enter value: ");
                var v = double.Parse(Console.ReadLine() ?? "0");
                Console.Write("Enter source unit name: ");
                var su = Console.ReadLine()?.Trim();
                Console.Write("Enter target unit name: ");
                var tu = Console.ReadLine()?.Trim();

                if (string.Equals(typeName, "LengthUnit", StringComparison.OrdinalIgnoreCase))
                {
                    var val = service.Convert(
                        v,
                        (LengthUnit)Enum.Parse(typeof(LengthUnit), su!, true),
                        (LengthUnit)Enum.Parse(typeof(LengthUnit), tu!, true)
                    );
                    Console.WriteLine($"Converted: {val}");
                }
                else if (string.Equals(typeName, "WeightUnit", StringComparison.OrdinalIgnoreCase))
                {
                    var val = service.Convert(
                        v,
                        (WeightUnit)Enum.Parse(typeof(WeightUnit), su!, true),
                        (WeightUnit)Enum.Parse(typeof(WeightUnit), tu!, true)
                    );
                    Console.WriteLine($"Converted: {val}");
                }
                else if (string.Equals(typeName, "VolumeUnit", StringComparison.OrdinalIgnoreCase))
                {
                    var val = service.Convert(
                        v,
                        (VolumeUnit)Enum.Parse(typeof(VolumeUnit), su!, true),
                        (VolumeUnit)Enum.Parse(typeof(VolumeUnit), tu!, true)
                    );
                    Console.WriteLine($"Converted: {val}");
                }
                else if (
                    string.Equals(typeName, "TemperatureUnit", StringComparison.OrdinalIgnoreCase)
                )
                {
                    var val = service.Convert(
                        v,
                        (TemperatureUnit)Enum.Parse(typeof(TemperatureUnit), su!, true),
                        (TemperatureUnit)Enum.Parse(typeof(TemperatureUnit), tu!, true)
                    );
                    Console.WriteLine($"Converted: {val}");
                }
                else
                {
                    Console.WriteLine("Unknown type");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void RunAdd(QuantityMeasurementService service)
        {
            try
            {
                Console.Write("Enter unit enum type (LengthUnit/WeightUnit/VolumeUnit): ");
                var typeName = Console.ReadLine()?.Trim();
                Console.Write("Enter first value: ");
                var v1 = double.Parse(Console.ReadLine() ?? "0");
                Console.Write("Enter first unit name: ");
                var u1 = Console.ReadLine()?.Trim();
                Console.Write("Enter second value: ");
                var v2 = double.Parse(Console.ReadLine() ?? "0");
                Console.Write("Enter second unit name: ");
                var u2 = Console.ReadLine()?.Trim();

                if (string.Equals(typeName, "LengthUnit", StringComparison.OrdinalIgnoreCase))
                {
                    var res = service.Add(
                        v1,
                        (LengthUnit)Enum.Parse(typeof(LengthUnit), u1!, true),
                        v2,
                        (LengthUnit)Enum.Parse(typeof(LengthUnit), u2!, true)
                    );
                    Console.WriteLine($"Sum: {res}");
                }
                else if (string.Equals(typeName, "WeightUnit", StringComparison.OrdinalIgnoreCase))
                {
                    var res = service.Add(
                        v1,
                        (WeightUnit)Enum.Parse(typeof(WeightUnit), u1!, true),
                        v2,
                        (WeightUnit)Enum.Parse(typeof(WeightUnit), u2!, true)
                    );
                    Console.WriteLine($"Sum: {res}");
                }
                else if (string.Equals(typeName, "VolumeUnit", StringComparison.OrdinalIgnoreCase))
                {
                    var res = service.Add(
                        v1,
                        (VolumeUnit)Enum.Parse(typeof(VolumeUnit), u1!, true),
                        v2,
                        (VolumeUnit)Enum.Parse(typeof(VolumeUnit), u2!, true)
                    );
                    Console.WriteLine($"Sum: {res}");
                }
                else
                {
                    Console.WriteLine("Unknown or unsupported type for add");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void PrintAvailableUnits()
        {
            Console.WriteLine();
            Console.WriteLine("Supported unit types and members:");
            Console.WriteLine(
                "LengthUnit: " + string.Join(", ", Enum.GetNames(typeof(LengthUnit)))
            );
            Console.WriteLine(
                "WeightUnit: " + string.Join(", ", Enum.GetNames(typeof(WeightUnit)))
            );
            Console.WriteLine(
                "VolumeUnit: " + string.Join(", ", Enum.GetNames(typeof(VolumeUnit)))
            );
            Console.WriteLine(
                "TemperatureUnit: " + string.Join(", ", Enum.GetNames(typeof(TemperatureUnit)))
            );
        }
    }
}
