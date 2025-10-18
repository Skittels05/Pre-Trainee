using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TaskApp.DataAccess;
using TasksFactory;
using TasksRepository;

namespace TasksApp
{
    internal class Program
    {
        
        static async Task Main()
        {
            IConfiguration config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

            string connectionString = config.GetConnectionString("DefaultConnection");
            IDbConnectionFactory connectionFactory = new SqlDatabaseConnectionFactory(connectionString);
            IDbExecutor dbExecutor = new DbExecutorDapper(connectionFactory);
            IGenericRepository<TaskItem> taskRepo = new TaskRepository(dbExecutor);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Task Manager ====");
                Console.WriteLine("1. Добавить задачу");
                Console.WriteLine("2. Просмотреть все задачи");
                Console.WriteLine("3. Обновить состояние задачи");
                Console.WriteLine("4. Удалить задачу");
                Console.WriteLine("5. Выйти");
                Console.Write("Выберите пункт меню: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddTask(taskRepo);
                        break;
                    case "2":
                        await ViewTasks(taskRepo);
                        break;
                    case "3":
                        await UpdateTask(taskRepo);
                        break;
                    case "4":
                        await DeleteTask(taskRepo);
                        break;
                    case "5":
                        Console.WriteLine("Выход из программы...");
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню. Нажмите Enter для продолжения.");
                        break;
                }

                Console.WriteLine("\nНажмите Enter, чтобы вернуться в меню...");
                Console.ReadLine();
            }
        }

        private static async Task AddTask(IGenericRepository<TaskItem> repo)
        {
            Console.Clear();
            Console.WriteLine("=== Добавление новой задачи ===");

            Console.Write("Введите название задачи: ");
            string? title = Console.ReadLine();

            Console.Write("Введите описание задачи: ");
            string? description = Console.ReadLine();

            var newTask = new TaskItem
            {
                Title = title ?? "",
                Description = description ?? "",
                IsCompleted = false,
                CreatedAt = DateTime.Now
            };

            int id = await repo.AddAsync(newTask);
            Console.WriteLine($"Задача добавлена с Id = {id}");
        }

        private static async Task ViewTasks(IGenericRepository<TaskItem> repo)
        {
            Console.Clear();
            Console.WriteLine("=== Список всех задач ===\n");

            var tasks = await repo.GetAllAsync();
            foreach (var task in tasks)
            {
                Console.WriteLine($"ID: {task.Id}");
                Console.WriteLine($"Название: {task.Title}");
                Console.WriteLine($"Описание: {task.Description}");
                Console.WriteLine($"Выполнена: {(task.IsCompleted ? "Да" : "Нет")}");
                Console.WriteLine($"Создана: {task.CreatedAt}");
                Console.WriteLine("-------------------------");
            }
        }

        private static async Task UpdateTask(IGenericRepository<TaskItem> repo)
        {
            Console.Clear();
            Console.WriteLine("=== Обновление состояния задачи ===");

            Console.Write("Введите ID задачи: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID");
                return;
            }

            var task = await repo.GetByIdAsync(id);
            if (task == null)
            {
                Console.WriteLine("Задача не найдена");
                return;
            }

            Console.WriteLine($"Текущее состояние: {(task.IsCompleted ? "Выполнена" : "Не выполнена")}");
            Console.Write("Отметить задачу как выполненную? (y/n): ");
            string? input = Console.ReadLine();

            if (input?.ToLower() == "y")
            {
                task.IsCompleted = true;
                await repo.UpdateAsync(task);
                Console.WriteLine("Задача обновлена!");
            }
            else
            {
                Console.WriteLine("Изменение отменено.");
            }
        }

        private static async Task DeleteTask(IGenericRepository<TaskItem> repo)
        {
            Console.Clear();
            Console.WriteLine("=== Удаление задачи ===");

            Console.Write("Введите ID задачи: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID");
                return;
            }

            bool deleted = await repo.DeleteAsync(id);
            Console.WriteLine(deleted ? "Задача удалена!" : "Задача не найдена или не удалена.");
        }
    }
}
