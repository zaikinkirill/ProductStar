using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Core.Domain.TaskAggregate;
using TaskManagement.Core.Ports;
using TaskManagement.Infrastructure.Adapters.Internal;
using Task = System.Threading.Tasks.Task;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var serviceProvider = ConfigureServices();
        
        var processingService = serviceProvider.GetRequiredService<IMediator>();
        
        Console.WriteLine("Приложение для управления задачами, которое позволяет пользователям добавлять, удалять, редактировать и выполнять задачи.");
        
        int keyMenu = -1;
        while (keyMenu != 10)
        {
            Console.WriteLine();
            Console.WriteLine("Выберите пункт меню:");
            Console.WriteLine("1. Вывести список всех задач");
            Console.WriteLine("2. Добавить задачу");
            Console.WriteLine("3. Редактировать задачу");
            Console.WriteLine("4. Удалить задачу");
            Console.WriteLine("5. Взять задачу в работу");
            Console.WriteLine("6. Выполнить задачу");
            Console.WriteLine("10. Выход из приложения");
            Console.Write("Ваш выбор: ");
            if (!int.TryParse(Console.ReadLine(), out keyMenu))
            {
                Console.WriteLine("Введите корректное число!");
                continue;
            }

            switch (keyMenu)
            {
                case 1:
                    var getAllTaskCommand= new TaskManagement.Core.Application.UseCases.Queries.GetAllTask.Query();
                    var getAllTaskResponse = await processingService.Send(getAllTaskCommand);
                    Console.WriteLine("Список всех задач: ");
                    if (getAllTaskResponse.Tasks.ToList().Count == 0)
                    {
                        Console.WriteLine("Задач нет!");
                    }
                    foreach (var task in getAllTaskResponse.Tasks)
                    {
                        Console.WriteLine(task.Id + "   " + task.Title + "   " + task.Description + "   " + task.Status.Name);
                    }
                    break;
                case 2:
                    Console.Write("Введите название задачи: ");
                    var titleCreate = Console.ReadLine();
                    Console.Write("Введите описание задачи: ");
                    var descriptionCreate = Console.ReadLine();
                    var createTaskCommand = new TaskManagement.Core.Application.UseCases.Commands.CreateTask.Command(titleCreate, descriptionCreate);
                    var createResponse = await processingService.Send(createTaskCommand);
                    Console.WriteLine(createResponse ? "Задача создана" : "Ошибка создания задачи");
                    break;
                case 3:
                    Console.WriteLine("Выберите задачу для изменения: ");
                    var getTasksFromChangeCommand =
                        new TaskManagement.Core.Application.UseCases.Queries.GetTaskByStatus.Query(Status.Created);
                    var tasksFromChange = await processingService.Send(getTasksFromChangeCommand);
                    Console.WriteLine("Список задач доступных для изменения: ");
                    if (tasksFromChange.Tasks.ToList().Count == 0)
                    {
                        Console.WriteLine("Задач нет!");
                        continue;
                    }

                    foreach (var task in tasksFromChange.Tasks)
                    {
                        Console.WriteLine(task.Id + "   " + task.Title + "   " + task.Description + "   " + task.Status.Name);
                    }

                    Console.Write("Введите Id задачи для изменения: ");
                    Guid guidFromChange;
                    if (!Guid.TryParse(Console.ReadLine(), out guidFromChange)) ;
                    {
                        if (guidFromChange == Guid.Empty)
                        {
                            Console.WriteLine("Такой задачи не существует!");
                            break;
                        }
                    }
                    var taskFromChange = tasksFromChange.Tasks.Where(x => x.Id == guidFromChange).FirstOrDefault();
                    if (taskFromChange == null)
                    {
                        Console.WriteLine("Такой задачи не существует");
                        continue;
                    }
                    Console.Write("Введите новое название задачи: ");
                    var titleChange = Console.ReadLine();
                    Console.Write("Введите новое описание задачи: ");
                    var descriptionСhange = Console.ReadLine();
                    var changeTaskCommand = new TaskManagement.Core.Application.UseCases.Commands.ChangeTask.Command(guidFromChange, titleChange, descriptionСhange);
                    var changeResponse = await processingService.Send(changeTaskCommand);
                    Console.WriteLine(changeResponse ? "Задача изменена" : "Ошибка изменения задачи");
                    break;
                case 4:
                    Console.WriteLine("Выберите задачу для удаления: ");
                    var getTasksFromDeleteCommand = new TaskManagement.Core.Application.UseCases.Queries.GetTaskByStatus.Query(Status.Created);
                    var tasksFromDelete = await processingService.Send(getTasksFromDeleteCommand);
                    Console.WriteLine("Список задач доступных для удаления: ");
                    if (tasksFromDelete.Tasks.ToList().Count == 0)
                    {
                        Console.WriteLine("Задач нет!");
                        continue;
                    }
                    foreach (var task in tasksFromDelete.Tasks)
                    {
                        Console.WriteLine(task.Id + "   " + task.Title + "   " + task.Description + "   " + task.Status.Name);
                    }
                    Console.Write("Введите Id задачи для удаления: ");
                    Guid guidFromDelete;
                    if (!Guid.TryParse(Console.ReadLine(), out guidFromDelete)) ;
                    {
                        if (guidFromDelete == Guid.Empty)
                        {
                            Console.WriteLine("Такой задачи не существует!");
                            break;
                        }
                    }
                    var deleteTaskCommand = new TaskManagement.Core.Application.UseCases.Commands.DeleteTask.Command(guidFromDelete);
                    var deleteResponse = await processingService.Send(deleteTaskCommand);
                    Console.WriteLine(deleteResponse ? "Задача удалена" : "Ошибка удаления задачи");
                    break;
                case 5:
                    Console.Write("Выберите задачу для взятия в работу: ");
                    var getTasksFromStartCommand = new TaskManagement.Core.Application.UseCases.Queries.GetTaskByStatus.Query(Status.Created);
                    var tasksFromStart = await processingService.Send(getTasksFromStartCommand);
                    Console.WriteLine("Список задач доступных для выполнения: ");
                    if (tasksFromStart.Tasks.ToList().Count == 0)
                    {
                        Console.WriteLine("Задач нет!");
                        continue;
                    }
                    foreach (var task in tasksFromStart.Tasks)
                    {
                        Console.WriteLine(task.Id + "   " + task.Title + "   " + task.Description + "   " + task.Status.Name);
                    }
                    Console.Write("Введите Id задачи для взятия в работу: ");
                    Guid guidFromStart;
                    if (!Guid.TryParse(Console.ReadLine(), out guidFromStart)) ;
                    {
                        if (guidFromStart == Guid.Empty)
                        {
                            Console.WriteLine("Такой задачи не существует!");
                            break;
                        }
                    }
                    var startTaskCommand = new TaskManagement.Core.Application.UseCases.Commands.StartTask.Command(guidFromStart);
                    var startResponse = await processingService.Send(startTaskCommand);
                    Console.WriteLine(startResponse ? "Задача взята в работу" : "Ошибка взятия задачи в работу");
                    break;
                case 6:
                    Console.Write("Выберите задачу для завершения: ");
                    var getTasksFromCompleteCommand = new TaskManagement.Core.Application.UseCases.Queries.GetTaskByStatus.Query(Status.Started);
                    var tasksFromComplete = await processingService.Send(getTasksFromCompleteCommand);
                    Console.WriteLine("Список задач доступных для завершения: ");
                    if (tasksFromComplete.Tasks.ToList().Count == 0)
                    {
                        Console.WriteLine("Задач нет!");
                        continue;
                    }
                    foreach (var task in tasksFromComplete.Tasks)
                    {
                        Console.WriteLine(task.Id + "   " + task.Title + "   " + task.Description + "   " + task.Status.Name);
                    }
                    Console.Write("Введите Id задачи для завершения: ");
                    Guid guidFromComplete;
                    if (!Guid.TryParse(Console.ReadLine(), out guidFromComplete)) ;
                    {
                        if (guidFromComplete == Guid.Empty)
                        {
                            Console.WriteLine("Такой задачи не существует!");
                            break;
                        }
                    }
                    var completeTaskCommand = new TaskManagement.Core.Application.UseCases.Commands.CompleteTask.Command(guidFromComplete);
                    var completeResponse = await processingService.Send(completeTaskCommand);
                    Console.WriteLine(completeResponse ? "Задача выполнена" : "Ошибка выполнения задачи");
                    break;
                case 0:
                    break;
            }
        }
    }
    
    public static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(TaskManagement.Core.Application.UseCases.Queries.GetAllTask.Handler).Assembly); 
        });
        
        // Переключение хранилищ, используя коллекцию регистрации сервисов
        services.AddSingleton<ITaskStorage, TaskInternalStorage>();
        return services.BuildServiceProvider();
    }
}


