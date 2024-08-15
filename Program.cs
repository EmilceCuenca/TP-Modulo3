int[] dias = [1, 2, 3, 4, 5, 6, 7];
int[] semanas = [1, 2, 3, 4, 5];
int[,] mes = new int[dias.Length, semanas.Length];
Titulo();
Console.WriteLine("Bienvenido. Por favor, elige una de las siguientes opciones para continuar");
Console.WriteLine();
Console.WriteLine("1. Ingresar manualmente las temperaturas mensuales.");
Console.WriteLine("2. Simular la carga de temperaturas automáticamente.");
int[] opciones = { 1, 2 };
string texto_opciones = "Opción no válida. Por favor, ingresa 1 o 2 para continuar.";
int opc = ObtenerOpcion(opciones, texto_opciones);
Console.Clear();
if (opc == 1)
{
    foreach (int semana in semanas)
    {
        Titulo();
        Console.WriteLine("Ingrese las temperaturas diarias correspondientes al mes completo.");
        Console.WriteLine("Asegurese de introducir los datos en grados Celsius.");
        Console.WriteLine();
        foreach (int dia in dias)
        {
            mes[dia - 1, semana - 1] = ObtenerTemperaturas(dia, semana);
            if (semana == 5 && dia == 3)
            {
                break;
            }
        }
        Console.Clear();
    }
}
else
{
    Random random = new Random();
    //Rango histórico menos margenes.
    int temperatura_inicial = random.Next((-79),46);
    int min = (temperatura_inicial - 10);
    int max = (temperatura_inicial + 10);
    foreach (int semana in semanas)
    {
        foreach (int dia in dias)
        {
            int temp_aleatoria = random.Next(min, max);
            mes[dia - 1, semana - 1] = temp_aleatoria;
            if (semana == 5 && dia == 3)
            {
                break;
            }
        }
    }
}
bool continuar = true;
do
{
    Titulo();
    MostrarCalendario();
    Console.WriteLine("MENÚ PRINCIPAL");
    Console.WriteLine();
    Console.WriteLine("Para continuar, elija una opción presionando el número correspondiente:");
    Console.WriteLine();
    Console.WriteLine("1. Ver temperatura de un día específico.");
    Console.WriteLine("2. Temperaturas Promedios Semanales.");
    Console.WriteLine("3. Temperaturas por encima del Umbral (20°).");
    Console.WriteLine("4. Temperatura Promedio del Mes.");
    Console.WriteLine("5. Temperatura Máxima del Mes.");
    Console.WriteLine("6. Temperatura Mínima del Mes.");
    Console.WriteLine("7. Pronóstico 5 días Posteriores.");
    Console.WriteLine("8. Salir.");
    Console.WriteLine();
    int[] opciones_menu = { 1, 2, 3, 4, 5, 6, 7, 8 };
    string texto_menu = "Opción no válida. Por favor seleccíone un número del menú.";
    int opcion = ObtenerOpcion(opciones_menu, texto_menu);
    Console.Clear();
    switch (opcion)
    {
        case 1:
            Opcion1(dias,semanas);
            break;
        case 2:
            Opcion2(dias,semanas);
            break;
        case 3:
            Opcion3();
            break;
        case 4:
            Opcion4();
            break;
        case 5:
            Opcion5(dias, semanas);
            break;
        case 6:
            Opcion6(dias, semanas);
            break;
        case 7:
            Opcion7();
            break;
        case 8:
            Titulo();
            continuar = false;
            Console.WriteLine("Gracias por utilizar nuestro servicio.");
            Console.WriteLine();
            Console.WriteLine("¡Hasta pronto!");
            break;
    }
}
while (continuar == true);
void Titulo()
{
    Console.WriteLine("Weather Forecast");
    Console.WriteLine();
}
int ObtenerOpcion(int[] opciones, string texto)
{
    bool opc_valida = int.TryParse(Console.ReadKey().KeyChar.ToString(), out int opcion);
    while (!opc_valida || !Array.Exists(opciones, op => op == opcion))
    {
        Console.WriteLine();
        Console.WriteLine(texto);
        opc_valida = int.TryParse(Console.ReadKey().KeyChar.ToString(), out opcion);
    }
    return opcion;
}
void MostrarCalendario()
{
    Console.WriteLine("Temperaturas Mensuales:");
    MostrarMatriz(mes);
}
int ObtenerTemperaturas(int dia, int semana)
{
    //Rango histórico de temperaturas registradas del planeta.
    int[] temps_validas = Enumerable.Range(-89, 56 - (-89) + 1).ToArray();
    Console.WriteLine("Por favor,ingrese la temperatura del día " + dia + " de la semana " + semana);
    bool es_valido = int.TryParse(Console.ReadLine(), out int numero);
    while (!es_valido || !Array.Exists(temps_validas, elem => elem == numero))
    {
        Console.WriteLine("Por favor, ingrese una temperatura válida.");
        es_valido = int.TryParse(Console.ReadLine(), out numero);
    }
    return numero;
}
void MostrarMatriz(int[,] matriz)
{
    Console.WriteLine();
    string[] sietedias = { "L", "M", "Mi", "J", "V", "S", "D" };
    Console.Write("S\\D|");
    foreach (string dia in sietedias)
    {
        Console.Write(dia.PadLeft(4));
    }
    Console.WriteLine(" |");
    Console.WriteLine("----------------------------------");
    for (int fila = 1; fila <= matriz.GetLength(1); fila++)
    {
        Console.Write(fila.ToString().PadRight(3) + "|");
        for (int columna = 1; columna <= matriz.GetLength(0); columna++)
        {
            if(columna > 3 && fila == 5)
            {
                Console.Write("*".PadLeft(4));
            }
            else
            {
                Console.Write(mes[columna - 1, fila - 1].ToString().PadLeft(4));
            }
        }
        Console.WriteLine(" |");
    }
    Console.WriteLine("");
}
int ObtenerNumero( string texto1, string texto2, int[] arreglo)
{
    Console.WriteLine("Por favor, "+texto1);
    bool es_valido = int.TryParse(Console.ReadKey().KeyChar.ToString(), out int numero);
    while (!es_valido || !Array.Exists(arreglo, elem => elem == numero))
    {
        Console.WriteLine();
        Console.WriteLine(texto2);
        es_valido = int.TryParse(Console.ReadKey().KeyChar.ToString(), out numero);
    }
    return numero;
}
string? Regresar()
{
    string? tecla;
    Console.WriteLine("Para volver a intentarlo, presionar la tecla 1.");
    Console.WriteLine("Para volver al menú principal, presionar cualquier otra.");
    tecla = Console.ReadKey().KeyChar.ToString();
    Console.Clear();
    return tecla;
}
string? RegresarDirecto()
{
    string? tecla;
    Console.WriteLine();
    Console.WriteLine("Para volver al menú principal, presionar cualquier tecla.");
    tecla = Console.ReadKey().KeyChar.ToString();
    Console.Clear();
    return tecla;
    
}
void Opcion1(int[] dias, int[] semanas)
{
    string? tecla;
    do
    {
        Titulo();
        Console.WriteLine("VER LA TEMPERATURA DE UN DÍA ESPECÍFICO.");
        Console.WriteLine();
        string pedido1 = "ingrese la semana en la cual se encuentra el día de la temperatura solicitada.";
        string reintento = "Ingreso no válido. Por favor, ingrese la semana en la cual se encuentra el día de la temperatura solicitada.";
        int semana = ObtenerNumero(pedido1, reintento, semanas);
        Console.WriteLine();
        pedido1 = "Por favor, ingrese el día específico.";
        reintento = "Ingreso no válido. Por favor, ingrese el día específico.";
        int dia = ObtenerNumero(pedido1, reintento, dias);
        Console.WriteLine();
        Console.WriteLine( "La temperatura del día "+dia+" de la semana "+semana+" fue de " +mes[dia-1,semana-1]+ "°C.");
        Console.WriteLine();
        switch(mes[dia-1,semana-1])
        {
            case < 0:
                Console.WriteLine("Hizo mucho frío.");
                break;
            case <= 20:
                Console.WriteLine("El clima estaba fresco.");
                break;
            case > 20:
                Console.WriteLine("Hizo calor afuera.");
                break;
        }
        Console.WriteLine();
        tecla = Regresar();
    }
    while (tecla == "1");
}
void Opcion2(int[] dias, int[] semanas)
{
    double[] PromediosSemanales(int[] dias, int[] semanas, int[,] mes)
    {
        double[] promedios = [];
        foreach(int semana in semanas)
        {
            int suma = 0;
            int cont_dias = 0;
            foreach (int dia in dias)
            {
                suma += mes[dia - 1, semana - 1];
                cont_dias++;
                if (semana == 5 && dia == 3)
                {
                    break;
                }
            }
            double promedio = suma / cont_dias;
            promedios = [..promedios, promedio];
        }
        return promedios;
    }
    string? tecla;
    double[] promediosSemanales = PromediosSemanales(dias, semanas, mes);
    do
    {
        Titulo();
        Console.WriteLine("TEMPERATURAS PROMEDIOS SEMANALES.");
        Console.WriteLine();
        string pedido1 = "ingrese el número de una semana para conocer la temperatura promedio de la misma.";
        string reintento = "Ingreso no válido. Por favor, ingrese la semana en la cual se encuentra el día de la temperatura solicitada.";
        int semana_elegida = ObtenerNumero(pedido1, reintento, semanas);
        Console.WriteLine();
        Console.WriteLine("La temperatura promedio de la semana "+ semana_elegida+" es de " + promediosSemanales[semana_elegida-1] + "°C");
        Console.WriteLine();
        tecla = Regresar();
    }
    while (tecla == "1");
}
void Opcion3()
{
    List<(int dia, int semana)> CalcularTempsMas20(int[] dias, int[] semanas, int[,] mes)
    {
        List<(int dia, int semana)> temps_mas20 = new List<(int dia, int semana)>();
        foreach (int semana in semanas)
        {
            foreach (int dia in dias)
            {
                if (mes[dia - 1, semana - 1] > 20)
                {
                    temps_mas20.Add((dia, semana));
                }
                if (semana == 5 && dia == 3)
                {
                    break;
                }
            }
        }
        return temps_mas20;
    }
    Titulo();
    Console.WriteLine("TEMPERATURAS POR ENCIMA DEL UMBRAL (20°C).");
    Console.WriteLine();
    Console.WriteLine("La siguiente lista contiene los días del mes en los que la temperatura superó el umbral de 20°C :");
    Console.WriteLine();
    List<(int dia, int semana)> temperaturasMas20 = CalcularTempsMas20(dias, semanas, mes);
    foreach ((int dia, int semana) in temperaturasMas20)
    {
        Console.WriteLine("* Día "+ dia +" de la semana "+ semana + " con la temperatura de "+ mes[(dia)-1,(semana)-1]+"°C.");
    }
    string? tecla = RegresarDirecto();
}
void Opcion4()
{
    Titulo();
    Console.WriteLine("TEMPERATURA PROMEDIO DEL MES.");
    Console.WriteLine();
    int suma = 0;
    foreach(int semana in semanas)
    {
        foreach(int dia in dias)
        {
            suma += mes[dia - 1, semana - 1];
            if (semana == 5 && dia == 3)
            {
                break;
            }
        }
    }
    double promedio = suma/31;
    Console.WriteLine("La temperatura promedio del mes es de " + promedio + "°C");
    string? tecla = RegresarDirecto();
}
void Opcion5(int[] dias, int[] semanas)
{
    Titulo();
    Console.WriteLine("TEMPERATURA MÁXIMA DEL MES.");
    Console.WriteLine();
    int temp_max=(-89);
    int dia_max=0;
    int semana_max=0;
    List<(int dia, int semana)> temps_maxs = new List<(int dia, int semana)>();
    foreach (int semana in semanas)
    {
        foreach(int dia in dias)
        {
            if (mes[dia-1,semana-1]>temp_max)
            {
                temp_max= mes[dia-1,semana-1];
                temps_maxs.Clear();
                dia_max = dia;
                semana_max= semana;
                temps_maxs.Add((dia, semana));
            }
            else if (mes[dia - 1, semana - 1] == temp_max)
            {
                temps_maxs.Add((dia, semana));
            }
            if (semana == 5 && dia == 3)
            {
                break;
            }
        }
    }
    if (temps_maxs.Count == 1)
    {
        Console.WriteLine("La temperatura máxima del mes fue de " + temp_max + "°C , correspodiente al día " + dia_max + " de la semana " + semana_max);
    }
    else
    {
        Console.WriteLine("La temperatura máxima del mes fue de " + temp_max + "°C ");
        Console.WriteLine("Correspodiente a los siguientes días: ");
        foreach (var (dia, semana) in temps_maxs)
        {
            Console.WriteLine("* Día " + dia + " de la semana " + semana);
        }
    }
    string? tecla = RegresarDirecto();
}
void Opcion6(int[] dias, int[] semanas)
{
    Titulo();
    Console.WriteLine("TEMPERATURA MÍNIMA DEL MES.");
    Console.WriteLine();
    int temp_min = 56;
    int dia_min=0;
    int semana_min=0;
    List<(int dia, int semana)> temps_mins = new List<(int dia, int semana)>();
    foreach (int semana in semanas)
    {
        foreach (int dia in dias)
        {
            if (mes[dia - 1, semana - 1] < temp_min)
            {
                temp_min = mes[dia - 1, semana - 1];
                temps_mins.Clear();
                dia_min = dia;
                semana_min = semana;
                temps_mins.Add((dia,semana));
            }
            else if (mes[dia - 1, semana - 1]==temp_min)
            {
                temps_mins.Add((dia, semana));
            }
            if (semana == 5 && dia == 3)
            {
                break;
            }
        }
    }
    if (temps_mins.Count == 1)
    {
        Console.WriteLine("La temperatura mínima del mes fue de " + temp_min + "°C , correspodiente al día "+dia_min+ " de la semana "+semana_min);
    }
    else
    {
        Console.WriteLine("La temperatura mínima del mes fue de " + temp_min + "°C ");
        Console.WriteLine("Correspodiente a los siguientes días: ");
        foreach (var (dia, semana) in temps_mins)
        {
            Console.WriteLine("* Día " + dia + " de la semana " + semana);
        }
    }
    string? tecla = RegresarDirecto();
}
void Opcion7()
{
    Titulo();
    Console.WriteLine("PRONÓSTICO PARA LOS 5 DÍAS POSTERIORES.");
    Console.WriteLine();
    Console.WriteLine("Las temperaturas previstas para los 5 días posteriores al mes son las siguientes:");
        int[] dias_post = { 1, 2, 3, 4, 5 };
        Random random = new Random();
        int min = (mes[2,4] - 5);
        int max = (mes[2,4] + 5);
        foreach (int dia in dias_post)
        {
            int temp_aleatoria = random.Next(min, max);
            Console.WriteLine("Para el" + dia + "° día: temperatura de " + temp_aleatoria + "°C .");
        }
    string? tecla = RegresarDirecto();
}