﻿using API.Utilities;
using Client.Contract;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Repository
{
    public class GeneralRepository<Entity, TId> : IGeneralRepository<Entity, TId>
     where Entity : class
    {
        protected readonly string _request;
        protected readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor contextAccessor;
        public GeneralRepository(string request)
        {
            _request = request;
            contextAccessor = new HttpContextAccessor();
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7114/api/")
            };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", contextAccessor.HttpContext?.Session.GetString("JWToken"));
        }
        public async Task<ResponseHandler<Entity>> Delete(TId id)
        {
            ResponseHandler<Entity> entityDto = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            using (var response = _httpClient.DeleteAsync(_request + "?guid=" + id).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityDto = JsonConvert.DeserializeObject<ResponseHandler<Entity>>(apiResponse);
            }
            return entityDto;
        }

        public async Task<ResponseHandler<IEnumerable<Entity>>> Get()
        {
            ResponseHandler<IEnumerable<Entity>> entityDto = null;
            using (var response = await _httpClient.GetAsync(_request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityDto = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<Entity>>>(apiResponse);
            }
            return entityDto;
        }

        public async Task<ResponseHandler<Entity>> Get(TId id)
        {
            ResponseHandler<Entity> entity = null;
            using (var response = await _httpClient.GetAsync(_request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<ResponseHandler<Entity>>(apiResponse);
            }
            return entity;
        }

        public async Task<ResponseHandler<Entity>> Post(Entity entity)
        {
            ResponseHandler<Entity> entityDto = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = _httpClient.PostAsync(_request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityDto = JsonConvert.DeserializeObject<ResponseHandler<Entity>>(apiResponse);
            }
            return entityDto;
        }

        public async Task<ResponseHandler<Entity>> Put(TId id, Entity entity)
        {
            ResponseHandler<Entity> entityDto = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = _httpClient.PutAsync(_request + "?guid=" + id, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityDto = JsonConvert.DeserializeObject<ResponseHandler<Entity>>(apiResponse);
            }
            return entityDto;
        }
    }
}
