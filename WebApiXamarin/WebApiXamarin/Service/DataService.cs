using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApiXamarin.Models;

namespace WebApiXamarin.Service
{
    public class DataService
    {
        HttpClient client = new HttpClient();

        public async Task<List<Produto>> ObterProdutosAsync()
        {
            string url = "http://www.macwebapi.somee.com/api/produtos/";

            var resposta = await client.GetStringAsync(url);

            var itens = JsonConvert.
                DeserializeObject<List<Produto>>(resposta);

            return itens;
        }

        public async Task AddProdutoAsync(Produto produto)
        {
            string url = "http://www.macwebapi.somee.com/api/produtos/{0}";

            var uri = new Uri(string.Format(url, produto.Id));

            var data = JsonConvert.SerializeObject(produto);

            var conteudo = new 
                StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage resposta = null;

            resposta = await client.PostAsync(uri, conteudo);

            if (!resposta.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao tentar incluir produto!");
            }
        }

        public async Task AtualizarProdutoAsync(Produto produto)
        {


            string url = "http://www.macwebapi.somee.com/api/produtos/{0}";

            var uri = new Uri(string.Format(url, produto.Id));

            var data = JsonConvert.SerializeObject(produto);

            var conteudo = new
                StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage resposta = null;

            resposta = await client.PutAsync(uri, conteudo);

            if (!resposta.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao tentar incluir produto!");
            }
        }

        public async Task ExcluirProdutoAsync(Produto produto)
        {
            string url = "http://www.macwebapi.somee.com/api/produtos/{0}";
            await client.
                DeleteAsync(string.Concat(url, produto.Id));
        }


    }
    }
