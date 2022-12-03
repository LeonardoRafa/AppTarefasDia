using AppTarefasDia.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace AppTarefasDia.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listagem : ContentPage
    {
        ObservableCollection<Tarefa> list_tarefas = new ObservableCollection<Tarefa>();
        public Listagem()
        {
            InitializeComponent();

            lst_tarefas.ItemsSource = list_tarefas;
        }

        private void ToolbarItem_Clicked_Add(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Formulario());
        }

        protected override void OnAppearing()
        {
            if (list_tarefas.Count == 0)
            {
                Task.Run(async () =>
                {
                    List<Tarefa> temp = await App.Database.GetAll();

                    foreach (Tarefa tarefa in temp)
                    {
                        list_tarefas.Add(tarefa);
                    }
                });
            }

            ref_carregando.IsRefreshing = false;
        }

        private async void MenuItem_Clicked_Remover(object sender, EventArgs e)
        {
            MenuItem disparador = sender as MenuItem;
            Tarefa tarefa_selecionada = (Tarefa)disparador.BindingContext;
            bool confirmacao = await DisplayAlert("Tem certeza?", "Remover " + tarefa_selecionada.Titulo + " da lista de afazeres?", "Sim", "Não");

            if (confirmacao)
            {
                await App.Database.Delete(tarefa_selecionada.Id);

                list_tarefas.Remove(tarefa_selecionada);
            }
        }

        private void ref_carregando_Refreshing(object sender, EventArgs e)
        {
            list_tarefas.Clear();

            Task.Run(async () =>
            {
                List<Tarefa> temp = await App.Database.GetAll();

                foreach (Tarefa tarefa in temp)
                {
                    list_tarefas.Add(tarefa);
                }
            });

            ref_carregando.IsRefreshing = false;
        }

        private async void lst_tarefas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Tarefa tarefa_selecionada = e.SelectedItem as Tarefa;

            await Navigation.PushAsync(new Formulario
            {
                BindingContext = tarefa_selecionada
            });
        }

        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            try
            {
                CheckBox disparador = sender as CheckBox;

                if(disparador.BindingContext != null)
                {
                    Tarefa tarefa_selecionada = (Tarefa)disparador.BindingContext;
                    await App.Database.Update(tarefa_selecionada);
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void txt_seach_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = e.NewTextValue;

            list_tarefas.Clear();

            Task.Run(async () =>
            {
                List<Tarefa> temp = await App.Database.Search(search);

                foreach (Tarefa tarefa in temp)
                {
                    list_tarefas.Add(tarefa);
                }
            });
        }
    }
}