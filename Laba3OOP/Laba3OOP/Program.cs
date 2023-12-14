using System;


public enum Building
{
    Residential,
    Religious,
    Industrial
}


public class Architecture
{
    public string Name { get; set; }
    public int Count { get; set; }
    public DateTime ConstructionDate { get; set; }


    public Architecture(string name, int count, DateTime constructionDate)
    {
        Name = name;
        Count = count;
        ConstructionDate = constructionDate;
    }


    public Architecture()
    {

    }


    public override string ToString()
    {
        return $"Name: {Name}, Count: {Count}, Construction Date: {ConstructionDate}";
    }
}


public enum Foundation
{
    Concrete,
    Brick,
    Wood,
}


public class DesignElements
{
    private Foundation foundation;
    private Building building;
    private int wallCount;
    private Architecture[] architectures;


    public DesignElements(Foundation foundation, Building building, int wallCount)
    {
        this.foundation = foundation;
        this.building = building;
        this.wallCount = wallCount;
        architectures = new Architecture[0];
    }


    public DesignElements()
    {

        architectures = new Architecture[0];
    }


    public Foundation Foundation
    {
        get { return foundation; }
        set { foundation = value; }
    }


    public Building Building
    {
        get { return building; }
        set { building = value; }
    }


    public int WallCount
    {
        get { return wallCount; }
        set { wallCount = value; }
    }


    public Architecture[] Architectures
    {
        get { return architectures; }
        set { architectures = value; }
    }


    public double AverageArchitectureCount
    {
        get
        {
            if (architectures.Length > 0)
            {
                int total = 0;
                foreach (var architecture in architectures)
                {
                    total += architecture.Count;
                }
                return (double)total / architectures.Length;
            }
            return 0;
        }
    }


    public bool this[Building b]
    {
        get { return building == b; }
    }


    public void AddArchitecture(params Architecture[] newArchitectures)
    {
        Array.Resize(ref architectures, architectures.Length + newArchitectures.Length);
        Array.Copy(newArchitectures, 0, architectures, architectures.Length - newArchitectures.Length, newArchitectures.Length);
    }


    public override string ToString()
    {
        string result = $"Foundation: {foundation}, Building: {building}, Wall Count: {wallCount}\n";
        result += "Architectures:\n";
        foreach (var architecture in architectures)
        {
            result += architecture + "\n";
        }
        return result;
    }


    public virtual string ToShortString()
    {
        return $"Foundation: {foundation}, Building: {building}, Wall Count: {wallCount}, Average Architecture Count: {AverageArchitectureCount}";
    }
}

class Program
{
    static void Main()
    {
        // Створення об'єкта типу DesignElements
        DesignElements design = new DesignElements();
        Console.WriteLine("Short String Representation:\n" + design.ToShortString());

        // Виведення значень індексатора для різних типів будівель
        Console.WriteLine("\nIndexer Values:");
        Console.WriteLine($"Residential: {design[Building.Residential]}");
        Console.WriteLine($"Religious: {design[Building.Religious]}");
        Console.WriteLine($"Industrial: {design[Building.Industrial]}");


        design.Foundation = Foundation.Concrete;
        design.Building = Building.Residential;
        design.WallCount = 4;


        Architecture arch1 = new Architecture("Building1", 10, DateTime.Now);
        Architecture arch2 = new Architecture("Building2", 15, DateTime.Now);


        design.AddArchitecture(arch1, arch2);


        Console.WriteLine("\nString Representation After Adding Architectures:\n" + design);


        CompareArrayOperations();
    }

    static void CompareArrayOperations()
    {

        int arraySize = 1000000;


        Architecture[] array1D = new Architecture[arraySize];
        DateTime start1D = DateTime.Now;
        for (int i = 0; i < arraySize; i++)
        {
            array1D[i] = new Architecture();
        }
        TimeSpan elapsed1D = DateTime.Now - start1D;
        Console.WriteLine($"\nTime for 1D Array: {elapsed1D.TotalMilliseconds} ms");


        Architecture[,] array2DRect = new Architecture[arraySize / 1000, 1000];
        DateTime start2DRect = DateTime.Now;
        for (int i = 0; i < arraySize / 1000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                array2DRect[i, j] = new Architecture();
            }
        }
        TimeSpan elapsed2DRect = DateTime.Now - start2DRect;
        Console.WriteLine($"Time for 2D Rectangular Array: {elapsed2DRect.TotalMilliseconds} ms");


        Architecture[][] array2DJagged = new Architecture[arraySize / 1000][];
        DateTime start2DJagged = DateTime.Now;
        for (int i = 0; i < arraySize / 1000; i++)
        {
            array2DJagged[i] = new Architecture[1000];
            for (int j = 0; j < 1000; j++)
            {
                array2DJagged[i][j] = new Architecture();
            }
        }
        TimeSpan elapsed2DJagged = DateTime.Now - start2DJagged;
        Console.WriteLine($"Time for 2D Jagged Array: {elapsed2DJagged.TotalMilliseconds} ms");
    }
}
