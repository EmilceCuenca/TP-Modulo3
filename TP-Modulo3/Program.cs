using System;

Controlador controlador = new Controlador();
controlador.Iniciar();

public class Vista
{
    public void MostrarTitulo()
    {
        Console.WriteLine("Weather Forecast");
        Console.WriteLine();
    }
    public int ElegirOpcionCarga()
    {
        Console.WriteLine("Bienvenido. Por favor, elige una de las siguientes opciones para continuar");
        Console.WriteLine();
        Console.WriteLine("1. Ingresar manualmente las temperaturas mensuales.");
        Console.WriteLine("2. Simular la carga de temperaturas automáticamente.");
        int[] opciones = { 1, 2 };
        string texto_opciones = "Opción no válida. Por favor, ingresa 1 o 2 para continuar.";
        int opc = Herramienta.ObtenerOpcion(opciones, texto_opciones);
        Console.Clear();
        return opc;
    }
    public int MostrarMenuPrincipal()
    {
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
        int opcion = Herramienta.ObtenerOpcion(opciones_menu, texto_menu);
        Console.Clear();
        return opcion;
    }
    public bool MostrarDespedida()
    {
        bool continuar = false;
        Console.WriteLine("Gracias por utilizar nuestro servicio.");
        Console.WriteLine();
        Console.WriteLine("¡Hasta pronto!");
        return continuar;
    }
}
public class Controlador
{
    Vista vista = new Vista();
    EstacionMeteorologica estacion = new EstacionMeteorologica();
    public void Iniciar()
    {
        vista.MostrarTitulo();
        int opcionCarga = vista.ElegirOpcionCarga();
        if (opcionCarga == 1)
        {
            estacion = EstacionMeteorologica.CargaManual();
        }
        else
        {
            estacion = new EstacionMeteorologica();
        }
        bool continuar = true;
        do
        {
            vista.MostrarTitulo();
            estacion.VerTemperaturas();
            int opcionMenu = vista.MostrarMenuPrincipal();
            switch (opcionMenu)
            {
                case 1:
                    Opcion1();
                    break;
                case 2:
                    Opcion2();
                    break;
                case 3:
                    Opcion3();
                    break;
                case 4:
                    Opcion4();
                    break;
                case 5:
                    Opcion5();
                    break;
                case 6:
                    Opcion6();
                    break;
                case 7:
                    Opcion7();
                    break;
                case 8:
                    continuar = vista.MostrarDespedida();
                    break;
            }
        }
        while(continuar == true);
    }
    public void Opcion1()
    {
        string? tecla;
        do
        {
            vista.MostrarTitulo();
            Console.WriteLine("VER LA TEMPERATURA DE UN DÍA ESPECÍFICO.");
            Console.WriteLine();
            string pedido1 = "ingrese la semana en la cual se encuentra el día de la temperatura solicitada.";
            string reintento = "Ingreso no válido. Por favor, ingrese la semana en la cual se encuentra el día de la temperatura solicitada.";
            int semana = Herramienta.ObtenerNumero(pedido1, reintento, CalculoTemperaturas.semanas);
            Console.WriteLine();
            pedido1 = "Por favor, ingrese el día específico.";
            reintento = "Ingreso no válido. Por favor, ingrese el día específico.";
            int dia = Herramienta.ObtenerNumero(pedido1, reintento, CalculoTemperaturas.dias);
            Console.WriteLine();
            estacion.TemperaturaDiaEspecifico(dia, semana);
            tecla = Herramienta.Regresar();
        }
        while (tecla == "1");
    }
    public void Opcion2()
    {
        string? tecla;
        double[] promediosSemanales = CalculoTemperaturas.PromediosSemanales(estacion.GetMes());
        do
        {
            vista.MostrarTitulo();
            Console.WriteLine("TEMPERATURAS PROMEDIOS SEMANALES.");
            Console.WriteLine();
            string pedido1 = "ingrese el número de una semana para conocer la temperatura promedio de la misma.";
            string reintento = "Ingreso no válido. Por favor, ingrese la semana en la cual se encuentra el día de la temperatura solicitada.";
            int semana_elegida = Herramienta.ObtenerNumero(pedido1, reintento, CalculoTemperaturas.semanas);
            Console.WriteLine();
            Console.WriteLine("La temperatura promedio de la semana " + semana_elegida + " es de " + double.Round(promediosSemanales[semana_elegida - 1],1) + "°C");
            Console.WriteLine();
            tecla = Herramienta.Regresar();
        }
        while (tecla == "1");
    }
    public void Opcion3()
    {
        vista.MostrarTitulo();
        Console.WriteLine("TEMPERATURAS POR ENCIMA DEL UMBRAL (20°C).");
        Console.WriteLine();
        List<(int dia, int semana)> temperaturasMas20 = CalculoTemperaturas.TemperaturasMas20(estacion.GetMes());
        if (temperaturasMas20.Count == 0)
        {
            Console.WriteLine("Durante este mes, ninguna temperatura superó el umbral de 20°C");
        }
        else
        { 
            Console.WriteLine("La siguiente lista contiene los días del mes en los que la temperatura superó el umbral de 20°C :");
            Console.WriteLine();

            foreach ((int dia, int semana) in temperaturasMas20)
            {
                Console.WriteLine("* Día " + dia + " de la semana " + semana + " con la temperatura de " + estacion.GetMes()[(dia) - 1, (semana) - 1].TemperaturaRegistrada + "°C.");
            }
        }
        Herramienta.RegresarDirecto();
    }
    public void Opcion4()
    {
        vista.MostrarTitulo();
        Console.WriteLine("TEMPERATURA PROMEDIO DEL MES.");
        Console.WriteLine();
        double promedioMensual = CalculoTemperaturas.PromedioMensual(estacion.GetMes());
        Console.WriteLine("La temperatura promedio del mes es de " + double.Round(promedioMensual,1) + "°C");
        Herramienta.RegresarDirecto();
    }
    public void Opcion5()
    {
        vista.MostrarTitulo();
        Console.WriteLine("TEMPERATURA MÁXIMA DEL MES.");
        Console.WriteLine();
        List<RegistroTemperatura> temperaturasMaximas = estacion.TemperaturaMaxima();
        if (temperaturasMaximas.Count == 1)
        {
            Console.WriteLine("La temperatura máxima del mes fue de " + temperaturasMaximas[0].TemperaturaRegistrada + "°C , correspodiente al día " + temperaturasMaximas[0].Dia + " de la semana " + temperaturasMaximas[0].Semana);
        }
        else
        {
            Console.WriteLine("La temperatura máxima del mes fue de " + temperaturasMaximas[0].TemperaturaRegistrada + "°C ");
            Console.WriteLine("Correspodiente a los siguientes días: ");
            foreach (var temperatura in temperaturasMaximas)
            {
                Console.WriteLine("* Día " + temperatura.Dia + " de la semana " + temperatura.Semana);
            }
        }
        Herramienta.RegresarDirecto();
    }
    public void Opcion6()
    {
        vista.MostrarTitulo();
        Console.WriteLine("TEMPERATURA MÁXIMA DEL MES.");
        Console.WriteLine();
        List<RegistroTemperatura> temperaturasMinimas = estacion.TemperaturaMinima();
        if (temperaturasMinimas.Count == 1)
        {
            Console.WriteLine("La temperatura mínima del mes fue de " + temperaturasMinimas[0].TemperaturaRegistrada + "°C , correspodiente al día " + temperaturasMinimas[0].Dia + " de la semana " + temperaturasMinimas[0].Semana);
        }
        else
        {
            Console.WriteLine("La temperatura mínima del mes fue de " + temperaturasMinimas[0].TemperaturaRegistrada + "°C ");
            Console.WriteLine("Correspodiente a los siguientes días: ");
            foreach (var temperatura in temperaturasMinimas)
            {
                Console.WriteLine("* Día " + temperatura.Dia + " de la semana " + temperatura.Semana);
            }
        }
        Herramienta.RegresarDirecto();
    }
    public void Opcion7()
    {
        vista.MostrarTitulo();
        Console.WriteLine("PRONÓSTICO PARA LOS 5 DÍAS POSTERIORES.");
        Console.WriteLine();
        Console.WriteLine("Las temperaturas previstas para los 5 días posteriores al mes son las siguientes:");
        List<double> pronostico5dias = estacion.Pronostico5Dias();
        foreach (double temperatura in pronostico5dias)
        {
            int indice = pronostico5dias.IndexOf(temperatura);
            Console.WriteLine("Para el " + (indice+1) + "° día: temperatura de " + double.Round(temperatura,1) + "°C .");
        }
        Herramienta.RegresarDirecto();
    }
}
public class Empleado
{
    public string Nombre { get; set; }
    public string Turno { get; set; }
    public Empleado(string nombre, string turno)
    {
        Nombre = nombre;
        Turno = turno;
    }
}
public class Profesional : Empleado
{
    public int NumMatricula { get; set; }
    public Profesional (string nombre, string turno, int numMatricula)
        : base (nombre,turno)
    {
        NumMatricula = numMatricula;
    }
}
public class Pasante : Empleado
{
    public int NumLegajo { get; set; }
    public Pasante(string nombre, string turno, int numLegajo)
        : base (nombre,turno)
    {
        NumLegajo = numLegajo;
    }
}
public class TurnosEmpleados
{
    private List<Empleado> empleados;
    private int TurnoActual;
    public TurnosEmpleados()
    {
    empleados = InicializarEmpleados();
    TurnoActual = 0;
    }
    private List<Empleado> InicializarEmpleados()
    {
        var empleados = new List<Empleado>
        {
            new Profesional("Juan Perez", "Mañana", 123),
            new Pasante("Luis Diaz", "Tarde", 002),
            new Profesional("Pedro Lopez", "Noche", 789),
            new Pasante("Jose Rios", "Mañana", 001),
            new Profesional("Maria Soto", "Tarde", 456),
            new Pasante("Ana Ruiz", "Noche", 003),
        };
        return empleados;
    }
    public Empleado ObtenerEmpleadoTurno()
    {
        Empleado empleado = empleados[TurnoActual];
        TurnoActual = (TurnoActual + 1) % empleados.Count;
        return empleado;
    }
}
public class Herramienta
{
    public static int ObtenerNumero(string texto1, string texto2, int[] arreglo)
    {
        Console.WriteLine("Por favor, " + texto1);
        bool es_valido = int.TryParse(Console.ReadKey().KeyChar.ToString(), out int numero);
        while (!es_valido || !Array.Exists(arreglo, elem => elem == numero))
        {
            Console.WriteLine();
            Console.WriteLine(texto2);
            es_valido = int.TryParse(Console.ReadKey().KeyChar.ToString(), out numero);
        }
        return numero;
    }
    public static int ObtenerOpcion(int[] opciones, string texto)
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
    public static double ObtenerTemperatura(int dia, int semana)
    {
        //Rango histórico de temperaturas registradas del planeta.
        double max = 56;
        double min = -89;
        Console.WriteLine("Por favor,ingrese la temperatura del día " + dia + " de la semana " + semana);
        bool es_valido = double.TryParse(Console.ReadLine(), out double numero);
        while (!es_valido || numero > max || numero < min)
        {
            Console.WriteLine("Por favor, ingrese una temperatura válida.");
            es_valido = double.TryParse(Console.ReadLine(), out numero);
        }
        return numero;
    }
    public static string? Regresar()
    {
        string? tecla;
        Console.WriteLine("Para volver a intentarlo, presionar la tecla 1.");
        Console.WriteLine("Para volver al menú principal, presionar cualquier otra.");
        tecla = Console.ReadKey().KeyChar.ToString();
        Console.Clear();
        return tecla;
    }
    public static string? RegresarDirecto()
    {
        string? tecla;
        Console.WriteLine();
        Console.WriteLine("Para volver al menú principal, presionar cualquier tecla.");
        tecla = Console.ReadKey().KeyChar.ToString();
        Console.Clear();
        return tecla;

    }
}
public class RegistroTemperatura
{
    public double TemperaturaRegistrada { get; }
    public Empleado PersonaTurno { get; set; }
    public DateTime FechayHoraRegistro { get; set; }
    public int Dia { get; set; }
    public int Semana {  get; set; }
    public RegistroTemperatura(double tempregistrada, Empleado personaTurno, DateTime fechayHoraRegistro, int dia, int semana)
    {
        TemperaturaRegistrada = tempregistrada;
        PersonaTurno = personaTurno;
        FechayHoraRegistro = fechayHoraRegistro;
        Dia = dia; 
        Semana = semana;
    }
}
public class EstacionMeteorologica
{
    private static readonly int[] dias = { 1, 2, 3, 4, 5, 6, 7 };
    private static readonly int[] semanas = { 1, 2, 3, 4, 5 };
    private RegistroTemperatura[,] Mes = new RegistroTemperatura[dias.Length, semanas.Length];
    public RegistroTemperatura[,] GetMes()
    { 
        return Mes; 
    }
    public EstacionMeteorologica()
    {
        TurnosEmpleados turnoEmpleados = new TurnosEmpleados();
        Random random = new Random();
        //Rango histórico menos margenes.
        double temperatura_inicial = random.NextDouble() * (46 - (-79)) + (-79);
        double min = (temperatura_inicial - 10);
        double max = (temperatura_inicial + 10);
        foreach (int semana in semanas)
        {
            foreach (int dia in dias)
            {
                double temp_aleatoria = random.NextDouble() * (max - min) + min;
                Empleado empleadoActual = turnoEmpleados.ObtenerEmpleadoTurno();
                RegistroTemperatura nuevoRegistro = new RegistroTemperatura(double.Round(temp_aleatoria, 1), empleadoActual, DateTime.Now, dia, semana);
                Mes[dia - 1, semana - 1] = nuevoRegistro;
                if (dia > 3 && semana == 5)
                {
                    Empleado sinempleado = new Empleado("-", "-");
                    DateTime dateTime = DateTime.Now;
                    RegistroTemperatura registronulo = new RegistroTemperatura(0, sinempleado, dateTime, dia, semana);
                    Mes[dia - 1, semana - 1] = registronulo;
                }
            }
        }
    }
    public EstacionMeteorologica(RegistroTemperatura[,] mes)
    {
        Mes = mes;
    }
    public void RegistrarTemperatura(RegistroTemperatura registroTemperatura, int dia, int semana)
    {
        Mes[dia - 1, semana - 1] = registroTemperatura;
    }
    public double[,] VerTemperaturas()
    {
        double[,] temperaturas = new double[dias.Length, semanas.Length];
        Console.WriteLine("Temperaturas Mensuales:");
        Console.WriteLine();
        string[] sietedias = { "L", "M", "Mi", "J", "V", "S", "D" };
        Console.Write("S\\D|");
        foreach (string letradia in sietedias)
        {
            Console.Write(letradia.PadLeft(7));
        }
        Console.WriteLine(" |");
        Console.WriteLine("-------------------------------------------------------");
        foreach (int fila in semanas)
        {
            Console.Write(fila.ToString().PadRight(3) + "|");
            foreach (int columna in dias)
            {
                if (fila == 5 && columna > 3)
                {
                    Console.Write("*".PadLeft(7));
                }
                else
                {
                    temperaturas[columna - 1, fila - 1] = Mes[columna - 1, fila - 1].TemperaturaRegistrada;
                    Console.Write((double.Round(Mes[columna - 1, fila - 1].TemperaturaRegistrada, 1) + "°").ToString().PadLeft(7));
                }
            }
            Console.WriteLine(" |");
        }
        Console.WriteLine("");
        return temperaturas;
    }
    public static EstacionMeteorologica CargaManual()
    {
        TurnosEmpleados turnoEmpleados = new TurnosEmpleados();
        RegistroTemperatura[,] mesManual = new RegistroTemperatura[dias.Length, semanas.Length];
        foreach (int semana in semanas)
        {
            Console.WriteLine("Ingrese las temperaturas diarias correspondientes al mes completo.");
            Console.WriteLine("Asegurese de introducir los datos en grados Celsius.");
            Console.WriteLine();
            foreach (int dia in dias)
            {
                // Texto para cargar temperatura
                double temperaturaCargada = Herramienta.ObtenerTemperatura(dia, semana);
                Empleado nuevoEmpleado = turnoEmpleados.ObtenerEmpleadoTurno();
                RegistroTemperatura nuevoRegistro = new RegistroTemperatura(temperaturaCargada, nuevoEmpleado, DateTime.Now, dia, semana);
                mesManual[dia - 1, semana - 1] = nuevoRegistro;
                if (semana == 5 && dia == 3)
                {
                    break;
                }
                Console.Clear();
            }
        }
        EstacionMeteorologica nuevaEstacion = new EstacionMeteorologica(mesManual);
        return nuevaEstacion;
    }
    public RegistroTemperatura TemperaturaDiaEspecifico(int dia, int semana)
    {
        RegistroTemperatura registro = Mes[dia - 1, semana - 1];
        double temperatura = registro.TemperaturaRegistrada;
        Empleado persona = registro.PersonaTurno;
        Console.WriteLine("La temperatura del día " + dia + " de la semana " + semana + " fue de " + temperatura + "°C.");
        Console.WriteLine();
        Console.WriteLine("Tomada por " + persona.Nombre + " del turno " + persona.Turno);
        Console.WriteLine();
        switch (temperatura)
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
        return registro;
    }
    public List<RegistroTemperatura> TemperaturaMaxima()
    {
        double temp_max = (-89);
        List<RegistroTemperatura> lista_maximas = new List<RegistroTemperatura>();
        foreach (int semana in semanas)
        {
            foreach (int dia in dias)
            {
                double temp_registrada = Mes[dia - 1, semana - 1].TemperaturaRegistrada;
                if (temp_registrada > temp_max)
                {
                    temp_max = Mes[dia - 1, semana - 1].TemperaturaRegistrada;
                    lista_maximas.Clear();
                    lista_maximas.Add(Mes[dia - 1, semana - 1]);
                }
                else if (temp_registrada == temp_max)
                {
                    lista_maximas.Add(Mes[dia - 1, semana - 1]);
                }
                if (semana == 5 && dia == 3)
                {
                    break;
                }
            }
        }
        return lista_maximas;
    }
    public List<RegistroTemperatura> TemperaturaMinima()
    {
        double temp_min = 56;
        List<RegistroTemperatura> lista_minimas = new List<RegistroTemperatura>();
        foreach (int semana in semanas)
        {
            foreach (int dia in dias)
            {
                double temp_registrada = Mes[dia - 1, semana - 1].TemperaturaRegistrada;
                if ( temp_registrada< temp_min)
                {
                    temp_min = Mes[dia - 1, semana - 1].TemperaturaRegistrada;
                    lista_minimas.Clear();
                    lista_minimas.Add(Mes[dia - 1, semana - 1]);
                }
                else if (temp_registrada == temp_min)
                {
                    lista_minimas.Add(Mes[dia - 1, semana - 1]);
                }
                if (semana == 5 && dia == 3)
                {
                    break;
                }
            }
        }
        return lista_minimas;
    }
    public List<double> Pronostico5Dias()
    {
        int[] dias_post = { 1, 2, 3, 4, 5 };
        List<double> pronostico5dias = new List<double>();
        Random random = new Random();
        double min = (Mes[2, 4].TemperaturaRegistrada - 5);
        double max = (Mes[2, 4].TemperaturaRegistrada + 5);
        foreach (int dia in dias_post)
        {
            double temp_aleatoria = random.NextDouble() * (max - min) + min;
            pronostico5dias.Add(temp_aleatoria);
        }
        return pronostico5dias;
    }
}
public static class CalculoTemperaturas
{
    public static readonly int[] dias = { 1, 2, 3, 4, 5, 6, 7 };
    public static readonly int[] semanas = { 1, 2, 3, 4, 5 };
    public static double[] PromediosSemanales(RegistroTemperatura[,] mes)
    {
        double[] promedios = [];
        foreach (int semana in semanas)
        {
            double suma = 0;
            int cont_dias = 0;
            foreach (int dia in dias)
            {
                suma += mes[dia - 1, semana - 1].TemperaturaRegistrada;
                cont_dias++;
                if (semana == 5 && dia == 3)
                {
                    break;
                }
            }
            double promedio = suma / cont_dias;
            promedios = [.. promedios, promedio];
        }
        return promedios;
    }
    public static List<(int dia, int semana)> TemperaturasMas20(RegistroTemperatura[,] mes)
    {
        {
            List<(int dia, int semana)> temps_mas20 = new List<(int dia, int semana)>();
            foreach (int semana in semanas)
            {
                foreach (int dia in dias)
                {
                    if (mes[dia - 1, semana - 1].TemperaturaRegistrada > 20)
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
    }
    public static double PromedioMensual(RegistroTemperatura[,] mes)
    {
        double suma = 0;
        foreach (int semana in semanas)
        {
            foreach (int dia in dias)
            {
                suma += mes[dia - 1, semana - 1].TemperaturaRegistrada;
                if (semana == 5 && dia == 3)
                {
                    break;
                }
            }
        }
        double promedio = suma / 31;
        return promedio;
    }
}



