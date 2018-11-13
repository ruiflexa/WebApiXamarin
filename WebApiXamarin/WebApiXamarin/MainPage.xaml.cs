using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiXamarin.Models;
using WebApiXamarin.Service;
using Xamarin.Forms;

namespace WebApiXamarin
{
    public partial class MainPage : ContentPage
    {
        private DataService dataService;
        private List<Produto> produtos;

        public MainPage()
        {
            InitializeComponent();
            dataService = new DataService();
            AtualizaDados();
        }

        async void AtualizaDados()
        {
            produtos = await dataService.ObterProdutosAsync();
            listaProdutos.ItemsSource = 
                produtos
                .OrderBy(i => i.Nome)
                .ToList();
        }

        private async void BtnAdicionar_OnClicked(object sender, EventArgs e)
        {
            Produto produto = new Produto()
            {
                Nome = txtNome.Text,
                Categoria = txtCategoria.Text,
                Preco = Convert.ToDecimal(txtPreco.Text)
            };
            await dataService.AddProdutoAsync(produto);
            AtualizaDados();
        }

        private void ListaProdutos_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var produto = e.SelectedItem as Produto;
            txtNome.Text = produto.Nome;
            txtCategoria.Text = produto.Categoria;
            txtPreco.Text = produto.Preco.ToString();
        }

        private async Task MenuItemApagar_OnClicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Produto produto = (Produto)mi.CommandParameter;

            await dataService.ExcluirProdutoAsync(produto);
            AtualizaDados();

        }

        private async Task MenuItem_OnClicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Produto produto = (Produto)mi.CommandParameter;
            produto.Nome = txtNome.Text;
            produto.Categoria = txtCategoria.Text;
            produto.Preco = Convert.ToDecimal(txtPreco.Text);

            await dataService.AtualizarProdutoAsync(produto);
            AtualizaDados();
        }


    }
}
