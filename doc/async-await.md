# Ejemplos de `async` y `await` en C#

## **1. Operación Asíncrona Básica**
```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Inicio del programa");
        string result = await GetMessageAsync();
        Console.WriteLine(result);
        Console.WriteLine("Fin del programa");
    }

    static async Task<string> GetMessageAsync()
    {
        await Task.Delay(2000); // Simula una operación que toma tiempo
        return "¡Hola desde una tarea asíncrona!";
    }
}
```

## **2. Descargar Datos desde una API (HttpClient)**
```csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Descargando datos...");
        string data = await DownloadDataAsync("https://jsonplaceholder.typicode.com/posts/1");
        Console.WriteLine(data);
    }

    static async Task<string> DownloadDataAsync(string url)
    {
        using HttpClient client = new HttpClient();
        return await client.GetStringAsync(url);
    }
}
```

## **3. Ejecutar Varias Tareas en Paralelo**
```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Task<int> task1 = CalculateAsync(10);
        Task<int> task2 = CalculateAsync(20);
        Task<int> task3 = CalculateAsync(30);

        int[] results = await Task.WhenAll(task1, task2, task3);
        Console.WriteLine($"Resultados: {string.Join(", ", results)}");
    }

    static async Task<int> CalculateAsync(int value)
    {
        await Task.Delay(1000); // Simula trabajo pesado
        return value * 2;
    }
}
```

## **4. Manejo de Excepciones en Métodos Asíncronos**
```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            string result = await FailingTaskAsync();
            Console.WriteLine(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static async Task<string> FailingTaskAsync()
    {
        await Task.Delay(1000);
        throw new InvalidOperationException("Algo salió mal");
    }
}
```

## **5. Usando `async` y `await` con `IAsyncEnumerable`**
```csharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        await foreach (var number in GenerateNumbersAsync())
        {
            Console.WriteLine(number);
        }
    }

    static async IAsyncEnumerable<int> GenerateNumbersAsync()
    {
        for (int i = 1; i <= 5; i++)
        {
            await Task.Delay(500); // Simula trabajo
            yield return i;
        }
    }
}
```

## **6. Llamadas a Métodos Asíncronos Anidados**
```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        await OuterMethodAsync();
    }

    static async Task OuterMethodAsync()
    {
        Console.WriteLine("Inicio del método externo");
        await InnerMethodAsync();
        Console.WriteLine("Fin del método externo");
    }

    static async Task InnerMethodAsync()
    {
        await Task.Delay(1500);
        Console.WriteLine("Ejecutando método interno");
    }
}

```

## **7. Uso de CancellationToken**
```csharp
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using var cts = new CancellationTokenSource();
        cts.CancelAfter(3000); // Cancelar automáticamente después de 3 segundos

        try
        {
            await PerformOperationAsync(cts.Token);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operación cancelada.");
        }
    }

    static async Task PerformOperationAsync(CancellationToken cancellationToken)
    {
        for (int i = 1; i <= 5; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Console.WriteLine($"Paso {i}");
            await Task.Delay(1000, cancellationToken); // Respetar el token
        }

        Console.WriteLine("Operación completada.");
    }
}
```

## **8. Consumir una Web API desde .NET Core**
```csharp
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var newPost = new { title = "foo", body = "bar", userId = 1 };
        string response = await PostDataAsync("https://jsonplaceholder.typicode.com/posts", newPost);
        Console.WriteLine(response);
    }

    static async Task<string> PostDataAsync(string url, object data)
    {
        using HttpClient client = new HttpClient();
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}
```


