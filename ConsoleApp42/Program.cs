//Анастасия Миронова
//11.1 Уровень Высокий; варинат 4

Console.Write("Введите количество объектов: ");
var buildingsNumber = int.Parse(Console.ReadLine()!);

var buildings = new Building[buildingsNumber];
for (var buildingIndex = 0; buildingIndex < buildingsNumber; buildingIndex++)
{
    Console.Write("\nВведите название компании: ");
    var companyName = Console.ReadLine()!;

    Console.Write("Введите название объекта: ");
    var buildName = Console.ReadLine()!;

    Console.Write("Введите значение площади: ");
    var buildArea = double.Parse(Console.ReadLine()!);

    Console.Write("Введите дату начала строительства: ");
    var startDate = DateOnly.Parse(Console.ReadLine()!);

    Console.Write("Введите планируемую дату окончания строительства: ");
    var endDate = DateOnly.Parse(Console.ReadLine()!);

    Console.Write("Выберите состояние объекта (0 - Строится, 1 - Сдан): ");
    var state = (State)int.Parse(Console.ReadLine()!);

    buildings[buildingIndex] = new Building(
        companyName,
        buildName,
        buildArea,
        startDate,
        endDate,
        state
    );
}

// 1 задание
var endInCurrentYear = buildings
    .Where(building => building.endDate.Year == DateOnly.FromDateTime(DateTime.Now).Year)
    .ToArray();

if (endInCurrentYear.Any())
{
    foreach (var building in endInCurrentYear)
        Console.WriteLine(building);

    var totalArea = endInCurrentYear.Sum(building => building.buildArea);
    Console.WriteLine($"\nВ этом году планируется построить: {endInCurrentYear.Length}." +
                      $"\nИх общая площадь составит {totalArea:F2} м2.");
}
else Console.WriteLine("В этом году ничего не планируется.");

// 2 задание
var minimalPeriodOfBuild = buildings.MinBy(building => building.endDate.DayNumber - building.startDate.DayNumber);
Console.WriteLine(minimalPeriodOfBuild);

// 3 задание
var failedToFulfill = buildings
    .Where(building =>
        DateOnly.FromDateTime(DateTime.Now).DayNumber - building.endDate.DayNumber > 183 && building.state == State.Construction)
    .ToArray();

if (failedToFulfill.Any())
{
    Console.WriteLine("\n\nОбязательства не выполнены по следующим объектам: ");
    foreach (var building in failedToFulfill)
        Console.WriteLine(building);
}
else Console.WriteLine("Все компании выполняют свои обязательства.");


struct Building
{
    public string companyName;

    public string buildName;
    public double buildArea;

    public DateOnly startDate;
    public DateOnly endDate;

    public State state;

    public Building(string companyName, string buildName, double buildArea, DateOnly startDate, DateOnly endDate,
        State state)
    {
        this.companyName = companyName;
        this.buildName = buildName;
        this.buildArea = buildArea;
        this.startDate = startDate;
        this.endDate = endDate;
        this.state = state;
    }

    public override string ToString()
    {
        return $"\nНазвание объекта: {buildName}\n- Название компании: {companyName}\n- Площадь объекта: {buildArea:F2}м2\n" +
               $"- Дата начала работ: {startDate}\n- Предположительная дата окончания работ: {endDate}\n - Состояние: {state.ToString()}";
    }
}

internal enum State
{
    Construction,
    Passed,
}
