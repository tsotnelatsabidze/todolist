using System.Net.Http.Json;
using Newtonsoft.Json;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.Services.WebApi
{
    public class TodoListWebApiService
    {
        public HttpClient Client { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoListWebApiService"/> class.
        /// </summary>
        public TodoListWebApiService()
        {
            this.Client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5276/")
            };
        }

        public List<TodoListDto> GetTodoLists()
        {
            var response = Client.GetAsync("/TodoList").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<TodoListDto>>(content);
        }

        public async Task<TodoListDto> GetTodoListDetails(int id)
        {
            var response = await Client.GetAsync($"TodoList?$expand=TodoTasks&$filter=Id eq {id}");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TodoListDto>>(content).First();
        }

        public async Task<IEnumerable<TodoListDto>> GetTodoListForUser(string userId)
        {
            var response = await Client.GetAsync($"TodoList?$expand=TodoTasks&$filter=CreatorUserId eq '{userId}'");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TodoListDto>>(content);
        }

        public async Task<TodoListDto> GetTodoList(int id)
        {
            var response = await Client.GetAsync($"/TodoList/{id}");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoListDto>(content);
        }

        public async Task<TodoListDto> AddNewAsync(TodoListCreateDto todoList)
        {
            var response = await Client.PostAsJsonAsync("/TodoList/", todoList);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoListDto>(content);
        }

        public async Task<TodoListDto> Delete(int todoListId)
        {
            var response = await Client.DeleteAsync($"/TodoList/{todoListId}");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoListDto>(content);
        }

        public async Task<TodoListDto> Update(int id, TodoListDto todoList)
        {
            var response = await Client.PutAsJsonAsync($"/TodoList/{id}", todoList);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoListDto>(content);
        }
    }
}
