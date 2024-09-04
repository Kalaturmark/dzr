using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace CitiesApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        public class City
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public int Population { get; set; }
        }


        [ApiController]
        [Route("api/[controller]")]
        public class CitiesController : ControllerBase
        {
            private static List<City> cities = new List<City>
            {
                new City { Id = 1, Name = "New York", Country = "USA", Population = 8419000 },
                new City { Id = 2, Name = "Kiev", Country = "Ukraine", Population = 2884000 },
                new City { Id = 3, Name = "Tokyo", Country = "Japan", Population = 9273000 },
            };

            [HttpGet]
            public ActionResult<IEnumerable<City>> GetCities()
            {
                return Ok(cities);
            }

            [HttpGet("{id}")]
            public ActionResult<City> GetCity(int id)
            {
                var city = cities.FirstOrDefault(c => c.Id == id);
                if (city == null)
                {
                    return NotFound();
                }
                return Ok(city);
            }

            [HttpPost]
            public ActionResult<City> CreateCity([FromBody] City city)
            {
                city.Id = cities.Max(c => c.Id) + 1
