using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestClientConsole.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestClientConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var url = "https://www.reddit.com/r/subreddit/new.json?sort=new";
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            var body = response.Content.ReadAsStringAsync();

            dynamic config = JsonConvert.DeserializeObject<ExpandoObject>(body.Result, new ExpandoObjectConverter());

            var childrens = (IEnumerable<dynamic>)config.data.children;
            var listChildrens = childrens.Select(y => y.data).Select(x => new Children { author_fullname = x.author_fullname, created = ConvertFromUnixTimestamp((double)x.created), selftext = x.selftext, thumbnail = x.thumbnail, title = x.title }).ToList();
            listChildrens = listChildrens.OrderBy(e =>e.created).ToList();

            var count = 1;
            foreach (var child in listChildrens)
            {               
                Console.WriteLine(" {0} ", count++);
                Console.WriteLine("Autor: {0} ", child.author_fullname );
                Console.WriteLine("Selftext: {0}  ", child.selftext);
                Console.WriteLine("Thumbnail: {0}  ", child.thumbnail);
                Console.WriteLine("Titulo: {0}  ", child.title);
                Console.WriteLine("Fecha_creacion : {0}  ", child.created);
                Console.WriteLine("\n");
            }

            //var url = "https://jsonplaceholder.typicode.com/posts";
            //var client = new HttpClient();

            //Posts post = new Posts
            //{
            //    userId = 50,
            //    body = "body",
            //    title = "title"
            //};

            //var data = JsonSerializer.Serialize<Posts>(post);

            //HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            //var httpResponse = await client.PostAsync(url,content);
            //var httpResponseGet = await client.GetStringAsync(url);
            //if (httpResponse.IsSuccessStatusCode)
            //{
            //    var response  = await httpResponse.Content.ReadAsStringAsync();
            //    var resp = JsonSerializer.Deserialize<Posts>(response);

            //    var listPost = JsonSerializer.Deserialize<List<Posts>>(httpResponseGet);
            //    foreach (var result in listPost)
            //    {
            //        Console.WriteLine("userid :" +result.userId);
            //        Console.WriteLine("id :" + result.id);
            //        Console.WriteLine("body :" + result.body);
            //        Console.WriteLine("title :" + result.title);
            //        Console.WriteLine("########################################");
            //    }

            //    Console.ReadLine();
            //}
            //List<Employee> empList = new List<Employee>();
            //var empp = new Employee()
            //{
            //    LastName = "Nuevoo apellido",
            //    Name = "Nuevo nombre",
            //    Rfc = "abc1234",
            //};
            //empList.Add(empp);

            //using (var db = new Models.DBMainContext())
            //{

            //    db.Employees.AddRange(empList);
            //    db.SaveChanges();


            //    IEnumerable<Models.Employee> employees = db.Employees.ToList();

            //    foreach (var emp in employees)
            //    {
            //        Console.WriteLine("Id :" + emp.Id);
            //        Console.WriteLine("LastName :" + emp.LastName);
            //        Console.WriteLine("Name :" + emp.Name);
            //        Console.WriteLine("Rfc :" + emp.Rfc);
            //        Console.WriteLine("########################################");
            //    }

            //}

        }

        static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
    }
}
